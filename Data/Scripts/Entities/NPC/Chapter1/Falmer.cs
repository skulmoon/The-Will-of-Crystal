using Godot;
using System;

public partial class Falmer : NPC
{
    public Timer TimerScared { get; set; }

    public Falmer() : base()
    {
        TimerScared = new Timer()
        {
            OneShot = false,
            WaitTime = 0.06,
        };
        AddChild(TimerScared);
        TimerScared.Timeout += (() =>
        {
            AnimatedSprite2D.Position = new Vector2((float)GD.RandRange(-0.6, 0.6), (float)GD.RandRange(-0.6, 0.6));
        });
    }
}
