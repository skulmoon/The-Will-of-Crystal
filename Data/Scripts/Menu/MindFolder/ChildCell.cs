using Godot;
using System;

public partial class ChildCell : MindCell
{
    [Export] public ConductivePath ConductivePath { get; set; }

    public ChildCell() : base("res://Data/Textures/MindFolder/ChildCell.png", "res://Data/Textures/MindFolder/PressedChildCell.png") { }

    public override void OnPressed()
    {
        Global.SceneObjects.Location.SetData(997, true, true);
        ConductivePath.Interaction();
    }
}
