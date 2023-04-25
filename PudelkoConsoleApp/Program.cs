using PudelkoLibrary;
using System.Globalization;

namespace PudelkoConsoleApp
{
    public class Program
    {
        public static Pudelko Kompresuj(Pudelko p)
        {
            double objetosc = p.Objetosc;
            double side = Math.Pow(objetosc, 1.0 / 3.0);

            return new Pudelko(side, side, side);
        }


        static void Main(string[] args)
        {
            // Sortowanie pudełek 
            List<Pudelko> pudelka = new List<Pudelko>()
            {
                new Pudelko(2, 3, 4),
                new Pudelko(3, 3, 3),
                new Pudelko(3, 4, 5),
                new Pudelko(5, 3, 2),
                new Pudelko(300.0, 230.0, 440.0, UnitOfMeasure.centimeter),
                new Pudelko(7000, 3000, 2300, UnitOfMeasure.milimeter)
            };

            Console.WriteLine("Pudełka przed sortowaniem");
            foreach (var p in pudelka)
                Console.WriteLine(p.ToString());

            Comparison<Pudelko> sortowanie = (p1, p2) =>
            {
                if (p1.Objetosc < p2.Objetosc)
                    return -1;
                else if (p1.Objetosc > p2.Objetosc)
                    return 1;
                else
                {
                    if (p1.Pole < p2.Pole)
                        return -1;
                    else if (p1.Pole > p2.Pole)
                        return 1;
                    else
                    {
                        double sumaKrawendziP1 = p1.A + p1.B + p1.C;
                        double sumaKrawendziP2 = p2.A + p2.B + p2.C;

                        if (sumaKrawendziP1 < sumaKrawendziP2)
                            return -1;
                        else if (sumaKrawendziP1 > sumaKrawendziP2)
                            return 1;
                        else
                            return 0;
                    }
                }
            };
            pudelka.Sort(sortowanie);

            Console.WriteLine("Pudelka posortowane");
            foreach (var p in pudelka)
                Console.WriteLine(p.ToString());

            // Kod potwierdzający implementacje

            Console.WriteLine("\n");

            // Nie włąsciwe wymiary w m
            try
            {
                Pudelko p1 = new Pudelko(11, 3, 9);
            }
            catch (ArgumentOutOfRangeException)
            {
                Console.WriteLine("Nie właściwe wymiary pudełka");
            }

            // Nie włąsciwe wymiary w cm
            try
            {
                Pudelko p2 = new Pudelko(100.2, 3000.0, 320.0, UnitOfMeasure.centimeter);
            }
            catch (ArgumentOutOfRangeException)
            {
                Console.WriteLine("Nie właściwe wymiary pudełka");
            }

            // Nie włąsciwe wymiary w mm
            try
            {
                Pudelko p3 = new Pudelko(4000, 50000, 321, UnitOfMeasure.milimeter);
            }
            catch (ArgumentOutOfRangeException)
            {
                Console.WriteLine("Nie właściwe wymiary pudełka \n\n");
            }

            Pudelko p4 = new(2.310, 3.780, 4.200);
            Pudelko p5 = new(2, 3, 4);

            // Implementacja properties A, B, C
            Console.Write($"Property A, B, C: {p4.A}, {p4.B}, {p4.C}\n\n");
            Console.Write($"Property A, B, C: {p5.A}, {p5.B}, {p5.C}\n\n");

            // Reprezentacja tekstowa toString();
            Console.WriteLine("Reprezentacja tekstowa toString()");
            Console.WriteLine(p4.ToString("m"));
            Console.WriteLine(p4.ToString("cm"));
            Console.WriteLine(p4.ToString("mm") + "\n\n");

            // Implementacja objętości
            Console.WriteLine("Implementacja objętości");
            Console.WriteLine(p5.Objetosc);
            Console.WriteLine(p4.Objetosc + "\n\n");

            // Implementacja pola
            Console.WriteLine("Implementacja pola");
            Console.WriteLine(p5.Pole);
            Console.WriteLine(p4.Pole + "\n\n");

            // Implementacja Equals i operatorów ==, !=
            Pudelko p6 = new(2, 3, 4);
            Console.WriteLine("Implementacja Equals i operatorów ==, !=");
            Console.WriteLine(p6.Equals(p5));
            Console.WriteLine(p4.Equals(p5));
            Console.WriteLine(p6 == p5);
            Console.WriteLine(p6 == p4);
            Console.WriteLine(p6 != p5);
            Console.WriteLine(p6 != p4);
            p6 = null;
            Console.WriteLine(p5.Equals(p6) + "\n\n");

            // Przeciążenie operatora +
            Console.WriteLine("Przeciążenie operatora +");
            var p7 = p4 + p5;
            Console.WriteLine(p7.ToString() + "\n\n");

            // Operacje konwersji explicit i implicit
            Console.WriteLine("Operacje konwersji explicit i implicit");
            double[] dimensions = (double[])p5;

            Console.WriteLine("Konwersja jawna");
            foreach (var d in dimensions)
                Console.Write($"{d}, ");

            Console.WriteLine("\n Konwersja niejawna");
            var box = (400, 2100, 4500);
            Pudelko p8 = box;
            Console.WriteLine(p8.ToString() + "\n\n");

            // Implementacja Indexera
            Console.WriteLine("Implementacja Indexera");
            double a = p8[0];
            double b = p8[1];
            double c = p8[2];
            Console.WriteLine($"Indeksy: [0] : {a} [1] : {b} [2] : {c}" + "\n\n");

            // Pętla foreach po krawędziach
            Console.WriteLine("Pętla foreach po krawędziach");
            foreach (var x in p8)
                Console.WriteLine(x);
            Console.WriteLine("\n");

            // Metoda parsująca String
            Console.WriteLine("Metoda parsująca String");
            // Z powodu polskiego Windowsa zamiast kropek muszą być przecinki
            var p9 = Pudelko.PudelkoDimensionsParse("2,500 m × 9,321 m × 1,500 m");
            Console.WriteLine(p9.ToString() + "\n\n");

            // Zapieczętowanie klasy
            Console.WriteLine("Zapieczętowanie klasy");
            bool isSeald = typeof(Pudelko).IsSealed;
            Console.WriteLine(isSeald);
        }
    }
}