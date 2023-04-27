using Svg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Xml.Linq;

namespace SVGObjectsDecomposer.Models;

using Helper = InkscapeSVGHelper;

//
//internal class SVGObject
public class SVGObject
{
    //readonly SvgDocument _svgDoc;

    readonly public SvgDocument SvgDoc;
    //public string LayerID {get; init;}
    readonly public string ElementName;
    readonly public bool Visible;
    readonly public RectangleF Bounds;

    public SVGObject(SvgElement element, SVGLayerTemplete layerTemplete, SVGDocumentTemplete docTemplete)
    {
        var document = docTemplete.CreateNewDocument();
        var layer = layerTemplete.CreateNewLayer();

        layer.Children.Add(element.DeepCopy());
        document.Children.Add(layer);

        SvgDoc = document;        

        //LayerID = layer.ID;

        ElementName = element.ID;

        //Visible = element.Visibility switch
        //{
        //    SvgVisibility.Hidden.ToString() => false,
        //    SvgVisibility.Inherit.ToString() => layer.Visible,
        //    _ => true
        //};

        // Overwrite ElementName if inkscape label is available
        if ( Helper.TryGetInkscapeLabel(element, out var inkscapeLabel) )
            ElementName = inkscapeLabel;

        Bounds = document.Bounds;

        // 
    }

    // string formatter (for xaml)

}
