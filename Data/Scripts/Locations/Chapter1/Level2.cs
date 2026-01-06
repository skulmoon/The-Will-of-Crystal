using Godot;
using System;

public partial class Level2 : Chapter1Location
{
    public override void _Ready()
    {
        base._Ready();
        if (GetData<bool?>(GetNode<MindFolder>("%MindFolder").ID) ?? false)
        {
            GetNode<Node2D>("%HideBackground").ChangeAlphaModulate(1);
            GetNode<Node2D>("%HideDecorativeObjects").ChangeAlphaModulate(1);
            GetNode<Node2D>("%Area2D2").ChangeAlphaModulate(1);
            RemoveChild(GetNode("%Barier"));
            return;
        }
        else
            GetNode<MindFolder>("%MindFolder").Used += OnUsed;
    }

    public void OnUsed()
    {
        CreateTween().TweenProperty(GetNode("%HideBackground"), "modulate:a", 1, 2);
        CreateTween().TweenProperty(GetNode("%HideDecorativeObjects"), "modulate:a", 1, 2);
        CreateTween().TweenProperty(GetNode("%Area2D2"), "modulate:a", 1, 2);
        RemoveChild(GetNode("%Barier"));
    }

    public override void _ExitTree()
    {
        GetNode<MindFolder>("%MindFolder").Used -= OnUsed;
    }
}
