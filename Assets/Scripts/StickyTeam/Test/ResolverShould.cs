using NUnit.Framework;
using StickyTeam.Infrastructure.Container;

namespace StickyTeam.Test
{
    public class ResolverShould
    {
        private Resolver _resolver;
        
        [SetUp]
        public void SetUp()
        {
            _resolver = new Resolver();
        }

        [Test]
        public void register_an_instance()
        {
            var instance = new TestClass();
            _resolver.Register(instance);
            
            var resolved = _resolver.Resolve<TestClass>();
            
            Assert.AreEqual(instance, resolved);
        }

        [Test]
        public void register_an_activator()
        {
            var instance = new TestClass();
            _resolver.Register(() => instance);
            
            var resolved = _resolver.Resolve<TestClass>();
            
            Assert.AreEqual(instance, resolved);
        }

        [Test]
        public void register_an_interface()
        {
            var instance = new TestClass();
            _resolver.Register<TestInterface>(instance);
            
            var resolved = _resolver.Resolve<TestInterface>();
            
            Assert.AreEqual(instance, resolved);
        }
        
        private interface TestInterface
        {
            
        }

        private class TestClass : TestInterface
        {
            
        }
    }
}