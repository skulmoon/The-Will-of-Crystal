using Godot;
using System;
using System.Reflection;

public partial class MindFolder : CanvasLayer
{
    private bool _isUsed = false;
    private FolderCell _cell;

    [Export] public TextureRect Dark { get; set; }
    [Export] public int ID { get; set; }


    public event Action Used;

    public override void _Ready()
    {
        _cell = (FolderCell)GetChildren()[1];
        Global.SceneObjects.LocationChanged += OnLocationChanged;
    }

    public void OnLocationChanged(Location location)
    {
        RemoveChild(_cell);
        if (location.GetData<bool?>(ID) ?? false)
        {
            _isUsed = true;
            return;
        }
        _cell.Position = GetViewport().GetWindow().Size / 2 / Scale;
        Color modulate = _cell.Modulate;
        modulate.A = 0;
        _cell.Modulate = modulate;
    }

    public void Open()
    {
        if (!_isUsed)
        {
            AddChild(_cell);
            _cell.Activate();
            Visible = true;
            Dark.Visible = true;
            CreateTween().TweenProperty(_cell, "modulate:a", 1, 0.5f);
            CreateTween().TweenProperty(Dark, "modulate:a", 1, 0.5f);
        }
    }

    public void Close()
    {
        Tween tween = CreateTween();
        tween.TweenProperty(_cell, "modulate:a", 0, 0.5f);
        tween.TweenCallback(new Callable(this, nameof(CloseEnded)));
        CreateTween().TweenProperty(Dark, "modulate:a", 0, 0.5f);
        RemoveChild(_cell);
        Global.SceneObjects.Location.SetData(ID, true);
    }

    public void CloseEnded()
    {
        Dark.Visible = false;
        Visible = false;
        _isUsed = true;
        Used?.Invoke();
    }

    public override void _ExitTree()
    {
        Global.SceneObjects.LocationChanged -= OnLocationChanged;
    }
}
