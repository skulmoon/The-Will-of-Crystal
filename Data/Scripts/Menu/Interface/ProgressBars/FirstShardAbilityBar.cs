using Godot;
using System;
using System.Collections.Generic;

public partial class FirstShardAbilityBar : AbilityBar
{
    private const string ABILITY_PATH = "res://Data/Textures/Entities/Shards/";

    [Export] public int ShardNumber { get; set; } = 0;
    [Export] public TextureRect AbilityTexture { get; set; } 
    [Export] public TextureRect Texture { get; set; }

    public override void OnPlayerChanged(Player player)
    {
        player.Shard.MainShard2DChanged += OnShardChanged;
    }

    public void OnShardChanged(List<Shard2D> shards)
    {
        if ((shards?.Count ?? -1) > ShardNumber)
        {
            if (shards[ShardNumber] is Shard2D shard)
            {
                if (shard is ShardAbility shardAbility)
                {
                    SetAbilityName(shardAbility.GetAbilityNames()[0]);
                    shardAbility.FirstAbilityReloadStarted += OnAbilityReloadStarted;
                    AbilityTexture.Texture = GD.Load<Texture2D>($"{ABILITY_PATH}{shardAbility.GetType()}/{shardAbility.GetType()}Abilities.png");
                    Texture.Texture = GD.Load<Texture2D>($"{ABILITY_PATH}{shardAbility.GetType()}.png");
                }
                else
                {
                    Close();
                    Texture.Texture = GD.Load<Texture2D>($"{ABILITY_PATH}{shard.GetType()}.png");
                    AbilityTexture.Texture = GD.Load<Texture2D>($"{ABILITY_PATH}EmptyAbilities.png");
                }
                if (shard.IsMain)
                    CreateTween().TweenProperty(GetParent(), "modulate:a", 1, 0.5f);
                else
                    CreateTween().TweenProperty(GetParent(), "modulate:a", 0.5f, 0.5f);
                GD.Print(shard.IsMain);
            }
        }
        else
        {
            Close();
            Texture.Texture = null;
            AbilityTexture.Texture = GD.Load<Texture2D>($"{ABILITY_PATH}EmptyAbilities.png");
            CreateTween().TweenProperty(GetParent(), "modulate:a", 0, 0.5f);
        }
    }
}
