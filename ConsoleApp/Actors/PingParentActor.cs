using Akka.Actor;
using Akka.DI.Core;
using ConsoleApp.Messages;
using System;
using System.Linq;
using System.Threading;

namespace ConsoleApp
{
    internal class PingParentActor : ReceiveActor
    {
        public PingParentActor()
        {
            PingChildActor = Context.ActorOf(Context.DI().Props<PingChildActor>(), "PingChildActor-" + Context.GetChildren().Count());
        }

        public IActorRef PingChildActor { get; private set; }
    }
}