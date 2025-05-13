using System;
using System.Collections.Generic;

namespace zadanie2 
{
    class Program
    {
        static void Main()
        {
            List<int> liczby = new List<int>();
            for (int i = 1000; i <= 9999; i++) 
            {
                double ilewa = i / 100;
                double iprawa = i % 100;
                double suma = Math.Pow(ilewa, 2) + Math.Pow(iprawa, 2);

                if (suma == i) 
                {
                    liczby.Add(i);
                }
            }

            foreach (int i in liczby) 
            {
                Console.WriteLine($"Liczby spełniające warunek to: {i}");
            }

            Console.ReadKey(); 

        }
    }
}
