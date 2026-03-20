using Godot;
using System;

public partial class PixelFormater : TextureRect
{
    private Vector2 _textureSize;

    public override void _Ready()
    {
        GD.Print(Size);
        _textureSize = Texture.GetSize();
        float xSize = Size.Y * _textureSize.X / _textureSize.Y;
        OffsetLeft = -xSize / 2;
        OffsetRight = xSize / 2;
        AddThemeFontSizeOverride("font_size", ((int)Mathf.Round(Size.Y) - 7) / 16 * 16);
    }
}
