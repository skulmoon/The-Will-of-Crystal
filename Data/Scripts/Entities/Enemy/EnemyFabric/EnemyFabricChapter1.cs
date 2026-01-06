using Godot;
using System;

public partial class EnemyFabricChapter1 : EnemyFabric
{
    [Export] public int MeleSkeletonCount { get; set; }
    [Export] public int ExplosionSkeletonCount { get; set; }
    [Export] public int DistantSkeletonCount { get; set; }
    [Export] public EnemyCore Core { get; set; }

    public override void Create()
    {
        foreach (var item in EnemyAreas)
            for (int j = 0; j < item.Difficulty; j++)
            {
                Enemy enemy = null;
                if (item.Type == EnemyType.Mele)
                {
                    int meleCount = MeleSkeletonCount + ExplosionSkeletonCount;
                    if (GD.RandRange(1, meleCount) <= MeleSkeletonCount)
                    {
                        enemy = new MeleSkeleton();
                        MeleSkeletonCount--;
                    }
                    else
                    {
                        enemy = new ExplosionSkeleton();
                        ExplosionSkeletonCount--;
                    }
                }
                else if (item.Type == EnemyType.Distant)
                {
                    enemy = new DistantSkeleton();
                    DistantSkeletonCount--;
                }
                if (enemy != null)
                {
                    item.PlaceEnemy(enemy);
                    Enemies.Add(enemy);
                }
            }
    }
}
