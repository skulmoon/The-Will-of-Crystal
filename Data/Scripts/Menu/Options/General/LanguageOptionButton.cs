using Godot;
using System;

public partial class LanguageOptionButton : OptionButton
{
    public override void _Ready()
    {
        base._Ready();
        Select(GetLenguageId(TranslationServer.GetLocale()));
        ItemSelected += OnItemSelected;
    }

    public void OnItemSelected(long index)
    {
        TranslationServer.SetLocale(GetLenguage(index));
    }

    public string GetLenguage() =>
        GetLenguage(Selected);

    public string GetLenguage(long index)
    {
        return index switch
        {
            0 => "en",
            1 => "ru",
            _ => "en"
        };
    }

    public int GetLenguageId(string index)
    {
        return index switch
        {
            "en" => 0,
            "ru" => 1,
            _ => 0
        };
    }
}
