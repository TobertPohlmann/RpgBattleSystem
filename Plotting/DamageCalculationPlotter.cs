using Microsoft.FSharp.Core;
using Plotly.NET;
using Plotly.NET.LayoutObjects;
using RpgBattleSystem.Characters;

namespace Plotting;

public class DamageCurvePlotter
{
    private static int[] _xDiff = Enumerable.Range(-1000, 2001).ToArray();
    private static int[] _xQuotient = Enumerable.Range(0, 10).ToArray();

    private static Title _title = Title.init(Text: "Damage Calculation");
    private static Layout _layout = Layout.init<IConvertible>(Title: _title, PlotBGColor: Color.fromString("#e5ecf6"));
    
    private static LinearAxis _xAxis =
        LinearAxis.init<IConvertible, IConvertible, IConvertible, IConvertible, IConvertible, IConvertible>(
            Title: Title.init("Defense - Attack"),
            ZeroLineColor: Color.fromString("#ffff"),
            GridColor: Color.fromString("#ffff"),
            ZeroLineWidth: 2,
            Range: new FSharpOption<StyleParam.Range>(StyleParam.Range.ofMinMax(-1000, 1000)));

    private static LinearAxis _yAxis =
        LinearAxis.init<IConvertible, IConvertible, IConvertible, IConvertible, IConvertible, IConvertible>(
            Title: Title.init("Damage"),
            ZeroLineColor: Color.fromString("#ffff"),
            GridColor: Color.fromString("#ffff"),
            ZeroLineWidth: 2);
    
    public static GenericChart.GenericChart? PlotFunction(Func<int,double> plotFunction, string filename = "Plot.html")
    {
        int[] Y = _xDiff.Select(x => (int)plotFunction(x)).ToArray();
        return PlotArray(Y);
    }

    public static GenericChart.GenericChart? PlotArray(int[] Y, string filename = "Plot.html")
    {
        return CreateFigure(_xDiff,Y,filename);
    }

    public static void PlotAllFigures(List<int[]> lines,List<string> names,string title, string filename="Plot.html")
    {
        var chartList = new List<GenericChart.GenericChart>();
        for(int i = 0; i < lines.Count; i++)
        {
            chartList.Add(Chart2D.Chart.Line<int, int, string>(_xDiff,lines[i],Name:names[i]));
        }

        var combinedChart = Chart.Combine(chartList);
        combinedChart.WithTitle(title)
            .WithXAxis(_xAxis)
            .WithYAxis(_yAxis);
        combinedChart.SaveHtml("/mnt/Windows-Daten/Tobi/Programmiersachen/RpgBattleSystem/RpgBattleSystem/Plotting/LevelCurvePlots/"+filename);
    }

    private static GenericChart.GenericChart? CreateFigure(int[] X, int[] Y, string filename)
    {
        var result = Chart2D.Chart
            .Scatter<int, int, string>(x: X, y: Y, mode: StyleParam.Mode.Markers)
            .WithLayout(_layout)
            .WithXAxis(_xAxis)
            .WithYAxis(_yAxis);
        result.SaveHtml("/mnt/Windows-Daten/Tobi/Programmiersachen/RpgBattleSystem/RpgBattleSystem/Plotting/"+filename);
        return result;
    }

}