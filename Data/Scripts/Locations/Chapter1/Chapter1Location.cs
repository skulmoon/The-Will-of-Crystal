using Godot;
using System;

public partial class Chapter1Location : Location
{
	public override void _Ready()
	{
		base._Ready();
		Global.Music.PlayMusic("Sunsefering.ogg");
	}
}
