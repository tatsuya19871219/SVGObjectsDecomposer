using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SVGObjectsDecomposer.Models;

public partial class EditableSVGContainer : ObservableObject, IDisposable
{
    readonly SVGContainer _svgContainer;

    [ObservableProperty] string filename;

    readonly internal ObservableCollection<EditableSVGLayer> Layers = new();
    readonly internal ObservableCollection<EditableSVGObject> Objects = new();

    internal EditableSVGContainer(SVGContainer svgContainer)
    {
        _svgContainer = svgContainer;

        Filename = _svgContainer.Filename;

        //foreach (var svgLayer in _svgContainer.SVGLayers)
        //    Layers.Add(new EditableSVGLayer(svgLayer));

        //foreach (var svgObject in _svgContainer.SVGObjects)
        //    Objects.Add(new EditableSVGObject(svgObject));
    }

    // ~EditableSVGContainer()
    // {
    //     Filename = null;
    // }

    public void Dispose()
    {
        Filename = null;
        Layers.Clear();
        Objects.Clear();
    }

    internal void SaveAll()
    {
        // After the instance is disposed, throw the exception
        if (Filename is null) throw new Exception("The instance is already disposed");

        
    }
}

