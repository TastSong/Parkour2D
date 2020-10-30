// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/CircularRing"
{
	Properties{
		_MainTex("Base (RGB) Trans (A)", 2D) = "white" {}
		_Angle("Pie Angle",Range(0,360)) = 360
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
					#pragma target 2.0
					#pragma multi_compile_fog

					#include "UnityCG.cginc"

					struct appdata_t {
						float4 vertex : POSITION;
						float2 texcoord : TEXCOORD0;
						UNITY_VERTEX_INPUT_INSTANCE_ID
					};

					struct v2f {
						float4 vertex : SV_POSITION;
						float2 texcoord : TEXCOORD0;
						UNITY_FOG_COORDS(1)
						UNITY_VERTEX_OUTPUT_STEREO
					};

					sampler2D _MainTex;
					float4 _MainTex_ST;

					v2f vert(appdata_t v)
					{
						v2f o;
						UNITY_SETUP_INSTANCE_ID(v);
						UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
						o.vertex = UnityObjectToClipPos(v.vertex);
						o.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);
						UNITY_TRANSFER_FOG(o,o.vertex);
						return o;
					}

					half _Angle;
					fixed _Gradient;

					fixed4 frag(v2f i) : SV_Target
					{
						fixed4 col = tex2D(_MainTex, i.texcoord);

						half2 cuv = i.texcoord - half2(0.5,0.5);
						half2 luv = half2(1,0);

						half2 s = cuv.x * luv.y - luv.x * cuv.y;
						half2 c = cuv.x * luv.x + cuv.y * luv.y;

						half2 angle = atan2(s, c) * (180 / 3.1416);

						angle += step(0,cuv.y) * 360;

						clip(angle - _Angle);

						UNITY_APPLY_FOG(i.fogCoord, col);
						return col;
					}
				ENDCG
			}
		}

}
