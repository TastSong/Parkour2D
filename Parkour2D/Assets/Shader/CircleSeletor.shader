Shader "Custom/CircleSeletor" {
	Properties{
	_MainTex("Base (RGB) Trans (A)", 2D) = "white" {}
	_Percentage("Percentage", Range(0, 0.5)) = 0
	}

		SubShader{
		Tags {"Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent"}
		LOD 100

		ZWrite Off
		Blend SrcAlpha OneMinusSrcAlpha

		Pass {
		CGPROGRAM
		#pragma vertex vert
		#pragma fragment frag

		#include "UnityCG.cginc"

		struct appdata_t {
		float4 vertex : POSITION;
		float2 texcoord : TEXCOORD0;
		};

		struct v2f {
		float4 vertex : SV_POSITION;
		half2 texcoord : TEXCOORD0;
		};

		sampler2D _MainTex;
		float4 _MainTex_ST;
		float _Percentage;

		v2f vert(appdata_t v)
		{
			v2f o;
			o.vertex = UnityObjectToClipPos(v.vertex);
			o.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);
			return o;
		}

		fixed4 frag(v2f i) : SV_Target
		{
			fixed4 col = tex2D(_MainTex, i.texcoord);
			float x = i.texcoord.x;
			float y = i.texcoord.y;
			// dis点与圆心的距离
			float dis = sqrt(pow((0.5f - x), 2) + pow((0.5f - y), 2));
			if (dis > 0.5) {
				if (_Percentage < 0.5) {
					col.a = 0;
				}
				else {
					col.a = 1;
				}
			}
			else {
			   if (dis < _Percentage) {
				   col.a = 1;
			   }
			   else {
				   col.a = 0;
			   }
			}

			return col;
			}
				ENDCG
			}
	}
		FallBack "Diffuse"
}