using System.Collections.Generic;
using System.Threading.Tasks;
using Arnis.API.Models;

namespace Arnis.API.Repositiories
{

    public interface IWorkspaceRepository
    {
        Task Create(Workspace workspace);
        Task Update(Workspace workspace);
        Workspace GetByName(string accountId, string workspaceName);
    }
}
