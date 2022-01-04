using fikatime_api.Models;
using fikatime_api.Resolvers;
using GraphQL;
using GraphQL.Types;

namespace fikatime_api.Query
{
    public class FikatimeGraphQLQuery : ObjectGraphType<object>
    {
        const string MonthParameterName = "month";
        public FikatimeGraphQLQuery(FikatimeResolver fikatimeResolver)
        {
            FieldAsync<ListGraphType<GraphQLFikaModel>>(
                   "Fikatimes",
                   "List of all fikatimes for a specified month.",
                   arguments: new QueryArguments(
                       new QueryArgument<NonNullGraphType<IntGraphType>> { Name = MonthParameterName, Description = "Month as int, 1-12" }
                       ),
                   resolve: async resolveFieldContext =>
                   await fikatimeResolver.ResolveFikatimesForSpecificMonth(
                       resolveFieldContext.GetArgument<int>(MonthParameterName))
                   );
        }
    }
}
