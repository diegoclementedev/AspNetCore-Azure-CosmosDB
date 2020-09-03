using CosmosDB.Domain;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CosmosDB.Repository
{
    public class ClienteRepository
    {
        public string Endpoint = @"https://localhost:8081/";
        public string Key = "C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==";
        public string DatabaseId = "CosmosDB";
        public string CollectionId = "ClienteCollection";
        public DocumentClient DocumentClient;
        public ClienteRepository()
        {
            this.DocumentClient = new DocumentClient(new Uri(Endpoint), Key);
            DocumentClient.CreateDatabaseIfNotExistsAsync(new Database() { Id = DatabaseId }).Wait();
            DocumentClient.CreateDocumentCollectionIfNotExistsAsync(UriFactory.CreateDatabaseUri(DatabaseId),
                                                                    new DocumentCollection() { Id = CollectionId },
                                                                    new RequestOptions()
                                                                    {
                                                                        OfferThroughput = 1000
                                                                    }).Wait();
        }
        public async Task Save(Cliente model) => await DocumentClient.CreateDocumentAsync(UriFactory.CreateDocumentCollectionUri(DatabaseId, CollectionId), model);
        public async Task Remove(Guid id) => await DocumentClient.DeleteDocumentAsync(UriFactory.CreateDocumentUri(DatabaseId, CollectionId, id.ToString()));
        public async Task Update(Guid id, Cliente model) => await DocumentClient.ReplaceDocumentAsync(UriFactory.CreateDocumentUri(DatabaseId, CollectionId, id.ToString()), model);
        public async Task<Cliente> Get(Guid id)
        {
            try
            {
                Document doc = await this.DocumentClient.ReadDocumentAsync(
                UriFactory.CreateDocumentUri(DatabaseId, CollectionId, id.ToString()));

                return JsonConvert.DeserializeObject<Cliente>(doc.ToString());
            }
            catch (DocumentClientException e)
            {
                if (e.StatusCode == System.Net.HttpStatusCode.NotFound)
                    return null;
                else
                    throw;
            }
        }
        public async Task<IEnumerable<Cliente>> GetAll()
        {
            var documents = this.DocumentClient.CreateDocumentQuery<Cliente>(
                    UriFactory.CreateDocumentCollectionUri(DatabaseId, CollectionId),
                    new FeedOptions { MaxItemCount = -1 }
                ).AsDocumentQuery();
            List<Cliente> result = new List<Cliente>();
            while (documents.HasMoreResults)
                result.AddRange(await documents.ExecuteNextAsync<Cliente>());
            return result;
        }
    }
}
