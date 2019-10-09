Shader "ImgEffects/Canny"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_Alpha ("Alpha", Range(0, 0.9)) = 0.9
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
				float4 worldPosition : TEXCOORD1;
                UNITY_VERTEX_OUTPUT_STEREO
			};

			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				return o;
			}
			
			sampler2D _MainTex;
			float4 _MainTex_TexelSize;
			float _Alpha;

			fixed4 frag (v2f i) : SV_Target
			{
                #define GRABPIXEL(kernelx) tex2D( _MainTex, i.uv + kernelx * _MainTex_TexelSize.xy);

				half4 main = tex2D( _MainTex, i.uv);
                half4 res = GRABPIXEL(float2(+1.0, 0.0));
                res += -1 * GRABPIXEL(float2(-1.0, 0.0));
				res.a = 1;
				res.rgb = res.rgb * (1 - _Alpha) + main.rgb * _Alpha;
				return res;
			}
			ENDCG
		}
	}
}
