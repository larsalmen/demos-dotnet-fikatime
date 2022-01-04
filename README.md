# demos-dotnet-fikatime
A demo application for showing a layered application architecture, database, API and clients.

The solution consists of an API exposing data stored as documents in a CosmosDB database, and clients consuming that data.

## API
Exposes the data through both REST and GraphQL endpoints.

This needs some settings for your database, place this in either your appsettings.***.json or in secrets:

```
"DocumentRepositoryConfig": {
      "ConnectionString": "your connectionstring",
      "DatabaseId": "your databaseid",
      "ContainerId": "your containerid"
    }
```

## Clients
Currently a sole Blazor WASM SPA is the only client.
