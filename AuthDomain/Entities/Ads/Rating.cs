using Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuthDomain.Entities.Ads
{
  public  class Rating : BaseEntityAuditWithDelete<string>
    {
        public string AdId { get; set; }
        public string ClientId { get; set; }
        public Ad Ad { get; set; }
        public Client Client { get; set; }
        public double? RatingValue { get; set; }
        public string RatingText { get; set; }
    }
}
