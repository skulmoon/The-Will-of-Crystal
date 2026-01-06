using Godot;
using System;

public partial class ActivateCell : MindCell
{
    public ActivateCell() : base("res://Data/Textures/MindFolder/ActivateCell.png", "res://Data/Textures/MindFolder/PressedActivateCell.png") { }

    public override void OnPressed()
    {
        Node parent = GetParent();
        while (parent is not MindFolder)
            parent = parent.GetParent();
        ((MindFolder)parent).Close();
    }
}

