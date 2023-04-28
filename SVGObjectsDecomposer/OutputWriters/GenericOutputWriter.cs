﻿using System;
using System.Collections.Generic;
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
        //readonly EditableSVGContainer _container;

        //readonly string _outputBaseDirname;
        
        internal GenericOutputWriter(EditableSVGContainer container, string outputBaseDirname)
                    : base(container, outputBaseDirname) {}
        // {
        //     _container = container;
        //     _outputBaseDirname = outputBaseDirname;
        // }
        public override void Execute()
        {
            Prepare();

            List<string> positionList = new();
            List<string> pathDataList = new();

            positionList.Add($"ViewBox format => {Formatter.ViewBoxFormatTemplete()}");
            positionList.Add($"Object bounds format => {Formatter.BoundsFormatTemplete()}");
            positionList.Add(string.Format("document viewbox: {0}", Formatter.ViewBoxFormat(_container.ViewBox)));

            foreach (var layer in _container.Layers)
            {
                string layerName = layer.LayerName;

                // string outputDirname = $"{_outputBaseDirname}/{layerName}";

                // if (!Directory.Exists(outputDirname)) Directory.CreateDirectory(outputDirname);

                bool shapeExport = layer.IsVisible;
                bool pathExport = layer.PathExport;

                foreach (var obj in layer.Objects)
                {
                    string filename = obj.ObjectName.ToLower() + ".svg";

                    //string outputFilePath = $"{outputDirname}/{filename}.svg";

                    //obj.SvgDoc.Write(outputFilePath);

                    if(shapeExport)
                    {
                        // perform trimming if requied (should be awaitable?)
                        var trimmedSvgDoc = InkscapeProcessHelper.Trim(obj.SvgDoc);

                        //trimmedSvgDoc.Write(outputFilePath);

                        WriteSvgDoc(filename, layerName, trimmedSvgDoc);

                        positionList.Add(string.Format("{0}: {1}", filename, Formatter.BoundsFormat(obj.Bounds)));
                    }

                    if(pathExport)
                    {
                        var pathSvgDoc = obj.IsPath ? obj.SvgDoc 
                                                    : InkscapeProcessHelper.ObjectToPath(obj.SvgDoc, obj.ID);

                        var pathObj = pathSvgDoc.Children[^1].Children;

                        pathDataList.Add(obj.ObjectName.ToLower());

                        foreach (SvgPath child in pathObj)
                        {
                            var pathString = child.PathData.ToString();

                            pathDataList.Add(pathString);
                        }
                    }

                }
            }

            //
            WriteStringList("PositionList.txt", positionList);

            WriteStringList("PathDataList.txt", pathDataList);
            
        }

    }
}
