using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SVGObjectsDecomposer.Models;

namespace SVGObjectsDecomposer.OutputWriters
{
    abstract class AbstractOutputWriter : IOutputWriter
    {
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

        // // Helper method for directory name strings
        // protected
    }
}
