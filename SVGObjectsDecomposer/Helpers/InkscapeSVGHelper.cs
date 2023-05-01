using Svg;
using System.Xml.Linq;

namespace SVGObjectsDecomposer.Helpers;

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
