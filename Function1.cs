using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Threading.Tasks;

namespace FunctionApp1
{
    public static class Function1
    {
        [FunctionName(nameof(Function1))]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ILogger log,
            [EventHub("todos", Connection = "EVENTHUB_CONNECTIONSTRING")] IAsyncCollector<string> events)
        {
            log.LogInformation($"{nameof(Function1)} function processed a request.");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

            await events.AddAsync(requestBody);

            return new OkObjectResult(requestBody);
        }
    }
}
