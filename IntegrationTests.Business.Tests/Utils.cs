using Microsoft.Data.SqlClient;

namespace IntegrationTestsAdo.Business.Tests;

public class Utils
{
    private readonly string _testDatabaseConnectionString;

    private const string RegenerateCommand = @"
            DROP TABLE Phones;
            DROP TABLE Brands;

            CREATE TABLE Brands (
                Id int PRIMARY KEY IDENTITY,
                Name varchar(20)
            );

            CREATE TABLE Phones (
                Id int PRIMARY KEY IDENTITY,
                BrandId int FOREIGN KEY REFERENCES Brands(Id),
                Type varchar(100)
            );

            INSERT INTO Brands (Name) VALUES ('Apple');

            INSERT INTO Phones (BrandId, Type) VALUES (1, 'iPhone 13');
        ";

    public Utils(string testDatabaseConnectionString)
    {
        _testDatabaseConnectionString = testDatabaseConnectionString;
    }

    public void RegenerateTable()
    {
        using SqlConnection connection = new(_testDatabaseConnectionString);
        using SqlCommand command = new(RegenerateCommand, connection);
        connection.Open();
        command.ExecuteNonQuery();
        connection.Close();
    }
}