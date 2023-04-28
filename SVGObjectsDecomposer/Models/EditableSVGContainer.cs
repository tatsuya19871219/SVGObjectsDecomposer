using CommunityToolkit.Mvvm.ComponentModel;
using Svg;
using System;
using System.Collections.ObjectModel;

namespace SVGObjectsDecomposer.Models;

public partial class EditableSVGContainer : ObservableObject, IDisposable
{
    readonly SVGContainer _svgContainer;

    [ObservableProperty] string _originalFilePath;
    [ObservableProperty] string _filename;
    [ObservableProperty] SvgViewBox _viewBox;

    public ObservableCollection<EditableSVGLayer> Layers {get; init;} = new();

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

    }

    public void Dispose()
    {
        Filename = null;
        foreach (var layer in Layers) layer.Dispose();
        Layers.Clear();
    }

