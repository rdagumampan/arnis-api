using Arnis.API.Models;
using MongoDB.Bson;

namespace Arnis.API.Repositiories
{
    public interface IAccountRepository
    {
        Account GetByUserName(string userName);
        Account GetByApiKey(string apiKey);
        Account GetById(ObjectId id);
        void Add(Account account);
    }
}