Shader "Custom/Surface Wobble" {

	//Variables
	Properties{
		_MainTexture("Main Texture(RGB)", 2D) = " white" {}
		_Color("Color", Color) = (1,1,1,1)

			_OutlineWidth("Outline Width", float) = 1
			_OutlineColor("Outline Color", Color) = (1,1,1,1)

			//Disolve stuff
			//_DissolveTexture("Cheese", 2D) = "white" {}
			//_DissolveAmount("Cheese Cut Out Amount", Range(0, 1)) = 1

		_ExtrudeAmount("Extrude Amount", Range(-0.1, 0.1)) = 1
	}

		CGINCLUDE
		#include "UnityCG.cginc"

		struct appdata {
			float4 vertex : POSITION;
			float2 uv : TEXCOORD0;
			float3 normal : NORMAL;

		};

		struct v2f {
			float4 position : SV_POSITION;
			float2 uv : TEXCOORD0;
			fixed4 color : COLOR;
		};

		float4 _OutlineColor;
		float _OutlineWidth;
		sampler2D _MainTexture;

		float4 _Color;

		//sampler2D _DissolveTexture;
		//float _DissolveAmount;

		float _ExtrudeAmount;
		ENDCG

	SubShader{
		//Outline Pas
		Pass {
		Cull Front
		CGPROGRAM
		#pragma vertex vert
		#pragma fragment frag
			//Vertex
			//Build the objects
			v2f vert(appdata IN) {
				v2f OUT;

				IN.vertex.xyz += IN.normal.xyz * _OutlineWidth;

				OUT.position = mul(UNITY_MATRIX_MVP, IN.vertex);
				OUT.uv = IN.uv;
				OUT.color = _OutlineColor;
				return OUT;
			}

			fixed4 frag(v2f IN) : SV_Target {
				float4 textureColor = IN.color;

				return textureColor;
			}
		ENDCG
		}


		//Drawing Pass
		Pass {
		CGPROGRAM
		#pragma vertex vert
		#pragma fragment frag

			//Vertex
			//Build the objects
			v2f vert(appdata IN) {
				v2f OUT;
				IN.vertex.xyz += IN.normal.xyz * _ExtrudeAmount * float4(sin(_Time.y * 4), cos(_Time.y * 5), 0, 0);
				OUT.position = mul(UNITY_MATRIX_MVP, IN.vertex);
				OUT.uv = IN.uv;

				return OUT;
			}

			//Fragment
			//Color it in
			fixed4 frag(v2f IN) : SV_Target{
				float4 textureColor = tex2D(_MainTexture, IN.uv) * _Color;
				//float4 dissolveColor = tex2D(_DissolveTexture, IN.uv);

				//clip(dissolveColor.rgb - _DissolveAmount);

				return textureColor;
			}

		ENDCG
		}
	}
}