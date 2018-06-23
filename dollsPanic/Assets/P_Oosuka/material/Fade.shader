Shader "UI/Fade"
{
	Properties
	{
		_ScreenShotTex("ScreenShot Texture", 2D) = "white" {}
		_MaskTex("Mask Texture", 2D) = "white" {}
		_Range("Range", Range (0, 1)) = 0
	}

	SubShader
	{
		Tags
		{ 
			"Queue"="AlphaTest" 
			"IgnoreProjector"="True" 
			"RenderType"="TransparentCutout" 
			"PreviewType"="Plane"
			"CanUseSpriteAtlas"="True"
		}

		Cull Off
		Lighting Off
		ZWrite Off
		ZTest [unity_GUIZTestMode]
		Blend SrcAlpha OneMinusSrcAlpha

		Pass
		{
		CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"
			#include "UnityUI.cginc"
			
			struct appdata_t
			{
				float4 vertex   : POSITION;
				float4 color    : COLOR;
				float2 texcoord : TEXCOORD0;
			};

			struct v2f
			{
				float4 vertex   : SV_POSITION;
				fixed4 color    : COLOR;
				half2 texcoord  : TEXCOORD0;
				float4 worldPosition : TEXCOORD1;
			};
			
			fixed4 _TextureSampleAdd;

			v2f vert(appdata_t IN)
			{
				v2f OUT;
				OUT.worldPosition = IN.vertex;
				OUT.vertex = UnityObjectToClipPos(OUT.worldPosition);

				OUT.texcoord = IN.texcoord;
				
				#ifdef UNITY_HALF_TEXEL_OFFSET
				OUT.vertex.xy += (_ScreenParams.zw - 1.0)*float2(-1, 1);
				#endif
				
				OUT.color = IN.color;
				return OUT;
			}

			sampler2D _ScreenShotTex;
			sampler2D _MaskTex;
			float _Range;

			fixed4 frag(v2f IN) : SV_Target
			{
				fixed4 color = tex2D(_ScreenShotTex, IN.texcoord);
			
				//アルファ値だけマスク利用			
				half mask = tex2D(_MaskTex, IN.texcoord).a - (-1 + _Range * 2);	// 0 ⇒ a+1	1 ⇒ a-1
//				half mask = tex2D(_MaskTex, IN.texcoord).a - ( 1 - _Range * 2);	// 0 ⇒ a-1	1 ⇒ a+1

				color.a = mask;
				return color;
			}
		ENDCG
		}
	}
}
