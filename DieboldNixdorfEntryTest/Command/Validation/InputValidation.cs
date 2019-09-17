using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DieboldNixdorfEntryTest.Command.Validation
{
    class InputValidation
    {
        public bool AllowNull { get; set; }

        public virtual bool TestInput(object input)
        {
            if (input == null && !AllowNull) return false;
            return true;
        }
    }
}
