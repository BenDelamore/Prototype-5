Shader "Custom/Energy"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_ScrollSpeed("Scroll Speed",Range(0.0,10.0)) = 1.0 //Range slider for scroll speed
		_ColorPow("Color Strength",Range(0.0,10.0)) = 1.0
		_Value("Value",Range(0.0,20.0)) = 1.0
	}
	SubShader
	{
		//Tags { "RenderType" = "Transparent" "Queue" = "Transparent"}
		Tags { "RenderType" = "Opaque" }
		LOD 100
		Cull Off //Draw both sides (double sided)
		//Zwrite Off //Draw without sorting - combines alpha result
		Blend One OneMinusSrcAlpha //set blend mode to "blendAdd"
		Blend One One


		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			
			#include "UnityCG.cginc"

			struct appdata //this is a list of vertex attributes that Andrew finds interesting
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
				float4 color : COLOR; //bring in the vertex colour
			};

			struct v2f // vertex2fragment
			{
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
				float4 color : COLOR; //<<<< added this line to access vertex colour from particle system
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			float _ScrollSpeed;
			float _ColorPow;
			float _Value;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				o.color = v.color; //<<<< added this line to access vertex colour from particle system

				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				//we need to add alpha to the sprite ends; we can do this using the uv.x value
				fixed endAlpha = smoothstep(0,.1,i.uv.x) * 1 - (smoothstep(.9,1.0,i.uv.x));


				fixed offset = tex2D(_MainTex,i.uv).b; // cloud pattern packed into blue channel used for warping
				float2 uv = i.uv; // we are going to offset the uv a bit and ass panner and warp
				uv.x += offset*.1; // uv warp
				uv.x += frac(_Time.y * _ScrollSpeed) ; // uv panner

				fixed4 col = tex2D(_MainTex,uv); // this texture sample will give us the energy shape (packed in Red channel)
				//col.rgb = lerp(i.color.rgb, float3(1, 1, 1),pow(col.r, _ColorPow))*col.r;//bring in vertex colour lerped against white for brighter look; we need to permultiply for "blendAdd"
				col.rgb *= endAlpha; // here we trim the ends
				col.rgb *= i.color.a ; // here we use vertex color and the particle "Color over Lifetime" to make these particles fade in and out

				col.a = pow(col.r,_ColorPow); //rgb are all the same
				//col.a = clamp(col.a, 0, 1);

				//return endAlpha; // enable this if you would like to preview the clipped ends

				
				return col * _Value;
			}
			ENDCG
		}
	}
}
