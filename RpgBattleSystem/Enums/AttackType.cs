namespace RpgBattleSystem.Enums;

public enum AttackType
{
    Strike,Cut,Pierce
}

static class AttackTyoeExtensions 
{
    public static Status Attack(this AttackType attackType) 
    {
        switch (attackType)
        {
            case AttackType.Strike: return Status.StrikeForce;
            case AttackType.Cut: return Status.Technique;
            case AttackType.Pierce: return Status.Precision;
            default: throw new Exception("No attack status registered for your attack type " + attackType);
        }
    }
    
    public static Status Defense(this AttackType attackType) 
    {
        switch (attackType)
        {
            case AttackType.Strike: return Status.StrikeDefense;
            case AttackType.Cut: return Status.CutDefense;
            case AttackType.Pierce: return Status.PierceDefense;
            default: throw new Exception("No defense status registered for your attack type " + attackType);

        }
    }
}