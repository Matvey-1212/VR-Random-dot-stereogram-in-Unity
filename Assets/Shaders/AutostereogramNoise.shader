
Shader "Hidden/Custom/AutostereogramNoise"
{
    HLSLINCLUDE

    #include "Packages/com.unity.postprocessing/PostProcessing/Shaders/StdLib.hlsl"

    /* Наши передаваемые параметры */
    int _UseNoise;
    int _Strips;
    int _PixelsPerStrip;
    float _DepthFactor;
    float _TimeFactor;

    /* Параметры экрана */
    float4 _MainTex_TexelSize;
    /* Текстура глубины */
    TEXTURE2D_SAMPLER2D(_CameraDepthTexture, sampler_CameraDepthTexture);

    float Rand ( float coord )
    {
        float position = coord;
        const float random_vec = 91.98765;
        float random_angle = position * random_vec;
        float random = sin(random_angle) ;
        return 1.0 - frac(random);
    }
    
    
    float4 Frag(VaryingsDefault i) : SV_Target
    {
        /* Текущее положение */
        float2 texture_coord = i.texcoord;
        /* Ширина полосы текстуры */
        float strip_width = 1.0 / _Strips;
        /* Всего "пикселей" в ширину и высоту */
        float pixels_width = _Strips * _PixelsPerStrip;
        float pixels_height = pixels_width * _MainTex_TexelSize.w / _MainTex_TexelSize.z;

        /* Подгоняем наши реальные координаты под размеры наших "пикселей" */
        texture_coord.x = floor(texture_coord.x * pixels_width) / pixels_width;
        texture_coord.y = floor(texture_coord.y * pixels_height) / pixels_height;
        
        [unroll(200)] // 200 проходов цикла
        /* Первую полосу оставляем нетронутой, меняем только то, что после */
        while( texture_coord.x > strip_width )
        {
            /* Копируем текущее положение на текстуре*/
            float2 depth_coord = texture_coord;
            /* Смещаем координаты текстуры глубины по формуле*/
            depth_coord.x = (depth_coord.x - strip_width) * (_Strips / (_Strips - 1.0));
            /* Берем величину глубины в этих координатах */
            float depth_value = 1.0 - Linear01Depth(SAMPLE_TEXTURE2D(_CameraDepthTexture,
                                                                    sampler_CameraDepthTexture,
                                                                    depth_coord).r);
            /* Вычисляем фактор смещения по формуле и подгоняем под размер "пикселей" */
            float displace_factor = (floor(depth_value * _PixelsPerStrip) / _PixelsPerStrip) * abs(_DepthFactor) * strip_width;
            /* Смещаем вправо на величину смещения с прошлой полосы*/
            texture_coord.x -= (strip_width - displace_factor);
        }

        /* Если вышли за текстуру*/
        if( texture_coord.x < 0 ) 
        {
            texture_coord.x += strip_width;
        }

        float noise_value = texture_coord.x;
        if(_UseNoise == 1)
        {
            /* Динамически смещаем коориданту */
            float random = Rand(texture_coord.y + _Time.y);
            texture_coord.y += floor(random * _TimeFactor);

            noise_value = (sin(dot(texture_coord.xy, float2(12.9898, 78.233))) * 43758.5453)
                     - floor(sin(dot(texture_coord.xy, float2(12.9898, 78.233))) * 43758.5453);
        }

        /* Возвращаем цвет */
        float4 RGBA = noise_value;
        return (RGBA);
    }
    
    ENDHLSL

    SubShader
    {
        Cull Off ZWrite Off ZTest Always

        Pass
        {
            HLSLPROGRAM

            #pragma vertex VertDefault
            #pragma fragment Frag

            ENDHLSL
        }
    }
}