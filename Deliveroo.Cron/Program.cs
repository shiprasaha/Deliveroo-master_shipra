using System;
using Deliveroo.Cron.Controllers;

namespace Deliveroo.Cron
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                Console.Write("Invalid CRON expression passed in.");
                return;
            }

            try 
            {
                var cronApplicationController = new CronApplicationController();
                var parsedExpression = cronApplicationController.ParseExpression(args[0]);
                Console.WriteLine(parsedExpression);
            }

            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}