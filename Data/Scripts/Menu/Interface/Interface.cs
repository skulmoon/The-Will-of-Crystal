using Godot;
using System;

public partial class Interface : CanvasLayer
{
    [Export] public UIDark Dark { get; set; }
    [Export] public UIDark MenuDark { get; set; }

    public Interface()
    {
        Global.CutSceneManager.StartedCutScene += HideBars;
        Global.CutSceneManager.EndedCutScene += ShowBars;
    }

    public void HideBars()
    {
        GetNodeOrNull<ShardCells>("Shards")?.HideBar();
        GetNodeOrNull<HealthAndStaminaBar>("HealthAndStaminaBar")?.HideBar();
    }

    public void ShowBars()
    {
        GetNodeOrNull<ShardCells>("Shards")?.ShowBar();
        GetNodeOrNull<HealthAndStaminaBar>("HealthAndStaminaBar")?.ShowBar();
    }

    public override void _ExitTree()
    {
        Global.CutSceneManager.StartedCutScene -= HideBars;
        Global.CutSceneManager.EndedCutScene -= ShowBars;
    }
}
