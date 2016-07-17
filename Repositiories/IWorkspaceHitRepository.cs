using System.Collections.Generic;
using System.Threading.Tasks;
using Arnis.Documents;

namespace Arnis.API.Repositiories
{
    public interface IWorkspaceHitRepository
    {
        Task Create(WorkspaceHit workspace);
        Task Update(WorkspaceHit workspace);
        List<WorkspaceHit> GetByWorkspaceId(string workspaceId);
    }
}