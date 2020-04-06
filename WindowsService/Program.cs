using System;

namespace WindowsService
{
    class Program
    {
        static void Main(string[] args)
        {
            var apiConsumer = new ApiConsumer();

            Console.WriteLine("API 1 result:");
            Console.WriteLine(apiConsumer.ConsumeAPI1().Result);

            Console.WriteLine();

            Console.WriteLine("API 2 result:");
            Console.WriteLine(apiConsumer.ConsumeAPI2().Result);
        }
    }
}