using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SVGObjectsDecomposer.Models;

namespace SVGObjectsDecomposer.OutputWriters
{
    class OutputWriterFactory
    {
        private OutputWriterFactory()
        {

        }

        static internal IOutputWriter Create(EditableSVGContainer container, OutputPurpose purpose)
        {
            switch (purpose)
            {
                case OutputPurpose.Generic:
                    return new GenericOutputWriter(container);
                    
                default:
                    throw new Exception("Unknown OutputPurpose is given.");
                    
            }
        }
    }
}
