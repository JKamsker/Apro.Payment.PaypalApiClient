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

        coll.AddHttpClient<PaypalHttpClient>(x =>
        {
            x.BaseAddress = new Uri("https://api-m.sandbox.paypal.com");
            // x.BaseAddress = new Uri("https://api-m.paypal.com");
        })
        .AddPolicyHandler(msg =>
        {
            var delay = Backoff.DecorrelatedJitterBackoffV2(medianFirstRetryDelay: TimeSpan.FromSeconds(1), retryCount: 5);

            return Policy<HttpResponseMessage>
                .Handle<HttpRequestException>()
                .OrResult(x => x.StatusCode == HttpStatusCode.TooManyRequests)
                .OrResult(x => (int)x.StatusCode >= (int)HttpStatusCode.InternalServerError && (int)x.StatusCode <= 599)
                .WaitAndRetryAsync(delay);
        })
        //.AddPolicyHandler(msg =>
        //{
        //    var delay = Backoff.DecorrelatedJitterBackoffV2(medianFirstRetryDelay: TimeSpan.FromSeconds(1), retryCount: 5);

        //    return Policy<HttpResponseMessage>
        //        .Handle<HttpRequestException>()
        //        .OrResult(x => x.StatusCode == HttpStatusCode.UnprocessableEntity)
        //        //.FallbackAsync(async token =>
        //        //{

        //        //})
                
        //        ;
        //})
        ;

        coll.AddTransient<PaypalApiClientFactory>();

        coll.AddSingleton<IDateTimeProvider, DateTimeProvider>();
        coll.AddSingleton<ICredentialStorage, InMemoryCredentialStorage>();
        coll.AddSingleton<PaypalAccessTokenManager>();
        coll.AddSingleton(new ApplicationContext
        {
            ReturnUrl = new Uri("https://google.at/success"),
            CancelUrl = new Uri("https://google.at/cancel")
        });
    })
    .Build();


var fac = host.Services.GetRequiredService<PaypalApiClientFactory>();
var credentials = host.Services.GetRequiredService<IOptions<PaypalCredentials>>();


var cli = fac.Create(credentials.Value);


var order = await cli.CreateOrderAsync(new PurchaseUnit("CustomId", Currency.Euro(1.2m)));

Console.WriteLine(order.Links.Approve.Href);
Console.ReadLine();
var order1 = await cli.GetOrderAsync(order.Id);
//if (order1.Status != PaypalOrderStatus.Approved)
//{
//    Console.WriteLine("Not Approved!");
//    return;
//}

var order2 = await cli.CaptureOrderAsync(order1.Id);
if (order2.Status != PaypalOrderStatus.Completed)
{
    Console.WriteLine("Not Completed!");
    return;
}

foreach (var purchaseUnit in order2.PurchaseUnits)
{
    //purchaseUnit.ReferenceId
}



Console.WriteLine("Hello, World!");
