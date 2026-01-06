using Godot;
using System;

public partial class LearningMindFolder : RichTextLabel
{
    private EnemyFabric _enemyFabric;

    public override void _Ready()
    {
        Global.SceneObjects.EnemyFabricChanged += OnEnemyFabricChanged;
    }

    public void OnEnemyFabricChanged(EnemyFabric fabric)
    {
        _enemyFabric = fabric;
        fabric.EnemiesDestroyed += Diactivate;
    }

    public void Diactivate()
    {
        CreateTween().TweenProperty(this, "self_modulate:a", 0, 0.5f);
    }

    public void OnBodyEntered(Node2D node)
    {
        if (Global.SceneObjects.Location.GetData<bool?>(1) ?? false)
            return;
        Global.SceneObjects.Location.SetData(1, true);
        Tween tween = CreateTween();
        tween.TweenProperty(this, "self_modulate:a", 0, 1f);
        tween.Chain();
        tween.TweenProperty(this, "self_modulate:a", 1, 0.5f);
    }

    public override void _ExitTree()
    {
        _enemyFabric.EnemiesDestroyed -= Diactivate;
        Global.SceneObjects.EnemyFabricChanged -= OnEnemyFabricChanged;
    }
}
