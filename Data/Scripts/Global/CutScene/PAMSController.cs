using Godot;
using System;
using System.Collections.Generic;
using System.Xml.Linq;

public partial class PAMSController : Node
{
    private CutSceneManager _cutSceneManager;
    private LinkedListNode<PAMS> _pamses;
    private List<NPC> _npcs = new List<NPC>();
    private int? _finalCustomize;
    private List<FinalValues> _finalValues;
    private int _endCount = 0;

    public bool IsDone { get; private set; } = true;

    public PAMSController(CutSceneManager cutSceneManager)
    {
        _cutSceneManager = cutSceneManager;
    }

    public void SetPAMS(List<PAMS> pamses)
    {
        if (pamses != null)
            _pamses = new LinkedList<PAMS>(pamses).First;
        else 
            _pamses = null;
    }

    int count = 0;

    public void NextPAMS()
    {
        count++;
        if (count == 5)
            GD.Print(_pamses);
        if (_pamses?.Value != null)
        {
            IsDone = false;
            if (_pamses.Value?.Music != null)
                Global.Music.PlayMusic(_pamses.Value?.Music);
            if (_pamses.Value?.PAData != null)
            {
                _finalCustomize = _pamses.Value?.FinalCustomize;
                _finalValues = _pamses.Value?.FinalValues;
                foreach (PAData paData in _pamses.Value?.PAData)
                    foreach (NPC npc in Global.SceneObjects.Npcs)
                        if (paData.NPCID == npc.ID)
                            npc.GetPA(paData, PADataEnd);
            }
        }
        _pamses = _pamses?.Next;
    }

    private void PADataEnd()
    {
        _endCount++;
        if (_endCount >= (_pamses?.Value?.PAData?.Count ?? 0))
        {
            IsDone = true;
            _endCount = 0;
            if (_pamses?.Value == null && !_cutSceneManager.IsPanelActive)
            {
                _cutSceneManager.NextCutScenePart();
            }
        }
    }

    public void EndPAMS()
    {
        IsDone = true;
        if (_finalCustomize != null)
            Global.SceneObjects.Location.GetCutSceneCustomize(_finalCustomize ?? 0)?.Invoke();
        if (_finalValues != null)
        {
            foreach (var finalValues in _finalValues)
                foreach (NPC npc in Global.SceneObjects.Npcs)
                    if (finalValues.ID == npc.ID)
                        npc.StopPAData(finalValues);
        }
    }
}
