using Godot;
using System;
using System.IO;

public partial class SaveConfigButton : ButtonMenuTransition
{
    [Export] public OptionsMenu Options { get; set; }

    public override void OnPressed()
    {
        base.OnPressed();
        Options.SetConfigInfo();
    }
}
