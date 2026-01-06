using Godot;
using System;

public partial class ItemCell : MindCell
{
    private bool _isTaked = false;

    [Export] public Item Item { get; set; }

    public ItemCell() : base("res://Data/Textures/MindFolder/ItemCell.png", "res://Data/Textures/MindFolder/PressedItemCell.png") { }

    public override void OnPressed()
    {
        if (!_isTaked)
        {
            //Global.Inventory.TakeItem((Item)Item.Clone());
            Tween tween = CreateTween();
            tween.TweenProperty(this, "modulate:a", 0, 0.5f);
            tween.TweenCallback(new Callable(this, nameof(AnimationEnded)));
            _isTaked = true;
        }
    }

    public void AnimationEnded()
    {
        GetParent().RemoveChild(this);
    }
}
