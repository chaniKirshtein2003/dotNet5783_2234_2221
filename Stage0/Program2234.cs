using System;

namespace Stage0
{
    partial class Program
    {
        static void Main(string[] args)
        {
            welcome2234();
            welcome2221();
        }
        static partial void welcome2221();
        private static void welcome2234()
        {
            Console.Write("Enter your name: ");
            string myName = Console.ReadLine();
            Console.WriteLine("{0}, welcome to my first console application", myName);
        }
    }
}