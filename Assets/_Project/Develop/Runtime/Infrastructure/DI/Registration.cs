using System;

namespace _Project.Develop.Runtime.Infrastructure.DI
{
    public class Registration
    {
        private readonly Func<DIContainer, object> _creator;
        private object _cachedInstance;

        public Registration(Func<DIContainer, object> creator)
        {
            _creator = creator;
        }

        public bool IsNonLazy { get; private set; }

        public Registration NonLazy()
        {
            IsNonLazy = true;
            return this;
        }

        public object CreateInstanceFrom(DIContainer container)
        {
            if (_cachedInstance == null)
            {
                _cachedInstance = _creator.Invoke(container);
            }

            return _cachedInstance;
        }
    }
}