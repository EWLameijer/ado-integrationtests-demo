using IntegrationTests.Business;
using Microsoft.Data.SqlClient;

namespace IntegrationTestsAdo.Business;

public class BrandService
{
    private readonly string _connectionString;

    public BrandService(string connectionString)
    {
        _connectionString = connectionString;
    }

    public int? GetIdByName(string name)
    {
        string query = $"SELECT * FROM Brands WHERE Name = '{name}'";

        static Brand? readBrand(SqlDataReader reader) =>
            reader.Read() ? new Brand()
            {
                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                Name = reader.GetString(reader.GetOrdinal("Name"))
            } : null;

        Brand? brand = SqlUtils.FirstOrNull(_connectionString, query, readBrand);
        return brand?.Id;
    }

    public IEnumerable<Brand> Get()
    {
        string query = "SELECT * FROM Brands";
        static Brand readBrand(SqlDataReader reader) =>
            new()
            {
                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                Name = reader.GetString(reader.GetOrdinal("Name"))
            };
        return SqlUtils.Get(_connectionString, query, readBrand);
    }

    public void Add(string name)
    {
        if (GetIdByName(name) != null)
            throw new ArgumentException($"BrandService.Add(): brand '{name}' already exists.");

        string addCommand = "INSERT INTO Brands (Name) VALUES (@Name)";

        List<(string, object)> parameters = new() { ("@Name", name) };

        SqlUtils.Add(_connectionString, addCommand, parameters);
    }
}