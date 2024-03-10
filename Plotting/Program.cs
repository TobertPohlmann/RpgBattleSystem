// See https://aka.ms/new-console-template for more information

using Plotting;
using RpgBattleSystem.Characters;

Console.WriteLine();

var levelFunction = new LevelCurve(20,150,10);

List<int[]> levelFunctions = new();
for (int i = 0; i <= 100; i += 20)
{
    levelFunctions.Add((new LevelCurve(0,150,i,CurveType.LateGrowth,70)).Curve);
}

LevelCurvePlotter.PlotAllFigures(levelFunctions);