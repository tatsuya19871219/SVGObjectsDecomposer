using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SVGObjectsDecomposer.Models;

internal class EditableSVGLayer
{
    readonly SVGLayer _svgLayer;

    internal EditableSVGLayer(SVGLayer svgLayer)
    {
        _svgLayer = svgLayer;
    }
}
