using PudelkoLib;
using System.Globalization;

namespace PudelkoConsoleApp
{
    public class Program
    {
        static void Main(string[] args)
        {
            Pudelko p1 = new(2.5, 9.321, 0.1);
            Console.WriteLine(p1.ToString());
            Console.WriteLine(p1.ToString("m", new CultureInfo("en-US")));
            Console.WriteLine(p1.ToString("cm", new CultureInfo("en-US")));
            Console.WriteLine(p1.ToString("mm", new CultureInfo("en-US")));

            Pudelko p2 = new(9321, 100, 2500, UnitOfMeasure.centimiter);
            Console.WriteLine(p1.Equals(p2));
        }
    }
}