Shader "Custom/BurnningToast2" {
	Properties {
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Main2Tex("Toasted Albedo (RGB)", 2D) = "white" {}
		_Main3Tex("Burnt Albedo (RGB)", 2D) = "white" {}
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0
		_Lerp("Lerp", Range(0,1)) = 0.0
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard vertex:vert fullforwardshadows

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;
		sampler2D _Main2Tex;
		sampler2D _Main3Tex;

		struct Input {
			float2 uv_MainTex;
			fixed4 color : COLOR;
		};

		struct v2f {
			float4 pos : SV_POSITION;
			fixed4 color : COLOR;
		};

		half _Glossiness;
		half _Metallic;
		half _Lerp;

		void vert(inout appdata_full v, out Input o)
		{
			UNITY_INITIALIZE_OUTPUT(Input, o);
			o.color = v.color; // Save the Vertex Color in the Input for the surf() method
		}

		void surf (Input IN, inout SurfaceOutputStandard o) {
			// Albedo comes from a texture tinted by color
			float4 c = float4(0.0f, 0.0f, 0.0f, 0.0f);
			if (IN.color.a < 0.5f) {
				c = lerp(tex2D(_MainTex, IN.uv_MainTex), tex2D(_Main2Tex, IN.uv_MainTex), IN.color.a * 2.0f);
			}
			else {
				c = lerp(tex2D(_Main2Tex, IN.uv_MainTex), tex2D(_Main3Tex, IN.uv_MainTex), (IN.color.a - 0.5f)* 2.0f);
			}
			o.Albedo = c.rgb;
			// Metallic and smoothness come from slider variables
			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;
			o.Alpha = c.a;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
