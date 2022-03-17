using Apro.Payment.PaypalApiClient.Models;
using Apro.Payment.PaypalApiClient.Models.Domain;
using Apro.Payment.PaypalApiClient.Services;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

using System.Net;
using Polly;
using Polly.Contrib.WaitAndRetry;
using Apro.Payment.PaypalApiClient.Configuration;
using System.Runtime.CompilerServices;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration((context, builder) =>
    {
        builder.SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{context.HostingEnvironment.EnvironmentName}.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables();
    })
    .ConfigureServices((ctx, coll) =>
    {
        coll.Configure<PaypalCredentials>(ctx.Configuration.GetSection("PaypalCredentials"));

        new PaypalApiClientConfigurator(coll)
            .UseDevelopment()
            .WithClientName("Apro-Smorder")
            .WithReturnUrls("https://www.youtube.com/watch?v=dQw4w9WgXcQ?success=true", "https://www.youtube.com/watch?v=dQw4w9WgXcQ?success=false")
            .Apply();
    })
    .Build();

var credentials = host.Services.GetRequiredService<IOptions<PaypalCredentials>>();


var fac = host.Services.GetRequiredService<PaypalApiClientFactory>();
var cli = fac.Create(credentials.Value);


var order = await cli.CreateOrderAsync(new PurchaseUnit("CustomId", Currency.Euro(1.2m)));

Console.WriteLine(order.Links.Approve.Href);
Console.ReadLine();
var order1 = await cli.GetOrderAsync(order.Id);
if (order1.Status != PaypalOrderStatus.Approved)
{
    Console.WriteLine("Not Approved!");
    return;
}

var order2 = await cli.CaptureOrderAsync(order1.Id);
if (order2.Status != PaypalOrderStatus.Completed)
{
    Console.WriteLine("Not Completed!");
    return;
}

await cli.RefundOrderAsync(order2.Id);




Console.WriteLine("Hello, World!");

PaypalApiClientFactory CreateFacWithoutHost()
{
    var facfac = PaypalApiClientConfigurator.New
    .UseDevelopment()
    .WithClientName("Apro-Smorder")
    .WithReturnUrls("https://www.youtube.com/watch?v=dQw4w9WgXcQ?success=true", "https://www.youtube.com/watch?v=dQw4w9WgXcQ?success=false")
    .Build();

    return facfac.Create();
}
