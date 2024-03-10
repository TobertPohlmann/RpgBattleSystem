using Microsoft.FSharp.Core;
using Plotly.NET;
using Plotly.NET.LayoutObjects;

namespace Plotting;

public class LevelCurvePlotter
{
    private static int[] _X = Enumerable.Range(1, 100).ToArray();
    
    private static Title _title = Title.init (Text : "Level Curve");
    private static Layout _layout = Layout.init<IConvertible>(Title:_title, PlotBGColor : Color.fromString("#e5ecf6"));
    
    private static LinearAxis _xAxis = LinearAxis.init<IConvertible, IConvertible, IConvertible, IConvertible, IConvertible, IConvertible>(
        Title: Title.init("Level"),
        ZeroLineColor: Color.fromString("#ffff"),
        GridColor: Color.fromString("#ffff"),
        ZeroLineWidth: 2,
        Range: new FSharpOption<StyleParam.Range>(StyleParam.Range.ofMinMax(-5, 105)));

    
    public static GenericChart.GenericChart? PlotFunction(Func<int,int> plotFunction, string filename = "Plot.html")
    {
        int[] Y = _X.Select(x => plotFunction(x)).ToArray();
        return PlotArray(Y);
    }

    public static GenericChart.GenericChart? PlotArray(int[] Y, string filename = "Plot.html")
    {
        return CreateFigure(_X,Y,filename);
    }

    public static void PlotAllFigures(List<int[]> lines,string filename="Plot.html")
    {
        var chartList = new List<GenericChart.GenericChart>();
        foreach (var line in lines)
        {
            chartList.Add(Chart2D.Chart.Line<int, int, string>(_X,line));
        }

        var combinedChart = Chart.Combine(chartList);
        combinedChart.SaveHtml("/mnt/Windows-Daten/Tobi/Programmiersachen/RpgBattleSystem/RpgBattleSystem/Plotting/LevelCurvePlots/"+filename);
    }

    private static GenericChart.GenericChart? CreateFigure(int[] X, int[] Y, string filename)
    {
        var yAxis = GetYAxisFor(Y);
        return Chart2D.Chart
            .Scatter<int, int, string>(x: X, y: Y, mode: StyleParam.Mode.Markers)
            .WithLayout(_layout)
            .WithXAxis(_xAxis)
            .WithYAxis(yAxis);
    }

    private static LinearAxis GetYAxisFor(int[] Y)
    {
        return LinearAxis.init<IConvertible, IConvertible, IConvertible, IConvertible, IConvertible, IConvertible>(
            Title:Title.init("Value"),
            ZeroLineColor:Color.fromString("#ffff"),
            GridColor:Color.fromString("#ffff"),
            ZeroLineWidth:2,
            Range: new FSharpOption<StyleParam.Range>(StyleParam.Range.ofMinMax(-5, 1.05*Y.Last())));
    }
}