using System;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;

namespace SVGObjectsDecomposer.Models;

public partial class EditableSVGLayer : ObservableObject, IDisposable
{
    readonly SVGLayer _svgLayer;

    public string ID => _svgLayer.ID;

    [ObservableProperty] string _layerName;
    [ObservableProperty] bool _isVisible; // Whether exporting shape or not
    [ObservableProperty] bool _pathExport; // Whether exporting path or not

    public ObservableCollection<EditableSVGObject> Objects {get; init;} = new();

    internal EditableSVGLayer(SVGLayer svgLayer)
    {
        _svgLayer = svgLayer;

        // Set ititial values of observable properties
        LayerName = _svgLayer.LayerName;
        IsVisible = _svgLayer.IsVisible;
        PathExport = false;

        foreach (var obj in _svgLayer.Objects)
            Objects.Add(new EditableSVGObject(obj));

    }

    public void Dispose()
    {
        LayerName = null;
        foreach (var obj in Objects) obj.Dispose();
        Objects.Clear();
    }
}
