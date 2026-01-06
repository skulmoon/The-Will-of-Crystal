using Godot;
using System;

public static class Node2DExtention
{
    public static void ChangeAlphaModulate(this Node2D node, float a = 0)
    {
        Color modulate = node.Modulate;
        modulate.A = a;
        node.Modulate = modulate;
    }
}
