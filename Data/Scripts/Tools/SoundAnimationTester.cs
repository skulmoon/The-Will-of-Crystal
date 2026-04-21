using Godot;
using System;

[Tool]
public partial class SoundAnimationTester : AudioStreamPlayer
{
    [Export] public string Sound { get; set; }
    [Export] public string Animation { get; set; }
    [Export] public bool Activate
    {
        get => false;
        set
        {
            Test();
        }
    }

    public void Test()
	{
        GetParent<AnimatedSprite2D>().Play(Animation);
        Bus = "Sound";
        Stream = ResourceLoader.Load<AudioStream>(Sound);
        Play();
    }
}
