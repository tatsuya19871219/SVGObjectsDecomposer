using System;
using System.Drawing;
using CommunityToolkit.Mvvm.ComponentModel;
using Svg;

namespace SVGObjectsDecomposer.Models;

public partial class EditableSVGObject : ObservableObject, IDisposable
{
    readonly SVGObject _svgObject;

    //public SvgDocument SvgDoc {get; private set;} 

    public bool IsVisible => _svgObject.IsVisible;
    public bool IsPath => _svgObject.IsPath;

    [ObservableProperty] SvgDocument _svgDoc;

    [ObservableProperty] string _objectName;
    [ObservableProperty] RectangleF _bounds;

    internal EditableSVGObject(SVGObject svgObject)
    {
        _svgObject = svgObject;

        SvgDoc = _svgObject.SvgDoc;

        // Set initial values of observable properties
        ObjectName = _svgObject.ObjectName;
    }

    public void Dispose()
    {
        ObjectName = null;
        SvgDoc = null;
    }
}
