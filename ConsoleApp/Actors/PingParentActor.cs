using Akka.Actor;
using Akka.DI.Core;
using ConsoleApp.Messages;
using System;
using System.Threading;

namespace ConsoleApp
{
    internal class PingParentActor : ReceiveActor
    {
        public PingParentActor()
        {
            PingChildActor = Context.ActorOf(Context.DI().Props<PingChildActor>());

            Receive<Start>(Handle);
        }

        private void Handle(Start msg)
        {
            Console.WriteLine("Hello");
            Thread.Sleep(1000);
            PongParentActor.Tell(msg);
        }

        public IActorRef PingChildActor { get; private set; }
        public IActorRef PongParentActor { get; internal set; }
    }
}