using System.Collections.Generic;
using System.Threading.Tasks;
using Arnis.API.Models;

namespace Arnis.API.Repositiories
{

    public interface IWorkspaceRepository
    {
        Task Create(Workspace workspace);
        Workspace GetByName(string workspaceName);
    }
}
