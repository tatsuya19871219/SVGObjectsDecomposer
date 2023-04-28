using Svg;
using System.Drawing;

namespace SVGObjectsDecomposer.Models;

using Helper = Helpers.InkscapeSVGHelper;

public class SVGObject
{
    readonly public SvgDocument SvgDoc;
    readonly public string ObjectName;
    readonly public bool IsVisible;
    readonly public RectangleF Bounds;

    public SVGObject(SvgElement element, SVGLayerTemplete layerTemplete, SVGDocumentTemplete docTemplete)
    {
        var document = docTemplete.CreateNewDocument();
        var layer = layerTemplete.CreateNewLayer();

        layer.Children.Add(element.DeepCopy());
        document.Children.Add(layer);

        SvgDoc = document;        

        ObjectName = element.ID;

        IsVisible = element.Display == "none" ? false : true;

        //Visible = element.Visibility switch
        //{
        //    SvgVisibility.Hidden.ToString() => false,
        //    SvgVisibility.Inherit.ToString() => layer.Visible,
        //    _ => true
        //};

        // Overwrite ElementName if inkscape label is available
        if ( Helper.TryGetInkscapeLabel(element, out var inkscapeLabel) )
            ObjectName = inkscapeLabel;

        Bounds = document.Bounds;

    }
}
