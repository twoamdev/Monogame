#if OPENGL
	#define SV_POSITION POSITION
	#define VS_SHADERMODEL vs_3_0
	#define PS_SHADERMODEL ps_3_0
#else
	#define VS_SHADERMODEL vs_4_0_level_9_1
	#define PS_SHADERMODEL ps_4_0_level_9_1
#endif

sampler s0;
float2 blurDirection;
float blurSpeed;


float4 MainPS(float2 texCoord : TEXCOORD0) : COLOR
{
     float4 color = tex2D(s0, texCoord);
    float4 accum = 0;
    float weight = 1.0 / 8.0;
    float2 offset = float2(0, 0);

    offset += blurDirection * blurSpeed * weight;

    for (int i = 1; i <= 8; i++)
    {
        accum += tex2D(s0, texCoord + offset);
        offset += blurDirection * blurSpeed * weight;
    }

    return (color + accum) / 9.0;

}

technique SpriteDrawing
{
	pass P0
	{
		PixelShader = compile PS_SHADERMODEL MainPS();
	}
};



