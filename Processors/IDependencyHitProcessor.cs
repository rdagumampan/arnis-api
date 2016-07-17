using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Arnis.API.Repositiories;
using Arnis.Documents;

namespace Arnis.API.Processors
{
    public interface IDependencyHitProcessor
    {
        void Process(Workspace workspace);        
    }

    public class DependencyHitProcessor : IDependencyHitProcessor
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IWorkspaceRepository _workspaceRepository;

        public DependencyHitProcessor(
            IAccountRepository accountRepository, 
            IWorkspaceRepository workspaceRepository)
        {
            _accountRepository = accountRepository;
            _workspaceRepository = workspaceRepository;
        }

        public void Process(Workspace workspace)
        {

        }
    }

}
