namespace StickyTeam.Infrastructure.Container
{
    public interface Module
    {
        void Register(Resolver resolver);
    }
}