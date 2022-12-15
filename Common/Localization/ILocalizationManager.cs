using Common.Dependency;


namespace Common.Localization
{
    public interface ILocalizationManager : ITransientDependency
    {
        ILocalizationProvider LocalizationProvider { get; }
     
    }
}