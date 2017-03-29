// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

Shader "Custom/CustomUIShader"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_BlendAmount("Blend Amount", Range(0, 1)) = 1
		_LeftColour("Left Colour", Color) = (0,0,1,1)
		_RightColour("Right Colour", Color) = (1,0,0,1)
	}
	SubShader
	{
		// No culling or depth
		Cull Off ZWrite Off ZTest Always

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
			};

			float4 _LeftColour;
			float4 _RightColour;
			float _BlendAmount;

			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				//_Object2World, builtin Unity Function to convert object vert points to world coordinates
				o.uv = mul(unity_ObjectToWorld, v.vertex).xyz;
				
				return o;
			}
			
			sampler2D _MainTex;

			fixed4 frag (v2f i) : SV_Target
			{
				fixed4 col = tex2D(_MainTex, i.uv);
				float4 gradient = lerp(_LeftColour, _RightColour, (i.uv.x) * cos(_Time.y) * _BlendAmount);
				
				// just invert the colors
				
				return gradient;
			}
			ENDCG
		}
	}
}
