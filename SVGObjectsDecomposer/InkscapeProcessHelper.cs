using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SVGObjectsDecomposer;

static class InkscapeProcessHelper
{
    static string s_inkscape = "inkscape.exe";

    internal static bool CheckInkscapeProcess()
    {
        try
        {
            using(Process proc = new Process())
            {
                proc.StartInfo.UseShellExecute = false;
                proc.StartInfo.FileName = s_inkscape;
                proc.StartInfo.Arguments = "--version";
                proc.Start();
                proc.WaitForExit();
            }

            return true;
        }
        catch
        {
            return false;
        }
    }
}
