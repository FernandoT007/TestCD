using System.Net;
using System.Text.Json;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using SedesFunction.Models;
using SedesFunction.Services;

namespace SedesFunction.Functions;

public class RegistrarSede
{
    private readonly SedeService _sedeService;

    public RegistrarSede(SedeService sedeService)
    {
        _sedeService = sedeService;
    }

    [Function("RegistrarSede")]
    public async Task<HttpResponseData> Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "sede")] HttpRequestData req
    )
    {
        var request = await new StreamReader(req.Body).ReadToEndAsync();
        var dataBody = JsonSerializer.Deserialize<SedeRequest>(request);

        if (dataBody is null)
        {
             return req.CreateResponse(HttpStatusCode.BadRequest);
        }


        var sede = await _sedeService.CreateSedeAsync(dataBody);
        var response = req.CreateResponse(HttpStatusCode.Created);
        await response.WriteAsJsonAsync(sede);
        return response;
    }
}