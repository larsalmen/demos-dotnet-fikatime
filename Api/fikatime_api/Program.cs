using fikatime_api.Configs;
using fikatime_api.Query;
using fikatime_api.Repositories;
using fikatime_api.Resolvers;
using fikatime_api.Services;
using GraphQL;
using GraphQL.Server;
using GraphQL.Types;

var builder = WebApplication.CreateBuilder(args);
var AllowAnyOrigin = "_allowAnyOrigin";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: AllowAnyOrigin,
        builder =>
        {
            builder.AllowAnyOrigin();
        });
});

// Add configurations
builder.Services.Configure<DocumentRepositoryConfig>(builder.Configuration.GetSection("DocumentRepositoryConfig"));

// Add services to the container.
builder.Services.AddSingleton<FikatimeDocumentRepository>();
builder.Services.AddScoped<FikatimeService>();
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Graphql below
builder.Services.AddSingleton<FikatimeResolver>();
builder.Services.AddSingleton<ISchema, FikatimeSchema>();

GraphQL.MicrosoftDI.GraphQLBuilderExtensions
    .AddGraphQL(builder.Services)
    .AddServer(true)
    .ConfigureExecution(options =>
    {
        options.EnableMetrics = false;
        var logger = options.RequestServices?.GetRequiredService<ILogger<Program>>();
        options.UnhandledExceptionDelegate = ctx => logger?.LogError("{Error} occurred", ctx.OriginalException.Message);
    })
    // Add required services for GraphQL request/response de/serialization
    .AddSystemTextJson()
    .AddErrorInfoProvider(opt => opt.ExposeExceptionStackTrace = builder.Environment.IsDevelopment())
    .AddGraphTypes(typeof(FikatimeSchema).Assembly);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors(AllowAnyOrigin);
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// GraphQL below
app.UseGraphQL<ISchema>();
app.UseGraphQLGraphiQL();

app.Run();
