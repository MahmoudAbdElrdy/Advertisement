using AuthDomain.Entities.Ads;
using AuthDomain.Entities.Auth;
using Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuthDomain.Entities
{
    public class ContactUs : BaseEntityAuditWithDelete<string>
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string ResponesAdmin { get; set; }
        public string Notes { get; set; }
        public bool? IsContact { get; set; }
        public string ClientId { get; set; } 
        public Client Client { get; set; }
    }
}
