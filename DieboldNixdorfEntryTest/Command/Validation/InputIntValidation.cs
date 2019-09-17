using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DieboldNixdorfEntryTest.Command.Validation
{
    class InputIntValidation : InputValidation
    {
        public int? MinValue { get; set; }

        public int? MaxValue { get; set; }

        public override bool TestInput(object input)
        {
            if (!base.TestInput(input)) return false;
            if (input.GetType() == typeof(string) && !int.TryParse(((string)input).Trim(), out int value)) return false;
            else if (input.GetType() == typeof(int)) value = (int)input;
            else return false;
            if ((MaxValue.HasValue && MaxValue.Value < value) || (MinValue.HasValue && MinValue.Value > value)) return false;
            return true;
        }
    }
}
