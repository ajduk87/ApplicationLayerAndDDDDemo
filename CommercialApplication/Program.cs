using CommercialApplicationCommand.DomainLayer.Repositories.Factory;
using Microsoft.Owin.Hosting;
using System;

namespace CommercialApplicationCommand
{
    public class Program
    {
        public static void Main(string[] args)
        {
            using (WebApp.Start<Startup>("http://localhost:12346"))
            {
                Console.WriteLine("Commercial Application Command Server is running.");

                Console.WriteLine("Press any key to shutdown the server.");
                Console.ReadLine();
            }
        }
    }
}