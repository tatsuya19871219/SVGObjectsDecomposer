using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;

namespace SVGObjectsDecomposer.Models;

internal partial class EditableSVGLayer : ObservableObject, IDisposable
{
    readonly SVGLayer _svgLayer;

    [ObservableProperty] string layerName;

    readonly internal ObservableCollection<EditableSVGObject> Objects = new();

    internal EditableSVGLayer(SVGLayer svgLayer)
    {
        _svgLayer = svgLayer;

        // Set ititial values of observable properties
        LayerName = _svgLayer.LayerName;

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
