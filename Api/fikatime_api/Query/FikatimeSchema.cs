using GraphQL.Types;

namespace fikatime_api.Query
{
    public class FikatimeSchema : Schema
    {
        public FikatimeSchema(IServiceProvider provider) : base(provider)
        {
            Query = provider.GetRequiredService<FikatimeGraphQLQuery>();
        }
    }
}
