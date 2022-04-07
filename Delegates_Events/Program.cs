using System;
using System.Reflection;

namespace Delegates_Events
{
    internal class Program
    {
        static void Main(string[] args)
        {

            DocumentsReceiver documentsReceiver = new DocumentsReceiver();

            documentsReceiver.TimedOut += DocumentsReceiver_TimedOut;
            documentsReceiver.DocumentsReady += DocumentsReceiver_DocumentsReady;

            var assemblyLocation = Assembly.GetExecutingAssembly().Location;
            assemblyLocation = assemblyLocation.Substring(0, assemblyLocation.LastIndexOf("\\"));

            documentsReceiver.Start(assemblyLocation, 5000);


            Console.ReadLine();

        }

        private static void DocumentsReceiver_DocumentsReady(DocumentsReceiver obj)
        {
            Console.WriteLine("Документы загружены");
        }

        private static void DocumentsReceiver_TimedOut(string obj)
        {
            Console.WriteLine(obj);
        }
    }
}
