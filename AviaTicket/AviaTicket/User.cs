using System;
using System.Collections.Generic;
using System.Text;

namespace AirTickets
{
  public class User
  {
    public Guid IdUser { get; set; } = Guid.NewGuid();
    public Guid IdFlight { get; set; } 
    public string Name { get; set; }
    public string SurName { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
  }
}
