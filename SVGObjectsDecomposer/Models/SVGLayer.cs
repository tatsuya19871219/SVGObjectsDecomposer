using System.Collections.Generic;
using Svg;

namespace SVGObjectsDecomposer.Models;

using Helper = Helpers.InkscapeSVGHelper;

class SVGLayer
{
    readonly public string LayerName;
    readonly public bool IsVisible;
    readonly public List<SVGObject> Objects = new();

    internal SVGLayer(SvgGroup layer, SVGDocumentTemplete templeteDocument)
    {
        LayerName = layer.ID;
        IsVisible = layer.Display == "none" ? false : true;

        if ( Helper.TryGetInkscapeLabel(layer, out var inkscapeLabel) )
            LayerName = inkscapeLabel;

        var templeteLayer = SVGLayerTemplete.Extract(layer, out var elements);

        foreach(var element in elements)
        {
            Objects.Add( new SVGObject(element, templeteLayer, templeteDocument) );
        }
    }
}