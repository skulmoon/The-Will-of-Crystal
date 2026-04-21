using Godot;
using System;

public partial class ConfigBase : Node
{
    public string Language { get; set; } = "en";
    public bool FistCutSceneActivated { get; set; } = false;
}
