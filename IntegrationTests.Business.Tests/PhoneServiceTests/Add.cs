using IntegrationTests.Business;

namespace IntegrationTestsAdo.Business.Tests.PhoneServiceTests;

public class Add : Base
{
    [Fact]
    public void Adding_phone_with_existing_brand_should_not_duplicate_brand()
    {
        // arrange
        Utils.RegenerateTable();
        string newType = "iPhone 14";
        Phone newPhone = new() { Type = newType, Brand = new Brand { Name = "Apple" } };

        // act
        PhoneService.Add(newPhone);

        // assert
        Assert.Single(BrandService.Get());
        Assert.Equal(newType, PhoneService.Get(2)!.Type);
    }

    [Fact]
    public void Adding_phone_with_new_brand_should_create_extra_brand()
    {
        // arrange
        Utils.RegenerateTable();
        string newType = "P50";
        string newBrand = "Huawei";
        Phone newPhone = new() { Type = newType, Brand = new Brand { Name = newBrand } };

        // act
        PhoneService.Add(newPhone);

        // assert
        Assert.Equal(2, BrandService.Get().Count());
        Assert.Equal(2, BrandService.GetIdByName(newBrand));
        Phone retrievedPhone = PhoneService.Get(2)!;
        Assert.Equal(newType, retrievedPhone.Type);
        Assert.Equal(newBrand, retrievedPhone.Brand.Name);
    }

    [Fact]
    public void Adding_duplicate_phone_should_fail()
    {
        // arrange
        Utils.RegenerateTable();
        Phone newPhone = new() { Type = "iPhone 13", Brand = new Brand { Name = "Apple" } };

        // act & assert
        Assert.Throws<ArgumentException>(() => PhoneService.Add(newPhone));
    }
}