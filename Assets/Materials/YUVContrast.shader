Shader "ImgEffects/YUVContrast"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_Contrast ("Contrast", Float) = 0.5
		_Min ("Min", Range(0, 1)) = 0.5
		_Max ("Max", Range(0, 1)) = 0.5

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
			float _Contrast;
			float _Min;
			float _Max;

			fixed4 frag (v2f i) : SV_Target
			{
				fixed4 col = tex2D(_MainTex, i.uv);

				//convert from RGB to YUV 
				// Y - brightness; U,V - color chanels
				fixed4 yuv = fixed4(1, 1, 1, 1); // y = r; u = g; v = b;
				//y component:
				yuv.r =  0.2126 * col.r + 0.7152 * col.g + 0.0722 * col.b;
				//u component:
				yuv.g = -0.0999 * col.r - 0.3360 * col.g + 0.4360 * col.b;
				//v component:
				yuv.b =  0.6150 * col.r - 0.5586 * col.g - 0.0563 * col.b;

				//Calculate contrast
				yuv.r = (yuv.r - _Min) * (_Contrast / (_Max - _Min));

				//convert from YUV to RGB
				col.r = yuv.r + 1.28 * yuv.b;
				col.g = yuv.r - 0.2148 * yuv.g - 0.3805 * yuv.b;
				col.b = yuv.r + 2.1279 * yuv.g;

				return col;
			}
			ENDCG
		}
	}
}
