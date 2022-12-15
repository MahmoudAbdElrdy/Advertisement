using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using AuthDomain.Enums.Notifications;
using Common.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace AuthDomain.Entities.Notification
{
     [Table("Notifications")]
    public class Notification : IAudit
    {
        public Notification()
        {
            Users = new HashSet<UserNotification>();
            CreatedDate = DateTime.UtcNow;
        }

        public int Id { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public bool Read { get; set; }
        public string SubjectAr { get; set; }
        public string SubjectEn { get; set; }
        public string BodyAr { get; set; }
        public string BodyEn { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public NotificationType Type { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public NotificationState State { get; set; }
        public ICollection<UserNotification> Users { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
    }
}