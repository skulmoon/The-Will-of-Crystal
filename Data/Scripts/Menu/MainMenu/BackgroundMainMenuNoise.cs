using Godot;
using System;
using System.Runtime.CompilerServices;

public partial class BackgroundMainMenuNoise : TextureRect
{
    private const int TIME = int.MaxValue;
    private FastNoiseLite noise;

    public override void _Ready()
    {
        if (Texture is NoiseTexture2D noiseTexture)
        {
            noiseTexture.Width = noiseTexture.Height * GetWindow().Size.X / GetWindow().Size.Y;
            Tween tween = CreateTween();
            tween.TweenProperty(noiseTexture.Noise, "offset:y", TIME, TIME);
            tween.Parallel();
            tween.TweenProperty(noiseTexture.Noise, "offset:z", (long)TIME * 8, TIME);
        }
    }
}
