using System.Threading.Tasks;
using Arnis.API.Models;

namespace Arnis.API.Repositiories
{
    public interface IAccountRepository
    {
        Task Create(Account account);
        Account GetByUserName(string userName);
        Account GetByApiKey(string apiKey);
    }
}