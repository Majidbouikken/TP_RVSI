Shader "Custom/BladeGlow"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Emission ("Emission", Color) = (0,0,0,0)
        _Glow ("Glow Intensity", Range (0, 10)) = 1.0
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
        _OutlineColor ("Outline Color", Color) = (0, 0, 0, 1)
        _Outline ("Outline width", Range (.002, 0.03)) = 0.005
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _MainTex;

        struct Input
        {
            float2 uv_MainTex;
        };

        half _Glossiness;
        half _Metallic;
        fixed4 _Color;
        fixed4 _Emission;
        half _Glow;
        fixed4 _OutlineColor;
        half _Outline;
        fixed4 LightingStandardFull (SurfaceOutputStandard s, fixed3 lightDir, fixed atten) {
            fixed4 c;
            c.rgb = s.Albedo;
            // Add emissive color multiplied by glow intensity
            c.rgb += s.Emission * _Glow;
            c.a = s.Alpha;
            return c;
        }

        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            o.Albedo = _Color;
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Emission = _Emission;
            o.Emission *= _Glow;
            float outline = ddx(IN.uv_MainTex.x) + ddy(IN.uv_MainTex.y);
            outline = step(_Outline, outline);
            o.Albedo += _OutlineColor * outline;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
