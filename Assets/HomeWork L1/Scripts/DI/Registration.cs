using System;

namespace Game.DI
{
    public class Registration
    {
        private readonly Func<DIContainer, object> _creator;

        private object _cachedInstance;

        public Registration(Func<DIContainer, object> creator)
        {
            _creator = creator;
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