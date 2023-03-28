using System.Collections.Generic;
using Svg;

namespace SVGObjectsDecomposer.Models;

class SVGLayer
{
    readonly public string LayerName;

    readonly public List<SVGObject> Objects = new();

    //readonly SvgGroup _layer;

    internal SVGLayer(SvgGroup layer, SVGDocumentTemplete templeteDocument)
    {
        //_layer = layer;

        LayerName = layer.ID;

        var templeteLayer = SVGLayerTemplete.Extract(layer, out var elements);

        foreach(var element in elements)
        {
            Objects.Add( new SVGObject(element, templeteLayer, templeteDocument) );
        }
    }
}