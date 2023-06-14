using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jhipster.Crosscutting.Utilities
{
    public static class CodeGenerator
    {
        public static string GenerateCode(string maxcode)
        {
            int num = int.Parse(maxcode.Substring(2));
            string sequenceNumber = String.Format("{0:D6}", num+1);
            string code = $"HL{sequenceNumber}";
            return code;
        }
    }
}
