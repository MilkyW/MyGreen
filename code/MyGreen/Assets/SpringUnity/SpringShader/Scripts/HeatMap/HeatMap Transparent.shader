// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "HeatMap/HeatMap Transparent"
{
    Properties
    {
        _Alpha("Alpha",Range(0,1)) = 0.8

		//MASK SUPPORT ADD
		_StencilComp("Stencil Comparison", Float) = 8
		_Stencil("Stencil ID", Float) = 0
		_StencilOp("Stencil Operation", Float) = 0
		_StencilWriteMask("Stencil Write Mask", Float) = 255
		_StencilReadMask("Stencil Read Mask", Float) = 255
		_ColorMask("Color Mask", Float) = 15
		//MASK SUPPORT END
    }

	SubShader 
    {
		Tags { "RenderType"="Transparent" "Opaque"="Transparent" "Queue"="Overlay"}

		//MASK SUPPORT ADD
		Stencil
	{
		Ref[_Stencil]
		Comp[_StencilComp]
		Pass[_StencilOp]
		ReadMask[_StencilReadMask]
		WriteMask[_StencilWriteMask]
	}
		ColorMask[_ColorMask]
		//MASK SUPPORT END

		LOD 200
		
        Pass
        {
            Cull OFF
            Blend SrcAlpha OneMinusSrcAlpha
            ztest ON
            CGPROGRAM

            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
            #include "UnityLightingCommon.cginc"

            struct a2v
            {
                float4 pos : POSITION;
                fixed4 color : COLOR;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                fixed4 color : COLOR;
            };

            float _Alpha;

            v2f vert( a2v i )
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(i.pos);
                o.color = i.color;
                return o;
            }

            fixed4 frag( v2f i ) : COLOR
            {
                return fixed4(i.color.rgb,_Alpha);
            }

	        ENDCG
        }
	}
}
