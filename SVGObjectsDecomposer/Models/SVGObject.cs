using Svg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace SVGObjectsDecomposer.Models;

//
//internal class SVGObject
public class SVGObject
{
    //readonly SvgDocument _svgDoc;

    public SvgDocument SvgDoc {get; init;}
    public string LayerID {get; init;}

    public SVGObject(SVGDocumentTemplete docTemplete, SVGLayerTemplete layerTemplete, SvgElement element)
    {
        var document = docTemplete.CreateNewDocument();
        var layer = layerTemplete.CreateNewLayer();

        layer.Children.Add(element.DeepCopy());
        document.Children.Add(layer);

        SvgDoc = document;        

        LayerID = layer.ID;
    }

    // public void SetLayerName(string name) => SvgLayerName = name;
    // public void SetPartsName(string name) => SvgPartsName = name;


    // string formatter (for xaml)

}
