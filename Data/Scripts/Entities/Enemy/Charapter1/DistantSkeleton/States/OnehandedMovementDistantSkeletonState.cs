using Godot;

public partial class OnehandedMovementDistantSkeletonState : Node2D, IDistantSkeletonState
{
    private DistantSkeleton _enemy;

    public OnehandedMovementDistantSkeletonState(DistantSkeleton enemy)
    {
        _enemy = enemy;
    }

    public override void _PhysicsProcess(double delta)
    {
        _enemy.Move(_enemy.PositionControl?.GetPosition() ?? Vector2.Zero, delta);
    }

    public void Attack()
    {
        _enemy.State = new OnehandedAttackDistanceSkeletonState(_enemy);
    }

    public string GetAnimation() =>
        "onehanded_movement";
}
