﻿Shader "ImgEffects/LogCorrection"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_Offset ("Offset", Range(0, 1)) = 0.01
		_Multiplicator ("Multiplicator", Range(0, 2)) = 0.01
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
			float _Offset;
			float _Multiplicator;

			fixed4 frag (v2f i) : SV_Target
			{
				half4 main = tex2D( _MainTex, i.uv);

				main.rgb = _Multiplicator * log(main.rgb + _Offset);

				return main;
			}
			ENDCG
		}
	}
}
