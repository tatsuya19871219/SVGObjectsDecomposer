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
        if ( TryGetInkscapeLabel(element, out var inkscapeLabel) )
            ElementName = inkscapeLabel;

    }

    bool TryGetInkscapeLabel(SvgElement element, out string name)
    {
        XNamespace nsInkscape = "http://www.inkscape.org/namespaces/inkscape";

        var elementXML = element.GetXML();
        var xmldoc = XDocument.Parse(elementXML);

        var xelement = xmldoc.FirstNode as XElement;

        var labelAttr = xelement.Attribute(nsInkscape + "label");

        if (labelAttr is null) 
        {
            name = null;
            return false;
        }
        else
        {
            name = labelAttr.Value;
            return true;
        }
        
    }


    // string formatter (for xaml)

}
