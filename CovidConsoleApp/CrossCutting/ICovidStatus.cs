using System.Threading.Tasks;

namespace CrossCutting
{
    public interface ICovidStatus
    {
        /// <summary>
        /// Get list of corona cases from googles site 
        /// </summary>
        /// <returns>
        /// Local,
        /// Confirmados,
        /// Mortes,
        /// Recuperados,
        /// DadosDoDia,
        /// 
        /// </returns>
        Task ListStatusAsync();
    }
}