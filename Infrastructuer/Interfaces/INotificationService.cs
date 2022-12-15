using System.Threading.Tasks;
using AuthDomain.Entities.Notification;
using Common;

namespace Infrastructure {
  public interface INotificationService
    {
    Task Save(Notification notification,string token);    
  }
}