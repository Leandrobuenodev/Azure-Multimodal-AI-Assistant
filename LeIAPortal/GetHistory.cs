using Azure.Data.Tables;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Configuration;

namespace LeIAPortal;

public class GetHistory
{
    private readonly string _connString;

    public GetHistory(IConfiguration config)
    {
        _connString = config["STORAGE_CONNECTION_STRING"] ?? "";
    }

    [Function("GetHistory")]
    public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequestData req)
    {
        var tableClient = new TableClient(_connString, "HistoricoConversas");
        await tableClient.CreateIfNotExistsAsync();

        // Query para pegar todas as mensagens
        var entities = tableClient.QueryAsync<ChatLogEntity>();

        // Usamos um Dictionary para agrupar por SessionId (PartitionKey) de forma eficiente
        var historySummary = new Dictionary<string, string>();

        await foreach (var entity in entities)
        {
            // Se a sessão ainda não está no dicionário ou se encontramos um título válido
            if (!historySummary.ContainsKey(entity.PartitionKey) || !string.IsNullOrEmpty(entity.Title))
            {
                // Prioriza o título que não seja "Nova Conversa" ou vazio
                if (!string.IsNullOrEmpty(entity.Title))
                    historySummary[entity.PartitionKey] = entity.Title;
                else if (!historySummary.ContainsKey(entity.PartitionKey))
                    historySummary[entity.PartitionKey] = "Nova Conversa";
            }
        }

        var responseList = historySummary.Select(x => new { id = x.Key, title = x.Value }).Reverse();

        var res = req.CreateResponse(System.Net.HttpStatusCode.OK);
        await res.WriteAsJsonAsync(responseList);
        return res;
    }
}
