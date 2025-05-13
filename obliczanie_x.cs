using System;
using System.Threading;

namespace obliczx
{
    class Program
    {
        static byte Sprawdzanie() 
        {
            byte zmienna;

            while (true)
            {
                Console.WriteLine($"podaj liczbę z przedziału od {byte.MinValue + 1} do {byte.MaxValue}");
                if (!byte.TryParse(Console.ReadLine(), out zmienna))
                {
                    Console.WriteLine($"Błąd zła licznba - podaj A z przedziału od {byte.MinValue + 1} do {byte.MaxValue}");
                    Thread.Sleep(1000);
                    Console.Clear();
                }
                else if (zmienna <= 0)
                {
                    Console.WriteLine("Błąd zła licznba - musi być większa od 0");
                    Thread.Sleep(1000);
                    Console.Clear();
                }
                else
                {
                    Console.WriteLine($"Poprawnie wprowadzono liczbę: {zmienna}");
                    Thread.Sleep(1000);
                    Console.Clear();
                    break;
                }
            }
            return zmienna;
        }
        static void Main()
        {

            byte A = Sprawdzanie();
            byte B = Sprawdzanie();
            byte C = Sprawdzanie();

            double X = (double)(C - B) / A;
            Console.Clear();
            Console.WriteLine("wynik obliczania X dla działania ax + b = c");
            Console.WriteLine($"to {X}");
            Thread.Sleep(5000);
            Console.Clear();
        }
    }
}
