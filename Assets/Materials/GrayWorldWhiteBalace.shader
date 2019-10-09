Shader "ImgEffects/GrayWorldWhiteBalace"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_Rw ("Rw", Range(0, 1)) = 0.5
		_Gw ("Gw", Range(0, 1)) = 0.5
		_Bw ("Bw", Range(0, 1)) = 0.5

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

			//_Avg = (Ravg + Gavg + Bavg) / 3;
			//_Rw = Ravg / _Avg;
			float _Rw; 
			//_Gw = Gavg / _Avg;
			float _Gw; 
			//_Bw = Bavg / _Avg;
			float _Bw; 
			
			fixed4 frag (v2f i) : SV_Target
			{
				fixed4 col = tex2D(_MainTex, i.uv);

				col.r /= _Rw;
				col.g /= _Gw;
				col.b /= _Bw;

				return col;
			}
			ENDCG
		}
	}
}
