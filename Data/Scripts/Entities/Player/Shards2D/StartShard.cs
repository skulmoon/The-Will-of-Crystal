using Godot;
using System;

public partial class StartShard : Shard2D
{
    public StartShard(Action<Shard2D> zeroHealth, int health, float damage, int speed, float timeReload, float critChance, int maxRange) : base(zeroHealth, health, damage, speed, timeReload, critChance, maxRange)
    {
        Sprite.Texture = GD.Load<Texture2D>("res://Data/Textures/Entities/Shards/StartShard.png");
        Light.Color = new Color(0.2f, 1, 1);
        EndParticles.Add(GD.Load<PackedScene>("res://Data/Scenes/Entities/Player/Shard2D/Particles/StartShard/StartShardParticlesDestroyed.tscn").Instantiate<DirectedParticle>());
    }

    public override float Attack()
    {
        float result = Health * Damage;
        result *= GD.Randf() > CritChance ? 2 : 1;
        Destroy();
        return result;
    }
}
