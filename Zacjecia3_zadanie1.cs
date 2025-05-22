using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Zacjecia3
{
    class Zamowienie
    {
        public string Produkt { get; set; }
        public double Suma { get; set; }

    }

    class ProduktWynik
    {
        public string Produkt { get; set; }
        public double Suma { get; set; }
    }

    class WynikZamowienia
    {
        public List<ProduktWynik> Zamowienie { get; set; }
        public double Razem { get; set; }
    }

    class Program
    {
        static void Main()
        {
            string jsonWejscie = File.ReadAllText("zamowienie.json");
            List<Zamowienie> dane = JsonConvert.DeserializeObject<List<Zamowienie>>(jsonWejscie);
            var grupy = dane
                .GroupBy(z => z.Produkt)
                .Select(g => new ProduktWynik
                {
                    Produkt = g.Key,
                    Suma = g.Sum(x => x.Suma)
                })
                .ToList();

            double lacznaSuma = dane.Sum(z => z.Suma);

            var wynik = new WynikZamowienia
            {
                Zamowienie = grupy,
                Razem = lacznaSuma
            };

            string jsonWynik = JsonConvert.SerializeObject(wynik, Formatting.Indented);
            File.WriteAllText("wynik.json", jsonWynik);
            Console.WriteLine(jsonWynik);
            Console.ReadKey();
        }
    }
}
