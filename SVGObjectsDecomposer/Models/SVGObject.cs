﻿using Svg;
using System.Drawing;

namespace SVGObjectsDecomposer.Models;

using Helper = Helpers.InkscapeSVGHelper;

public class SVGObject
{
    readonly public SvgDocument SvgDoc;
    readonly public string ID;
    readonly public string ObjectName;
    readonly public bool IsVisible;
    readonly public bool IsPath;
    readonly public RectangleF Bounds;

    public SVGObject(SvgElement element, SVGLayerTemplete layerTemplete, SVGDocumentTemplete docTemplete)
    {
        var document = docTemplete.CreateNewDocument();
        var layer = layerTemplete.CreateNewLayer();

        layer.Children.Add(element.DeepCopy());
        document.Children.Add(layer);

        SvgDoc = document;

        ID = element.ID;

        // Overwrite ElementName if inkscape label is available
        if (Helper.TryGetInkscapeLabel(element, out var inkscapeLabel))
            ObjectName = inkscapeLabel;
        else
            ObjectName = ID;

        IsVisible = element.Display == "none" ? false : true;

        IsPath = element is Svg.SvgPath ? true : false;

        var stroke = element.Stroke;
        var strokeWidth = element.StrokeWidth;
        var bounds = document.Bounds;

        // if (stroke == SvgPaintServer.None)
        // {
        //     Bounds = new RectangleF(bounds.X + strokeWidth / 2, bounds.Y + strokeWidth / 2, bounds.Width - strokeWidth, bounds.Height - strokeWidth);
        // }
        // else Bounds = bounds;

        Bounds = bounds;
        
    }
}
