using Godot;
using System;
using System.Collections.Generic;

public partial class SkeletonBase : Location
{
    public override void _Ready()
    {
        base._Ready();
        Global.Music.PlayMusic("Ending.ogg");
        CutSceneCustomizes.Add((1, () =>
        {
            GetNode<UIDark>("%MenuDark").CurrentDarkPower = 1.6f;
            GetNode<Control>("%Titles").Visible = true;
            Tween tween = CreateTween();
            tween.TweenProperty(GetNode("%Titles"), "position:y", GetNode<Control>("%Titles").Position.Y, 5);
            tween.Chain();
            tween.TweenProperty(GetNode("%Titles"), "position:y", GetWindow().Size.Y * -3.4, 50);
            tween.Chain();
            tween.TweenProperty(GetNode("%Titles"), "position:y", GetWindow().Size.Y * -3.4, 3);
            tween.TweenProperty(GetNode("%MenuDark2"), "CurrentDarkPower", 1.6, 3);
            tween.TweenCallback(new Callable(this, "OpenMainMenu"));
        }
        ));
        Global.CutSceneManager.OutputCutScene(0, 1);
    }

    public void OpenMainMenu()
    {
        Global.SaveManager.SaveGame();
        Global.Inventory.Clear();
        Global.SceneObjects.ChangeScene("res://Data/Scenes/Menu/MainMenu.tscn");
        Global.Settings.CutScene = false;
    }
}