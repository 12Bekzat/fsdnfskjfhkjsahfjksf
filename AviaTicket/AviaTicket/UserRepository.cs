using EntityFramework.BulkInsert;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace AirTickets
{
  public class UserRepository
  {
    private readonly string connectionString;
    private readonly DbProviderFactory providerFactory;

    public UserRepository(string connectionString, string providerName)
    {
      this.connectionString = connectionString;
      providerFactory = DbProviderFactories.GetFactory(providerName);
    }

    public void Add(User user)
    {
      using (DbConnection connection = providerFactory.CreateConnection())
      using (DbCommand sqlCommand = connection.CreateCommand())
      {
        string query = $"insert into Users values(@Id, " +
            $"@IdFlight" +
            $"@Name, " +
            $"@SurName, " +
            $"@PhoneNumber," +
            $"@Email);";
        sqlCommand.CommandText = query;

        DbParameter parameter = providerFactory.CreateParameter();
        parameter.DbType = System.Data.DbType.Guid;
        parameter.ParameterName = "@Id";
        parameter.Value = user.IdUser;
        sqlCommand.Parameters.Add(parameter);

        parameter = providerFactory.CreateParameter();
        parameter.DbType = System.Data.DbType.Guid;
        parameter.ParameterName = "@IdFlight";
        parameter.Value = user.IdFlight;
        sqlCommand.Parameters.Add(parameter);

        parameter = providerFactory.CreateParameter();
        parameter.DbType = System.Data.DbType.String;
        parameter.ParameterName = "@Name";
        parameter.Value = user.Name;
        sqlCommand.Parameters.Add(parameter);

        parameter = providerFactory.CreateParameter();
        parameter.DbType = System.Data.DbType.String;
        parameter.ParameterName = "@SurName";
        parameter.Value = user.SurName;
        sqlCommand.Parameters.Add(parameter);

        parameter = providerFactory.CreateParameter();
        parameter.DbType = System.Data.DbType.String;
        parameter.ParameterName = "@PhoneNumber";
        parameter.Value = user.PhoneNumber;
        sqlCommand.Parameters.Add(parameter);

        parameter = providerFactory.CreateParameter();
        parameter.DbType = System.Data.DbType.String;
        parameter.ParameterName = "@Email";
        parameter.Value = user.Email;
        sqlCommand.Parameters.Add(parameter);

        connection.ConnectionString = connectionString;
        connection.Open();
        using (DbTransaction transaction = connection.BeginTransaction())
        {
          try
          {
            sqlCommand.Transaction = transaction;
            sqlCommand.ExecuteNonQuery();

            transaction.Commit();
          }
          catch
          {
            transaction.Rollback();
          }
        }
      }
    }
    public ICollection<User> GetAll()
    {
      using (DbConnection connection = providerFactory.CreateConnection())
      using (DbCommand sqlCommand = connection.CreateCommand())
      {
        string query = $"select * from Categories";
        sqlCommand.CommandText = query;

        connection.ConnectionString = connectionString;
        connection.Open();
        DbDataReader sqlDataReader = sqlCommand.ExecuteReader();

        List<User> users = new List<User>();
        while (sqlDataReader.Read())
        {
          users.Add(new User
          {
            IdUser = Guid.Parse(sqlDataReader["Id"].ToString()),
            IdFlight = Guid.Parse(sqlDataReader["IdFlight"].ToString()),
            Name = sqlDataReader["Name"].ToString(),
            SurName = sqlDataReader["SurName"].ToString(),
            PhoneNumber = sqlDataReader["PhoneNumber"].ToString(),
            Email = sqlDataReader["Email"].ToString(),
          });
        }

        return users;
      }
    }
    public void Update(User user, Guid userId)
    {
      using (DbConnection connection = providerFactory.CreateConnection())
      using (DbCommand sqlCommand = connection.CreateCommand())
      {
        string query = $"update User" +
          $"set Id = @Id," +
          $"IdFlight = @IdFlight" +
          $"Name = @Name," +
          $"SurName = @SurName," +
          $"PhoneNumber = @PhoneNumber" +
          $"Email = @Email" +
          $"where Id = @UserId;";

        sqlCommand.CommandText = query;

        DbParameter parameter = providerFactory.CreateParameter();
        parameter.DbType = System.Data.DbType.Guid;
        parameter.ParameterName = "@Id";
        parameter.Value = user.IdUser;
        sqlCommand.Parameters.Add(parameter);

        parameter = providerFactory.CreateParameter();
        parameter.DbType = System.Data.DbType.Guid;
        parameter.ParameterName = "@IdFlight";
        parameter.Value = user.IdUser;
        sqlCommand.Parameters.Add(parameter);

        parameter = providerFactory.CreateParameter();
        parameter.DbType = System.Data.DbType.Guid;
        parameter.ParameterName = "@UserId";
        parameter.Value = userId;
        sqlCommand.Parameters.Add(parameter);

        parameter = providerFactory.CreateParameter();
        parameter.DbType = System.Data.DbType.String;
        parameter.ParameterName = "@Name";
        parameter.Value = user.Name;
        sqlCommand.Parameters.Add(parameter);

        parameter = providerFactory.CreateParameter();
        parameter.DbType = System.Data.DbType.String;
        parameter.ParameterName = "@SurName";
        parameter.Value = user.SurName;
        sqlCommand.Parameters.Add(parameter);

        parameter = providerFactory.CreateParameter();
        parameter.DbType = System.Data.DbType.String;
        parameter.ParameterName = "@PhoneNumber";
        parameter.Value = user.PhoneNumber;
        sqlCommand.Parameters.Add(parameter);

        parameter = providerFactory.CreateParameter();
        parameter.DbType = System.Data.DbType.String;
        parameter.ParameterName = "@Email";
        parameter.Value = user.Email;
        sqlCommand.Parameters.Add(parameter);

        connection.ConnectionString = connectionString;
        connection.Open();
        sqlCommand.ExecuteNonQuery();
      }
    }
  }
}
