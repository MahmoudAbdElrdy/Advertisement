
using Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Persistence.MYSQL {
  public class MySqlAppDbContext : AppDbContext {
    public MySqlAppDbContext(DbContextOptions options, IAuditService auditService=null) : base(options, auditService) {
    }
  }
}
