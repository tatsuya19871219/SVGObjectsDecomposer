using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SVGObjectsDecomposer.Models;

namespace SVGObjectsDecomposer.OutputWriters
{
    class GenericOutputWriter : IOutputWriter
    {
        readonly EditableSVGContainer _container;

        readonly string _outputBaseDirname;
        
        internal GenericOutputWriter(EditableSVGContainer container, string outputBaseDirname)
        {
            _container = container;
            _outputBaseDirname = outputBaseDirname;
        }
        public void Execute()
        {
            // After the container instance is disposed, throw the exception
            if (_container.Filename is null) throw new Exception("The instance is already disposed");

            
            if (!Directory.Exists(_outputBaseDirname)) Directory.CreateDirectory(_outputBaseDirname);

            foreach (var layer in _container.Layers)
            {
                string layerName = layer.LayerName;

                string outputDirname = $"{_outputBaseDirname}/{layerName}";

                if (!Directory.Exists(outputDirname)) Directory.CreateDirectory(outputDirname);

                foreach (var obj in layer.Objects)
                {
                    string filename = obj.ElementName.ToLower();

                    string outputFilePath = $"{outputDirname}/{filename}.svg";

                    obj.SvgDoc.Write(outputFilePath);
                }
            }

        }

    }
}
