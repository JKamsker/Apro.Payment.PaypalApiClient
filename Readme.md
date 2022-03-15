Sample usage:

   ````csharp
using Microsoft.Extensions.DependencyInjection;
using PaypalPaymentProvider.Models.Order;
using PaypalPaymentProvider.Models;
using PaypalPaymentProvider.Services;

var creds = new PaypalCredentials()
{
    UserName = "USERNAME",
    Secret = "SECRET",
};

var sp = BuildServiceProvider();

var fac = sp.GetRequiredService<PaypalApiClientFactory>();
var cli = fac.Create(creds);

var order = await cli.CreateOrderAsync(new PurchaseUnit("CustomId", Currency.Euro(10)));

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



static IServiceProvider BuildServiceProvider()
{
    var coll = new ServiceCollection();
    coll.AddHttpClient<PaypalHttpClient>(x =>
    {
        x.BaseAddress = new Uri("https://api-m.sandbox.paypal.com");
    });

    coll.AddTransient<PaypalApiClientFactory>();

    coll.AddSingleton<IDateTimeProvider, DateTimeProvider>();
    coll.AddSingleton<ICredentialStorage, InMemoryCredentialStorage>();
    coll.AddSingleton<PaypalAccessTokenManager>();
    coll.AddSingleton(new ApplicationContext
    {
        ReturnUrl = new Uri("https://google.at/success"),
        CancelUrl = new Uri("https://google.at/cancel")
    });


    coll.AddTransient<PaypalApiClient>();
    coll.AddTransient<UserScopedPaypalAccessTokenManager>();

    var sp = coll.BuildServiceProvider();
    return sp;
}


````