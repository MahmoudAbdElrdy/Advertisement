using Common.Interfaces;
using System;

namespace Common {
  public class BaseEntity {
  }
  public class BaseEntity<T> : BaseEntity {
    public T Id { get; set; }
  }

  public class BaseAudit<T> : BaseEntity<T> {
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    public string CreatedBy { get; set; }
  }

  public class BaseEntityAudit<T> : BaseEntity<T>, IAudit {
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    public string CreatedBy { get; set; }
    public DateTime? UpdatedDate { get; set; }
    public string UpdatedBy { get; set; }
  }
  public class BaseEntityAuditWithDelete<T> : BaseEntityAudit<T>, IDeleteEntity {
    public bool IsDeleted { get ; set ; }
    public string DeletedBy { get; set; }
    public DateTime? DeletedDate { get ; set ; }
  }

}