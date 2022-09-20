namespace IntegrationTestsAdo.Business.Tests.BrandServiceTests;

public class GetIdByName : Base
{
    [Fact]
    public void GetIdByName_should_return_correct_id_for_name()
    {
        // arrange
        Utils.RegenerateTable();

        // act
        int id = BrandService.GetIdByName("Apple")!.Value;

        // assert
        Assert.Equal(1, id);
    }

    [Fact]
    public void GetIdByName_should_return_null_if_name_does_not_exist()
    {
        // arrange
        Utils.RegenerateTable();

        // act
        int? id = BrandService.GetIdByName("Pear");

        // assert
        Assert.Null(id);
    }
}