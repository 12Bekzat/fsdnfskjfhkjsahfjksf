using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.IO;
using System.Linq;

namespace AirTickets
{
  class Program
  {
    static void Main(string[] args)
    {
      var builder = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", false, true);

      IConfigurationRoot configurationRoot = builder.Build();

      string providerName = configurationRoot
            .GetSection("AppConfig")
            .GetChildren().Single(item => item.Key == "ProviderName")
            .Value;

      DbProviderFactories.RegisterFactory(providerName, SqlClientFactory.Instance);

      string connectionString = configurationRoot.GetConnectionString("DebugConnectionString");

      List<Ticket> tickets = new List<Ticket>
      {
        new Ticket
        {
          // значения к билетам


                  
        }
      };

      List<Flight> flights = new List<Flight> {
        new Flight
        {

        }
      };


      Registration repository = new Registration(
        flights,
        tickets,
        connectionString,
        providerName
      );

      // покупка билета


      //repository.Add(user);
      Console.ReadLine();
    }
  }
}
