using Svg;
using SVGObjectsDecomposer.Models;
using System;
using System.Collections.Generic;

namespace SVGObjectsDecomposer;

class SVGContainer
{
    readonly internal SvgDocument OriginalSVGDocument;
    readonly Uri _baseUri;
    readonly public string Filename;
    readonly public SvgViewBox ViewBox;
    readonly public List<SVGLayer> Layers = new();
    
    internal SVGContainer(SvgDocument document)
    {
        OriginalSVGDocument = document;

        _baseUri = document.BaseUri;

        //Filename = _baseUri.Segments.Last();
        Filename = _baseUri.Segments[^1]; // Same as above

        ViewBox = document.ViewBox;

        var templeteDocument = SVGDocumentTemplete.Extract(document, out var firstLayers);

        foreach (var layer in firstLayers)
            Layers.Add( new SVGLayer(layer, templeteDocument) );

    }

    internal Uri GetBaseUri() => _baseUri;
    
}