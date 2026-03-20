using Godot;
using System;

public partial class DefaultAnimationPlayer : AnimationPlayer
{
    [Export] public string DefaultAnimation { get; set; }

    public override void _Ready()
    {
        if (DefaultAnimation != string.Empty)
            Play(DefaultAnimation);
    }
}
