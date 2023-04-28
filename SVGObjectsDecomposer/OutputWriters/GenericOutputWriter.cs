using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SVGObjectsDecomposer.Models;

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

            positionList.Add(string.Format("document viewbox: {0}", _container.ViewBox));

            foreach (var layer in _container.Layers)
            {
                string layerName = layer.LayerName;

                string outputDirname = $"{_outputBaseDirname}/{layerName}";

                if (!Directory.Exists(outputDirname)) Directory.CreateDirectory(outputDirname);

                bool shapeExport = layer.IsVisible;
                bool pathExport = layer.PathExport;

                foreach (var obj in layer.Objects)
                {
                    string filename = obj.ObjectName.ToLower();

                    string outputFilePath = $"{outputDirname}/{filename}.svg";

                    obj.SvgDoc.Write(outputFilePath);
                }
            }

            // Output path data
            //StreamWriter streamWriter = new StreamWriter($"{_outputBaseDirname}/posisionList.txt", false, Encoding.UTF8);

            //streamWriter.WriteLine(string.Format("document viewbox: {0}", _container.ViewBox.ToString()));
        }

    }
}
