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
        
        internal GenericOutputWriter(EditableSVGContainer container)
        {
            _container = container;
        }
        public void Execute()
        {
            // After the container instance is disposed, throw the exception
            if (_container.Filename is null) throw new Exception("The instance is already disposed");

            string originalDirname = GetOriginalDirname();
            string originalFileBasename = GetOriginalFileBasename();

            // for test
            string outputBaseDirname = $"{originalDirname}/output_{originalFileBasename}";

            if (!Directory.Exists(outputBaseDirname)) Directory.CreateDirectory(outputBaseDirname);

            foreach (var layer in _container.Layers)
            {
                string layerName = layer.LayerName;

                string outputDirname = $"{outputBaseDirname}/{layerName}";

                if (!Directory.Exists(outputDirname)) Directory.CreateDirectory(outputDirname);

                foreach (var obj in layer.Objects)
                {
                    string filename = obj.ElementName.ToLower();

                    string outputFilePath = $"{outputDirname}/{filename}.svg";

                    obj.SvgDoc.Write(outputFilePath);
                }
            }

        }

        //string GetOriginalFilename() => _container.Filename;
        string GetOriginalDirname() => Path.GetDirectoryName(_container.OriginalFilePath);
        string GetOriginalFileBasename()
            => Path.GetFileNameWithoutExtension(_container.OriginalFilePath);
        
    }
}
