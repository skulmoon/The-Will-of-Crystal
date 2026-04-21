using Godot;
using System;

public partial class CustomButton : TextureButton
{
    private bool _isButtonDown = false;
    private bool _isMouseEntered = false;
    private Texture2D _textureFocused;
    private Label _label = new();
    [Export] public string Text { get => _label.Text; set => _label.Text = value; }
    [Export] public bool AutoCorrect { get; set; } = false;
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
        if (HasFocus())
            CreateTween().TweenProperty(_label, "modulate:a", 1f, 0.2f);
        _textureFocused = TextureFocused;
        _label.SetAnchorsPreset(LayoutPreset.FullRect);
        _label.HorizontalAlignment = HorizontalAlignment.Center;
        _label.VerticalAlignment = VerticalAlignment.Center;
        if (AutoCorrect)
        {
            Vector2 textureSize = TextureNormal.GetSize();
            float xSize = Size.Y * textureSize.X / textureSize.Y;
            OffsetLeft = -(xSize * (1 - RatioX));
            OffsetRight = (xSize * RatioX);
        }
        if (AutoCorrectPivotOffset)
        {
            PivotOffset = new Vector2(Size.X * PivotOffsetAnchorX, Size.Y * PivotOffsetAnchorY);
        }
        _label.LabelSettings = new LabelSettings
        {
            Font = GD.Load<Font>("res://Data/Textures/EpilepsySans.ttf"),
            FontSize = this.CalculateFontSize(Size.Y),
        };
        _label.Modulate = new Color(1, 1, 1, 0.6f);
        AddChild(_label);
        Tween tween = CreateTween().SetLoops(int.MaxValue);
        tween.TweenProperty(_label.LabelSettings, "font_color:a", 1, 1);
        tween.Chain();
        tween.TweenProperty(_label.LabelSettings, "font_color:a", 0.9f, 1);
        OnButtonUp();
        ButtonDown += OnButtonDown;
        ButtonUp += OnButtonUp;
        MouseEntered += OnMouseEntered;
        MouseExited += OnMouseExited;
        FocusExited += () => CreateTween().TweenProperty(_label, "modulate:a", 0.6f, 0.2f);
        Pressed += () => Global.Music.PlaySound("ButtonPressed.ogg");
        base._Ready();
    }

    public void OnButtonDown()
    {
        _isButtonDown = true;
        if (_isMouseEntered)
        {
            UpdateLabelLayout(0.2f, 1);
            TextureFocused = TexturePressed;
        }
    }

    public void OnButtonUp()
    {
        _isButtonDown = false;
        UpdateLabelLayout(0, 0.9f);
        TextureFocused = _textureFocused;
    }

    public virtual void OnMouseEntered()
    {
        Global.Music.PlaySound("ButtonSelected.ogg", 0.2f);
        _isMouseEntered = true;
        CreateTween().TweenProperty(_label, "modulate:a", 1, 0.2f);
        if (_isButtonDown)
        {
            UpdateLabelLayout(0.2f, 1);
            TextureFocused = TexturePressed;
        }
    }

    public virtual void OnMouseExited()
    {
        if (!HasFocus())
            CreateTween().TweenProperty(_label, "modulate:a", 0.6f, 0.2f);
        _isMouseEntered = false;
        UpdateLabelLayout(0, 0.9f);
        TextureFocused = _textureFocused;
    }

    private void UpdateLabelLayout(float top, float bottom)
    {
        RemoveChild(_label);
        _label.SetAnchor(Side.Top, top);
        _label.SetAnchor(Side.Bottom, bottom);
        AddChild(_label);
    }
}
