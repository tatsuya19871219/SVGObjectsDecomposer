using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Svg;

namespace SVGObjectsDecomposer.Models;

static class InkscapeSVGHelper
{
    static XNamespace s_nsInkscape = "http://www.inkscape.org/namespaces/inkscape";

    internal static bool TryGetInkscapeLabel(SvgElement element, out string name)
    {
        
        var elementXML = element.GetXML();
        var xmldoc = XDocument.Parse(elementXML);

        var xelement = xmldoc.FirstNode as XElement;

        var labelAttr = xelement.Attribute(s_nsInkscape + "label");

        if (labelAttr is null) 
        {
            name = null;
            return false;
        }
        else
        {
            name = labelAttr.Value;
            return true;
        }
        
    }


}
