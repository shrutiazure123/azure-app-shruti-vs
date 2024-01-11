using Microsoft.Azure.Cosmos;

namespace Blazor_Azure_Cosmos_DBDemo.Data
{
    public class EngineerService : IEngineerService
    {
        private readonly string CosmosDbConnectionString = "AccountEndpoint=https://azure-dev-cosmos.documents.azure.com:443/;AccountKey=gK1rBFROM1v8GEL9ySDHnMOQJ43YkUlNShuAuMRytUxxsmpDlWAWX0IlguKnVIu7AGbtDtZ3ePSHACDbQSXS9w==;";
        private readonly string CosmosDbname = "Contractors";
        private readonly string CosmosDbContainerName = "Engineers";

        private Container GetContainerClient()
        {
            var cosmosDbClient = new CosmosClient(CosmosDbConnectionString);
            var container = cosmosDbClient.GetContainer(CosmosDbname, CosmosDbContainerName);
            return container;
        }

        //public async Task AddEngineer(Engineer engineer)
        //{
        //    try
        //    {
        //        engineer.id = Guid.NewGuid();
        //        var container = GetContainerClient();
        //        var response = await container.CreateItemAsync(engineer, new PartitionKey(engineer.id.ToString()));
        //        Console.WriteLine(response.StatusCode);
        //    }
        //    catch (Exception ex)
        //    {
        //        ex.Message.ToString();
        //    }
        //}

        public async Task UpsertEngineer(Engineer engineer)
        {
            try
            {
                if(engineer.id==null)
                {
                    engineer.id = Guid.NewGuid();
                }
                var container = GetContainerClient();
                var response = await container.UpsertItemAsync(engineer, new PartitionKey(engineer.id.ToString()));
                Console.WriteLine(response.StatusCode);
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }
        public async Task DeleteEngineer(String? id, string? PartitionKey)
        {
            try
            {
                var container = GetContainerClient();
                var response = await container.DeleteItemAsync<Engineer>(id, new PartitionKey(PartitionKey));
                Console.WriteLine(response.StatusCode);
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }
        public async Task<List<Engineer>> GetEngineerDetails()
        {
            List<Engineer> engineers = new List<Engineer>();
            try
            {
                var container = GetContainerClient();
                var sql = "SELECT * from c";
                QueryDefinition queryDefinition = new QueryDefinition(sql);
                FeedIterator<Engineer> queryResultsSetIterator = container.GetItemQueryIterator<Engineer>(queryDefinition);
                while (queryResultsSetIterator.HasMoreResults)
                {
                    FeedResponse<Engineer> currentResultSet = await queryResultsSetIterator.ReadNextAsync();
                    foreach (var engineer in currentResultSet)
                    {
                        engineers.Add(engineer);
                    }
                }
                return engineers;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }
        public async Task<Engineer> GetEngineerDetailsbyid(String? id, string? PartitionKey)
        {
            try
            {
                var container = GetContainerClient();
                var response = await container.ReadItemAsync<Engineer>(id, new PartitionKey(PartitionKey));
                return response.Resource;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }







    }

}
