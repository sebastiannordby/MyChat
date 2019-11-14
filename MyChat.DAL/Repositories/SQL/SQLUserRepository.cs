using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using MyChat.Domain.Common;
using MyChat.Domain.Models;
using MyChat.Domain.Repositories;
using MyChat.Domain.Common.UnitOfWork;
using MyChat.Infrastructure.UnitOfWork;

namespace MyChat.Infrastructure.Repositories.SQL
{
    public class SQLUserRepository : SQLRepository<User>, IUserRepository
    {
        public SQLUserRepository(SQLUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }

        public override void Execute(IOperation operation)
        {
            if(operation.OperationType == OperationType.Add)
            {
                AddUser((User) operation.Entity);
            }
            else if(operation.OperationType == OperationType.Update)
            {
                UpdateUser((User) operation.Entity);
            }
            else if(operation.OperationType == OperationType.Delete)
            {
                DeleteUser((User) operation.Entity);
            }
        }

        public override User Get(Guid id)
        {
            using(var sqlConnection = _unitOfWork.Connection())
            using (var sqlCommand = new SqlCommand(UserRepositoryCommands.GetUserById(), sqlConnection))
            {
                sqlConnection.Open();
                sqlCommand.Parameters.Add("Id", SqlDbType.UniqueIdentifier);
                sqlCommand.Parameters["Id"].Value = id;

                using(var reader = sqlCommand.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return AssembleUserFromReader(reader);
                    }
                }
            }

            return null;
        }

        public IEnumerable<User> GetByKeyword(string keyword)
        {
            var users = new List<User>();

            using (var sqlConnection = _unitOfWork.Connection())
            using (var sqlCommand = new SqlCommand(UserRepositoryCommands.GetUsersByKeyword(), sqlConnection))
            {
                sqlConnection.Open();
                sqlCommand.Parameters.Add("Keyword", SqlDbType.VarChar);
                sqlCommand.Parameters["Keyword"].Value = keyword;

                using (var reader = sqlCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        users.Add(AssembleUserFromReader(reader));   
                    }
                }
            }

