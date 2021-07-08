using System;
using Microsoft.Owin.Hosting;

namespace CommercialApplicationQueries
{
    public class Program
    {
        public static void Main(string[] args)
        {
            
            using (WebApp.Start<Startup>("http://localhost:12347"))
            {
                Console.WriteLine("Commercial Application Query Server is running.");

                Console.WriteLine("Press any key to shutdown the server.");
                Console.ReadLine();
            }
           
        }
    }
}
