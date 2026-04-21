using Godot;
using System;

public abstract partial class EnemyAttack : CharacterBody2D
{
	private Timer _lifeTime;
	private CollisionShape2D _collision = new CollisionShape2D();

	public int Damage { get; private set; } = 10;
    public AnimatedSprite2D Sprite { get; private set; }

    public Area2D AttackArea { get; set; }

    public CollisionShape2D Collision
    {
        get => _collision;
        set => _collision = value;
    }

    public Shape2D Shape 
	{
		get => _collision.Shape;
        set => _collision.Shape = value;
	}

    public EnemyAttack(int damage, double lifeTime, string texture = "")
	{

        Area2D area = new Area2D();
        area.Position += new Vector2(0, 32);
        area.AddChild(_collision);
        area.BodyEntered += OnPlayerAttackEntered;
        area.AreaEntered += OnPlayerAttackEntered;
        AddChild(area);
        area.CollisionLayer = 16;
        area.CollisionMask = 16;
        AttackArea = area;
        Damage = damage;
		_lifeTime = new Timer()
		{
			WaitTime = lifeTime,
			Autostart = true,
			OneShot = true,

		};
		AddChild(_lifeTime);
		_lifeTime.Timeout += Destroy;
		if (texture != string.Empty)
		{
            Sprite = GD.Load<PackedScene>($"res://Data/Textures/Entities/Enemys/{texture}").Instantiate<AnimatedSprite2D>();
            AddChild(Sprite);
            Sprite.Play("default");
        }
    }

    public override void _Ready()
    {
        Position += new Vector2(0, -32);
        base._Ready();
    }

    public virtual void OnPlayerAttackEntered(Node2D node)
	{
		if (node is PlayerAttack playerAttack)
			AttackPlayerAttack(playerAttack);
		else if (node is HitBox hitBox)
			AttackPlayer(hitBox);
	}

    public virtual void Destroy() =>
		QueueFree();

    public virtual void AttackPlayerAttack(PlayerAttack playerAttack)
    {
        playerAttack.TakeDamage(Damage, this);
        Destroy();
    }

    public virtual void AttackPlayer(HitBox hitBox)
	{
        hitBox.TakeDamage(Damage);
        Destroy();
    }
}
