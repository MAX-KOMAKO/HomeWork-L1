using System;
using System.Collections;
using System.Collections.Generic;

namespace Game.Configs
{
    public interface IConfigsLoader
    {
        IEnumerator LoadAsync(Action<Dictionary<Type, object>> onConfigsLoaded);
    }
}