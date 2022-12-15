using Common.Dependency;

namespace Common.Localization
{
    public interface ILocalizationProvider : ITransientDependency
    {
        string Localize(string str, string cultureName);
        string Localize(string str);

        string Localize(string str, string cultureName, params object[] args);
    }
}