using Godot;
using System;

public partial class DistanSkeletonThrowHandAttack : EnemyAttack
{
    private Vector2 _direction;

    public DistanSkeletonThrowHandAttack(int damage, double lifeTime, Vector2 enemyPosition, Vector2 targetPosition) : base(damage, lifeTime)
    {
        Shape = new RectangleShape2D()
        {
            Size = new Vector2(4, 30)
        };
        GlobalPosition = enemyPosition;
        _direction = GlobalPosition.DirectionTo(targetPosition);
        Sprite2D sprite = GD.Load<PackedScene>("res://Data/Textures/Entities/Enemys/Chapter1/ShovelAttack.tscn").Instantiate<Sprite2D>();
        Collision.AddChild(sprite);
    }

    public override void _PhysicsProcess(double delta)
    {
        GlobalPosition += _direction * 600 * (float)delta;
        Rotate(Mathf.DegToRad(-300) * (float)delta);
    }
}
