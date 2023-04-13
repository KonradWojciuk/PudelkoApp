using PudelkoLibrary;
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

            Pudelko p2 = new(932.1, 10.0, 250.0, UnitOfMeasure.centimeter);
            Console.WriteLine(p1.Equals(p2));
            Console.WriteLine(p1 == p2);
            Console.WriteLine(p2 != p1);

            double a = p1[0];
            double b = p1[1];
            double c = p1[2];

            Console.WriteLine($"{a}, {b}, {c}");

            foreach (var pudelko in p1)
            {
                Console.WriteLine(pudelko);
            }
        }
    }
}