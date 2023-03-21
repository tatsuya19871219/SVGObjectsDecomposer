using Svg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace SVGObjectsDecomposer;

//
//internal class SVGObject
public class SVGObject
{
    SvgDocument _svgDoc;

    // RectangleF _partsBounds;

    // SvgViewBox _viewBox;

    // SvgUnitType _unitType;

    public SvgDocument Image => _svgDoc;

    public string LayerID {get; set;}

    // public string SvgPartsName { get; private set; }
    // public string SvgLayerName { get; private set; }

    // PartsImageWidth/Height equal to the Width/Height properties
    // of the original SVG Image if parts is not trimmed.
    //public SvgUnit PartsImageWidth { get; private set; }
    //public SvgUnit PartsImageHeight { get; private set; }

    //public SvgUnit PartsWidth { get; private set; }
    //public SvgUnit PartsHeight { get; private set; }

    //public SvgUnit PartsCenterX => ConvertToSvgUnit(PartsWidth.Value / 2);
    //public SvgUnit PartsCenterY => ConvertToSvgUnit(PartsHeight.Value / 2);

    //public SvgUnit Top { get; private set; }
    //public SvgUnit Left { get; private set; }

    //public SvgUnit AbsoluteCenterX => ConvertToSvgUnit(PartsCenterX.Value + Left.Value);
    //public SvgUnit AbsoluteCenterY => ConvertToSvgUnit(PartsCenterY.Value + Top.Value);

    // Relative Size (to ViewBox)
    //public double PartsWidthRatio => _partsBounds.Width / _viewBox.Width;
    //public double PartsHeightRatio => _partsBounds.Height / _viewBox.Height;

    //// Anchors (The positional vlaue that represents ratio to the ViewBox
    //public double PartsTopLeftAnchorX => _partsBounds.X / _viewBox.Width; // Left
    //public double PartsTopLeftAnchorY => _partsBounds.Y / _viewBox.Height; // Top
    
    //public double PartsCenterAnchorX => _partsBounds.Width/2 / _viewBox.Width;
    //public double PartsCenterAnchorY => _partsBounds.Height/2 / _viewBox.Height;

    //public double AbsoluteCenterAnchorX => PartsTopLeftAnchorX + PartsCenterAnchorX;
    //public double AbsoluteCenterAnchorY => PartsTopLeftAnchorY + PartsCenterAnchorY;


    // Convert function
    //private SvgUnit ConvertToSvgUnit(float value) => new SvgUnit(_unitType, value);

    // public SVGObject(SvgDocument svgDoc) //, RectangleF partsBounds, RectangleF docBounds)
    // {
    //     _svgDoc = svgDoc;
    //     _unitType = svgDoc.Height.Type;

    //     _partsBounds = svgDoc.Bounds;
    //     _viewBox = svgDoc.ViewBox;


    //     // The units of bounds variables are [mm]

    //     // Collect svg info and set them to properties
    //     //PartsImageWidth = svgDoc.Width;
    //     //PartsImageHeight = svgDoc.Height;

    //     //Left = ConvertToSvgUnit(partsBounds.Left);
    //     //Top = ConvertToSvgUnit(partsBounds.Top);

    //     //PartsWidth = ConvertToSvgUnit(partsBounds.Width);
    //     //PartsHeight = ConvertToSvgUnit(partsBounds.Height);
    // }

    public SVGObject(SVGDocumentTemplete docTemplete, SVGLayerTemplete layerTemplete, SvgElement element)
    {
        var document = docTemplete.CreateNewDocument();
        var layer = layerTemplete.CreateNewLayer();

        layer.Children.Add(element.DeepCopy());
        document.Children.Add(layer);

        _svgDoc = document;        

        LayerID = layer.ID;
    }

    // public void SetLayerName(string name) => SvgLayerName = name;
    // public void SetPartsName(string name) => SvgPartsName = name;


    // string formatter (for xaml)

}
