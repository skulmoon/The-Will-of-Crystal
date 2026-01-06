using Godot;
using System;

public partial class LerningConductivePathText : RichTextLabel
{
    public override void _Ready()
    {
        Text = string.Format(Text, this.GetActionKey("interact"));
    }

    public void Activate()
    {
        if (SelfModulate.A == 0)
        {
            Tween tween = CreateTween();
            tween.TweenProperty(this, "self_modulate:a", 0, 1f);
            tween.Chain();
            tween.TweenProperty(this, "self_modulate:a", 1, 0.5f);
        }
    }
}
