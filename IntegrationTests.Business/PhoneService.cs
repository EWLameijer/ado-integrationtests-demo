using IntegrationTests.Business;
using Microsoft.Data.SqlClient;

namespace IntegrationTestsAdo.Business;

public class PhoneService
{
    private readonly string _connectionString;
    private readonly BrandService _brandService;

    public PhoneService(string connectionString)
    {
        _connectionString = connectionString;
        _brandService = new BrandService(connectionString);
    }

    public Phone? Get(int id)
    {
        string query = "SELECT * FROM Phones INNER JOIN Brands " +
            $"ON Phones.Id = {id} AND Brands.Id = Phones.BrandId";

        static Phone? readPhone(SqlDataReader reader) =>
            reader.Read() ? new Phone()
            {
                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                Brand = new Brand { Name = reader.GetString(reader.GetOrdinal("Name")) },
                Type = reader.GetString(reader.GetOrdinal("Type")),
            } : null;

        return SqlUtils.FirstOrNull(_connectionString, query, readPhone);
    }

    public bool Exists(string name, string type)
    {
        string query = @$"SELECT 1 FROM Phones INNER JOIN Brands ON Brands.Id = Phones.BrandId
            WHERE Type='{type}' AND Name = '{name}';";

        return SqlUtils.Exists(_connectionString, query);
    }

    public void Add(Phone phone)
    {
        string? brandName = phone.Brand?.Name;

        if (string.IsNullOrEmpty(brandName) || string.IsNullOrEmpty(phone.Type))
            throw new ArgumentException("PhoneService.Add(): Name or Type is wrong!");

        string phoneType = phone.Type;

        if (Exists(brandName, phoneType))
            throw new ArgumentException("PhoneService.Add(): Cannot add duplicate phone!");

        AddCorrectPhone(brandName, phoneType);
    }

    private void AddCorrectPhone(string? brandName, string phoneType)
    {
        int? existingBrandId = _brandService.GetIdByName(brandName!);

        List<(string, object)> parameters = new() { ("@Type", phoneType), ("@Name", brandName) };

        string addCommand = existingBrandId == null ? $@"
            BEGIN TRANSACTION
                INSERT INTO Brands (Name) VALUES (@Name);
                INSERT INTO Phones (BrandId, Type) VALUES
                    ((SELECT Id FROM Brands WHERE Name = @Name), @Type);
            COMMIT;"
            :
            @$"INSERT INTO Phones (BrandId, Type) VALUES ({existingBrandId}, @Type);";

        SqlUtils.Add(_connectionString, addCommand, parameters);
    }
}