using Godot;
using System;

public abstract partial class AbilityBar : ProgressBar
{
    private Label _label;
    private string _text;
    private Tween _currentTween;

    [Export] public Vector2 HidePosition { get; set; } = new Vector2(-80, 0);

    public override void _Ready()
    {
        Global.SceneObjects.PlayerChanged += OnPlayerChanged;
    }

    public abstract void OnPlayerChanged(Player player);

    public void OnAbilityReloadStarted(float time)
    {
        Value = time;
        MaxValue = time;
        _currentTween = CreateTween();
        _currentTween.TweenProperty(this, "value", 0, time);
    }

    public void SetAbilityName(string name)
    {
        if (_label == null)
            _text = name;
    }

    public void Close()
    {
        SetAbilityName(string.Empty);
        _currentTween?.Stop();
    }

    public override void _ExitTree()
    {
        Global.SceneObjects.PlayerChanged -= OnPlayerChanged;
    }

    public void HideBar()
    {
        CreateTween().TweenProperty(this, "position", Position + HidePosition, 0.5f);
        CreateTween().TweenProperty(this, "modulate:a", 0, 0.5f);
    }

    public void ShowBar()
    {
        CreateTween().TweenProperty(this, "position", Position - HidePosition, 0.5f);
        CreateTween().TweenProperty(this, "modulate:a", 1, 0.5f);
    }
}
