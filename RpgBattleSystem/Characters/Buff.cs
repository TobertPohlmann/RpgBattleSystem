using RpgBattleSystem.Enums;

namespace RpgBattleSystem.Characters;


public enum BuffMode
{
    absolute, relative
}

public record Buff
{
    public Status Status;
    public BuffMode Mode;
    public double Amount;
}