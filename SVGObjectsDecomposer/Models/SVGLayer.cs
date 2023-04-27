using System.Collections.Generic;
using Svg;

namespace SVGObjectsDecomposer.Models;

using Helper = InkscapeSVGHelper;

class SVGLayer
{
    readonly public string LayerName;
    readonly public bool Visible;

    readonly public List<SVGObject> Objects = new();

    //readonly SvgGroup _layer;

    internal SVGLayer(SvgGroup layer, SVGDocumentTemplete templeteDocument)
    {
        //_layer = layer;

        LayerName = layer.ID;
        Visible = layer.Visible;

        if ( Helper.TryGetInkscapeLabel(layer, out var inkscapeLabel) )
            LayerName = inkscapeLabel;

        var templeteLayer = SVGLayerTemplete.Extract(layer, out var elements);

        foreach(var element in elements)
        {
            Objects.Add( new SVGObject(element, templeteLayer, templeteDocument) );
        }
    }
}