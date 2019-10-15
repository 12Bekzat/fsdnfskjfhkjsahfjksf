using System;
using System.Collections.Generic;
using System.Data.Common;

namespace AirTickets
{
  public class FlightRepository 
  {
    private readonly string connectionString;
    private readonly DbProviderFactory providerFactory;

    public FlightRepository(string connectionString, string providerName)
    {
      this.connectionString = connectionString;
      providerFactory = DbProviderFactories.GetFactory(providerName);
    }

    public void Add(Flight flight)
    {
      using (DbConnection connection = providerFactory.CreateConnection())
      using (DbCommand sqlCommand = connection.CreateCommand())
      {
        string query = $"insert into Flights values(@Id, " +
            $"@IdTicket, " +
            $"@FromFly, " +
            $"@ToFly," +
            $"@IsArrive" +
            $"@IsFlew" +
            $"@IsTardiness" +
            $"@DepartureDate" +
            $"@CountTicket);";
        sqlCommand.CommandText = query;

        DbParameter parameter = providerFactory.CreateParameter();
        parameter.DbType = System.Data.DbType.Guid;
        parameter.ParameterName = "@Id";
        parameter.Value = flight.Id;
        sqlCommand.Parameters.Add(parameter);

        parameter = providerFactory.CreateParameter();
        parameter.DbType = System.Data.DbType.Guid;
        parameter.ParameterName = "@IdTicket";
        parameter.Value = flight.IdTicket;
        sqlCommand.Parameters.Add(parameter);

        parameter = providerFactory.CreateParameter();
        parameter.DbType = System.Data.DbType.String;
        parameter.ParameterName = "@FromFly";
        parameter.Value = flight.FromFly;
        sqlCommand.Parameters.Add(parameter);

        parameter = providerFactory.CreateParameter();
        parameter.DbType = System.Data.DbType.String;
        parameter.ParameterName = "@ToFly";
        parameter.Value = flight.ToFly;
        sqlCommand.Parameters.Add(parameter);

        parameter = providerFactory.CreateParameter();
        parameter.DbType = System.Data.DbType.Boolean;
        parameter.ParameterName = "@IsArrive";
        parameter.Value = flight.IsArrive;
        sqlCommand.Parameters.Add(parameter);

        parameter = providerFactory.CreateParameter();
        parameter.DbType = System.Data.DbType.Boolean;
        parameter.ParameterName = "@IsFlew";
        parameter.Value = flight.IsFlew;
        sqlCommand.Parameters.Add(parameter);

        parameter = providerFactory.CreateParameter();
        parameter.DbType = System.Data.DbType.Boolean;
        parameter.ParameterName = "@IsTardiness";
        parameter.Value = flight.IsTardiness;
        sqlCommand.Parameters.Add(parameter);

        parameter = providerFactory.CreateParameter();
        parameter.DbType = System.Data.DbType.DateTime;
        parameter.ParameterName = "@DepartureDate";
        parameter.Value = flight.IsArrive;
        sqlCommand.Parameters.Add(parameter);

        parameter = providerFactory.CreateParameter();
        parameter.DbType = System.Data.DbType.Int32;
        parameter.ParameterName = "@CountTicket";
        parameter.Value = flight.CountTicket;
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

    public ICollection<Flight> GetAll()
    {
      using (DbConnection connection = providerFactory.CreateConnection())
      using (DbCommand sqlCommand = connection.CreateCommand())
      {
        string query = $"select * from Flights";
        sqlCommand.CommandText = query;

        connection.ConnectionString = connectionString;
        connection.Open();
        DbDataReader sqlDataReader = sqlCommand.ExecuteReader();

        List<Flight> categories = new List<Flight>();
        while (sqlDataReader.Read())
        {
          categories.Add(new Flight
          {
            Id = Guid.Parse(sqlDataReader["Id"].ToString()),
            IdTicket = Guid.Parse(sqlDataReader["IdTicket"].ToString()),
            FromFly = sqlDataReader["FromFly"].ToString(),
            ToFly = sqlDataReader["ToFly"].ToString(),
            IsArrive = Boolean.Parse(sqlDataReader["IsArrive"].ToString()),
            IsFlew = Boolean.Parse(sqlDataReader["IsFlew"].ToString()),
            IsTardiness = Boolean.Parse(sqlDataReader["IsTardiness"].ToString()),
            DepartureDate = DateTime.Parse(sqlDataReader["DepartureDate"].ToString()),
            CountTicket = int.Parse(sqlDataReader["CountTicket"].ToString())
          });
        }

        return categories;
      }
    }

    public void Update(Flight flight, Guid flightId)
    {
      using (DbConnection connection = providerFactory.CreateConnection())
      using (DbCommand sqlCommand = connection.CreateCommand())
      {
        string query = $"update Flights" +
          $"set Id = '{flight.Id}', " +
          $"IdTicket = '{flight.IdTicket}'," +
          $"FromFly = '{flight.FromFly}', " +
          $"ToFly = '{flight.ToFly}', " +
          $"IsArrive = '{flight.IsArrive}'" +
          $"IsFlew = '{flight.IsArrive}'" +
          $"IsTardiness = '{flight.IsTardiness}'" +
          $"DepartureDate = '{flight.DepartureDate}'" +
          $"CountTicket = '{flight.CountTicket}'" +
          $"where Id = '{flightId}';";
        sqlCommand.CommandText = query;

        connection.ConnectionString = connectionString;
        connection.Open();
        sqlCommand.ExecuteNonQuery();
      }
    }
  }
}
