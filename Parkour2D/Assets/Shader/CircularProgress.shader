// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/CircularProgress" {
	Properties{
	_MainTex("Base (RGB) Trans (A)", 2D) = "white" {}
	_Percentage("Percentage", Range(0, 360)) = 0 //传进来角度
	_InnerRadius("InnerRadius", Range(0, 0.5)) = 0.3
	_Blur("Blur", Float) = 0.15
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
		float _InnerRadius;
		float _Blur;

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
			float x = i.texcoord.x - 0.5f;
			float y = i.texcoord.y - 0.5f;
			// dis点与圆心的距离
			float dis = sqrt(pow(x, 2) + pow(y, 2));
			if (dis > 0.5) {
				// 外圆外清空
				col.a = 0;
			}
			else {
				// 外圆内
				if (dis > _InnerRadius) {
					//夹角
					float angle = degrees(atan2(x, -y)) + 180;

					float arcR = (0.5f - _InnerRadius) / 2;
					float centerDistance = arcR + _InnerRadius;
					// 圆弧两端的圆角在大圆中的角度
					float arcAngle = degrees(atan(arcR / centerDistance));
					// 圆弧内的渲染
					col.a = col.a * saturate(angle - _Percentage - arcAngle + 1);
					// 圆弧的圆角的渲染					
					float arcRX = -centerDistance * sin(radians(_Percentage + arcAngle));
					float arcRY = centerDistance * cos(radians(_Percentage + arcAngle));
					float arcDis = sqrt(pow((x - arcRX), 2) + pow((y - arcRY), 2));
					// 圆弧的两端
					if ((angle - _Percentage) > 0 && (angle - _Percentage - arcAngle) < 0) {
						if (arcDis < arcR) {
							col.a = 1;
							col = col * (1 - smoothstep(arcR - _Blur, arcR, arcDis));
						}
						else {
							col.a = 0;
						}
					}
				}
				else {
					col.a = 0;
				}
			}
			half radius = distance(float2(0.5, 0.5), i.texcoord);
			col = col * (1 - smoothstep(0.5 - _Blur, 0.5, radius));
			col = col * (1 - smoothstep(_InnerRadius + _Blur, _InnerRadius, radius));
			return col;
		}
			ENDCG
		}
	}
		FallBack "Diffuse"
}