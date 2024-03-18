// See https://aka.ms/new-console-template for more information

using Plotting;
using RpgBattleSystem.Characters;
using RpgBattleSystem.Functions;

Console.WriteLine();

//LevelCurvePlotter.PlotAll();


Func<int, double> linear = x => x;
Func<int, double> exp = x => Math.Exp(x);
Func<int, double> dmgFnct = UsefulFunctions.Piecewise(exp, linear, 1);


DamageCurvePlotter.PlotFunction(dmgFnct);