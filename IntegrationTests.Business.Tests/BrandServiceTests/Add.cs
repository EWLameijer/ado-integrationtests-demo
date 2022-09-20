namespace IntegrationTestsAdo.Business.Tests.BrandServiceTests;

public class Add : Base
{
    [Fact]
    public void Can_add_brand()
    {
        // arrange
        Utils.RegenerateTable();

        // act
        BrandService.Add("Pear");

        // assert
        int id = BrandService.GetIdByName("Pear")!.Value;
        Assert.Equal(2, id);
    }

    [Fact]
    public void Cannot_add_existing_brand()
    {
        // arrange
        Utils.RegenerateTable();

        // act & assert
        Assert.Throws<ArgumentException>(() => BrandService.Add("Apple"));
    }
}