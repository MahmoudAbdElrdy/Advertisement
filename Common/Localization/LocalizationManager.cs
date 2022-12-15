
namespace Common.Localization
{
    public class LocalizationManager : ILocalizationManager
    {
        public LocalizationManager(ILocalizationProvider localizationProvider)
        {
            LocalizationProvider = localizationProvider;
          
        }

        public ILocalizationProvider LocalizationProvider { get; }
 
    }
}