using Godot;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.WebSockets;

public partial class DialogPanel : TextureRect
{
    private DialogText _dialogText;
    private NPCDialogue _dialogue;
    private AudioStreamPlayer _player; 
    private Timer _delayTimer = new Timer { OneShot = true };
    private bool _startDialog = false;
    private int[] _numberOptions = new int[5];

    public bool IsSelected { get => (_dialogText?.Control?.Visible ?? false) || _delayTimer.TimeLeft != 0; }
    public bool IsPrinting { get => _dialogText?.IsPrinting ?? false; }

    public DialogPanel() =>
        Global.SceneObjects.DialoguePanel = this;

    public override void _Ready()
    {
        Position = new Vector2(Position.X, GetParentAreaSize().Y + 100);
        _dialogText = GetNode<DialogText>("DialogText");
        _player = GetNode<AudioStreamPlayer>("AudioStreamPlayer");
        AddChild(_delayTimer);
    }

    public void OutputSpeech(NPCDialogue dialogue)
    {
        _dialogue = dialogue;
    }

    public override void _Process(double delta)
    {
        if (IsSelected && Global.CutSceneManager.IsChargeComplete)
        {
            if (Input.IsActionJustPressed("up"))
                _dialogText.UpOption();
            else if (Input.IsActionJustPressed("down"))
                _dialogText.DownOption();
            else if (Input.IsActionJustPressed("interact"))
            {
                _delayTimer.Start(0.1);
                _dialogText.Control.Visible = false;
                Global.CutSceneManager.OutputCutScene(_numberOptions[_dialogText.CurrentPosition]);
            }
        }
    }

    public void NextDialogue(int currentCutScene)
    {
        if (_dialogue == null)
            return;
        if (_dialogue.Speech.Count > currentCutScene)
        {
            if (_dialogue.Speech[currentCutScene] != null)
            {
                PanelShow();
                _dialogText.PrintText(_dialogue.Speech[currentCutScene].Text, _dialogue.Speech[currentCutScene].Name);
            }
            else
                EndDialogue();
        }
        else
            EndDialogue();
    }
    
    public void EndDialogue()
    {
        _dialogText.ClearText();
        PanelHide();
    }

    public void ShowOptions()
    {
        if (_dialogue.Options != null)
        {
            _dialogText.Text = "";
            _dialogText.Control.Visible = true;
            foreach (var option in _dialogText.OptionsText)
                option.Text = string.Empty;
            _dialogText.CountOfOptions = _dialogue.Options.Count;
            for (int i = 0; i < _dialogText.CountOfOptions; i++)
            {
                _dialogText.OptionsText[i].Text = _dialogue.Options[i].OptionText;
                _numberOptions[i] = _dialogue.Options[i].NextDialogue;
            }
        }
    }

    public void PanelShow()
    {
        if (!Visible)
        {
            Visible = true;
            CreateTween().TweenProperty(this, "position:y", 1180 - 455, 0.4f).SetTrans(Tween.TransitionType.Sine);
        }
    }

    public void PanelHide()
    {
        if (Visible)
        {
            Tween tween = CreateTween();
            tween.TweenProperty(this, "position:y", 1180, 0.4f).SetTrans(Tween.TransitionType.Sine);
            tween.TweenCallback(new Callable(this, "SetVisible"));
        }
    }

    public void EndSpeech() =>
        _dialogText.StopPrinting();

    public void SetVisible() =>
        Visible = false;
}
