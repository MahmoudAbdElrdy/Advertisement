using AuthDomain.Enums;
using Common;
using Common.Infrastructures;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuthDomain.Entities.Ads {

  public class Installment : BaseEntityAuditWithDelete<string> {
 
     public DateTime DueDate { get; set; }
    public bool IsPaid { get; set; }
    public string AdIntervalId { get; set; }
    public decimal Amount { get; set; }
    public PaymentType PaymentType { get; set; }
    public AdInterval AdInterval { get; set; }
   }

}
