using Godot;
using System;

public partial class Chapter1Location : Location
{
	public override void _Ready()
	{
		base._Ready();
        Modulate = Modulate.Darkened(0.7f);
        Global.Music.PlayMusic("Sunsefering.ogg");
	}
}
