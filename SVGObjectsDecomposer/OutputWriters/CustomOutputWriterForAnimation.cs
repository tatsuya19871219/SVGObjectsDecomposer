using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SVGObjectsDecomposer.Models;

namespace SVGObjectsDecomposer.OutputWriters
{
    internal class CustomOutputWriterForAnimation : AbstractOutputWriter
    {
        internal CustomOutputWriterForAnimation(EditableSVGContainer container, string outputBaseDirname)
                    : base(container, outputBaseDirname) {}

        public override void Execute()
        {
            Prepare();

            foreach (var layer in _container.Layers)
            {
                string layerName = layer.LayerName;

                string outputDirname = $"{_outputBaseDirname}/{layerName}";

                if (!Directory.Exists(outputDirname)) Directory.CreateDirectory(outputDirname);

                foreach (var obj in layer.Objects)
                {
                    string filename = obj.ObjectName.ToLower();

                    string outputFilePath = $"{outputDirname}/{filename}.svg";

                    // perform trimming if requied (should be awaitable?)
                    var trimmedSvgDoc = InkscapeProcessHelper.Trim(obj.SvgDoc);

                    trimmedSvgDoc.Write(outputFilePath);
                }
            }

            // write anchor lists of the objects
        }
    }
}
