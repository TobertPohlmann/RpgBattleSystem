namespace RpgBattleSystem.Functions;

public class UsefulFunctions
{
    public static Func<int, double> FermiFunction(double mu, double kbT, double A, double C=0)
    {
        return x => A*(1 / (Math.Exp(-(x - mu) / kbT) + 1) + C);
    }

    public static Func<int, double> Piecewise(Func<int, double> func1, Func<int, double> func2, int xTransition = 0,
        double smoothness = 1)
    {
        Func<int, double> stepFunction = FermiFunction(xTransition, smoothness,1,0);
        Func<int, double> intermediate = x => func2(x) * stepFunction(x) + (1.0 - stepFunction(x)) * func1(x);
        return x => (x <= xTransition - 1 * smoothness) ? func1(x) : 
            ((x >= xTransition + 1 * smoothness) ? func2(x) : intermediate(x));
    }
}