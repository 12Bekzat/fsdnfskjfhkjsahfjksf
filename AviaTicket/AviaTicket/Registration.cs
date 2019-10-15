using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace AirTickets
{
  public class Registration
  {
    private UserRepository userRepository;
    private FlightRepository flightRepository;
    private TicketRepository ticketRepository;

    private readonly string connectionString;
    private readonly string providerName;
    public Registration(List<Flight> flights, List<Ticket> tickets, string providerName, string connectionString)
    {
      this.connectionString = connectionString;
      this.providerName = providerName;

      foreach (var flight in flights)
      {
        flightRepository = new FlightRepository(connectionString, providerName);
        flightRepository.Add(flight);
      }

      foreach (var ticket in tickets)
      {
        ticketRepository = new TicketRepository(connectionString, providerName);
        ticketRepository.Add(ticket);
      }
    }
    public void Register(User user)
    {
      userRepository = new UserRepository(providerName, connectionString);

      userRepository.Add(user);
    }
  }
}
