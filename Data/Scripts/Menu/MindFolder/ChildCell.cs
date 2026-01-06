using Godot;
using System;

public partial class ChildCell : MindCell
{
    [Export(PropertyHint.FilePath)] public string Scene { get; set; }

    public ChildCell() : base("res://Data/Textures/MindFolder/ChildCell.png", "res://Data/Textures/MindFolder/PressedChildCell.png") { }

    public override void OnPressed()
    {
        if (Scene != "res://")
            Global.SceneObjects.ChangeScene(Scene);
    }
}
