using System;
using System.Threading;
using System.Threading.Tasks;

namespace DIThreadStatic
{
    class Program
    {
        [ThreadStatic]
        private static Person p;

        static void Main(string[] args)
        {
            Start().Wait();
            Console.ReadKey();
        }

        private static async Task Start()
        {
            p = new Person { name = "kyle" };
            var machine = new EchoMachine(p);
            Console.WriteLine("Started on thread [{0}]", Thread.CurrentThread.ManagedThreadId);
            machine.sayHello();

            await Sleepy();

            Console.WriteLine("Started on thread [{0}]", Thread.CurrentThread.ManagedThreadId);
            machine.sayHello();
        }

        private static async Task Sleepy()
        {
            Console.WriteLine("Was on thread [{0}]", Thread.CurrentThread.ManagedThreadId);
            await Task.Delay(1000);
            Console.WriteLine("Now on thread [{0}]", Thread.CurrentThread.ManagedThreadId);
        }
    }

    class Person
    {
        public string name;
    }

    class EchoMachine
    {
        private readonly Person p;

        public EchoMachine(Person p)
        {
            this.p = p;
        }

        public void sayHello()
        {
            Console.WriteLine("Hello " + p.name);
        }
    }
}
