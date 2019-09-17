using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DieboldNixdorfEntryTest.Utility
{
    interface ICipher
    {
        string Encrypt(string value);

        string Decrypt(string value);
    }
}
