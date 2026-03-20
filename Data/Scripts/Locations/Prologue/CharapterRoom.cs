using Godot;
using System;
using System.Collections.Generic;

public partial class CharapterRoom : Location
{
    public override void _Ready()
    {
        base._Ready();
        List<NPC> NPCs = Global.SceneObjects.Npcs;
        CutSceneCustomizes.Add((1, () => {
            Global.CutSceneManager.NextCutScenePart();
            GetNode<ConductivePath>("%Bed")?.Interaction();
        }));
        CutSceneCustomizes.Add((2, () => {
            GetNode<Node2D>("%TileMapLayer").Visible = false;
            GetNode<Node2D>("%DecorativeObjects").Visible = false;
            Global.Music.PlayMusic(null);
        }
        ));
        CutSceneCustomizes.Add((3, () => { 
            GetNode<Node2D>("%Human").Visible = false;
        }));
        CutSceneCustomizes.Add((4, () => base.StarAnimation()));
        if (GetData<bool?>(1) ?? true)
        {
            SetData(1, false);
            Global.CutSceneManager.OutputCutScene(0, 3);
        }
    }

    public override void StarAnimation()
    {
        UIDark dark = GetNode<Interface>("%Interface").Dark;
        dark.CurrentDarkPower = 1;
        dark.Visible = true;
    }
}
