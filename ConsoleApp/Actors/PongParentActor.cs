using Akka.Actor;
using Akka.DI.Core;
using ConsoleApp.Messages;
using System;

namespace ConsoleApp
{
    internal class PongParentActor : ReceiveActor
    {
        public PongParentActor()
        {
            PongChildActor = Context.ActorOf(Context.DI().Props<PongChildActor>());

            Receive<Start>(Handle);

        }

        private void Handle(Start msg)
        {
            PingParentActor.Tell("World");
        }

        public IActorRef PongChildActor { get; private set; }
        public IActorRef PingParentActor { get; internal set; }
    }
}