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
        AnimatedSprite2D sprite = GD.Load<PackedScene>("res://Data/Textures/Entities/Enemys/Chapter1/FlyingHand.tscn").Instantiate<AnimatedSprite2D>();
        AddChild(sprite);
        sprite.Play("default");
        GlobalPosition = enemyPosition;
        _direction = GlobalPosition.DirectionTo(targetPosition);
    }

    public override void _PhysicsProcess(double delta)
    {
        GlobalPosition += _direction * 500 * (float)delta;
        Collision.Rotate(Mathf.DegToRad(225) * (float)delta);
    }
}
