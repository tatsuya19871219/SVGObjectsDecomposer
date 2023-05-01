using Svg;
using System.Collections.Generic;

namespace SVGObjectsDecomposer.Models;

public class SVGDocumentTemplete
{
    SvgDocument _templete;

    private SVGDocumentTemplete(SvgDocument document)
    {
        _templete = document.DeepCopy<SvgDocument>() as SvgDocument;
    }

    static internal SVGDocumentTemplete Extract(SvgDocument document, out List<SvgGroup> layers)
    {
        var documentTemplete = new SVGDocumentTemplete(document);

        documentTemplete.ExtractLayers(out layers);

        return documentTemplete;
    }

    void ExtractLayers(out List<SvgGroup> layers)
    {
        layers = new();

        foreach (var child in _templete.Children)
            if (child is SvgGroup) layers.Add((SvgGroup)child);

        foreach(var layer in layers)
            _templete.Children.Remove(layer);
    }

    internal SvgDocument CreateNewDocument() 
                            => _templete.DeepCopy<SvgDocument>() as SvgDocument;
}