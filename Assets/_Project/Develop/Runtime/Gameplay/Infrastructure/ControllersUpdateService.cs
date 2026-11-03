using System;
using System.Collections.Generic;

namespace _Project.Develop.Runtime.Gameplay.Infrastructure
{
    public class ControllersUpdateService
    {
        private readonly List<Action> _updatables = new();

        public void Add(Action updateAction)
        {
            _updatables.Add(updateAction);
        }

        public void Remove(Action updateAction)
        {
            _updatables.Remove(updateAction);
        }

        public void Update()
        {
            foreach (Action updateAction in _updatables)
            {
                updateAction?.Invoke();
            }
        }
    }
}