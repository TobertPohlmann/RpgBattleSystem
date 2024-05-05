using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using RpgBattleSystem.Characters;
using RpgBattleSystem.Enums;

namespace ConsoleView.Messages;

public class Pronoun
{
    private Sex _gender = Sex.Male;
    private Numerus _numerus = Numerus.Singular;
    private Casus _casus = Casus.Nominative;

    public static Pronoun New()
    {
        return new Pronoun();
    }

    public static Pronoun ForParty(List<Character> party)
    {
        return New().ForNumber(party.Count).With(party[0].Base.Sex);
    }
    
    public Pronoun With(Sex gender)
    {
        _gender = gender;
        return this;
    }

    public Pronoun ForNumber(int number)
    {
        _numerus = number == 1 ? Numerus.Singular : Numerus.Plural;
        return this;
    }
    
    public Pronoun With(Numerus numerus)
    {
        _numerus = numerus;
        return this;
    }
    
    public Pronoun With(Casus casus)
    {
        _casus = casus;
        return this;
    }
    
    public string Get()
    {
        return _numerus == Numerus.Plural ? GetForPlural() : GetForSingular();
    }

    private string GetForPlural()
    {
        return _casus switch
        {
            Casus.Nominative => "they",
            Casus.Genitive => "their",
            _ => "them"
        };
    }

    private string GetForSingular()
    {
        switch (_gender)
        {
            case Sex.Female: return GetForFemale();
            case Sex.Male: return GetForMale();
            default: return GetForNeutrum();
        }
    }

    private string GetForMale()
    {
        return _casus switch
        {
            Casus.Nominative => "he",
            Casus.Genitive => "his",
            _ => "him"
        };
    }
    
    private string GetForFemale()
    {
        return _casus switch
        {
            Casus.Nominative => "she",
            _ => "her"
        };
    }
    
    private string GetForNeutrum()
    {
        return _casus switch
        {
            Casus.Genitive => "its",
            _ => "it"
        };
    }
}