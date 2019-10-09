Shader "ImgEffects/GammaCorrection"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_Gamma ("Gamma", Range(0.01, 5)) = 1
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

			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				return o;
			}
			
			sampler2D _MainTex;
			float _Gamma;

			fixed4 frag (v2f i) : SV_Target
			{
				half4 main = tex2D( _MainTex, i.uv);

				main.rgb = pow(main.rgb, _Gamma);

				return main;
			}
			ENDCG
		}
	}
}
