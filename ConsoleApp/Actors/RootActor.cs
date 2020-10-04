using Akka.Actor;
using Akka.DI.Core;
using Akka.Util.Internal;
using ConsoleApp.Messages;
using System;

namespace ConsoleApp
{
    internal class RootActor : ReceiveActor
    {
        public RootActor()
        {
            Receive<Start>(Handle);
        }

        private void Handle(Start msg)
        {
            PongParentActor = Context.ActorOf(Context.DI().Props<PongParentActor>());
            PingParentActor = Context.ActorOf(Context.DI().Props<PingParentActor>());

            PingParentActor.Tell("Hello", PongParentActor);
        }

        public IActorRef PongParentActor { get; private set; }
        public IActorRef PingParentActor { get; private set; }
    }
}