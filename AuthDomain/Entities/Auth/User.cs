using System;
using System.Collections.Generic;
using AuthDomain.Entities.Notification;
using Common.Attributes;
using Common.Infrastructures;
using Common.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace AuthDomain.Entities.Auth
{
    //[Table("AspNetUsers")]
    public class User : IdentityUser, IAudit, IDeleteEntity
    {
        public string FirstName { get; set; }
        public string WebToken { get; set; }
        public string UserLang { get; set; }
        public string LastName { get; set; }
        public string FullName { get { return $"{FirstName} {LastName}"; } }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public bool IsDeleted { get; set; }
        public string DeletedBy { get; set; }
        public DateTime? DeletedDate { get; set; }
        public SystemModule AllowedModules { get; set; }

        public string Permissions { get; set; }

        public HashSet<UserRole> UserRoles { get; set; } = new HashSet<UserRole>();
        public ICollection<UserNotification> Notifications { get; set; } = new HashSet<UserNotification>();

        public UserAvatar Avatar { get; set; }
        public HashSet<UserCode> Codes { get; set; } = new HashSet<UserCode>();
        public HashSet<Transaction> Transactions { get; set; } = new HashSet<Transaction>();
        public HashSet<Service> Services { get; set; } = new HashSet<Service>();

    }

}