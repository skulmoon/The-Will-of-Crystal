using Godot;
using System;
using System.Collections.Generic;

public partial class NPC : CharacterBody2D
{
    private LinkedListNode<NpcCSData> _nextPAData;
	private Area2D _interactionArea;
    private Timer _timer = new Timer { OneShot = true };
    private Tween _CurrentTween;
    private Action _endPAData;

    [Export] public string NPCInteractionPath { get; set; } = "res://Data/Scripts/Entities/NPC/InteractionDefault.cs";
    [Export] public int ID { get; set; }
    [Export] public float Speed { get; set; } = 60;
    [Export] public AnimatedSprite2D AnimatedSprite2D { get; set; }
    public bool IsMove { get; set; }

    public NPC()
    {
        Global.SceneObjects.Npcs.Add(this);
    }

    public override void _Ready()
	{
        AddChild(_timer);
        _timer.Timeout += ActivePA;
        if (AnimatedSprite2D == null)
            AnimatedSprite2D = GetNodeOrNull<AnimatedSprite2D>("Sprite2D");
        AnimatedSprite2D?.Play();
        try
        {
            _interactionArea = GetNodeOrNull<Area2D>("NPCInteractionArea");
            _interactionArea.SetScript(GD.Load(NPCInteractionPath));
        }
        catch { }
    }

    public override void _ExitTree() =>
        Global.SceneObjects.Npcs.Remove(this);

    public override void _Process(double delta)
    {
        if (IsMove)
        {
            Vector2 direction = (_nextPAData.Value.Position - Position);
            if (direction.Length() > Speed * (float)delta)
            {
                Velocity = Velocity.Lerp(direction.Normalized() * Speed, 20 * (float)delta);
                MoveAndSlide();
            }
            else if (_timer.TimeLeft == 0)
                ActivePA();
        }
    }

    public void GetPA(PAData PAData, Action action)
    {
        var list = new LinkedList<NpcCSData>(PAData.Data);
        _nextPAData = list.First;
        _endPAData += action;
        ActivePA();
        IsMove = true;
        ProcessMode = ProcessModeEnum.Always;
    }

    public void ActivePA()
    {
        if (_nextPAData?.Value.Customize != null)
            ((Location)GetTree().CurrentScene).GetCutSceneCustomize((int)_nextPAData.Value.Customize)?.Invoke();
        if (_nextPAData?.Next != null)
        {
            Position = _nextPAData.Value.Position;
            _nextPAData = _nextPAData.Next;
            if (_nextPAData.Value.Animation != null)
            {
                AnimatedSprite2D.Animation = _nextPAData.Value.Animation;
                AnimatedSprite2D.Play();
            }
            if (_nextPAData.Value.Time != null)
                _timer.Start(_nextPAData.Value.Time ?? 0);
            if (_nextPAData.Value.Sound != null)
                Global.Music.PlaySound(_nextPAData.Value.Sound);
        }
        else
        {
            Position = _nextPAData.Value.Position;
            IsMove = false;
            _endPAData?.Invoke();
        }
    }

    public void StopPAData(FinalValues finalValues)
    {
        _timer.Stop();
        Position = finalValues.Position ?? Vector2.Zero;
        if (finalValues.Animation != null)
        {
            AnimatedSprite2D.Animation = finalValues.Animation;
            AnimatedSprite2D.Play();
        }
        IsMove = false;
        _endPAData?.Invoke();
    }
}
