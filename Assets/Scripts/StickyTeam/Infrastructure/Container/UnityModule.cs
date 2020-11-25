using UnityEngine;

namespace StickyTeam.Infrastructure.Container
{
    public abstract class UnityModule : ScriptableObject, Module
    {
        public abstract void Register(Resolver resolver);
    }
}