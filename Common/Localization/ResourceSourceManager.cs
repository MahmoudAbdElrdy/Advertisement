using Common.Dependency;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Common.Localization
{
    public class ResourceSourceManager : IResourceSourceManager, ISingletonDependency
    {
        protected IList<Type> RegisteredSourceTypes { get; }

        public ResourceSourceManager()
        {
            RegisteredSourceTypes = new List<Type>();
        }

        public IReadOnlyList<Type> GetRegisteredSourceTypes()
        {
            return RegisteredSourceTypes.ToList();
        }

        public void RegisterSourceType(Type type)
        {
            RegisteredSourceTypes.Add(type);
        }
    }
}