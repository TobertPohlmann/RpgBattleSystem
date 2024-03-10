namespace RpgBattleSystem.Characters;

public class LevelCurve
{
    private int _y1;
    private int _y100;
    private int _nonLinearity;
    private CurveType _curveType;
    private int _softCapLevel;
    public int[] Curve { get; private set; }

    public LevelCurve(int yOffset, int yMaximum, int nonLinearity, CurveType curveType = CurveType.LateGrowth, int softCapLevel = 100)
    {
        if (nonLinearity < 0 || nonLinearity > 100)
        {
            throw new Exception("Nonlinearity parameter must be in between 0 and 100.");
        }

        if (yOffset < 0 || yMaximum <= yOffset)
        {
            throw new Exception("The relation 0 <= yOffset <= yMaximum must hold.");
        }

        _y1 = yOffset;
        _y100 = yMaximum;
        _nonLinearity = nonLinearity;
        _curveType = curveType;
        _softCapLevel = softCapLevel;
        CalculateCurve();
    }

    public int GetValueForLevel(int level)
    {
        return Curve[level];
    }

    private void CalculateCurve()
    {
        Func<int, int> funct = LevelCurveFunction();

        if (_softCapLevel < 100)
        {
            funct = SoftCap(funct);
        }
        
        int[] X = Enumerable.Range(1, 100).ToArray();
        Curve = X.Select(x => funct(x)).ToArray();
    }

    private Func<int,int> LevelCurveFunction()
    {
        Func<int, double> unscaledFunct = _curveType switch
        {
            CurveType.LateGrowth => LateGrowth(),
            CurveType.EarlyGrowth => EarlyGrowth(),
            CurveType.MidLevelGrowth => MidLevelGrowth(),
            CurveType.Linear => LinearGrowth()

        };

        return x => (int)NormalizeToRange(unscaledFunct,_y1,_y100)(x);
    }

    private Func<int,double> NormalizeToRange(Func<int, double> func,int y1 = 0, int y100 = 100)
    {
        double amplitude = (y100-y1)/(func(100)-func(1));
        double offset = -amplitude*func(1)+y1;
        return x => amplitude * func(x) + offset;
    }

    private Func<int, double> FermiFunction(double mu, double kbT, double A, double C=0)
    {
        return x => A*(1 / (Math.Exp(-(x - mu) / kbT) + 1) + C);
    }

    private Func<int, double> MidLevelGrowth()
    {
        Func<int, double> scaleKbt = x => Math.Pow(x, 2);
        double kbT = scaleKbt(_nonLinearity) * 40 / scaleKbt(100) + 3;

        return FermiFunction(50, kbT, 1, 0);
    }
    
    private Func<int, double> EarlyGrowth()
    {
        Func<int, double> scaleKbt = x => Math.Pow(x, 1.8);
        double kbT = scaleKbt(_nonLinearity) * 80 / scaleKbt(100) + 5;
        
        return FermiFunction(0, kbT, 2, 0);
    }
    
    private Func<int, double> LateGrowth()
    {
        Func<int, double> scaleKbt = x => Math.Pow(x, 1.8);
        double kbT = scaleKbt(_nonLinearity) * 80 / scaleKbt(100) + 15;
        return FermiFunction(100, kbT, 100, 0);
    }
    
    private Func<int, double> LinearGrowth()
    {
        return x => x;
    }

    private Func<int, int> SoftCap(Func<int, int> func)
    {
        int kbT = 4;
        int valueAtSoftCap = func(_softCapLevel + kbT);
        double levelProgressionLeft = (100-_softCapLevel) / 100.0;
        
        Func<int,int> afterSoftCap = x => (int)(valueAtSoftCap+0.15*levelProgressionLeft*valueAtSoftCap/(100-_softCapLevel)*(x-_softCapLevel));
        return x => (int)(FermiFunction(_softCapLevel-kbT/2, kbT, 1, 0)(x) * afterSoftCap(x) +
                          (1.0 - FermiFunction(_softCapLevel-kbT/2, kbT, 1, 0)(x)) * func(x));
    }
}

public enum CurveType{
    Linear, LateGrowth, EarlyGrowth, MidLevelGrowth 
}