            return users;
        }


        public bool IsUserNameAvailable(string username)
        {
            using (var sqlConnection = _unitOfWork.Connection())
            using (var sqlCommand = new SqlCommand(UserRepositoryCommands.IsUserNameAvailable(), sqlConnection))
            {
                sqlConnection.Open();
                sqlCommand.Parameters.AddWithValue("@UserName", username);

                using (var reader = sqlCommand.ExecuteReader())
                {
                    return !reader.HasRows;
                }
            }
        }

        public bool IsValidLogin(string username, string password)
        {
            using (var sqlConnection = _unitOfWork.Connection())
            using (var sqlCommand = new SqlCommand(UserRepositoryCommands.IsValidLogin(), sqlConnection))
            {
                sqlConnection.Open();
                sqlCommand.Parameters.AddWithValue("@UserName", username);
                sqlCommand.Parameters.AddWithValue("@Password", password);

                using (var reader = sqlCommand.ExecuteReader())
                {
                    return reader.HasRows;
                }
            }
        }

        private void AddUser(User entity)
        {
            using (var sqlCommand = _unitOfWork.Command(UserRepositoryCommands.AddUser()))
            {
                sqlCommand.Parameters.AddWithValue("@Id", entity.Id);
                sqlCommand.Parameters.AddWithValue("@UserName", entity.UserName);
                sqlCommand.Parameters.AddWithValue("@Password", entity.Password);
                sqlCommand.Parameters.AddWithValue("@EmailAddress", entity.EmailAddress);
                sqlCommand.Parameters.AddWithValue("@FirstName", entity.FirstName);
                sqlCommand.Parameters.AddWithValue("@LastName", entity.LastName);
                int result = sqlCommand.ExecuteNonQuery();

                if (result < 0)
                {
                    throw new Exception("AddUser: Failed to persist User to DataBase.");
                }
            }
        }

        private void UpdateUser(User entity)
        {
            using (var sqlCommand = _unitOfWork.Command(UserRepositoryCommands.UpdateUser()))
            {
                sqlCommand.Parameters.AddWithValue("@UserName", entity.UserName);
                sqlCommand.Parameters.AddWithValue("@Password", entity.Password);
                sqlCommand.Parameters.AddWithValue("@EmailAddress", entity.EmailAddress);
                sqlCommand.Parameters.AddWithValue("@FirstName", entity.FirstName);
                sqlCommand.Parameters.AddWithValue("@LastName", entity.LastName);
                int result = sqlCommand.ExecuteNonQuery();

                if (result < 0)
                {
                    throw new Exception("UpdateUser: Failed to persist User to DataBase.");
                }
            }
        }

        private void DeleteUser(User entity)
        {
            using (var sqlCommand = _unitOfWork.Command(UserRepositoryCommands.DeleteUser()))
            {
                sqlCommand.Parameters.AddWithValue("@Id", entity.Id);
                int result = sqlCommand.ExecuteNonQuery();

                if (result < 0)
                {
                    throw new Exception("Failed to delete User.");
                }
            }
        }

        private User AssembleUserFromReader(SqlDataReader reader)
        {
            return new User(
                id: (Guid)reader["Id"],
                userName: reader["UserName"].ToString(),
                password: reader["password"].ToString(),
                emailAddress: reader["EmailAddress"].ToString(),
                firstName: reader["FirstName"].ToString(),
                lastName: reader["LastName"].ToString()
            );
        }

        public User GetUserByUserName(string username)
        {
            using (var sqlConnection = _unitOfWork.Connection())
            using (var sqlCommand = new SqlCommand(UserRepositoryCommands.GetUserByUserName(), sqlConnection))
            {
                sqlConnection.Open();
                sqlCommand.Parameters.AddWithValue("@UserName", username);

                using (var reader = sqlCommand.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return AssembleUserFromReader(reader);
                    }
                }
            }

            return null;
        }
    }

    internal static class UserRepositoryCommands
    {
        public static string GetUserById() 
        {
            return $"SELECT * FROM [Users] WHERE [Id] = @Id;";
        }
      
        public static string GetUsersByKeyword()
        {
            return $"" +
                $"SELECT * FROM [Users] WHERE " +
                $"UPPER([UserName])     LIKE '%' + @Keyword + '%' OR " +
                $"UPPER([FirstName])    LIKE '%' + @Keyword + '%' OR " +
                $"UPPER([LastName])     LIKE '%' + @Keyword + '%' OR " +
                $"UPPER([EmailAddress]) LIKE '%' + @Keyword + '%';"; 
        }

        public static string AddUser()
        {
            return $"INSERT INTO [Users]" +
                $"([Id], [UserName], [Password], [EmailAddress], [FirstName], [LastName])" +
                $"VALUES" +
                $"(@Id, @UserName, @Password, @EmailAddress, @FirstName, @LastName);";
        }

        public static string UpdateUser()
        {
            return $"UPDATE [UserName]" +
                   $"SET [UserName] = @UserName," +
                   $"SET [Password] = @Password," +
                   $"SET [EmailAddress] = @EmailAddress," +
                   $"SET [FirstName] = @user.FirstName," +
                   $"SET [LastName] = @user.LastName;";
        }

        public static string DeleteUser()
        {
            return $"DELETE [Users] WHERE [Id] = @Id;";
        }

        public static string IsUserNameAvailable()
        {
            return $"SELECT [UserName] FROM Users " +
                   $"WHERE UPPER([UserName]) = @UserName;";
        }

        public static string IsValidLogin()
        {
            return $"SELECT [UserName] FROM Users WHERE " +
                   $"[UserName] = @UserName AND [Password] = @Password;";
        }

        public static string GetUserByUserName()
        {
            return $"SELECT * FROM [Users] WHERE [UserName] = @UserName;";
        }
    }
}
