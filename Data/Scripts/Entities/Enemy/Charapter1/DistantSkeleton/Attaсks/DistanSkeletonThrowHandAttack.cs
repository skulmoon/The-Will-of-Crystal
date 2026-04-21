using Godot;
using System;

public partial class DistanSkeletonThrowHandAttack : EnemyAttack
{
    private Vector2 _direction;

    public DistanSkeletonThrowHandAttack(int damage, double lifeTime, Vector2 enemyPosition, Vector2 targetPosition) : base(damage, lifeTime, "Chapter1/FlyingHand.tscn")
    {
        Shape = new RectangleShape2D()
        {
            Size = new Vector2(4, 20)
        };
        GlobalPosition = enemyPosition;
        _direction = GlobalPosition.DirectionTo(targetPosition);
    }

    public override void _PhysicsProcess(double delta)
    {
        MoveAndCollide(_direction * 400 * (float)delta);
        Collision.Rotate(Mathf.DegToRad(225) * (float)delta);
    }
}
