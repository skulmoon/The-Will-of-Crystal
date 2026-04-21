using Godot;
using System;

public partial class DialogueButton : CustomButton
{
    [Export] public DialogText DialogText { get; set; }
    [Export] public bool IsDownButton { get; set; }

    public override void _Ready()
    {
        Pressed += OnPressed;
        base._Ready();
    }

    public void OnPressed()
    {
        if (IsDownButton)
            DialogText.DownOption();
        else
            DialogText.UpOption();
    }

    public override void OnMouseEntered()
    {
        base.OnMouseEntered();
        CreateTween().TweenProperty(this, "scale", new Vector2(1.2f, 1.2f), 0.2);
    }

    public override void OnMouseExited()
    {
        base.OnMouseExited();
        CreateTween().TweenProperty(this, "scale", new Vector2(1, 1), 0.2);
    }   
}
