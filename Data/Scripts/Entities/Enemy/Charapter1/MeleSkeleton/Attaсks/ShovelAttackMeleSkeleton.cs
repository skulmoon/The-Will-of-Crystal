using Godot;
using System;
using static Godot.TextServer;

public partial class ShovelAttackMeleSkeleton : EnemyAttack
{
    public ShovelAttackMeleSkeleton(int damage, Vector2 enemyPosition, Vector2 targetPosition) : base(1, 0.5f, "Chapter1/ShovelAttack.tscn")
    {
        Shape = new RectangleShape2D()
        {
            Size = new Vector2(4, 20)
        };
        GlobalPosition = enemyPosition;
        Node2D node = new Node2D();
        AddChild(node);
        RemoveChild(AttackArea);
        node.AddChild(AttackArea);
        //AttackArea.Position = new Vector2(0, 32);
        Collision.Position = new Vector2(0, -40);
        Sprite.Rotation = enemyPosition.AngleToPoint(targetPosition) + Mathf.DegToRad(90);
        Sprite.Position = Vector2.FromAngle(Sprite.Rotation + Mathf.DegToRad(-90)) * 8 + new Vector2(0, 32);
        AttackArea.Rotation = enemyPosition.AngleToPoint(targetPosition) + Mathf.DegToRad(30);
        //Collision.Rotate(Mathf.DegToRad(-60));
    }

    public override void _PhysicsProcess(double delta)
    {
        AttackArea.Rotate(Mathf.DegToRad(240) * (float)delta);
    }
}
