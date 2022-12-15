using System.Linq;

namespace Persistence {
  public partial class AppDbInitializer {
    public AppDbInitializer() {

    }
    public static void Initialize(AppDbContext context) {
      var initializer = new AppDbInitializer();
      initializer.SeedAuthEverything(context);
    }    
    
  }

}