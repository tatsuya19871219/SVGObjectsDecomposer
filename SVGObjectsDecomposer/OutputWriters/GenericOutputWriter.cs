using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Svg;
using SVGObjectsDecomposer.Helpers;
using SVGObjectsDecomposer.Models;
using Formatter = SVGObjectsDecomposer.Helpers.StringFormatHelper;

namespace SVGObjectsDecomposer.OutputWriters
{
    class GenericOutputWriter : AbstractOutputWriter
    {       
        internal GenericOutputWriter(EditableSVGContainer container, string outputBaseDirname)
                    : base(container, outputBaseDirname) {}
        
        public override void Execute()
        {
            Prepare();

            // Copy the original svg file
            CopyOriginalSvgDoc();

            List<string> positionList = new();
            List<string> pathDataList = new();

            positionList.Add($"ViewBox format => {Formatter.ViewBoxFormatTemplete()}");
            positionList.Add($"Object bounds format => {Formatter.BoundsFormatTemplete()}");
            positionList.Add(string.Format("document viewbox: {0}", Formatter.ViewBoxFormat(_container.ViewBox)));

            foreach (var layer in _container.Layers)
            {
                string layerName = layer.LayerName;

                bool shapeExport = layer.IsVisible;
                bool pathExport = layer.PathExport;

                if (!shapeExport && !pathExport) continue;

                foreach (var obj in layer.Objects)
                {                                        
                    if(shapeExport)
                    {
                        string filename = obj.ObjectName.ToLower() + ".svg";

                        ExportShape(obj, layerName, filename, out var partsBounds);

                        positionList.Add(string.Format("{0}: {1}", filename, Formatter.BoundsFormat(partsBounds)));
                    }

                    if(pathExport)
                    {
                        ExtractPathData(obj, out var pathData);

                        pathDataList.AddRange(pathData);
                    }

                }
            }

            WriteStringList("PositionList.txt", positionList);
            if(pathDataList.Count > 0) WriteStringList("PathDataList.txt", pathDataList);
            
        }

        #region Helper functions for generating output
        void ExportShape(EditableSVGObject obj, string layerName, string filename, out RectangleF bounds)
        {            
            var partsSvgDoc = InkscapeProcessHelper.Trim(obj.SvgDoc);

            PointF partsCenter = new PointF(obj.Bounds.X + obj.Bounds.Width/2, obj.Bounds.Y + obj.Bounds.Height/2);

            var partsViewBox = partsSvgDoc.ViewBox;

            Debug.Assert(partsViewBox.MinX == 0 && partsViewBox.MinY == 0);

            bounds = new RectangleF(partsCenter.X - partsViewBox.Width/2, partsCenter.Y - partsViewBox.Height/2, partsViewBox.Width, partsViewBox.Height);

            WriteSvgDoc(filename, layerName, partsSvgDoc);
        }

        void ExtractPathData(EditableSVGObject obj, out List<string> pathData)
        {
            pathData = new();

            var pathSvgDoc = obj.IsPath ? obj.SvgDoc 
                                        : InkscapeProcessHelper.ObjectToPath(obj.SvgDoc, obj.ID);

            var objectToPathObjs = pathSvgDoc.Children[^1].Children;

            List<SvgPath> pathObj = new();

            foreach(var otpObj in objectToPathObjs)
            {
                if (otpObj is SvgPath otpPath) pathObj.Add(otpPath);
                else if (otpObj is SvgGroup otpGroup) 
                {
                    List<SvgPath> flatten = new();
                    FlattenPathGroup(otpGroup, ref flatten);
                    pathObj.AddRange(flatten);
                }
                else throw new Exception("Unexpected SvgElement Type after ObjectToPath operation.");
            }

            pathData.Add(obj.ObjectName.ToLower());

            foreach (SvgPath child in pathObj)
            {
                var pathString = child.PathData.ToString();

                pathData.Add(pathString);
            }
        }
        #endregion

        // Helper function for nested path group
        void FlattenPathGroup(SvgGroup group, ref List<SvgPath> flatten)
        {
            foreach (var item in group.Children)
            {
                if (item is SvgGroup)
                {
                    FlattenPathGroup(item as SvgGroup, ref flatten);
                    continue;
                }

                Debug.Assert(item is SvgPath);

                flatten.Add(item as SvgPath);
            }
        }

    }
}
