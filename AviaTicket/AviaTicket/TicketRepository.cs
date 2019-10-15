using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace AirTickets
{
  public class TicketRepository
  {
    private readonly string connectionString;
    private readonly DbProviderFactory providerFactory;

    public TicketRepository(string connectionString, string providerName)
    {
      this.connectionString = connectionString;
      providerFactory = DbProviderFactories.GetFactory(providerName);
    }

    public void Add(Ticket ticket)
    {
      using (DbConnection connection = providerFactory.CreateConnection())
      using (DbCommand sqlCommand = connection.CreateCommand())
      {
        string query = $"insert into Tickets values(@Id, " +
            $"@ReceiptTicketDate" +
            $"@Cost, " +
            $"@IsFlight);";
        sqlCommand.CommandText = query;

        DbParameter parameter = providerFactory.CreateParameter();
        parameter.DbType = System.Data.DbType.Guid;
        parameter.ParameterName = "@Id";
        parameter.Value = ticket.Id;
        sqlCommand.Parameters.Add(parameter);

        parameter = providerFactory.CreateParameter();
        parameter.DbType = System.Data.DbType.DateTime;
        parameter.ParameterName = "@ReceiptTicketDate";
        parameter.Value = ticket.ReceiptTicketDate;
        sqlCommand.Parameters.Add(parameter);

        parameter = providerFactory.CreateParameter();
        parameter.DbType = System.Data.DbType.Double;
        parameter.ParameterName = "@Cost";
        parameter.Value = ticket.Cost;
        sqlCommand.Parameters.Add(parameter);

        parameter = providerFactory.CreateParameter();
        parameter.DbType = System.Data.DbType.Boolean;
        parameter.ParameterName = "@IsFlight";
        parameter.Value = ticket.IsFlight;
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
    public ICollection<Ticket> GetAll()
    {
      using (DbConnection connection = providerFactory.CreateConnection())
      using (DbCommand sqlCommand = connection.CreateCommand())
      {
        string query = $"select * from Tickets";
        sqlCommand.CommandText = query;

        connection.ConnectionString = connectionString;
        connection.Open();
        DbDataReader sqlDataReader = sqlCommand.ExecuteReader();

        List<Ticket> tickets = new List<Ticket>();
        while (sqlDataReader.Read())
        {
          tickets.Add(new Ticket
          {
            Id = Guid.Parse(sqlDataReader["Id"].ToString()),
            ReceiptTicketDate = DateTime.Parse(sqlDataReader["ReceiptTicketDate"].ToString()),
            Cost = Double.Parse(sqlDataReader["Cost"].ToString()),
            IsFlight = Boolean.Parse(sqlDataReader["IsFlight"].ToString())
          });
        }

        return tickets;
      }
    }
    public void Update(Ticket ticket, Guid userId)
    {
      using (DbConnection connection = providerFactory.CreateConnection())
      using (DbCommand sqlCommand = connection.CreateCommand())
      {
        string query = $"update Tickets" +
          $"set Id = @Id," +
          $"ReceiptTicketDate = @ReceiptTicketDate," +
          $"Cost = #cost," +
          $"IsFlight = @IsFlight";

        sqlCommand.CommandText = query;

        DbParameter parameter = providerFactory.CreateParameter();
        parameter.DbType = System.Data.DbType.Guid;
        parameter.ParameterName = "@Id";
        parameter.Value = ticket.IdUser;
        sqlCommand.Parameters.Add(parameter);

        parameter = providerFactory.CreateParameter();
        parameter.DbType = System.Data.DbType.DateTime;
        parameter.ParameterName = "@ReceiptTicketDate";
        parameter.Value = ticket.ReceiptTicketDate;
        sqlCommand.Parameters.Add(parameter);

        parameter = providerFactory.CreateParameter();
        parameter.DbType = System.Data.DbType.Double;
        parameter.ParameterName = "@Cost";
        parameter.Value = ticket.Cost;
        sqlCommand.Parameters.Add(parameter);

        parameter = providerFactory.CreateParameter();
        parameter.DbType = System.Data.DbType.Boolean;
        parameter.ParameterName = "@IsFlight";
        parameter.Value = ticket.IsFlight;
        sqlCommand.Parameters.Add(parameter);

        connection.ConnectionString = connectionString;
        connection.Open();
        sqlCommand.ExecuteNonQuery();
      }
    }
  }
}
