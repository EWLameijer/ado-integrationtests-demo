using Microsoft.Data.SqlClient;

namespace IntegrationTestsAdo.Business;

internal static class SqlUtils
{
    internal static T? FirstOrNull<T>(string connectionString, string query, Func<SqlDataReader, T?> read)
    {
        using SqlConnection connection = new(connectionString);
        SqlDataReader reader = GetReader(query, connection);
        return read(reader);
    }

    internal static IEnumerable<T> Get<T>(string connectionString, string query, Func<SqlDataReader, T> read)
    {
        using SqlConnection connection = new(connectionString);
        SqlDataReader reader = GetReader(query, connection);
        while (reader.Read()) yield return read(reader);
    }

    private static SqlDataReader GetReader(string query, SqlConnection connection)
    {
        SqlCommand command = new(query, connection);
        connection.Open();
        return command.ExecuteReader();
    }

    internal static bool Exists(string connectionString, string query)
    {
        using SqlConnection connection = new(connectionString);
        SqlDataReader reader = GetReader(query, connection);
        return reader.Read();
    }

    internal static void Add(string connectionString, string addCommand, List<(string, object)> parameters)
    {
        using SqlConnection connection = new(connectionString);
        using SqlCommand command = new(addCommand, connection);
        parameters.ForEach(param => command.Parameters.AddWithValue(param.Item1, param.Item2));
        connection.Open();
        command.ExecuteNonQuery();
    }
}