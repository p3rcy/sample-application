using System.Threading.Tasks;

namespace sample_application.ApiClients
{
    public interface IPasswordBreachClient
    {
        Task<bool> HasPasswordBreached(string substringHashedPassword, string fullPassword);
    }
}