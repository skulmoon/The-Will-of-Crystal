using Godot;
using System;

public partial class NoiseChanger : Node
{
    private const int TIME = int.MaxValue;
    [Export] NoiseTexture2D Noise;

    public override void _Ready()
    {
        Tween tween = CreateTween();
        tween.TweenProperty(Noise.Noise, "offset:y", TIME, TIME);
        tween.Parallel();
        tween.TweenProperty(Noise.Noise, "offset:z", (long)TIME * 8, TIME);
    }
}
