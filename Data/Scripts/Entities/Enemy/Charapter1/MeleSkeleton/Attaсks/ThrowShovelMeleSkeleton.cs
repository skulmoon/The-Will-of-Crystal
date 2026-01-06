using Godot;
using System;
using static Godot.TextServer;

public partial class ThrowShovelMeleSkeleton : EnemyAttack
{
    private Vector2 _direction;

    public ThrowShovelMeleSkeleton(int damage, double lifeTime, Vector2 enemyPosition, Vector2 targetPosition) : base(damage, lifeTime)
    {
        Shape = new RectangleShape2D()
        {
            Size = new Vector2(4, 30)
        };
        AnimatedSprite2D sprite = GD.Load<PackedScene>("res://Data/Textures/Entities/Enemys/Charapter1/SkeletonShovel.tscn").Instantiate<AnimatedSprite2D>();
        sprite.Play("default");
        AddChild(sprite);
        GlobalPosition = enemyPosition;
        _direction = GlobalPosition.DirectionTo(targetPosition);
    }

    public override void _PhysicsProcess(double delta)
    {
        GlobalPosition += _direction * 600 * (float)delta;
        Collision.Rotate(Mathf.DegToRad(-300) * (float)delta);
    }
}
