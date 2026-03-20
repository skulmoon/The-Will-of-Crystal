using Godot;
using System;

public partial class TheBeginning : Location
{
	private int _labelNumber;

	[Export] public Label[] Labels { get; set; }
	public string[] Text { get; set; }

	public override void _Ready()
	{
		base._Ready();
		Text = new string[Labels.Length];
		for (int i = 0; i < Labels.Length; i++)
			Text[i] = Labels[i].Text;
		CutSceneCustomizes.Add((1, () =>
		{
			_labelNumber = 0;
			ChangeVisible(_labelNumber);
			CreateTween().TweenMethod(new Callable(this, nameof(PrintText)), 0, Text[_labelNumber].Length, 3);
		}
		));
		CutSceneCustomizes.Add((2, () =>
		{
			_labelNumber = 1;
			ChangeVisible(_labelNumber);
			CreateTween().TweenMethod(new Callable(this, nameof(PrintText)), 0, Text[_labelNumber].Length, 3);
		}
		));
		CutSceneCustomizes.Add((3, () =>
		{
			_labelNumber = 2;
			ChangeVisible(_labelNumber);
			CreateTween().TweenMethod(new Callable(this, nameof(PrintText)), 0, Text[_labelNumber].Length, 3);
		}
		));
		CutSceneCustomizes.Add((4, () =>
		{
			_labelNumber = 3;
			ChangeVisible(_labelNumber);
			CreateTween().TweenMethod(new Callable(this, nameof(PrintText)), 0, Text[_labelNumber].Length, 3);
		}
		));
		CutSceneCustomizes.Add((5, () =>
		{
			_labelNumber = 4;
			ChangeVisible(_labelNumber);
			CreateTween().TweenMethod(new Callable(this, nameof(PrintText)), 0, Text[_labelNumber].Length, 3);
		}
		));
		CutSceneCustomizes.Add((6, () =>
		{
		}
		));
		CutSceneCustomizes.Add((7, () =>
		{
			ConductivePath conductive = new ConductivePath()
			{
				Path = "Chapter1/StartingTrail",
				EndPosition = new Vector2(240, 350),
			};
			AddChild(conductive);
			Global.Settings.CutScene = false;
			conductive.Interaction();
		}
		));
		Global.CutSceneManager.OutputCutScene(0, 1);
	}

	public void PrintText(float symbolsCount)
	{
		Labels[_labelNumber].Text = Text[_labelNumber][0..(int)MathF.Floor(symbolsCount)];
	}

	public void ChangeVisible(int number)
	{
		foreach (Label label in Labels)
			label.Visible = false;
		Labels[number].Visible = true;
	}
}
