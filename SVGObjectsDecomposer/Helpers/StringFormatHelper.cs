using Svg;
using System.Drawing;

namespace SVGObjectsDecomposer.Helpers;

static class StringFormatHelper
{
    static public string ViewBoxFormat(SvgViewBox viewbox, string delimiter = ", ")
    {
        return ConcatWithDelimiter(delimiter, viewbox.MinX, viewbox.MinY,
                                                viewbox.Width, viewbox.Height);
    }

    static public string ViewBoxFormatTemplete()
    {
        return ConcatWithDelimiter(", ", "MinX", "MinY", "Width", "Height");
    }

    static public string BoundsFormat(RectangleF bounds, string delimiter = ", ")
    {
        return ConcatWithDelimiter(delimiter, bounds.X, bounds.Y,
                                                bounds.Width, bounds.Height);
    }

    static public string BoundsFormatTemplete()
    {
        return ConcatWithDelimiter(", ", "Left", "Top", "Width", "Height");
    }

    static string ConcatWithDelimiter(string delimiter, params object[] contents)
    {
        string line = "";

        foreach(var value in contents)
        {
            line += value.ToString() + delimiter;
        }

        return line;
    }

}
