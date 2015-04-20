using System;
using HowMetYourPattern.Core.Structural;
using IComponent = HowMetYourPattern.Core.Structural.IComponent;

namespace HowMetYourPattern
{
    class Program
    {
        static void Main(string[] args)
        {
            //ExecuteDecorator();

            ExecuteProxy();
        }

        private static void ExecuteProxy()
        {

        }

        private static void ExecuteDecorator()
        {
            //Decorator;
            Console.WriteLine("Decorator Pattern\n");

            var component = new Decorator();
            Display("1. Basic component: ", component);
            Display("2. A-decorated : ", new DecoratorA(component));
            Display("3. B-decorated : ", new DecoratorB(component));
            Display("4. B-A-decorated : ", new DecoratorB(new DecoratorA(component)));
            // Explicit DecoratorB
            var b = new DecoratorB(new Decorator());
            Display("5. A-B-decorated : ", new DecoratorA(b));
            // Invoking its added state and added behavior
            Console.WriteLine("\t\t\t" + b.AddedState + b.AddedBehavior());
        }

        private static void Display(string s, IComponent c)
        {
            Console.WriteLine(s + c.Operation());
        }
    }
}
