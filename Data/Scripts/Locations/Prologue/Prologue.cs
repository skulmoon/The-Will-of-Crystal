using Godot;
using System;
using System.Collections.Generic;
using System.Security;

public partial class Prologue : Location
{
    private Timer _timer;

    public override void _Ready()
    {
        base._Ready();
        Global.Music.PlayMusic("FirstCutScene.ogg");
        List<NPC> NPCs = Global.SceneObjects.Npcs;
        CutSceneCustomizes.Add((1, () => NPCs.Find((x) => x.ID == 2).Velocity = Vector2.Zero));
        CutSceneCustomizes.Add((2, () =>
        {
            NPCs.Find((x) => x.ID == 2).Velocity = Vector2.Zero;
            CreateTween().TweenProperty(NPCs.Find((x) => x.ID == 2), "Speed", 300, 5);
            NPCs.Find((x) => x.ID == 0).Visible = true;
        }
        ));
        CutSceneCustomizes.Add((4, () => {
            CreateTween().TweenProperty(NPCs.Find((x) => x.ID == 2), "Speed", 20, 4.5);
            CreateTween().TweenProperty(NPCs.Find((x) => x.ID == 3), "Speed", 20, 4.5);
            CreateTween().TweenProperty(GetTree().CurrentScene.GetNode<Interface>("%Interface").Dark, "modulate:a", 0, 3);
        }));
        CutSceneCustomizes.Add((5, () => NPCs.Find((x) => x.ID == 3).Velocity = Vector2.Zero));
        CutSceneCustomizes.Add((6, () => { 
            Global.SceneObjects.Npcs.Find((x) => x.ID == 3).Velocity = Vector2.Zero;
            NPCs.Find((x) => x.ID == 3).Speed = 80;
        }
        ));
        CutSceneCustomizes.Add((7, () => {
            foreach (Tween tween in GetTree().GetProcessedTweens())
                if (tween.IsValid())
                    tween.Kill();
            Global.SceneObjects.ChangeScene("res://Data/Scenes/Location/Prologue/CharapterRoom.tscn");
        }
        ));
        CutSceneCustomizes.Add((8, () => NPCs.Find((x) => x.ID == 0).Visible = true));
        Global.CutSceneManager.OutputCutScene(2, 1);
    }
}
