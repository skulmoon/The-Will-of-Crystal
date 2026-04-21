using Godot;
using System;
using System.Collections.Generic;

public partial class OptionsMenu : Control
{
	public void SetConfigInfo()
	{
		Global.JSON.SaveConfig();
	}
}
