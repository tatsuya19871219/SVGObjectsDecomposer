using Svg;
using System.Collections.Generic;

namespace SVGObjectsDecomposer.Models;

using Helper = Helpers.InkscapeSVGHelper;

class SVGLayer
{
    readonly public string ID;
    readonly public string LayerName;
    readonly public bool IsVisible;
    readonly public List<SVGObject> Objects = new();

    internal SVGLayer(SvgGroup layer, SVGDocumentTemplete templeteDocument)
    {
        ID = layer.ID;

        if ( Helper.TryGetInkscapeLabel(layer, out var inkscapeLabel) )
            LayerName = inkscapeLabel;
        else
            LayerName = ID;

        IsVisible = layer.Display == "none" ? false : true;
        
        var templeteLayer = SVGLayerTemplete.Extract(layer, out var elements);

        foreach(var element in elements)
        {
            Objects.Add( new SVGObject(element, templeteLayer, templeteDocument) );
        }
    }
}