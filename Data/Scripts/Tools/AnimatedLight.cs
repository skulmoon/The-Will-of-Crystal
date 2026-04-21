using Godot;
using System;

public partial class AnimatedLight : PointLight2D
{
    [Export] public float MinEnergy { get; set; }
    [Export] public float MaxEnergy { get; set; }
    [Export] public float Time { get; set; }

    public override void _Ready()
	{
        base._Ready();
        Energy = MinEnergy;
        Tween tween = CreateTween();
        tween.TweenProperty(this, "energy", MaxEnergy, Time / 2).SetTrans(Tween.TransitionType.Sine);
        tween.TweenProperty(this, "energy", MinEnergy, Time / 2).SetTrans(Tween.TransitionType.Sine);
        tween.SetLoops();
    }
}
