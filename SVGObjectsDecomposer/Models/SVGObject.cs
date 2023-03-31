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

    public SvgDocument SvgDoc {get; init;}
    //public string LayerID {get; init;}
    public string ElementName {get; init;}

    public SVGObject(SvgElement element, SVGLayerTemplete layerTemplete, SVGDocumentTemplete docTemplete)
    {
        var document = docTemplete.CreateNewDocument();
        var layer = layerTemplete.CreateNewLayer();

        layer.Children.Add(element.DeepCopy());
        document.Children.Add(layer);

        SvgDoc = document;        

        //LayerID = layer.ID;

        ElementName = element.ID;

        // Overwrite ElementName if inkscape label is available
        if ( Helper.TryGetInkscapeLabel(element, out var inkscapeLabel) )
            ElementName = inkscapeLabel;

    }

    // string formatter (for xaml)

}
