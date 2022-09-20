using IntegrationTests.Business;

namespace IntegrationTestsAdo.Business.Tests.BrandServiceTests;

public class Get : Base
{
    [Fact]
    public void Should_get_all_brands()
    {
        // arrange
        Utils.RegenerateTable();

        // act
        List<Brand> brands = BrandService.Get().ToList();

        // assert
        Assert.Single(brands);
        Assert.Equal("Apple", brands[0].Name);
    }
}