using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using SedesFunction.Services;

namespace SedesFunction.Functions;

public class GetSede
{
    private readonly SedeService _sedeService;

    public GetSede(SedeService sedeService)
    {
        _sedeService = sedeService;
    }

    [Function("GetSede")]
    public async Task<HttpResponseData> Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "sede/{id:guid}")] HttpRequestData req,
        Guid id
    )
    {
        var sede = await _sedeService.GetSedeByIdAsync(id);
        if (sede is null)
        {
            return req.CreateResponse(HttpStatusCode.NotFound);
        }

        var response = req.CreateResponse(HttpStatusCode.OK);
        await response.WriteAsJsonAsync(sede);
        return response;
    }
}