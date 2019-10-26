using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using OrderProcessor.Dto;

namespace OrderProcessor.Functions
{
    public static class OnPaymentReceived
    {
        [FunctionName("OnPaymentReceived")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("Payment received.");

            using var streamReader = new StreamReader(req.Body);
            var requestBody = await streamReader.ReadToEndAsync();
            var order = JsonConvert.DeserializeObject<Order>(requestBody);

            log.LogInformation($"Order {order.OrderId} received from {order.Email} for product {order.ProductId}.");

            return new OkObjectResult($"Your order for product {order.ProductId} is sent for processing. Thank you for your purchase.");
        }
    }
}
