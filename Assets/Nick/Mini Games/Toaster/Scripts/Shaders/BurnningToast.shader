Shader "Custom/BurnningToast"
{
	Properties
	{
		_MainTex ("BaseToast", 2D) = "white" {}
		_Main2Tex("BurntToast", 2D) = "white" {}
	}
	SubShader
	{
		// No culling or depth
		Cull Off ZWrite Off ZTest Always

		Pass
		{
			CGPROGRAM
// Upgrade NOTE: excluded shader from DX11; has structs without semantics (struct v2f members lerpVal)
#pragma exclude_renderers d3d11
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
				float2 uv2 : TEXCOORD1;
				float4 color : COLOR;
			};

			struct v2f
			{
				float2 uv2 : TEXCOORD1;
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
				float4 lerpVal: COLOR;

			};

			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				o.uv = v.uv;
				o.uv2 = v.uv2;
				o.lerpVal = v.color;
				return o;
			}
			
			sampler2D _MainTex;
			sampler2D _Main2Tex;

			float4 frag (v2f i) : SV_Target
			{
				float4 Lerp0 = lerp(tex2D(_MainTex,i.uv),tex2D(_Main2Tex,i.uv2), i.lerpVal.a);

				return  Lerp0;
			}
			ENDCG
		}
	}
}
