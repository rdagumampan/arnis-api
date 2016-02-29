using System.Collections.Generic;
using Arnis.API.Models;
using MongoDB.Bson;

namespace Arnis.API.Repositiories
{

    public interface IWorkspaceRepository
    {
        IEnumerable<Workspace> All();

        Workspace GetById(ObjectId id);

        void Add(Workspace workspace);

        void Update(Workspace workspace);

        bool Remove(ObjectId id);
    }
}
