using Akka.Actor;
using Akka.DI.AutoFac;
using Autofac;
using Akka.DI.Core;
using ConsoleApp.Messages;
using System;

namespace ConsoleApp
{
    internal class Program
    {
        private static IContainer CompositeRoot()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<RootActor>();
            builder.RegisterType<PongParentActor>();
            builder.RegisterType<PingParentActor>();
            builder.RegisterType<PongChildActor>();
            return builder.Build();
        }

        private static void Main(string[] args)
        {
            using (var system = ActorSystem.Create("Sample"))
            {
                var resolver = new AutoFacDependencyResolver(CompositeRoot(), system);

                var rootActor = system.ActorOf(system.DI().Props<RootActor>(), "RootActor");

                rootActor.Tell(new Start { });

                Console.ReadLine();
            }
        }
    }
}