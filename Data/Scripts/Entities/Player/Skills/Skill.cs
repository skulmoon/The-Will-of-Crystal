using Godot;
using System;
using static System.Net.Mime.MediaTypeNames;

public partial class Skill : Item
{
    [Export] public string SkillType { get; set; }

    public Skill(int id, int maxCount, string itemName, string description, string skillType) : base(id, maxCount, itemName, description)
    {
        SkillType = skillType;
    }

    public override void UpdateInfo()
    {
        Skill newShard = GD.Load<Skill>($"res://Data/Resources/Items/Skills/{ID}.tres");
        UpdateInfo(newShard);
        SkillType = newShard.SkillType;
    }
}
