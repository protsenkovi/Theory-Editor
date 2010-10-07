using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Protsenko.TheoryEditor.Core.Debug
{
    public class Operation
    {
        public string Name { get; set; }

        public Operation()
        {
        }

        public Operation(string name)
        {
            Name = name;
        }
    }
}
