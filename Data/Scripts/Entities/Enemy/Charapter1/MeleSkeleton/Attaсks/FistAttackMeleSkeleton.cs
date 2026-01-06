using Godot;
using System;

public partial class FistAttackMeleSkeleton : EnemyAttack
{
    private Vector2 _direction;

    public FistAttackMeleSkeleton(int damage, Vector2 enemyPosition, Vector2 targetPosition) : base(damage, 0.25, "res://Data/Textures/Entities/Enemys/Charapter1/WhiteCube.tscn")
    {
        Shape = new CircleShape2D()
        {
            Radius = 8
        };
        GlobalPosition = enemyPosition;
        _direction = GlobalPosition.DirectionTo(targetPosition);
    }

    public override void _PhysicsProcess(double delta)
    {
        GlobalPosition += _direction * 400 * (float)delta;
    }
}
