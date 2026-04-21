using Godot;
using System;

public partial class CustomLabel : Label
{
    const int INTERFACE_TEORETICAL_Y_SIZE = 256;

    [Export] public bool AutoSetShaerParameters { get; set; } = false;
    [Export] public bool AutoCorrectFontSize { get; set; } = false;
    [Export] public bool AutoCorrectSize { get; set; } = false;
    [Export] public float TheoreticalXSize { get; set; }
    [Export(PropertyHint.Range, "0,1")] public float RatioX { get; set; } = 0.5f;
    [Export] public bool AutoCorrectPivotOffset { get; set; } = false;
    [Export(PropertyHint.Range, "0,1,or_greater,or_less")] public float PivotOffsetAnchorX { get; set; } = 0;
    [Export(PropertyHint.Range, "0,1,or_greater,or_less")] public float PivotOffsetAnchorY { get; set; } = 0;
    [Export] public bool CustomYSize { get; set; } = false;
    [Export] public float TheoreticalYSize { get; set; }

    public override void _Ready()
    {
        Text = Tr(Text);
        if (AutoCorrectSize)
        {
            float xSize;
            if (!CustomYSize)
                xSize = (Size.Y / ((AnchorBottom - AnchorTop) * INTERFACE_TEORETICAL_Y_SIZE)) * TheoreticalXSize;
            else
                xSize = (Size.Y / TheoreticalYSize) * TheoreticalXSize;
            OffsetLeft = -(xSize * (1 - RatioX));
            OffsetRight = (xSize * RatioX);
        }
        if (AutoCorrectFontSize)
        {
            LabelSettings = new LabelSettings
            {
                Font = GD.Load<Font>("res://Data/Textures/EpilepsySans.ttf"),
                FontSize = this.CalculateFontSize(Size.Y),
            };
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
