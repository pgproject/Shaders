Shader "Custom/Dissolve" {
    Properties {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _DissolveMap("Dissolve Map (RGB)", 2D) = "white" {}
        _DissolveAmount("Dissolve Amount", Range(0.0, 1.0)) = 0     
        _DissolveEffect("_Dissolve Effect (RGB)", 2D) = "white" {}    
        _DissolveEffectSize("Dissolve Effect Size", Range(0.0, 1.0)) = 0.025 
        _Emission("Emission", float) = 2.0
    }
    SubShader {
        Tags { "RenderType"="Opaque" }
        LOD 200
        Cull Off
        CGPROGRAM
        #pragma surface surf Lambert addshadow
        #pragma target 3.0
        #pragma multi_compile __ ALPHA_CUTOFF_ENABLED
 
        fixed4 _Color;
        sampler2D _MainTex;
        sampler2D _BumpMap;
        sampler2D _DissolveMap;     
        sampler2D _DissolveEffect;
        float _DissolveAmount;
        float _DissolveEffectSize;       
        float _Emission;
        half _CutOffValue;
 
        struct Input {
            float2 uv_MainTex;
        };
 
        void surf (Input IN, inout SurfaceOutput o) {
            fixed4 mainTex = tex2D (_MainTex, IN.uv_MainTex) * _Color;
            half dissolve = tex2D(_DissolveMap, IN.uv_MainTex).rgb - _DissolveAmount;
            
            #ifdef ALPHA_CUTOFF_ENABLED
                if (mainTex.a < _CutOffValue)
                    clip(-1);
            #endif   
            
            clip(dissolve);             
            
            if (_DissolveAmount > 0 && dissolve < _DissolveEffectSize) 
                o.Emission = tex2D(_DissolveEffect, float2(dissolve * (1 / _DissolveEffectSize), 0)) * _Emission;    
                      
            o.Albedo = mainTex.rgb;
            o.Alpha = mainTex.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}