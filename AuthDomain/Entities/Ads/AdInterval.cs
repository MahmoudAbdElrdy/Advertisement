using AuthDomain.Enums;
using Common;
using Common.Infrastructures;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuthDomain.Entities.Ads
{

    public class AdInterval : BaseEntityAuditWithDelete<string>
    { 
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public decimal Price { get; set; }
        public int NumberOfShowTimes { get; set; }
        //public decimal DurationTime { get; set; }
        public string AdId { get; set; }
        public string ClientId { get; set; }
        public Ad Ad { get; set; }
        public Client Client { get; set; }
        public HashSet<PaidService> PaidServices { get; set; } = new HashSet<PaidService>();
        public HashSet<OrderComplaint> OrderComplaints { get; set; } = new HashSet<OrderComplaint>();
        public HashSet<Installment> Installments { get; set; } = new HashSet<Installment>();

    }

}

