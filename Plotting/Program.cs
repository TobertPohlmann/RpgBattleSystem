// See https://aka.ms/new-console-template for more information

using Plotting;
using RpgBattleSystem.Characters;

Console.WriteLine();


foreach (Status status in Enum.GetValues(typeof(Status)))
{
    LevelCurvePlotter.PlotStatusValue(status);
}

