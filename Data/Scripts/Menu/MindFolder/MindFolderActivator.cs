using Godot;
using System;

public partial class MindFolderActivator : Area2D, IInteractionArea
{
    public MindFolderActivator()
    {
        CollisionLayer = 8;
        CollisionMask = 8;
    }

    public void Interaction()
    {
        GetNode<MindFolder>("%MindFolder").Open();
    }
}
