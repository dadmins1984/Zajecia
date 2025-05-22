using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Zacjecia3
{
    class User
    {
        public byte Id { get; set; }
        public string Imie { get; set; }
        public int Wiek { get; set; }
        public string Miasto { get; set; }

    }

    class Program
    {
        static void Main()
        {
            string jsonWejscie = File.ReadAllText("users.json");
            List<User> dane = JsonConvert.DeserializeObject<List<User>>(jsonWejscie);
            var wynik = dane.Where(w => w.Wiek >= 30 && w.Miasto == "Warszawa");

            foreach (var u in wynik)
            {
                Console.WriteLine($"Imię: {u.Imie}, Wiek: {u.Wiek}, Miasto: {u.Miasto}");
            }

            string jsonWynik = JsonConvert.SerializeObject(wynik, Formatting.Indented);
            File.WriteAllText("wynik.json", jsonWynik);
            Console.ReadKey();
        }
    }
}
