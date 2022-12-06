using System;
namespace Decorator.Examples
{
    class MainApp
    {
        static void Main()
        {
            // Create ConcreteComponent and two Decorators
            MyChristmasTree c = new MyChristmasTree();
            DecoratedWithToys d1 = new DecoratedWithToys();
            DecoratedWithGarland d2 = new DecoratedWithGarland();

            // Link decorators
            d1.SetComponent(c);
            d2.SetComponent(d1);

            d2.Operation();

            // Wait for user
            Console.Read();
        }
    }
    // "Component"
    abstract class ChristmasTree // component
    {
        public abstract void Operation();
    }

    // "ConcreteComponent"
    class MyChristmasTree : ChristmasTree// ConcreteComponent
    {
        public override void Operation()
        {
            Console.WriteLine("Christmas Tree is done!");
        }
    }
    // "Decorator"
    abstract class Decorator : ChristmasTree
    {
        protected ChristmasTree myChristmasTree;

        public void SetComponent(ChristmasTree myChristmasTree)
        {
            this.myChristmasTree = myChristmasTree;
        }
        public override void Operation()
        {
            if (myChristmasTree != null)
            {
                myChristmasTree.Operation();
            }
        }
    }

    // "ConcreteDecoratorA"
    class  DecoratedWithToys: Decorator//ConcreteDecoratorA
    {
        private string christmasToys;

        public override void Operation()
        {
            base.Operation();
            christmasToys = "Toy angels";
            Console.WriteLine("Chrismas toys on!");
        }
    }

    // "ConcreteDecoratorB" 
    class DecoratedWithGarland: Decorator//ConcreteDecoratorB
    {
        bool canGlow = false;
        public override void Operation()
        {
            base.Operation();
            AddedBehavior();
            Console.WriteLine("it can glow!");
        }
        void AddedBehavior()
        { 
            canGlow = true;
        }
    }
}
