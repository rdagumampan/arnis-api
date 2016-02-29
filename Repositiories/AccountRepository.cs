using Microsoft.Extensions.OptionsModel;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Linq;
using Arnis.API.Models;

namespace Arnis.API.Repositiories
{
    public class AccountRepository : RepositoryBase, IAccountRepository
    {
        public AccountRepository(IOptions<Settings> settings) : base(settings)
        {
        }

        public Account GetByApiKey(string apiKey)
        {
            var query = Builders<Account>.Filter.Eq(e => e.ApiKey, apiKey);
            var workspaces = Database
                .GetCollection<Account>("accounts")
                .Find(query)
                .ToListAsync();

            return workspaces.Result
                .FirstOrDefault();
        }

        public Account GetByUserName(string userName)
        {
            var query = Builders<Account>
                .Filter.Eq(e => e.UserName, userName);
            var workspaces = Database
                .GetCollection<Account>("accounts")
                .Find(query)
                .ToListAsync();

            return workspaces.Result
                .FirstOrDefault();
        }

        public Account GetById(ObjectId id)
        {
            throw new System.NotImplementedException();
        }

        public void Add(Account account)
        {
            Database
                .GetCollection<Account>("accounts")
                .InsertOneAsync(account);
        }
    }
}