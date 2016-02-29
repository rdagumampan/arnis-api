using Microsoft.Extensions.OptionsModel;
using MongoDB.Driver;

namespace Arnis.API.Repositiories
{
    public abstract class RepositoryBase
    {
        private readonly IOptions<Settings> _settings;
        public IMongoDatabase Database { get; set; }

        public RepositoryBase(IOptions<Settings> settings)
        {
            _settings = settings;
            Database = Connect();
        }

        private IMongoDatabase Connect()
        {
            var client = new MongoClient(_settings.Value.ConnectionString);
            var database = client.GetDatabase(_settings.Value.Database);

            return database;
        }
    }
}