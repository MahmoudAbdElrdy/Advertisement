using Common.Dependency;
using System;
using System.Collections.Generic;

namespace Common.Localization
{
    public interface IResourceSourceManager : ISingletonDependency
    {
        IReadOnlyList<Type> GetRegisteredSourceTypes();
        void RegisterSourceType(Type type);
    }
}