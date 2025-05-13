using System;
using System.Collections.Generic;

namespace zadanie3 
{
    class Prostokąd
    {
        double wysokość;
        double szerokość;

        public Prostokąd(double _szerokość, double _wysokość) 
         {
            szerokość = _szerokość;
            wysokość = _wysokość;
         }

        public double ObliczPole() 
        {
            return szerokość * wysokość;
        }

        public double ObliczObwód() 
        {
            return 2 * (szerokość + wysokość);
        }
    }

    class Program
    {
        static void Main()
        {
            Prostokąd p = new Prostokąd(5, 10);
            Console.WriteLine($"pole prostokątu = {p.ObliczPole()}");
            Console.WriteLine($"obwód prostokątu = {p.ObliczObwód()}");
            Console.ReadKey();
        }
    }
}
