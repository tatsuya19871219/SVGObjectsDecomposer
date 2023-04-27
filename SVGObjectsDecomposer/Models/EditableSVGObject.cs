using System;
using System.Drawing;
using CommunityToolkit.Mvvm.ComponentModel;
using Svg;

namespace SVGObjectsDecomposer.Models;

public partial class EditableSVGObject : ObservableObject, IDisposable
{
    readonly SVGObject _svgObject;

    public SvgDocument SvgDoc {get; private set;} 

    [ObservableProperty] string _elementName;
    [ObservableProperty] RectangleF _bounds;

    internal EditableSVGObject(SVGObject svgObject)
    {
        _svgObject = svgObject;

        SvgDoc = _svgObject.SvgDoc;

        // Set initial values of observable properties
        ElementName = _svgObject.ElementName;
    }

    public void Dispose()
    {
        ElementName = null;
    }
}
