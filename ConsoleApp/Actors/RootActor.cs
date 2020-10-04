using Akka.Actor;
using Akka.DI.Core;
using Akka.Util.Internal;
using ConsoleApp.Messages;
using System;
using System.Linq;

namespace ConsoleApp
{
    internal class RootActor : ReceiveActor
    {
        public RootActor(IMessagePrintService messagePrintService)
        {
            Receive<Start>(Handle);
            MessagePrintService = messagePrintService;
        }

        private void Handle(Start msg)
        {
            PongParentActor = Context.ActorOf(Context.DI().Props<PongParentActor>(), "PongParentActor-" + Context.GetChildren().Count());
            PingParentActor = Context.ActorOf(Context.DI().Props<PingParentActor>(), "PingParentActor-" + Context.GetChildren().Count());

            PongParentActor.Tell(msg);

            MessagePrintService.Print(string.Empty);
        }

        public IActorRef PongParentActor { get; private set; }
        public IActorRef PingParentActor { get; private set; }
        public IMessagePrintService MessagePrintService { get; }
    }
}