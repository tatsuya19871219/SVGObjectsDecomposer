using System;
using System.Collections.Generic;
using System.Linq;
using Svg;
using SVGObjectsDecomposer.Models;

namespace SVGObjectsDecomposer;

class SVGContainer
{
    readonly public string Filename;
    readonly public int LayerCounts;
    readonly public List<string> LayerNames;

    readonly public List<SVGLayer> SVGLayers;
    readonly public List<SVGObject> SVGObjects;

    readonly Uri _baseUri;
    readonly SvgDocument _document;

    readonly SVGDocumentTemplete _templeteDocument;

    readonly List<SvgGroup> _firstLayers;

    readonly List<SVGLayerTemplete> _templeteLayers;

    readonly List<SvgElement> _svgElements;

    //readonly SvgDocument _templete;

    //readonly List<SvgGroup> _firstLayers;

    //readonly List<SVGObject> _objects;

    internal SVGContainer(SvgDocument document)
    {
        _document = document;

        _baseUri = document.BaseUri;
        //Filename = _baseUri.Segments.Last();
        Filename = _baseUri.Segments[^1];

        _templeteDocument = SVGDocumentTemplete.Extract(document, out _firstLayers);

        _templeteLayers = new();
        _svgElements = new();
        SVGObjects = new();

        foreach (var layer in _firstLayers)
        {
            var templeteLayer = SVGLayerTemplete.Extract(layer, out var elements);

            _templeteLayers.Add(templeteLayer);

            _svgElements.AddRange(elements);


            foreach(var element in elements)
            {
                SVGObjects.Add( new SVGObject(_templeteDocument, templeteLayer, element) );
            }
        }

        //_templete = document.DeepCopy<SvgDocument>() as SvgDocument;

        //_firstLayers = GetSubLayers(_templete);

        // foreach(var layer in _firstLayers)
        //     _templete.Children.Remove(layer);

        // create svgparts list
        //_objects = Decompose();
        
    }

    // internal SvgDocument CreateSvgDocumentOf()
    // {
    //     var svgdoc = _templeteDocument.CreateNewDocument();

        
    // }

    // internal List<SVGObject>[] Decompose()
    // {
    //     //List<SvgElement> elements = new();
    //     //var svgObjects = new List<SVGObject>[_firstLayers.Count];

    //     // for each layer, collect objects at first level
    //     foreach(var layer in _firstLayers)
    //     {
            
    //     }

    // }

    //
    // List<SvgGroup> GetSubLayers(SvgDocument document)
    // {
    //     List<SvgGroup> layers = new();

    //     foreach (var child in document.Children)
    //         if (child is SvgGroup) layers.Add((SvgGroup)child);

    //     return layers;
    // }

    //
    // List<SvgElement> GetElements(SvgGroup layer)
    // {
    //     var templeteLayer = layer.DeepCopy<SvgGroup>();
    //     List<SvgElement> elements = new();

    //     foreach (var element in templeteLayer.Children)
    //         elements.Add((SvgElement)element);

    //     foreach ()
    // }
}