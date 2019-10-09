Shader "ImgEffects/LinearContrast"
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
				col.rgb = (col.rgb - _Min) * (_Contrast / (_Max - _Min));
				return col;
			}
			ENDCG
		}
	}
}
