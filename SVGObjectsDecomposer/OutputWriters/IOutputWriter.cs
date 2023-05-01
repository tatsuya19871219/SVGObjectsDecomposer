namespace SVGObjectsDecomposer.OutputWriters;

interface IOutputWriter
{
    string OutputBaseDirname {get;}
    void Execute();
}
