using Godot;
using System;
using System.Collections.Generic;

public partial class MindCellConnection : Area2D
{
    private TextureRect _texture;

    public float RepulsingPower { get; private set; }
    public List<Area2D> RepulsionConnections { get; set; } = new List<Area2D>();

    public override void _Ready()
    {
        _texture = GetNode<TextureRect>("TextureRect");
        Area2D area = GetNode<Area2D>("Repulsion");
        area.AreaEntered += OnRepulsionEntered;
        area.AreaExited += OnRepulsionExited;
        RepulsingPower = ((CircleShape2D)area.GetNode<CollisionShape2D>("CollisionShape2D").Shape).Radius;
    }

    public void OnRepulsionEntered(Area2D area)
    {
        RepulsionConnections.Add(area);
    }

    public void OnRepulsionExited(Area2D area)
    {
        RepulsionConnections.Remove(area);
    }

    public void SetLineSize(float size) =>
        _texture.Size = new Vector2(2, size + 2);
}
