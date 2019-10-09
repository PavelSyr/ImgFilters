Shader "ImgEffects/BlurX"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
	}
	SubShader
	{
		// No culling or depth
		Cull Off ZWrite Off ZTest Always

		Pass
		{
			CGPROGRAM
			#pragma target 3.0
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
			float4 _MainTex_TexelSize;

			fixed4 frag (v2f i) : SV_Target
			{
				float4 sum = 0;
				int weightSum = 0;
				//the weights of the neighbouring pixels
				int weights[15] = {1, 2, 3, 4, 5, 6, 7, 8, 7, 6, 5, 4, 3, 2, 1};
				//we are taking 15 samples
				for (int wi = 0; wi < 15; wi++)
				{
					//7 to the left, self and 7 to the right
					float2 cord = float2(i.uv.x + _MainTex_TexelSize.x * (wi-7), i.uv.y);
					//the samples are weighed according to their relation to the current pixel
					sum += tex2D(_MainTex, cord) * weights[wi];
					//while going through the loop we are summing up the weights
					weightSum += weights[wi];
				}
				sum /= weightSum;
				return float4(sum.rgb, 1);
			}
			ENDCG
		}
	}
}
