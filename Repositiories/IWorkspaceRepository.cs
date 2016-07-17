using System.Threading.Tasks;
using Arnis.Documents;

namespace Arnis.API.Repositiories
{

    public interface IWorkspaceRepository
    {
        Task Create(Workspace workspace);
        Task Update(Workspace workspace);
        Workspace GetByName(string accountId, string workspaceName);
    }
}
