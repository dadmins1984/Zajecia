using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace zadanie4
{
    public interface IZadania
    {
        bool DodajStudenta(string imie, string nazwisko, byte klasa);
        void UsuńStudenta(string studentIdString);
        void EdytujStudenta(Guid studentId, string imie, string nazwisko, byte klasa);
        Student WyświetlStudenta(string studentIdString);
        void WyświetlStudentów();
    }

    public class Student
    {
        public string imie { get; set; }
        public string nazwisko { get; set; }
        public byte klasa { get; set; }
        public Guid studentId { get; set; }

        public Student(Guid _studentId, string _imie, string _nazwisko, byte _klasa)
        {
            studentId = _studentId;
            imie = _imie;
            nazwisko = _nazwisko;
            klasa = _klasa;
        }

        public bool SprawdźDane()
        {
            bool daneOk = true;

            if (string.IsNullOrWhiteSpace(imie))
            {
                Console.WriteLine("Imię nie może być puste.");
                Thread.Sleep(1000);
                daneOk = false;
            }

            if (string.IsNullOrWhiteSpace(nazwisko))
            {
                Console.WriteLine("Nazwisko nie może być puste.");
                Thread.Sleep(1000);
                daneOk = false;
            }

            if (klasa == 0 || klasa > 12)
            {
                Console.WriteLine("Klasa musi być w zakresie od 1 do 12.");
                Thread.Sleep(1000);
                daneOk = false;
            }
            return daneOk;
        }
    }

    public class StudentManager : IZadania
    {
        private Dictionary<Guid, Student> Studenci = new Dictionary<Guid, Student>();
        public bool możeEdytowac { get; set; }

        public StudentManager()
        {
            możeEdytowac = true;
        }

        public bool DodajStudenta(string imie, string nazwisko, byte klasa)
        {
            var id = Guid.NewGuid();
            var nowyStudent = new Student(id, imie, nazwisko, klasa);

            bool daneOk = nowyStudent.SprawdźDane();
            if (daneOk)
            {
                Studenci.Add(id, nowyStudent);
                Console.WriteLine("Dodano studenta.");
                Thread.Sleep(1000);
                Console.Clear();
            }

            return daneOk;
        }

        public void UsuńStudenta(string studentIdString)
        {
            if (Guid.TryParse(studentIdString, out Guid studentId))
            {
                if (Studenci.ContainsKey(studentId))
                {
                    Studenci.Remove(studentId);
                    Console.WriteLine("Usunięto studenta.");
                }
                else
                {
                    Console.WriteLine("Student o podanym ID nie istnieje.");
                }
            }
            else
            {
                Console.WriteLine("Podano niepoprawny identyfikator studenta.");
            }
            Thread.Sleep(1000);
            Console.Clear();
        }

        public void EdytujStudenta(Guid studentId, string imie, string nazwisko, byte klasa)
        {
            var nowyStudent = new Student(studentId, imie, nazwisko, klasa);

            bool daneOk = nowyStudent.SprawdźDane();
            if (daneOk)
            {
                if (Studenci.ContainsKey(studentId))
                {
                    var student = Studenci[studentId];
                    student.imie = imie;
                    student.nazwisko = nazwisko;
                    student.klasa = klasa;
                    Console.WriteLine("Student został zaktualizowany.");
                }
                else
                {
                    Console.WriteLine("Id studenta nie zostało wykryte.");
                }
                Thread.Sleep(1000);
                Console.Clear();
            }
            else
            {
                Console.Clear();
            }
        }

        public void WyświetlStudentów()
        {
            if (Studenci.Count == 0)
            {
                Console.WriteLine("Brak studentów.");
            }
            else
            {
                foreach (var s in Studenci)
                {
                    Console.WriteLine($"ID: {s.Key} | Imię: {s.Value.imie}, Nazwisko: {s.Value.nazwisko}, Klasa: {s.Value.klasa}");
                }
            }
        }

        public Student WyświetlStudenta(string studentIdString)
        {
            Student student;
            if (Guid.TryParse(studentIdString, out Guid studentId))
            {
                if (Studenci.ContainsKey(studentId))
                {
                    student = Studenci.FirstOrDefault(s => s.Key == studentId).Value;
                }
                else
                {
                    Console.WriteLine("Student o podanym ID nie istnieje.");
                    student = null;
                }
            }
            else
            {
                Console.WriteLine("Podano niepoprawny identyfikator studenta.");
                student = null;
            }
            Thread.Sleep(1000);
            Console.Clear();
            return student;
        }
    }

    class Program
    {
        static void Main()
        {
            string imie;
            string nazwisko;
            Guid Id;
            byte klasa;
            StudentManager studentManager = new StudentManager();

            while (true)
            {
                Console.WriteLine("*****************************************");
                Console.WriteLine("************* Menu główne ***************");
                Console.WriteLine("*****************************************");
                Console.WriteLine("");
                Console.WriteLine("Podaj swój Id jeśli chcesz edytować dane.");
                Console.WriteLine("Wpisz Manager jeśli chcesz zarządzać studentami.");
                Console.WriteLine("Wpisz exit żeby zakończyć");
                Console.WriteLine("");
                Console.WriteLine("*****************************************");
                Console.WriteLine("");
                studentManager.WyświetlStudentów();
                imie = Console.ReadLine();
                if (imie == "exit")
                {
                    break;
                }
                else if (imie == "Manager" && studentManager.możeEdytowac)
                {
                    while (true)
                    {
                        Console.Clear();
                        Console.WriteLine("*****************************************");
                        Console.WriteLine("************* Menu manager **************");
                        Console.WriteLine("*****************************************");
                        Console.WriteLine("");
                        Console.WriteLine("Jaką chcesz wybrać operację? Podaj cyfrę:");
                        Console.WriteLine("1 - Dodaj studenta");
                        Console.WriteLine("2 - Edytuj studenta");
                        Console.WriteLine("3 - Usuń studenta");
                        Console.WriteLine("0 - Wyjdź z menu Managera");
                        Console.WriteLine("");
                        Console.WriteLine("*****************************************");
                        Console.WriteLine("");
                        studentManager.WyświetlStudentów();

                        int wybór;
                        string w = Console.ReadLine();
                        if (!string.IsNullOrWhiteSpace(w) && char.IsDigit(w[0]))
                        {
                            wybór = w[0] - '0';
                        }
                        else
                        {
                            Console.WriteLine("Nieprawidłowy wybór. wyjście.");
                            Thread.Sleep(1000);
                            Console.Clear();
                            wybór = 0;
                        }

                        if (wybór == 1)
                        {
                            Console.Clear();
                            Console.WriteLine("Podaj imię studenta:");
                            imie = Console.ReadLine();
                            Console.WriteLine("Podaj nazwisko studenta:");
                            nazwisko = Console.ReadLine();
                            Console.WriteLine("Podaj klasę studenta:");
                            string input = Console.ReadLine();
                            if (byte.TryParse(input, out klasa))
                            {
                                studentManager.DodajStudenta(imie, nazwisko, klasa);
                            }
                            else
                            {
                                Console.WriteLine("Błąd! Wprowadź poprawną liczbę.");
                            }
                        }
                        else if (wybór == 2)
                        {
                            Console.Clear();
                            Console.WriteLine("Podaj ID studenta:");
                            string inputId = Console.ReadLine();
                            Console.WriteLine("Podaj imię studenta:");
                            imie = Console.ReadLine();
                            Console.WriteLine("Podaj nazwisko studenta:");
                            nazwisko = Console.ReadLine();
                            Console.WriteLine("Podaj klasę studenta:");
                            string inputKlasa = Console.ReadLine();
                            if (byte.TryParse(inputKlasa, out klasa) && Guid.TryParse(inputId, out Id))
                            {
                                studentManager.EdytujStudenta(Id, imie, nazwisko, klasa);
                            }
                            else
                            {
                                Console.WriteLine("Błąd! Wprowadź poprawne dane.");
                            }
                        }
                        else if (wybór == 3)
                        {
                            Console.Clear();
                            Console.WriteLine("Podaj ID studenta:");
                            string inputId = Console.ReadLine();

                            studentManager.UsuńStudenta(inputId);

                        }
                        else if (wybór == 0)
                        {
                            Console.Clear();
                            break;
                        }
                    }
                }
                else
                {
                    if (Guid.TryParse(imie, out Guid id))
                    {
                        var student = studentManager.WyświetlStudenta(id.ToString());
                        if (student is null)
                        {
                            Console.WriteLine("błędne dane");
                            Thread.Sleep(1000);
                            Console.Clear();
                            continue;
                        }
                        else
                        {
                            while (true)
                            {
                                Console.Clear();
                                Console.WriteLine("*****************************************");
                                Console.WriteLine("************* Menu student **************");
                                Console.WriteLine("*****************************************");
                                Console.WriteLine("");
                                Console.WriteLine("Jaką chcesz wybrać operację? Podaj cyfrę:");
                                Console.WriteLine("1 - Edytuj dane");
                                Console.WriteLine("0 - Wyjdź z menu Studenta");
                                Console.WriteLine("");
                                Console.WriteLine("*****************************************");
                                Console.WriteLine("");
                                Console.WriteLine($"Imie: {student.imie}, Nazwisko: {student.nazwisko}, Klasa: {student.klasa}");

                                int wybór;
                                string w = Console.ReadLine();
                                if (!string.IsNullOrWhiteSpace(w) && char.IsDigit(w[0]))
                                {
                                    wybór = w[0] - '0';
                                }
                                else
                                {
                                    Console.WriteLine("Nieprawidłowy wybór. wyjście.");
                                    Thread.Sleep(1000);
                                    Console.Clear();
                                    wybór = 0;
                                }

                                if (wybór == 1)
                                {
                                    Console.Clear();
                                    string inputId = student.studentId.ToString();
                                    Console.WriteLine("Podaj imię studenta:");
                                    imie = Console.ReadLine();
                                    Console.WriteLine("Podaj nazwisko studenta:");
                                    nazwisko = Console.ReadLine();
                                    Console.WriteLine("Podaj klasę studenta:");
                                    string inputKlasa = Console.ReadLine();
                                    if (byte.TryParse(inputKlasa, out klasa) && Guid.TryParse(inputId, out Id))
                                    {
                                        studentManager.EdytujStudenta(Id, imie, nazwisko, klasa);
                                    }
                                    else
                                    {
                                        Console.WriteLine("Błąd! Wprowadź poprawne dane.");
                                    }
                                }
                                else if (wybór == 0)
                                {
                                    Console.Clear();
                                    break;
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
