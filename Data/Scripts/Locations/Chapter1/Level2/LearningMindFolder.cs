using Godot;
using System;

public partial class LearningMindFolder : RichTextLabel
{
    public override void _Ready()
    {
        GetNode<MindFolder>("%MindFolder").Used += Diactivate;
    }

    public void Diactivate()
    {
        CreateTween().TweenProperty(this, "self_modulate:a", 0, 0.5f);
        Global.SceneObjects.Location.SetData(1, true);
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
}
