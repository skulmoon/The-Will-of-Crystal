using Godot;
using System;
using System.Collections.Generic;

public abstract partial class EnemyFabric : Node2D
{
    public List<Enemy> Enemies { get; set; } = new List<Enemy>();
    [Export] public EnemyArea[] EnemyAreas { get; set; }

    public event Action<List<Enemy>> EnemiesCreated;
    public event Action EnemiesDestroyed;

    public EnemyFabric()
    {
        Global.SceneObjects.EnemyFabric = this;
        Global.SceneObjects.LocationChanged += OnLocationChanged;
    }

    public void OnLocationChanged(Location location)
    {
        if (location.GetData<bool?>(999) ?? false)
        {
            return;
        }
        Create();
        Global.SceneObjects.Enemies = Enemies;
        EnemiesCreated?.Invoke(Enemies);
        foreach (Enemy enemy in Enemies)
        {
            enemy.EnemyDeaded += CheckEnemyCount;
        }
        Global.SceneObjects.Player.HitBox.ChangeHealth += OnChangeHealth;
    }

    public void CheckEnemyCount(Enemy enemy)
    {
        Enemies.Remove(enemy);
        enemy.EnemyDeaded -= CheckEnemyCount;
        if (Enemies.Count <= 0)
        {
            Global.SceneObjects.Location.SetData(999, true);
            EnemiesDestroyed?.Invoke();
        }
    }

    public void OnChangeHealth(int health)
    {
        if (health <= 0 && GetParent() != null)
            GetParent().RemoveChild(this);
    }

    public override void _ExitTree()
    {
        Global.SceneObjects.LocationChanged -= OnLocationChanged;
    }

    public abstract void Create();
}
