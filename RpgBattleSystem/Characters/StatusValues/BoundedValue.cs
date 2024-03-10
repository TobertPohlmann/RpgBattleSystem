namespace RpgBattleSystem.Characters;

public record BoundedValue
{
    public int CurrentValue { get; private set; }
    private readonly int _minValue;
    private readonly int _maxValue;

    public BoundedValue(int initialValue, int minimum = 1, int maximum = 100)
    {
        _minValue = minimum;
        _maxValue = maximum;
        CurrentValue = initialValue;
    }

    public bool CanBeSetTo(int newValue)
    {
        return (newValue > _minValue) && (newValue < _maxValue);
    }

    public bool CanBeIncrementedBy(int increment)
    {
        return CanBeSetTo(CurrentValue + increment);
    }

    public void SetValue(int newValue)
    {
        if (!CanBeSetTo(newValue))
        {
            throw new Exception("Value is bound by " + _minValue + " and "+ _maxValue +
            ". Cannot be set to" +newValue+".");
        }
        CurrentValue = newValue;
    }

    public void IncreaseValueBy(int increment)
    {
        SetValue(CurrentValue + increment);
    }
}