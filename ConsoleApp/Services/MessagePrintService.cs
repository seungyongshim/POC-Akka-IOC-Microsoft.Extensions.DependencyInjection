using System;

namespace ConsoleApp.Services
{
    internal class MessagePrintService : IMessagePrintService
    {
        int CountPrint { get; set; }
        public MessagePrintService()
        {
        }

        public void Print(string msg)
        {
            CountPrint++;
            Console.WriteLine(msg + CountPrint.ToString());
        }


    }
}
