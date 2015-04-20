namespace HowMetYourPattern.Core.Structural
{

    /*        
    Role
    The role of the Decorator pattern is to provide a way of attaching new state and
    behavior to an object dynamically. The object does not know it is being “decorated,”
    which makes this a useful pattern for evolving systems. A key implementation
    point in the Decorator pattern is that decorators both inherit the original class
    and contain an instantiation of it.
    */
    public class Decorator : IComponent
    {

        public string Operation()
        {
            return "I am walking ";
        }

    }

    public class DecoratorA : IComponent
    {
        private readonly IComponent _component;

        public DecoratorA(IComponent c)
        {
            _component = c;
        }

        public string Operation()
        {
            var s = _component.Operation();
            s += "and listening to Classic FM ";
            return s;
        }
    }

    public class DecoratorB : IComponent
    {
        private readonly IComponent _component;
        public string AddedState = "past the Coffee Shop ";

        public DecoratorB(IComponent c)
        {
            _component = c;
        }

        public string Operation()
        {
            var s = _component.Operation();
            s += "to school ";
            return s;
        }

        public string AddedBehavior()
        {
            return "and I bought a cappuccino ";
        }
    }

    public interface IComponent
    {
        string Operation();
    }
}