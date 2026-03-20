using Godot;
using System;
using static System.Net.Mime.MediaTypeNames;

[GlobalClass]
public partial class Armor : Item
{
    [Export] public string ArmorType { get; set; }
    [Export] public float Protection { get; set; }
    [Export] public int AdditionalHealth { get; set; }

    public Armor() : base() { }

    public Armor(int id, int maxCount, string itemName, string description, float protection, int additionalHealth, string armorType) : base(id, maxCount, itemName, description)
    {
        Protection = protection;
        AdditionalHealth = additionalHealth;
        ArmorType = armorType;
    }

    public override void UpdateInfo()
    {
        Armor newArmor = GD.Load<Armor>($"res://Data/Resources/Items/Armors/{ID}.tres");
        UpdateInfo(newArmor);
        Protection = newArmor.Protection;
        AdditionalHealth = newArmor.AdditionalHealth;
        ArmorType = newArmor.ArmorType;
    }

    public override object Clone()
    {
        Item item = new Armor(ID, MaxCount, Name, Description, Protection, AdditionalHealth, ArmorType);
        item.Count = Count;
        return item;
    }
}