Shader "CustomShaders/DrunkEffect"
{
    Properties
    {
        //basic properties
        _Color("Color", Color) = (1,1,1,1)
        _MainTex ("Texture", 2D) = "white" {}
        
        //distortion textures
        _DistTex("Distortion Texture", 2D) = "white" {}
        _DistAmount("Distortion Amount", Range(0,0.1)) = 0.02
        _DistSpeed("Distortion Speed", Range(0,10)) = 2
    }
    SubShader
    {
        LOD 100
        Cull Off ZWrite Off ZTest Always
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            
            #include "UnityCG.cginc"
            #include "Lighting.cginc"

            struct AppData
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct Interpolators
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                
            };

            //declaring properties
            sampler2D _MainTex, _DistTex;
            float4 _Color;
            float _DistAmount, _DistSpeed;
            
            

            Interpolators vert (AppData v)
            {
                Interpolators o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                
                return o;
            }
            

            float4 frag (Interpolators i) : SV_Target
            {
                float time = _Time.x;
                float t = 0.5+ sin(time * _DistSpeed);
                float2 uv  = i.uv;
                float2 changingUV = uv;
                
                float2 distortion =  tex2D(_DistTex, changingUV).xy;

                
                distortion = ((distortion*2)-1)* _DistAmount;

                
                // sample the texture
                //fixed4 col = tex2D(_MainTex, i.uv);
                float4 col = tex2D(_MainTex, i.uv);
                float4 distorted = tex2D(_MainTex, lerp(uv,uv+distortion,t));


                float4 finalRender = lerp(col,distorted,t);
                
                return col+distorted;
            }
            ENDCG
        }
    }
}