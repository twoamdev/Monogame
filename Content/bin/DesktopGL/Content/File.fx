#if OPENGL
	#define SV_POSITION POSITION
	#define VS_SHADERMODEL vs_3_0
	#define PS_SHADERMODEL ps_3_0
#else
	#define VS_SHADERMODEL vs_4_0_level_9_1
	#define PS_SHADERMODEL ps_4_0_level_9_1
#endif

matrix WorldViewProjection;

struct VertexShaderInput
{
	float4 Position : POSITION0;
	float4 Color : COLOR0;
};

struct VertexShaderOutput
{
	float4 Position : SV_POSITION;
	float4 Color : COLOR0;
};

VertexShaderOutput MainVS(in VertexShaderInput input)
{

    // Applying our cool effect. What it does is: when drawing pixel X:Y, instead of taking the
	// pixel from texture position X:Y, take it from (X+Y*0.2:Y) to create a slanted effected
	float2 tex2; // I am using a temp var because I don't know if we can/should modify input.TC
	tex2[0] = input.TextureCoordinates[0] - input.TextureCoordinates[1] * 0.2f;
	tex2[1] = input.TextureCoordinates[1];

    return tex2D(SpriteTextureSampler,tex2) * input.Color;
    
    
	//VertexShaderOutput output = (VertexShaderOutput)0;

	//output.Position = mul(input.Position, WorldViewProjection);
	//output.Color = input.Color;

	//return output;
}

float4 MainPS(VertexShaderOutput input) : COLOR
{
	return input.Color;
}

technique BasicColorDrawing
{
	pass P0
	{
		VertexShader = compile VS_SHADERMODEL MainVS();
		PixelShader = compile PS_SHADERMODEL MainPS();
	}
};