using Common.Interfaces;
using Common.Resources;
using System;
using System.Globalization;
using System.Resources;

namespace Common.Localization
{
    public class LocalizationProvider : ILocalizationProvider
    {
        private readonly IResourceSourceManager _resourceSourceManager;
        private readonly IAuditService _auditService;

        public LocalizationProvider(IResourceSourceManager resourceSourceManager, IAuditService auditService)
        {
            _resourceSourceManager = resourceSourceManager;
            _auditService = auditService;

        }

        public string Localize(string str, string cultureName)
        {
            var result = TryLocalize(str, cultureName);
            if (!string.IsNullOrWhiteSpace(result))
                return result;

            return str;
        }
        public string Localize(string str)
        {
            string cultureName = _auditService.UserLanguage ;
            var result = TryLocalize(str, cultureName);
            if (!string.IsNullOrWhiteSpace(result))
                return result;

            return str;
        }

        public string Localize(string str, string cultureName, params object[] args)
        {
            return string.Format(Localize(str, cultureName), args);
        }

        private string TryLocalize(string str, string cultureName)
        {
            var ci = new CultureInfo(cultureName);
            var resourceManager = new ResourceManager(typeof(Messages));
            var result = resourceManager.GetString(str, ci);
            return result;
        }
    }
}