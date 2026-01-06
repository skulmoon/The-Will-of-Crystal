using Godot;
using System;

public partial class UIDark : TextureRect
{
    private const int TIME = int.MaxValue;
    private bool _darkVisible;
    private float _currentDarkPower = 0.1f;

    private Action DarkShowed;
    private Action DarkHided;

    private FastNoiseLite noise;
    [Export] public float MinlDarkPower { get; set; } = 0.1f;
    [Export] public float CurrentDarkPower
    {
        get => _currentDarkPower;
        set
        {
            _currentDarkPower = value;
            ((ShaderMaterial)Material).SetShaderParameter("dark_power", value);
        }
    }
    [Export] public float DurationTranslate { get; set; } = 0.1f;
    [Export] public bool DarkVisible 
    { 
        get => _darkVisible;
        set
        {
            if (_darkVisible != value)
            {
                _darkVisible = value;
                if (value)
                    ChangeDarkPower(MinlDarkPower);
                else
                    ChangeDarkPower(1);
            }
        }
    }

    public override void _Ready()
    {
        if (Texture is NoiseTexture2D noiseTexture)
        {
            noiseTexture.Width = noiseTexture.Height * GetWindow().Size.X / GetWindow().Size.Y;
            Tween tween = CreateTween();
            tween.TweenProperty(noiseTexture.Noise, "offset:y", TIME, TIME);
            tween.Parallel();
            tween.TweenProperty(noiseTexture.Noise, "offset:z", (long)TIME * 8, TIME);
        }
        CurrentDarkPower = CurrentDarkPower;
        if (DarkVisible)
            ChangeDarkPower(1);
        else
            ChangeDarkPower(MinlDarkPower);
    }

    public override void _Process(double delta)
    {
        Vector2 windowHalf = GetWindow().Size / 2;
        ((ShaderMaterial)Material).SetShaderParameter("cursor_position", (-(GetGlobalMousePosition() - windowHalf) / windowHalf) * 0.05f);
    }

    public void SwitchVisible(Action VisibleEnded)
    {
        if (DarkVisible)
            ShowDark();
        else
            HideDark();
    }

    public void ShowDark(Action darkShowed)
    {
        DarkShowed += darkShowed;
        ShowDark();
    }

    public void ShowDark()
    {
        Tween tween = CreateTween();
        tween.TweenMethod(new Callable(this, "ChangeDarkPower"), MinlDarkPower, 1f, DurationTranslate);
        tween.TweenCallback(new Callable(this, "DarkShowNotify"));
    }

    public void HideDark(Action darkHided)
    {
        DarkHided += darkHided;
        HideDark();
    }

    public void HideDark()
    {
        Tween tween = CreateTween();
        tween.TweenMethod(new Callable(this, "ChangeDarkPower"), 1f, MinlDarkPower, DurationTranslate);
        tween.TweenCallback(new Callable(this, "DarkHideNotify"));
    }

    public void ChangeDarkPower(float power)
    {
        CurrentDarkPower = power;
    }

    public void DarkShowNotify()
    {
        DarkShowed?.Invoke();
        _darkVisible = true;
    }

    public void DarkHideNotify()
    {
        DarkHided?.Invoke();
        _darkVisible = false;
    }

    public override void _ExitTree()
    {
        DarkShowed = null;
        DarkHided = null;
    }
}
