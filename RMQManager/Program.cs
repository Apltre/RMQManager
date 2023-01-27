using Refit;
using RMQDel;
using System.Text;
using System.Text.RegularExpressions;


var url = "http://localhost:9080";
var username = "guest";
var password = "guest";
var pattern = @"Test.*";


var authHeader = Convert.ToBase64String(Encoding.GetEncoding("ISO-8859-1").GetBytes(username + ":" + password));
var refitSettings = new RefitSettings() 
{
    AuthorizationHeaderValueGetter = () => Task.FromResult(authHeader)
};
var rmqApi = RestService.For<IRMQManagement>(url, refitSettings);
var queues = (await rmqApi.GetQueues()).Content!;
var exchanges = (await rmqApi.GetExchanges()).Content!;

var filteredQueues = queues.Where(q => Regex.IsMatch(q.Name, pattern)).Select(q => q.Name).ToList();
var filteredExchanges = exchanges.Where(e => Regex.IsMatch(e.Name, pattern)).Select(e => e.Name).ToList();

foreach (var queue in filteredQueues)
{
    await rmqApi.DeleteQueue(queue);
}

foreach (var exhange in filteredExchanges)
{
    try
    {
        await rmqApi.DeleteExchange(exhange);
    }
    catch (Exception ex) { }
}

Console.WriteLine("Topology deleted successfully.");
Console.ReadLine();