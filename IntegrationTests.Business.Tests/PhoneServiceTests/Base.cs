namespace IntegrationTestsAdo.Business.Tests.PhoneServiceTests;

public class Base
{
    private const string TestDatabaseConnectionString = "Data Source=(localdb)\\ProjectModels;" +
        "Initial Catalog=AdoNetIntegrationTestTestDatabase;Integrated Security=True;" +
        "Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;" +
        "ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

    protected PhoneService PhoneService = new(TestDatabaseConnectionString);

    protected BrandService BrandService = new(TestDatabaseConnectionString);

    protected Utils Utils = new(TestDatabaseConnectionString);
}