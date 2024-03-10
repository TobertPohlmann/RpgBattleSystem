namespace RpgBattleSystem.Characters;

public class LevelCurve
{
    private int _y1;
    private int _y100;
    private int _nonLinearity;
    private CurveType _curveType;
    
    public int[] Curve { get; private set; }

    public LevelCurve(int yOffset, int yMaximum, int nonLinearity, CurveType curveType = CurveType.Exponential)
    {
        if (nonLinearity < 0 || nonLinearity > 100)
        {
            throw new Exception("Konvex parameter must be in between -100 and 100.");
        }

        if (yOffset < 0 || yMaximum <= yOffset)
        {
            throw new Exception("The relation 0 <= yOffset <= yMaximum must hold.");
        }

        _y1 = yOffset;
        _y100 = yMaximum;
        _nonLinearity = nonLinearity;
        _curveType = curveType;
        CalculateCurve();
    }

    public int GetValueForLevel(int level)
    {
        return Curve[level];
    }

    private void CalculateCurve()
    {
        Func<int, int> funct = LevelCurveFunction();

        int[] X = Enumerable.Range(1, 100).ToArray();
        Curve = X.Select(x => funct(x)).ToArray();
    }

    private Func<int,int> LevelCurveFunction()
    {
        Func<int, double> unscaledFunct = _curveType switch
        {
            CurveType.Exponential => MakeLevelCurveFrom(LateGrowth()),
            CurveType.Inflecting => MakeLevelCurveFrom(InflectingFunction())
//            CurveType.Inflecting => MakeLevelCurveFrom(InverseInflectingFunction())

        };

        return x => (int)NormalizeToRange(unscaledFunct,_y1,_y100)(x);
    }

    private Func<int,double> NormalizeToRange(Func<int, double> func,int y1 = 0, int y100 = 100)
    {
        double amplitude = (y100-y1)/(func(100)-func(1));
        double offset = -amplitude*func(1)+y1;
        return x => amplitude * func(x) + offset;
    }

    private Func<int, double> MakeLevelCurveFrom(Func<int, double> nonLinFunct)
    {
        double nonLinPortion = Math.Abs(_nonLinearity) / 100.0;
        return x => (1-nonLinPortion)*x + nonLinPortion*nonLinFunct(x);
    }

    private Func<int, double> CubicFunction()
    {
        Func<int, double> cubicFunction = x => Math.Pow(x, -3);
        return NormalizeToRange(cubicFunction);
    }

    private Func<int, double> FermiFunction(double mu, double kbT, double A, double C=0)
    {
        return x => A / (Math.Exp(-(x - mu) / kbT) + 1) + C;
    }

    private Func<int, double> InflectingFunction()
    {
        Func<int, double> nonLinFunction = _nonLinearity switch
        {
            >= 0 => x => Math.Pow((x-50),5),
            _ => x => Math.Atan(0.2*Math.Pow(x-50,1))+_y100/2
        };
        return NormalizeToRange(nonLinFunction);
    }
    
    private Func<int, double> LateGrowth()
    {
        double nonLinPortion = Math.Abs(_nonLinearity) / 100.0;
        Func<int, double> nonLinFunction = _nonLinearity switch
        {
            >= 0 => x => Math.Exp(0.046 * Math.Pow(x,1+nonLinPortion/8)) - 1,
            _ => x => 1/Math.Exp(0.046 * Math.Pow(x,1+nonLinPortion/8))
        };
        return NormalizeToRange(nonLinFunction);
    }
    
}

public enum CurveType{
    Exponential, Inflecting 
}