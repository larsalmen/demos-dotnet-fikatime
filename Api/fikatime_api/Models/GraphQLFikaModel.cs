using GraphQL.Types;

namespace fikatime_api.Models
{
    public class GraphQLFikaModel : ObjectGraphType<FikaDTO>
    {
        public GraphQLFikaModel()
        {
            Name = "Fikatime";

            Field(fikamodelDTO => fikamodelDTO.Name, type: typeof(StringGraphType)).Description("Commonly known name of the fikatime");
            Field(fikamodelDTO => fikamodelDTO.Date, type: typeof(DateTimeGraphType)).Description("Date of the occurence of this fikatime");
            Field(fikamodelDTO => fikamodelDTO.Description, type: typeof(StringGraphType)).Description("Descriptive text");
            Field(fikamodelDTO => fikamodelDTO.WikiUrl, type: typeof(StringGraphType)).Description("WikiPedia link to relevant information");
        }
    }
}
