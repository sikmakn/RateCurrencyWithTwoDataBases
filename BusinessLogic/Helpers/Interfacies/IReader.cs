using System.Threading.Tasks;

namespace BusinessLogic.Helpers.Interfacies
{
    public interface IReader
    {
        Task<string> HttpClientRead(string url);
    }
}
