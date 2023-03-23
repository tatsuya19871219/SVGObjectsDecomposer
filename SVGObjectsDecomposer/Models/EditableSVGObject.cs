using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SVGObjectsDecomposer.Models;

internal class EditableSVGObject
{
    readonly SVGObject _svgObject;

    internal EditableSVGObject(SVGObject svgObject)
    {
        _svgObject = svgObject;
    }
}
