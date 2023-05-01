using Svg;
using SVGObjectsDecomposer.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SVGObjectsDecomposer.OutputWriters;

abstract class AbstractOutputWriter : IOutputWriter
{
    public string OutputBaseDirname => _outputBaseDirname;
    readonly protected EditableSVGContainer _container;
    readonly protected string _outputBaseDirname;

    internal AbstractOutputWriter(EditableSVGContainer container, string outputBaseDirname)
    {
        _container = container;
        _outputBaseDirname = outputBaseDirname;
    }

    public abstract void Execute();

    protected void Prepare()
    {
        CheckContainer();
        
        // this flow will be modified to allow overwrite the output
        if (!Directory.Exists(_outputBaseDirname)) 
                Directory.CreateDirectory(_outputBaseDirname);
        //else throw new Exception("Output directory is already exists.");

    }
    void CheckContainer()
    {
        // After the container instance is disposed, throw the exception
        if (_container.Filename is null) throw new Exception("The instance is already disposed");
    }

    protected void WriteStringList(string filename, List<string> contents)
    {
        try
        {
            StreamWriter streamWriter = new StreamWriter($"{_outputBaseDirname}/{filename}", false, Encoding.UTF8);

            foreach(var line in contents)
                streamWriter.WriteLine(line);

            streamWriter.Close();
        }
        catch (Exception ex)
        {
            Console.Write(ex.Message);
        }
    }


    protected void CopyOriginalSvgDoc()
    {
        _container.OriginalSVGDocument.Write($"{_outputBaseDirname}/original.svg");
    }

    protected void WriteSvgDoc(string filename, string dirname, SvgDocument svgdoc)        
    {
        string outputDirname = $"{_outputBaseDirname}/{dirname}";

        if (!Directory.Exists(outputDirname)) Directory.CreateDirectory(outputDirname);

        svgdoc.Write($"{outputDirname}/{filename}");
    }
}
