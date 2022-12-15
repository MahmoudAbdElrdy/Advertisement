using AuthDomain.Enums;
using Common;
using Common.Infrastructures;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuthDomain.Entities.Ads {

  public class AdClient : BaseEntityAuditWithDelete<string> {
     public string AdId { get; set; } 
     public decimal Price { get; set; }
    public string ClientId { get; set; }
    public Ad Ad { get; set; } 
    public Client Client { get; set; }
    public HashSet<Installment> Installments { get; set; } = new HashSet<Installment>();
    public HashSet<OrderComplaint> OrderComplaints { get; set; } = new HashSet<OrderComplaint>();
    }

}
