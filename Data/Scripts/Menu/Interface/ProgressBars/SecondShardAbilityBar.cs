using Godot;
using System;
using System.Collections.Generic;

public partial class SecondShardAbilityBar : AbilityBar
{
    [Export] public int ShardNumber { get; set; } = 0;

    public override void OnPlayerChanged(Player player)
    {
        player.Shard.MainShard2DChanged += OnShardChanged;
    }

    public void OnShardChanged(List<Shard2D> shards)
    {
        if ((shards?.Count ?? -1) > ShardNumber)
            if (shards[ShardNumber] is ShardAbility shardAbility)
            {
                SetAbilityName(shardAbility.GetAbilityNames()[1]);
                shardAbility.SecondAbilityReloadStarted += OnAbilityReloadStarted;
            }
            else
                Close();
    }
}
