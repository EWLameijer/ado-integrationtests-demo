using IntegrationTests.Business;

namespace IntegrationTestsAdo.Business.Tests.PhoneServiceTests;

public class Get : Base
{
    [Fact]
    public void Get_should_return_correct_phone()
    {
        // arrange
        Utils.RegenerateTable();

        // act
        Phone phone = PhoneService.Get(1)!;

        // assert
        Assert.Equal("Apple", phone.Brand.Name);
        Assert.Equal("iPhone 13", phone.Type);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(0)]
    [InlineData(2)]
    public void Get_should_return_null_for_invalid_id(int id)
    {
        // arrange
        Utils.RegenerateTable();

        // act
        Phone? phone = PhoneService.Get(id);

        // assert
        Assert.Null(phone);
    }
}