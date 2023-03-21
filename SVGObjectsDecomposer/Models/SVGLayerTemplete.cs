using System.Collections.Generic;
using Svg;

namespace SVGObjectsDecomposer;

public class SVGLayerTemplete
{
    SvgGroup _templete;

    private SVGLayerTemplete(SvgGroup group)
    {
        _templete = group.DeepCopy<SvgGroup>() as SvgGroup;
    }

    static internal SVGLayerTemplete Extract(SvgGroup group, out List<SvgElement> elements)
    {
        var layerTemplete = new SVGLayerTemplete(group);

        layerTemplete.ExtractElements(out elements);

        return layerTemplete;
    }

    void ExtractElements(out List<SvgElement> elements)
    {
        elements = new();

        foreach (var child in _templete.Children)
            elements.Add((SvgElement)child);

        foreach(var element in elements)
            _templete.Children.Remove(element);
    }

    internal SvgGroup CreateNewLayer() 
                        => _templete.DeepCopy<SvgGroup>() as SvgGroup;
}