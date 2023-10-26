using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailsParser.Interfaces
{
    internal interface IParsingService
    {
        Task ParseAsync(string path);
    }
}
