using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SVGObjectsDecomposer.Models;

namespace SVGObjectsDecomposer.OutputWriters;

internal class CustomOutputWriterForPositionTracking : AbstractOutputWriter
{
    internal CustomOutputWriterForPositionTracking(EditableSVGContainer container, string outputBaseDirname)
                : base(container, outputBaseDirname) {}

    public override void Execute()
    {
        Prepare();

        string outputFilePath = $"{_outputBaseDirname}/{_container.Filename}";

        File.Copy(_container.OriginalFilePath, outputFilePath);
    }
}
