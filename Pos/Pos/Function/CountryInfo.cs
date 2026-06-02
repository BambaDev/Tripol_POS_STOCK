using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pos.Function
{
    internal class CountryInfo
    {
        public string Name { get; set; }
        public string Code { get; set; }

        public CountryInfo(string name, string code)
        {
            Name = name;
            Code = code;
        }
    }
}
