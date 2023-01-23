using System;
using System.Collections.Generic;
using System.Linq;
using Interfaces;
using UniRx;

namespace Installers
{
    public class ScriptBinder
    {
        public BoolReactiveProperty IsInitialised { get; } = new BoolReactiveProperty();
            
        private List<IBindable> Bindables { get; } = new();

        public void Bind<T>(T obj) where T : IBindable
        {
            if (obj == null)
            {
#if UNITY_EDITOR
                throw new NullReferenceException();
#endif
                return;
            }
            
            Bindables.Add(obj);
        }

        public T Get<T>() where T : IBindable
        {
            var result = Bindables.FirstOrDefault(c => c is T);

            if (result == null)
            {
                throw new ArgumentException($"Can't resolve type {typeof(T)}");
            }

            return (T)result;
        }

        public void Initialize()
        {
            foreach (var bindable in Bindables)
            {
                bindable.Initialize(this);
            }

            IsInitialised.Value = true;
        }
    }
}