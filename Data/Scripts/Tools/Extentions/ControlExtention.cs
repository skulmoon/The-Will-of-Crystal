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
}
