using System;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Arnis.API.Models;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;

namespace Arnis.API.Repositiories
{
    public class WorkspaceRepository : RepositoryBase, IWorkspaceRepository
    {
        public WorkspaceRepository()
        {
        }
        public async Task Create(Workspace workspace)
        {
            await this.Client.CreateDocumentAsync(UriFactory.CreateDocumentCollectionUri(this.Database, "workspaces"), workspace);
            Debug.WriteLine("Created Workspace {0}", workspace.Name);
        }

        public Workspace GetByName(string workspaceName)
        {
            try
            {
                var collectionUri = UriFactory.CreateDocumentCollectionUri(Database, "workspaces");
                var queryOptions = new FeedOptions { MaxItemCount = -1 };
                var account = this.Client.CreateDocumentQuery<Workspace>(
                        collectionUri, queryOptions)
                        .Where(f => f.Name == workspaceName)
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