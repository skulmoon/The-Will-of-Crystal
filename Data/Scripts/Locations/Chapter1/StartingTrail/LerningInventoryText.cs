using Godot;
using System;

public partial class LerningInventoryText : RichTextLabel
{
    public override void _Ready()
    {
        Text = string.Format(Text, this.GetActionKey("open_inventory"));
    }

    public void Activate()
    {
        Tween tween = CreateTween();
        tween.TweenProperty(this, "self_modulate:a", 0, 1f);
        tween.Chain();
        tween.TweenProperty(this, "self_modulate:a", 1, 0.5f);
    }

    public void Diactivate()
    {
        CreateTween().TweenProperty(this, "self_modulate:a", 0, 0.5f);
        GetNode<LerningConductivePathText>("%LocationText").Activate();
    }

    public override void _Input(InputEvent @event)
    {
        if (@event.IsActionPressed("open_inventory"))
            Diactivate();
    }
}
