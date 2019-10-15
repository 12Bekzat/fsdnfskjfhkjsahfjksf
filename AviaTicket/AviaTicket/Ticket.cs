using System;
using System.Collections.Generic;
using System.Text;

namespace AirTickets
{
  public class Ticket
  {
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTime ReceiptTicketDate { get; set; } = DateTime.Now;
    public double Cost { get; set; }
    public bool IsFlight { get; set; }
  }
}
