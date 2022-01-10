using fikatime_api.Configs;
using fikatime_api.Models;
using Microsoft.Extensions.Options;
using Microsoft.Azure.Cosmos;

namespace fikatime_api.Repositories
{
    public class FikatimeDocumentRepository
    {
        private readonly DocumentRepositoryConfig _repositoryConfig;

        private CosmosClient? _cosmosClient;
        private Container? _container;

        public FikatimeDocumentRepository(IOptions<DocumentRepositoryConfig> repositoryConfig)
        {
            _repositoryConfig = repositoryConfig.Value;
        }

        private async Task<Container> GetOrInitializeCosmosClientAndContainer()
        {
            _cosmosClient ??= await CosmosClient.CreateAndInitializeAsync(
                _repositoryConfig.ConnectionString,
                new List<(string?, string?)> { (_repositoryConfig.DatabaseId, _repositoryConfig.ContainerId) },
                new CosmosClientOptions
                {
                    ConnectionMode = ConnectionMode.Gateway // ConnectionMode.Direct is the default, had to use gateway due to firewall limitations.
                });

            return _container ??= _cosmosClient.GetContainer(_repositoryConfig.DatabaseId, _repositoryConfig.ContainerId);
        }

        internal async Task<IReadOnlyCollection<FikaModel>> GetAllFikatimesForSpecificMonth(int month)
        {
            var container = await GetOrInitializeCosmosClientAndContainer();

            var fikaModels = new List<FikaModel>();

            using (FeedIterator<FikaModel> resultSet = container.GetItemQueryIterator<FikaModel>(
                queryDefinition: null,
                requestOptions: new QueryRequestOptions()
                {
                    PartitionKey = new PartitionKey(month)
                }))
            {
                while (resultSet.HasMoreResults)
                {
                    FeedResponse<FikaModel> response = await resultSet.ReadNextAsync();

                    if (response.Diagnostics != null)
                    {
                        Console.WriteLine($" Diagnostics {response.Diagnostics}");
                    }

                    fikaModels.AddRange(response);
                }
            }

            return fikaModels;
        }
    }
}
