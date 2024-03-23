namespace RpgBattleSystem.Characters.StatusValues;

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
        int updatedValue;
        if (newValue > _maxValue)
        {
            updatedValue = _maxValue;
        } else if (newValue < _minValue)
        {
            updatedValue = _minValue;
        }
        else
        {
            updatedValue = newValue;
        }
        CurrentValue = updatedValue;
    }

    public void IncreaseValueBy(int increment)
    {
        SetValue(CurrentValue + increment);
    }

    public static BoundedValue operator +(BoundedValue a, BoundedValue b)
    {
        a.IncreaseValueBy(b.CurrentValue);
        return a;
    }

    public static BoundedValue operator +(BoundedValue a, int b)
    {
        a.IncreaseValueBy(b);
        return a;
    }

    public static BoundedValue operator +(int a, BoundedValue b) => b + a;
    
    public static BoundedValue operator -(BoundedValue a, BoundedValue b)
    {
        a.IncreaseValueBy(-b.CurrentValue);
        return a;
    }

    public static BoundedValue operator -(BoundedValue a, int b)
    {
        a.IncreaseValueBy(-b);
        return a;
    }

    public static BoundedValue operator -(int a, BoundedValue b)
    {
        int value = a - b.CurrentValue;
        b.SetValue(value);
        return b;
    }

}