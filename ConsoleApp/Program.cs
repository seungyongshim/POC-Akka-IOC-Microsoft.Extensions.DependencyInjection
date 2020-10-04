using Akka.Actor;
using Akka.DI.Extensions.DependencyInjection;
using Akka.DI.Core;
using ConsoleApp.Messages;
using System;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using FluentAssertions.Extensions;
using ConsoleApp.Services;

namespace ConsoleApp
{
    internal class Program
    {
        private static IServiceProvider CompositeRoot()
        {
            var services = new ServiceCollection();
            services.AddTransient<RootActor>();
            services.AddTransient<PongParentActor>();
            services.AddTransient<PingParentActor>();
            services.AddTransient<PongChildActor>();
            services.AddTransient<PingChildActor>();
            services.AddScoped<IMessagePrintService, MessagePrintService>();
            return services.BuildServiceProvider();
        }

        private static async Task Main(string[] args)
        {
            using (var system = ActorSystem.Create("Sample"))
            {
                var resolver = new ServiceProviderDependencyResolver(CompositeRoot(), system);
                var rootActor = system.ActorOf(system.DI().Props<RootActor>(), "RootActor");

                while (true)
                {
                    Console.ReadLine();

                    rootActor.Tell(new Start { });

                    var root = await system.ActorSelection("/").ResolveOne(1000.Milliseconds());

                    PrintChildrenPath(root as ActorRefWithCell);
                }
            }
        }

        private static void PrintChildrenPath(ActorRefWithCell actor)
        {
            foreach (var item in actor.Children)
            {
                Console.WriteLine(item);
                PrintChildrenPath(item as ActorRefWithCell);
            }
        }
    }
}