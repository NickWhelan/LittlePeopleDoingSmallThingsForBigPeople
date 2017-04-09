Shader "Custom/BurnningToast2" {
	Properties {
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Main2Tex("Toasted Albedo (RGB)", 2D) = "white" {}
		_Main3Tex("Burnt Albedo (RGB)", 2D) = "white" {}
		_Glossiness("Smoothness", Range(0,1)) = 0.5
		_Metallic("Metallic", Range(0,1)) = 0.0
		_OutLineNormal("Thickness",Range(-1,1)) = 0.005
		_Crust("Crust",Color) = (0,0,0,1)
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard vertex:vert fullforwardshadows

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 4.0

		sampler2D _MainTex;
		sampler2D _Main2Tex;
		sampler2D _Main3Tex;

		struct Input {
			float2 uv_MainTex;
			fixed4 color : COLOR;
			float3 vertex;
			float3 normal;
		};

		struct v2f {
			float4 pos : SV_POSITION;
			fixed4 color : COLOR;
		};

		half _Glossiness;
		half _Metallic;
		half _OutLineNormal;
		half4 _Crust;

		void vert(inout appdata_full v, out Input o)
		{
			UNITY_INITIALIZE_OUTPUT(Input, o);
			o.color = v.color; // Save the Vertex Color in the Input for the surf() method
			o.normal = v.normal;
			o.vertex = v.vertex;
		}

		void surf(Input IN, inout SurfaceOutputStandard o) {

			float4 c = float4(0.0f, 0.0f, 0.0f, 0.0f);
			if (IN.color.a < 0.5f) {
				c = lerp(tex2D(_MainTex, IN.uv_MainTex), tex2D(_Main2Tex, IN.uv_MainTex), IN.color.a * 2.0f);
			}
			else {
				c = lerp(tex2D(_Main2Tex, IN.uv_MainTex), tex2D(_Main3Tex, IN.uv_MainTex), (IN.color.a - 0.5f)* 2.0f);
			}

			// Albedo comes from a texture tinted by color
			o.Albedo = c.rgb;
			// Metallic and smoothness come from slider variables
			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;
			o.Alpha = c.a;

			/*
			float3 eyedir = mul((float3x3)UNITY_MATRIX_MVP, IN.vertex);
			float silhouette = max(dot(IN.normal, normalize(-eyedir.xyz)), float3(0.0,0.0,0.0));

			if (silhouette < _OutLineNormal) {
				o.Albedo = _Crust;
				o.Alpha = 1;
			}*/

		}
		ENDCG
	}
	FallBack "Diffuse"
}
