Shader "ImgEffects/Shape45Quad"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_Radius ("Radius", Range(0, 0.9)) = 0.9
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
			float _Radius;

			fixed4 frag (v2f i) : SV_Target
			{
				float2 vec = abs(i.uv - 0.5);
				float len = vec.x + vec.y;

				half4 main = tex2D( _MainTex, i.uv);

				main.rgb = saturate(_Radius - len) * main.rgb;

				return main;
			}
			ENDCG
		}
	}
}
