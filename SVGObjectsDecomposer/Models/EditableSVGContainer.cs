using CommunityToolkit.Mvvm.ComponentModel;
using Svg;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SVGObjectsDecomposer.Models;

public partial class EditableSVGContainer : ObservableObject, IDisposable
{
    readonly SVGContainer _svgContainer;

    [ObservableProperty] string _originalFilePath;
    [ObservableProperty] string _filename;
    [ObservableProperty] SvgViewBox _viewBox;

    readonly internal ObservableCollection<EditableSVGLayer> Layers = new();
    //readonly internal ObservableCollection<EditableSVGObject> Objects = new();

    internal EditableSVGContainer(SVGContainer svgContainer)
    {
        _svgContainer = svgContainer;

        // Set ititial values of observable properties
        Uri baseUri = _svgContainer.GetBaseUri();

        OriginalFilePath = baseUri.AbsolutePath;

        Filename = _svgContainer.Filename;

        ViewBox = _svgContainer.ViewBox;

        foreach (var layer in _svgContainer.Layers)
           Layers.Add(new EditableSVGLayer(layer));

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
        foreach (var layer in Layers) layer.Dispose();
        Layers.Clear();
    }

    // internal void SaveAll()
    // {
    //     // After the instance is disposed, throw the exception
    //     if (Filename is null) throw new Exception("The instance is already disposed");

    //     string originalDirname = Path.GetDirectoryName(OriginalFilePath);
    //     string originalFileBasename = Path.GetFileNameWithoutExtension(OriginalFilePath);

    //     // for test
    //     string outputBaseDirname = $"{originalDirname}/output_{originalFileBasename}";

    //     if (!Directory.Exists(outputBaseDirname)) Directory.CreateDirectory(outputBaseDirname);

    //     foreach (var layer in Layers)
    //     {
    //         string layerName = layer.LayerName;

    //         string outputDirname = $"{outputBaseDirname}/{layerName}";

    //         if (!Directory.Exists(outputDirname)) Directory.CreateDirectory(outputDirname);

    //         foreach (var obj in layer.Objects)
    //         {
    //             string filename = obj.ElementName.ToLower();

    //             string outputFilePath = $"{outputDirname}/{filename}.svg";

    //             obj.SvgDoc.Write(outputFilePath);
    //         }
    //     }
    // }
}

