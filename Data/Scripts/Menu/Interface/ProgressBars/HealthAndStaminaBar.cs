using Godot;
using System;

public partial class HealthAndStaminaBar : TextureRect
{
    public override void _Ready()
    {
        OffsetLeft = -Size.Y * (16f/7f);
        AddThemeFontSizeOverride("font_size", ((int)Mathf.Round(Size.Y) - 7) / 16 * 16);
        ((StyleBoxFlat)GetThemeStylebox("normal")).SetExpandMarginAll(Size.Y / 16);
        ((StyleBoxFlat)GetThemeStylebox("focus")).SetExpandMarginAll(Size.Y / 16);
    }

    public void HideBar()
    {
        CreateTween().TweenProperty(this, "position:x", Position.X + 80, 0.5f);
        CreateTween().TweenProperty(this, "modulate:a", 0, 0.5f);
    }

    public void ShowBar()
    {
        CreateTween().TweenProperty(this, "position:x", Position.X - 80, 0.5f);
        CreateTween().TweenProperty(this, "modulate:a", 1, 0.5f);
    }
}
