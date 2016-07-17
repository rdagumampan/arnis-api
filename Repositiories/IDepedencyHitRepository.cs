using System.Threading.Tasks;
using Arnis.Documents;

namespace Arnis.API.Repositiories
{
    public interface IDepedencyHitRepository
    {
        Task Create(DependencyHit dependencyHit);
        Task Update(DependencyHit dependencyHit);
        DependencyHit GetByName(string dependecyName);
    }
}