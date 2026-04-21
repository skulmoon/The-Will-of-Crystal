using Godot;
using System;

public partial class DeathRoom : Node2D
{
	public override void _Ready()
    {
        Global.SaveManager.LoadSave(Global.Settings.CurrentSave);
        Global.SceneObjects.Storage.GetTree().ChangeSceneToFile($"res://Data/Scenes/Location/{Global.Settings.SaveData.CurrentLocation}.tscn");
    }
}
