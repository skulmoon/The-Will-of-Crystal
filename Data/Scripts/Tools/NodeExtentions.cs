using Godot;
using System;

public static class NodeExtentions
{
    public static string GetActionKey(this Node node, string action)
    {
        string strKey = string.Empty;
        foreach (var key in InputMap.ActionGetEvents(action))
            if (key is InputEventKey eventKey)
                strKey = eventKey.Keycode.ToString();
            else if (key is InputEventMouseButton eventMouse)
                strKey = eventMouse.ButtonIndex.GetName();
        return strKey;
    }
}
