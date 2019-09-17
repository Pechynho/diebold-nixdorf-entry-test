using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DieboldNixdorfEntryTest.Command.Validation
{
    class InputStringValidation : InputValidation
    {
        public int? MinLength { get; set; }

        public int? MaxLength { get; set; }

        public bool AllowEmpty { get; set; }

        public bool AllowOnlyWhitespaces { get; set; }

        public char[] BlacklistCharacters { get; set; }

        public override bool TestInput(object input)
        {
            if (!base.TestInput(input)) return false;
            if (input.GetType() != typeof(string)) return false;       
            string value = (string)input;
            if ((MinLength.HasValue && MinLength.Value > value.Length) || (MaxLength.HasValue && MaxLength.Value < value.Length) || (!AllowEmpty && string.IsNullOrEmpty(value)) || (!AllowOnlyWhitespaces && string.IsNullOrWhiteSpace(value))) return false;
            if (!BlacklistCharacters.FirstOrDefault(o => value.Contains(o)).Equals('\0')) return false;
            return true;
        }
    }
}
