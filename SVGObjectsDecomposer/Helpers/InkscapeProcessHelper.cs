using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Svg;

namespace SVGObjectsDecomposer.Helpers;

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

    internal static SvgDocument Trim(SvgDocument original)
    {
        try
        {
            using (Process proc = new Process())
            {
                proc.StartInfo.UseShellExecute = false;
                proc.StartInfo.FileName = s_inkscape;
                proc.StartInfo.Arguments = "--pipe --export-area-drawing --export-filename=-";
                proc.StartInfo.RedirectStandardInput = true;
                proc.StartInfo.RedirectStandardOutput = true;

                proc.Start();

                string textSvgDoc = original.GetXML();

                StreamWriter streamWriter = proc.StandardInput;
                streamWriter.WriteLine(textSvgDoc);
                streamWriter.Close();

                StreamReader streamReader = proc.StandardOutput;
                string output = streamReader.ReadToEnd();

                proc.WaitForExit();

                return SvgDocument.FromSvg<SvgDocument>(output);
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    // object-to-path
    // ex.
    // inkscape --select=path1262 --actions="object-to-path" --export-filename=bbb.svg .\aaa.svg
    // it also works for layer-wise:
    // inkscape --select=layer1 --actions="object-to-path" --export-filename=hoge.svg .\TileBase.svg
    internal static SvgDocument ObjectToPath(SvgDocument original, string ID)
    {
        try 
        {
            using (Process proc = new Process())
            {
                proc.StartInfo.UseShellExecute = false;
                proc.StartInfo.FileName = s_inkscape;
                proc.StartInfo.Arguments = $"--pipe --select={ID} --actions=\"object-to-path\" --export-filename=-";
                proc.StartInfo.RedirectStandardInput = true;
                proc.StartInfo.RedirectStandardOutput = true;

                proc.Start();

                string textSvgDoc = original.GetXML();

                StreamWriter streamWriter = proc.StandardInput;
                streamWriter.WriteLine(textSvgDoc);
                streamWriter.Close();

                StreamReader streamReader = proc.StandardOutput;
                string output = streamReader.ReadToEnd();

                proc.WaitForExit();

                return SvgDocument.FromSvg<SvgDocument>(output);
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
