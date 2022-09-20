namespace IntegrationTestsAdo.Business.Tests.BrandServiceTests;

public class Base
{
    private const string TestDatabaseConnectionString = "Data Source=(localdb)\\ProjectModels;" +
        "Initial Catalog=AdoNetIntegrationTestTestDatabase;Integrated Security=True;" +
        "Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;" +
        "ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

    protected BrandService BrandService = new BrandService(TestDatabaseConnectionString);

    protected Utils Utils = new(TestDatabaseConnectionString);
}