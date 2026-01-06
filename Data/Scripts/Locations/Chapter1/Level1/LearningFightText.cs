using Godot;
using System;

public partial class LearningFightText : RichTextLabel
{
    public override void _Ready()
    {
        Global.SceneObjects.LocationChanged += OnLocationChanged;
    }

    public void OnLocationChanged(Location location)
    {
        if (location.GetData<bool?>(1) ?? false)
            return;
        location.SetData(1, true);
        Tween tween = CreateTween();
        tween.TweenProperty(this, "self_modulate:a", 0, 1f);
        tween.Chain();
        tween.TweenProperty(this, "self_modulate:a", 1, 0.5f);
        Global.SceneObjects.EnemyFabric.EnemiesDestroyed += Diactivate;
    }

    public void Diactivate()
    {
        CreateTween().TweenProperty(this, "self_modulate:a", 0, 0.5f);
    }

    public override void _ExitTree()
    {
        Global.SceneObjects.LocationChanged -= OnLocationChanged;
        Global.SceneObjects.EnemyFabric.EnemiesDestroyed -= Diactivate;
    }
}
