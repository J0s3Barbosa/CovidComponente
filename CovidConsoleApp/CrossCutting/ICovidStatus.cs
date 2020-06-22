using System.Threading.Tasks;

namespace CrossCutting
{
    public interface ICovidStatus
    {
        Task ListStatusAsync();
    }
}