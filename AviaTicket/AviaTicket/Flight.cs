using System;
using System.Collections.Generic;
using System.Text;

namespace AirTickets
{
  public class Flight
  {
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid IdTicket { get; set; } 
    public string FromFly { get; set; }
    public string ToFly { get; set; }
    public bool IsArrive { get; set; }
    public bool IsFlew { get; set; }
    public bool IsTardiness { get; set; }
    public DateTime DepartureDate { get; set; }
    public int CountTicket { get; set; }
  }
}
