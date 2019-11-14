using MyChat.Infrastructure.Models;
using MyChat.Infrastructure.Repositories.Interfaces;
using System;
using System.Data.SqlClient;

namespace MyChat.Infrastructure.Repositories.SQL
{
    public class SQLApiKeyRepository : IApiKeyRepository
    {
        private readonly string _connectionString;

        public SQLApiKeyRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Add(ApiKey apiKey)
        {
            using(var connection = new SqlConnection(_connectionString))
            using(var command = new SqlCommand(ApiKeyRepositoryCommands.InsertApiKey(), connection))
            {
                connection.Open();
                command.Parameters.AddWithValue("@Id", apiKey.Id);
                command.Parameters.AddWithValue("@Key", apiKey.Key);
                command.Parameters.AddWithValue("@Owner", apiKey.Owner);
                command.Parameters.AddWithValue("@IssuedBy", apiKey.IssuedBy);
                command.Parameters.AddWithValue("@IssuedDateTime", apiKey.IssuedDateTime);
                command.Parameters.AddWithValue("@ValidFromDateTime", apiKey.IssuedDateTime);
                command.Parameters.AddWithValue("@ValidToDateTime", apiKey.IssuedDateTime);

                var result = command.ExecuteNonQuery();

                if(result < 0)
                {
                    throw new Exception("Failed to persist ApiKey to database.");
                }
            }
        }

        public ApiKey GetByKey(string apiKey)
        {
            using(var connection = new SqlConnection(_connectionString))
            using(var command = new SqlCommand(ApiKeyRepositoryCommands.GetByKey(), connection))
            {
                connection.Open();
                command.Parameters.AddWithValue("@Key", apiKey);

                using(var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new ApiKey(
                            id: Guid.Parse(reader["Id"].ToString()),
                            key: reader["Key"].ToString(),
                            owner: reader["Owner"].ToString(),
                            issuedBy: reader["IssuedBy"].ToString(),
                            issuedDateTime: DateTime.Parse(reader["IssuedDateTime"].ToString()),
                            validFromDateTime: DateTime.Parse(reader["ValidFromDateTime"].ToString()),
                            validToDateTime: DateTime.Parse(reader["ValidToDateTime"].ToString())
                        );
                    }
                }
            }

            return null;
        }
    }

    public static class ApiKeyRepositoryCommands
    {
        public static string InsertApiKey()
        {
            return $"INSERT INTO ApiKeys([Id], [Key], [Owner], [IssuedBy], [IssuedDateTime], [ValidFromDateTime], [ValidToDateTime])" +
                $"VALUES(@Id, @Key, @Owner, @IssuedBy, @IssuedDateTime, @ValidFromDateTime, @ValidToDateTime);";
        }

        public static string GetByKey()
        {
            return $"SELECT * FROM ApiKeys WHERE [Key] = @Key;";
        }
    }
}
