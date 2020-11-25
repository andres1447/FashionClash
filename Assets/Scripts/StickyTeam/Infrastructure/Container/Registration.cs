using System;

namespace StickyTeam.Infrastructure.Container
{
    public class Registration
    {
        private object _instance;
        private Func<object> _activator;

        public object Instance => _instance ?? (_instance = _activator());

        public Registration(object instance)
        {
            _instance = instance;
        }

        public Registration(Func<object> instantiator)
        {
            _activator = instantiator;
        }
    }
}