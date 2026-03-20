using Godot;
using System;
using System.Collections.Generic;

public static class ListExtention
{
    public static void UpdateItemsInfo(this List<Item> items)
    {
        foreach (Item item in items)
            if (item != null)
                item.UpdateInfo();
    }
}
