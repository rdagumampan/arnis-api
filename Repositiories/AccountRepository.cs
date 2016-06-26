﻿using System;
using System.Diagnostics;
using System.Linq;
using System.Net;
using Arnis.API.Models;
using System.Threading.Tasks;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;

namespace Arnis.API.Repositiories
{
    public class AccountRepository : RepositoryBase, IAccountRepository
    {
        public AccountRepository()
        {
        }

        public async Task Create(Account account)
        {
            await this.Client.CreateDocumentAsync(UriFactory.CreateDocumentCollectionUri(this.Database, "accounts"), account);
            Debug.WriteLine("Created Account {0}", account.UserName);
        }

        public Account GetByApiKey(string apiKey)
        {
            try
            {
                var collectionUri = UriFactory.CreateDocumentCollectionUri(Database, "accounts");
                var queryOptions = new FeedOptions { MaxItemCount = -1 };
                var account = this.Client.CreateDocumentQuery<Account>(
                        collectionUri, queryOptions)
                        .Where(f => f.ApiKey == apiKey)
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

        public Account GetByUserName(string userName)
        {
            try
            {
                var collectionUri = UriFactory.CreateDocumentCollectionUri(Database, "accounts");
                var queryOptions = new FeedOptions { MaxItemCount = -1 };
                var account = this.Client.CreateDocumentQuery<Account>(
                        collectionUri, queryOptions)
                        .Where(f => f.UserName == userName)
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
            } return null;
        }
    }
}