using Godot;
using System;

public partial class DistantSkeleton : Enemy
{
    private IDistantSkeletonState _state;
    private Player _player;

    public CustomTrigger Trigger { get; set; }
    public IDistantSkeletonState State
    {
        get => _state;
        set
        {
            SetState((Node)value, (Node)_state);
            _state = value;
        }
    }

    public DistantSkeleton() : base(speed: 200, damage: 20, health: 40, "Charapter1/DistantSkeleton.tscn")
    {
        State = new СalmDistantSkeletonState(this);
        Trigger = new CustomTrigger(4,
            new CircleShape2D()
            {
                Radius = 400,
            },
            (node) =>
            {
                if (node is Player)
                    State.Attack();
            }
        );
        AddChild(Trigger);
    }

    public override EnemyType GetEnemyType() =>
        EnemyType.Distant;
}
