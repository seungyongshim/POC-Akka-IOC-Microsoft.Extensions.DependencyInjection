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
        }

        public IActorRef PongChildActor { get; private set; }
    }
}