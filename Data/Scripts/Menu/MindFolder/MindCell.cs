using Godot;
using System;

public abstract partial class MindCell : CharacterBody2D
{
    public Vector2 CenterPosition = Vector2.Zero;
    public float Speed { get; set; } = 5000;
    public TextureButton Button { get; private set; }
    public Node2D Scaler { get; private set; }
    public MindCellConnection Line { get; private set; }
    public Vector2 RepulsingVector { get; set; }

    public MindCell(string textureNormal = "", string texturePressed = "")
    {
        Line = GD.Load<PackedScene>("res://Data/Scenes/Menu/MindFolder/mind_cell_connection.tscn").Instantiate<MindCellConnection>();
        AddChild(Line);
        Scaler = new Node2D();
        AddChild(Scaler);
        Button = new TextureButton();
        Scaler.AddChild(Button);
        Button.TextureNormal = GD.Load<Texture2D>(textureNormal);
        Button.TexturePressed = GD.Load<Texture2D>(texturePressed);
        Button.MouseExited += OnMouseExited;
        Button.MouseEntered += OnMouseEntered; 
        Button.Pressed += OnPressed;
    }

    public override void _Ready()
    {
        Button.Position = new Vector2(-16, -16);
        Button.Size = new Vector2(32, 32);
        CenterPosition = GetViewport().GetWindow().Size / 2 / 3;
    }

    public abstract void OnPressed();

    public void OnMouseExited()
    {
        CreateTween().TweenProperty(Scaler, "scale", new Vector2(1, 1), 0.2f).SetTrans(Tween.TransitionType.Sine);
    }

    public void OnMouseEntered()
    {
        CreateTween().TweenProperty(Scaler, "scale", new Vector2(1.2f, 1.2f), 0.2f).SetTrans(Tween.TransitionType.Sine);
    }

    public override void _PhysicsProcess(double delta)
    {
        Vector2 repulsingVector = Vector2.Zero;
        Vector2 connectionVector = Vector2.Zero;
        foreach (var connection in Line.RepulsionConnections)
        {
            connectionVector = GlobalPosition - connection.GlobalPosition;
            repulsingVector += (connectionVector.Normalized() - connectionVector / Line.RepulsingPower) * 2;
        }
        Velocity = Velocity.Lerp((GetGravityVector() + repulsingVector) * (float)delta * Speed, 0.6f);
        MoveAndSlide();
    }

    public Vector2 GetGravityVector()
    {
        Vector2 vector = Vector2.Zero;
        if (GlobalPosition.DistanceTo(CenterPosition) > 8)
            vector = GlobalPosition.DirectionTo(CenterPosition);
        return vector;
    }

    public virtual void Activate()
    {
        Visible = true;
        ProcessMode = ProcessModeEnum.Inherit;
    }

    public virtual void Deactivate()
    {
        Visible = false;
        ProcessMode = ProcessModeEnum.Disabled;
    }
}
