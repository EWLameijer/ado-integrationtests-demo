// See https://aka.ms/new-console-template for more information
using IntegrationTests.Business;
using IntegrationTestsAdo.Business;

Console.WriteLine("Hello!");
const string ConnectionString = "Data Source=(localdb)\\ProjectModels;" +
    "Initial Catalog=AdoNetIntegrationTest;Integrated Security=True;" +
    "Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;" +
    "ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

PhoneService phoneService = new PhoneService(ConnectionString);
Console.WriteLine(phoneService.Get(1));
try
{
    Phone newPhone = new() { Type = "Huawei", Brand = new() { Name = "Y6s" } };
    phoneService.Add(newPhone);
    Console.WriteLine(phoneService.Get(2));
}
catch
{
    Console.WriteLine("Something went wrong...");
}
Console.WriteLine("Goodbye!");