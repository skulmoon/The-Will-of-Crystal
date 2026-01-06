using Godot;
using System;

public partial class HealthProgressBar : ProgressBar
{
    public override void _Ready()
    {
        Global.SceneObjects.PlayerChanged += PlayerChanged;
    }

    public void PlayerChanged(Player player)
    {
        player.HitBox.ChangeHealth += ChangeHealth;
        MaxValue = player.HitBox.MaxHealth;
        Value = player.HitBox.Health;
    }

    public void ChangeHealth(int health)
    {
        Value = health;
    }

    public override void _ExitTree()
    {
        Global.SceneObjects.PlayerChanged -= PlayerChanged;
    }

    public void HideBar()
    {
        CreateTween().TweenProperty(this, "position:x", Position.X - 80, 0.5f);
        CreateTween().TweenProperty(this, "modulate:a", 0, 0.5f);
    }

    public void ShowBar()
    {
        CreateTween().TweenProperty(this, "position:x", Position.X + 80, 0.5f);
        CreateTween().TweenProperty(this, "modulate:a", 1, 0.5f);
    }
}
