using Godot;
using System;
using System.Collections.Generic;

public partial class FolderCell : MindCell
{
    private bool _isOpen;

    [Export] public bool IsActive { get; set; }
    public List<MindCell> MindCells { get; set; } = new List<MindCell>();

    public bool IsOpen 
    { 
        get => _isOpen; 
        set => _isOpen = value; 
    }

    private Texture2D _normalTexture;

    public FolderCell() : base("res://Data/Textures/MindFolder/FolderCell.png", "res://Data/Textures/MindFolder/PressedFolderCell.png")
    {
        _normalTexture = Button.TextureNormal;
    }

    public override void _Ready()
    {
        base._Ready();
        foreach (Node node in GetChildren())
            if (node is MindCell cell)
                MindCells.Add(cell);
    }

    public override void OnPressed()
    {
        if (!_isOpen)
        {
            foreach (var cell in MindCells)
            {
                if (cell is ChildCell childCell)
                    if (Global.SceneObjects?.Location?.GetData<bool?>(997) ?? false)
                        continue;
                cell.GlobalPosition = GlobalPosition + new Vector2((float)GD.RandRange(-1f, 1f), (float)GD.RandRange(-1f, 1f));
                cell.Activate();
            }
        }
        else
        {
            foreach (var cell in MindCells)
                cell.Deactivate();
        }
        _isOpen = !_isOpen;
    }

    public override void _Process(double delta)
    {
        base._Process(delta);
        if (_isOpen)
        {
            foreach (var cell in MindCells)
            {
                cell.Line.SetLineSize(cell.GlobalPosition.DistanceTo(GlobalPosition));
                cell.Line.Rotation = cell.GlobalPosition.AngleToPoint(GlobalPosition) + Mathf.DegToRad(-90);
            }
        }
    }

    public override void Activate()
    {
        base.Activate();
        if (_isOpen)
            foreach (var cell in MindCells)
            {
                cell.Activate();
                cell.GlobalPosition = GlobalPosition + new Vector2(GD.RandRange(-4, 4), GD.RandRange(-4, 4));
            }
    }

    public override void Deactivate()
    {
        base.Deactivate();
        if (_isOpen)
            foreach (var cell in MindCells)
                cell.Deactivate();
    }
}
