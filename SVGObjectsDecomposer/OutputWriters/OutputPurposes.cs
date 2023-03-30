using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SVGObjectsDecomposer.OutputWriters
{
    public enum OutputPurpose
    {
        Generic,
        Animation,
        PositionTracking
    }

    //// hot fix
    //public static class OutputPurposeExtension
    //{
    //    public static bool Equals(this OutputPurpose p1, OutputPurpose p2) {  return p1.Equals(p2); }
    //}
}
