using System;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Arnis.Core.Documents;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;

namespace Arnis.API.Repositiories
{
    public class WorkspaceRepository : RepositoryBase, IWorkspaceRepository
    {
        private readonly string collectionId = "workspaces";

        public WorkspaceRepository()
        {
        }
        public async Task Create(Workspace workspace)
        {
            await this.Client.CreateDocumentAsync(UriFactory.CreateDocumentCollectionUri(this.Database, collectionId), workspace);
            Debug.WriteLine("Created Workspace {0}", workspace.Name);
        }

        public async Task Update(Workspace workspace)
        {
            await this.Client.ReplaceDocumentAsync(UriFactory.CreateDocumentCollectionUri(this.Database, collectionId), workspace);
            Debug.WriteLine("Updated Workspace {0}", workspace.Name);
        }

        public Workspace GetByName(string accountId, string workspaceName)
        {
            try
            {
                var collectionUri = UriFactory.CreateDocumentCollectionUri(Database, collectionId);
                var queryOptions = new FeedOptions { MaxItemCount = -1 };
                var account = this.Client.CreateDocumentQuery<Workspace>(
                        collectionUri, queryOptions)
                        .Where(f => 
                            f.AccountId == accountId && 
                            f.Name == workspaceName)
                        .AsEnumerable()
                        .FirstOrDefault();

                return account;
            }
            catch (AggregateException ex)
            {
                var innerException = ex.InnerExceptions.FirstOrDefault();
                var documentException = innerException as DocumentClientException;
                if (documentException?.StatusCode == HttpStatusCode.NotFound)
                    return null;
            }
            return null;
        }
    }
}