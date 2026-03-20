using Godot;
using System;

public partial class SkeletonBaseHeadquarters : Chapter1Location
{
    public override void _Ready()
    {
        base._Ready();
        Global.Music.PlayMusic("Ending.ogg");
        CutSceneCustomizes.Add((1, () =>
        {
            Tween tween = CreateTween();
            tween.TweenProperty(GetNode("%MenuDark"), "CurrentDarkPower", 1.6, 3);
            tween.TweenCallback(new Callable(this, nameof(OpenChangeLocation)));
        }
        ));
        switch (Global.CutSceneData.GetChoice(5, 1))
        {
            case 2:
                switch (Global.CutSceneData.GetChoice(5, 4))
                {
                    case 5:
                        Global.CutSceneManager.OutputCutScene(5, 21);
                        break;
                    case 11:
                        Global.CutSceneManager.OutputCutScene(5, 22);
                        break;
                    case -1:
                        Global.CutSceneManager.OutputCutScene(5, 4);
                        break;
                };
                break;
            case 3:
                switch (Global.CutSceneData.GetChoice(5, 7))
                {
                    case 9:
                        Global.CutSceneManager.OutputCutScene(5, 21);
                        break;
                    case 8:
                        Global.CutSceneManager.OutputCutScene(5, 14);
                        break;
                    case 12:
                        Global.CutSceneManager.OutputCutScene(5, 14);
                        break;
                    case -1:
                        Global.CutSceneManager.OutputCutScene(5, 7);
                        break;
                };
                break;
            case -1:
                Global.CutSceneManager.OutputCutScene(5, 1);
                break;
        };
    }

    public void OpenChangeLocation()
    {
        ConductivePath conductive = new ConductivePath()
        {
            Path = "Chapter1/SkeletonBase",
            EndPosition = new Vector2(832, -272),
        };
        AddChild(conductive);
        ((CameraNPC)Global.SceneObjects.Npcs.Find(x => x.ID == 3)).ChangeEnabled(false);
        Global.Settings.CutScene = false;
        conductive.Interaction();
    }
}
