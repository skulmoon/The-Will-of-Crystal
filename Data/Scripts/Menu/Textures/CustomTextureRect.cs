using Godot;
using System;
using System.Drawing;

public partial class CustomTextureRect : TextureRect
{
    const int INTERFACE_TEORETICAL_Y_SIZE = 256;

    [Export] public bool AutoSetShaerParameters { get; set; } = false;
    [Export] public bool AutoCorrectSize { get; set; } = false;
    [Export] public float TheoreticalXSize { get; set; }
    [Export(PropertyHint.Range, "0,1")] public float RatioX { get; set; } = 0.5f;
    [Export] public bool AutoCorrectPivotOffset { get; set; } = false;
    [Export(PropertyHint.Range, "0,1,or_greater,or_less")] public float PivotOffsetAnchorX { get; set; } = 0;
    [Export(PropertyHint.Range, "0,1,or_greater,or_less")] public float PivotOffsetAnchorY { get; set; } = 0;


    public override void _Ready()
	{
        if (AutoCorrectSize)
        {
            float xSize = (Size.Y / ((AnchorBottom - AnchorTop) * INTERFACE_TEORETICAL_Y_SIZE)) * TheoreticalXSize;
            OffsetLeft = -(xSize * (1 - RatioX));
            OffsetRight = (xSize * RatioX);
        }
        if (AutoCorrectPivotOffset)
        {
            PivotOffset = new Vector2(Size.X * PivotOffsetAnchorX, Size.Y * PivotOffsetAnchorY);
        }
        if (AutoSetShaerParameters)
        {
            SetInstanceShaderParameter("node_rect_size", Size);
        }
        base._Ready();
    }
}
