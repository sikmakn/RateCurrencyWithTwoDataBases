using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.RateUpdate.Interfacies
{
    public interface IReader
    {
        Task<string> HttpClientRead(string url);
    }
}
