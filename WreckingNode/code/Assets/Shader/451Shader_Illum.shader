Shader "Unlit/451Shader_Illum"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 200
		Cull Off

		Pass
		{
			CGPROGRAM
			#pragma vertex MyVert
			#pragma fragment MyFrag
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float3 normal : NORMAL;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float4 vertex : SV_POSITION;
				float3 normal : NORMAL;
				float2 uv : TEXCOORD0;
				float3 vertexWC : TEXCOORD3;
			};

            // our own matrix
            float4x4 MyXformMat;  // our own transform matrix!!
            
			sampler2D _MainTex;

			float4 LightPosition;
			fixed4 LightColor;
			float LightNear;
			float LightFar;
			float Intensity;
			
			v2f MyVert (appdata v)
			{
				v2f o;

				//Uses our transform matrix in place of UNITY_MATRIX_M
                o.vertex = mul(MyXformMat, v.vertex);  
				o.vertexWC = mul(MyXformMat, v.vertex);
                o.vertex = mul(UNITY_MATRIX_VP, o.vertex);   // camera transform only                

				float3 p = v.vertex + 10 * v.normal;
				p = mul(UNITY_MATRIX_M, p);
				o.normal = normalize(p - o.vertexWC);
				o.uv = v.uv;
                return o;
			}

			// our own function
			float ComputeDiffuse(v2f i) {
				float3 l = normalize(LightPosition - i.vertexWC);
				return clamp(dot(i.normal, l), 0, 1);
				//float3 l = LightPosition - i.vertexWC;
				//float d = length(l);
				//l = l / d;
				//float strength = 1;

				//float ndotl = clamp(dot(i.normal, l), 0, 1);
				//if (d > LightNear) {
				//	if (d < LightFar) {
				//		float range = LightFar - LightNear;
				//		float n = d - LightNear;
				//		strength = smoothstep(0, 1, 1.0 - (n * n) / (range * range));
				//	}
				//	else {
				//		strength = 0;
				//	}
				//}
				//return Intensity * ndotl * strength;
			}
			
			fixed4 MyFrag (v2f i) : SV_Target
			{
				// sample the texture
                fixed4 col = tex2D(_MainTex, i.uv);

				float diff = ComputeDiffuse(i);
				return col * diff * LightColor;
			}
			ENDCG
		}
	}
}