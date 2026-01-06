using Godot;
using System;

public partial class AnimatedSprite : AnimatedSprite2D
{
    public override void _Ready()
    {
        Play("default");
    }
}
