using Godot;
using System;

public partial class TwohandedAttackDistanceSkeletonState : Node, IDistantSkeletonState
{
	private DistantSkeleton _enemy;
	private Timer _endTimer = new Timer { Autostart = true, OneShot = true, WaitTime = 3 };
	private Timer _attackTimer = new Timer { Autostart = true, OneShot = true, WaitTime = 2 };

    public TwohandedAttackDistanceSkeletonState(DistantSkeleton enemy)
	{
		_enemy = enemy;
		AddChild(_attackTimer);
		AddChild(_endTimer);
		_attackTimer.Timeout += () => _enemy.Attack(new DistanSkeletonThrowHandAttack(_enemy.Damage, 3, _enemy.GlobalPosition, Global.SceneObjects.Player?.GlobalPosition ?? Vector2.Zero));
		_endTimer.Timeout += OnEndTimeout;
    }

	public void OnEndTimeout()
	{
        ((CircleShape2D)_enemy.Trigger.Shape).Radius = 200;
        if (((CircleShape2D)_enemy.Trigger.Shape).Radius + 64 > _enemy.GlobalPosition.DistanceTo(Global.SceneObjects.Player?.GlobalPosition ?? _enemy.GlobalPosition))
        {
            _enemy.State = new OnehandedAttackDistanceSkeletonState(_enemy);
        }
        else
        {
            _enemy.State = new OnehandedMovementDistantSkeletonState(_enemy);
        }
    }

    public string GetAnimation() =>
        "twohanded_attack";
}
