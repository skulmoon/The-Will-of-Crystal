using Godot;
using System;

public static class ControlExtention
{
    public static void ChangeAlphaModulate(this Control node, float a = 0)
    {
        Color modulate = node.Modulate;
        modulate.A = a;
        node.Modulate = modulate;
    }

    public static int CalculateFontSize(this Control node, float ySize)
    {
        int fontSize = Mathf.RoundToInt(ySize / 1.6f) / 13 * 13;
        return fontSize > 0 ? fontSize : 13;
    }
}
