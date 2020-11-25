using System;
using System.Collections.Generic;

namespace StickyTeam.Infrastructure.Container
{
    public class Resolver
    {
        Dictionary<Type, Registration> _registrations = new Dictionary<Type, Registration>();

        public void Register<T>(T instance) where T : class
        {
            _registrations.Add(typeof(T), new Registration(instance));
        }

        public void Register<T>(Func<T> activator) where T : class
        {
            _registrations.Add(typeof(T), new Registration(activator));
        }

        public T Resolve<T>() where T : class
        {
            return _registrations[typeof(T)].Instance as T;
        }
    }
}