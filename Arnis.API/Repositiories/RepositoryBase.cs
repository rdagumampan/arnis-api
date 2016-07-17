using System;
using System.Diagnostics;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;

namespace Arnis.API.Repositiories
{
    public abstract class RepositoryBase
    {
        protected string Database => "arnisdb";
        private const string EndpointUri = "https://arnisdb.documents.azure.com:443/";
        private const string PrimaryKey = "Qz1iWZxei7SN7CKlkUB8rEf4ECHjDtMjdtQcjXEQbDKEr5gDqlocsLXdqQ2EyIj17EEIcwyZ0VIy33uCmyM75g==";

        protected RepositoryBase()
        {
            InitializeDb().Wait();
        }

        private DocumentClient _client;
        protected DocumentClient Client => _client;

        private async Task InitializeDb()
        {
            this._client = new DocumentClient(new Uri(EndpointUri), PrimaryKey);

            await this.CreateDatabaseIfNotExists(Database);
            await this.CreateDocumentCollectionIfNotExists(Database, "accounts");
            await this.CreateDocumentCollectionIfNotExists(Database, "workspaces");
            await this.CreateDocumentCollectionIfNotExists(Database, "dependencies");
            await this.CreateDocumentCollectionIfNotExists(Database, "dependencyhits");
            await this.CreateDocumentCollectionIfNotExists(Database, "workspacehits");
        }

        private async Task CreateDatabaseIfNotExists(string databaseName)
        {
            // check to verify a database with the id=arnisdb does not exist
            try
            {
                await this._client.ReadDatabaseAsync(UriFactory.CreateDatabaseUri(databaseName));
                Debug.WriteLine("Found {0}", databaseName);
            }
            catch (DocumentClientException de)
            {
                // if the database does not exist, create a new database
                if (de.StatusCode == HttpStatusCode.NotFound)
                {
                    await this._client.CreateDatabaseAsync(new Database { Id = databaseName });
                    Debug.WriteLine("Created {0}", databaseName);
                }
                else
                {
                    throw;
                }
            }
        }

        private async Task CreateDocumentCollectionIfNotExists(string databaseName, string collectionName)
        {
            try
            {
                await this._client.ReadDocumentCollectionAsync(UriFactory.CreateDocumentCollectionUri(databaseName, collectionName));
                Debug.WriteLine("Found {0}", collectionName);
            }
            catch (DocumentClientException de)
            {
                // if the document collection does not exist, create a new collection
                if (de.StatusCode == HttpStatusCode.NotFound)
                {
                    // configure collections for maximum query flexibility including string range queries.
                    DocumentCollection collectionInfo = new DocumentCollection
                    {
                        Id = collectionName,
                        IndexingPolicy = new IndexingPolicy(new RangeIndex(DataType.String) {Precision = -1})
                    };

                    // cere we create a collection with 400 RU/s.
                    await this._client.CreateDocumentCollectionAsync(UriFactory.CreateDatabaseUri(databaseName), collectionInfo, new RequestOptions { OfferThroughput = 400 });
                    Debug.WriteLine("Created {0}", collectionName);
                }
                else
                {
                    throw;
                }
            }
        }
    }
}