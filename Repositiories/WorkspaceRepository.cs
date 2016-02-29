using System.Collections.Generic;
using Microsoft.Extensions.OptionsModel;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Linq;
using Arnis.API.Models;

namespace Arnis.API.Repositiories
{
    public class WorkspaceRepository : RepositoryBase, IWorkspaceRepository
    {
        public WorkspaceRepository(IOptions<Settings> settings) : base(settings)
        {
        }

        public IEnumerable<Workspace> All()
        {
            var workspaces = Database
                .GetCollection<Workspace>("workspaces")
                .Find(new BsonDocument())
                .ToListAsync();

            return workspaces.Result;
        }

        public Workspace GetById(ObjectId id)
        {
            var query = Builders<Workspace>
                .Filter.Eq(e => e.Id, id);

            var workspaces = Database
                .GetCollection<Workspace>("workspaces")
                .Find(query)
                .ToListAsync();

            return workspaces.Result.FirstOrDefault();
        }

        public Workspace GetByAccountId(ObjectId accountId)
        {
            var query = Builders<Workspace>
                .Filter.Eq(e => e.AccountId, accountId);

            var workspaces = Database
                .GetCollection<Workspace>("workspaces")
                .Find(query)
                .ToListAsync();

            return workspaces.Result.FirstOrDefault();
        }

        public void Add(Workspace workspace)
        {
            Database
                .GetCollection<Workspace>("workspaces")
                .InsertOneAsync(workspace);
        }

        public bool Remove(ObjectId id)
        {
            var query = Builders<Workspace>.Filter.Eq(e => e.Id, id);
            var result = Database
                .GetCollection<Workspace>("workspaces")
                .DeleteOneAsync(query);

            return GetById(id) == null;
        }

        public void Update(Workspace workspace)
        {
            var query = Builders<Workspace>.Filter.Eq(e => e.AccountId, workspace.AccountId);
            var update = Database
                .GetCollection<Workspace>("workspaces")
                .ReplaceOneAsync(query, workspace);
        }
    }
}