using Godot;
using System;

public partial class ShardCells : Control
{
    public override void _Ready()
    {
        OnResized();
        Resized += OnResized;
    }
    public void HideBar()
    {
        CreateTween().TweenProperty(this, "position:x", Position.X - 80, 0.5f);
        CreateTween().TweenProperty(this, "modulate:a", 0, 0.5f);
    }

    public void ShowBar()
    {
        CreateTween().TweenProperty(this, "position:x", Position.X + 80, 0.5f);
        CreateTween().TweenProperty(this, "modulate:a", 1, 0.5f);
    }

    public void OnResized()
    {
        OffsetRight = Size.Y * 2;
        Position = new Vector2(Size.Y / 32, Position.Y);
        AddThemeFontSizeOverride("font_size", ((int)Mathf.Round(Size.Y) - 7) / 16 * 16);
        ((StyleBoxFlat)GetThemeStylebox("normal")).SetExpandMarginAll(Size.Y / 16);
        ((StyleBoxFlat)GetThemeStylebox("focus")).SetExpandMarginAll(Size.Y / 16);
    }
}
