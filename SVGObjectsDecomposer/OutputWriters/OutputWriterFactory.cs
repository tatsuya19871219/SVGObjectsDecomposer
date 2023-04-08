using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SVGObjectsDecomposer.Models;

namespace SVGObjectsDecomposer.OutputWriters
{
    class OutputWriterFactory
    {
        readonly EditableSVGContainer _container;
        internal OutputWriterFactory(EditableSVGContainer container)
        {
            _container = container;
        }

        internal IOutputWriter Create(string outputBaseDirname, OutputPurpose purpose)
        {
            switch (purpose)
            {
                case OutputPurpose.Generic:
                    return new GenericOutputWriter(_container, outputBaseDirname);
                    
                case OutputPurpose.Animation:
                    return new CustomOutputWriterForAnimation(_container, outputBaseDirname);

                case OutputPurpose.PositionTracking:    
                    return new CustomOutputWriterForPositionTracking(_container, outputBaseDirname);

                default:
                    throw new Exception("Unknown OutputPurpose is given.");
                    
            }
        }

        //string GetOriginalFilename() => _container.Filename;
        internal string GetOriginalDirname() => Path.GetDirectoryName(_container.OriginalFilePath);
        internal string GetOriginalFileBasename()
            => Path.GetFileNameWithoutExtension(_container.OriginalFilePath);

        //
        internal string GetDefaultOutputBaseDirname()
        {
            string originalDirname = GetOriginalDirname();
            string originalFileBasename = GetOriginalFileBasename();

            // for test
            string outputBaseDirname = $"{originalDirname}/output_{originalFileBasename}";

            return outputBaseDirname;
        }
    }
}
