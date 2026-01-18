using System;

namespace Laboratorium2
{
    // ==========================================
    // CZĘŚĆ 1: DZIEDZICZENIE I POLIMORFIZM
    // ==========================================

    // ZADANIE 1: Klasa bazowa Zwierze
    public class Zwierze
    {
        // Pole chronione (protected) - dostępne w tej klasie i klasach pochodnych
        protected string nazwa;

        // Konstruktor inicjalizujący nazwę
        public Zwierze(string nazwa)
        {
            this.nazwa = nazwa;
        }

        // Metoda wirtualna (można ją nadpisać w klasach pochodnych)
        public virtual void DajGlos()
        {
            Console.WriteLine("...");
        }
    }

    // ZADANIE 2: Klasa Pies
    public class Pies : Zwierze
    {
        // ZADANIE 5: Konstruktor wywołujący konstruktor bazowy (base)
        public Pies(string nazwa) : base(nazwa)
        {
        }

        public override void DajGlos()
        {
            Console.WriteLine($"{nazwa} robi woof woof!");
        }
    }

    // ZADANIE 3: Klasa Kot
    public class Kot : Zwierze
    {
        // ZADANIE 5: Konstruktor wywołujący konstruktor bazowy
        public Kot(string nazwa) : base(nazwa)
        {
        }

        public override void DajGlos()
        {
            Console.WriteLine($"{nazwa} robi miau miau!");
        }
    }

    // ZADANIE 4: Klasa Waz
    public class Waz : Zwierze
    {
        // ZADANIE 5: Konstruktor wywołujący konstruktor bazowy
        public Waz(string nazwa) : base(nazwa)
        {
        }

        public override void DajGlos()
        {
            Console.WriteLine($"{nazwa} robi ssssssss!");
        }
    }

    // ==========================================
    // CZĘŚĆ 2: KLASY ABSTRAKCYJNE
    // ==========================================

    // ZADANIE 8: Klasa abstrakcyjna Pracownik
    public abstract class Pracownik
    {
        // Metoda abstrakcyjna (nie ma ciała, musi być zaimplementowana w klasie pochodnej)
        public abstract void Pracuj();
    }

    // ZADANIE 9: Klasa Piekarz
    public class Piekarz : Pracownik
    {
        public override void Pracuj()
        {
            Console.WriteLine("Trwa pieczenie...");
        }
    }

    // ==========================================
    // CZĘŚĆ 3: KONSTRUKTORY I DZIEDZICZENIE
    // ==========================================

    // ZADANIE 12: Klasa A
    public class A
    {
        public A()
        {
            Console.WriteLine("To jest konstruktor A");
        }
    }

    // ZADANIE 13: Klasa B dziedzicząca po A
    public class B : A
    {
        public B() : base() // Wywołanie konstruktora bazowego (jawne, choć domyślnie też by zadziałało)
        {
            Console.WriteLine("To jest konstruktor B");
        }
    }

    // ZADANIE 14: Klasa C dziedzicząca po B
    public class C : B
    {
        public C() : base()
        {
            Console.WriteLine("To jest konstruktor C");
        }
    }

    class Program
    {
        // ZADANIE 6: Globalna metoda statyczna (wewnątrz klasy Program)
        public static void PowiedzCos(Zwierze z)
        {
            z.DajGlos();
        }

        // ZADANIE 7: Funkcja Main (Punkt wejścia)
        static void Main(string[] args)
        {
            Console.WriteLine("=== CZĘŚĆ 1: Zwierzęta ===");

            // Tworzenie obiektów
            Zwierze zwykleZwierze = new Zwierze("Nieznane stworzenie");
            Pies pies = new Pies("Burek");
            Kot kot = new Kot("Mruczek");
            Waz waz = new Waz("Ssykacz");

            // Tablica obiektów dla łatwiejszego wywołania (opcjonalnie), 
            // ale zrobimy to po kolei zgodnie z poleceniem, aby wypisać typy.

            // Wywołanie PowiedzCos i wypisanie typu
            Console.Write("Obiekt 1: ");
            PowiedzCos(zwykleZwierze);
            Console.WriteLine($"Typ obiektu: {zwykleZwierze.GetType().Name}\n");

            Console.Write("Obiekt 2: ");
            PowiedzCos(pies);
            Console.WriteLine($"Typ obiektu: {pies.GetType().Name}\n");

            Console.Write("Obiekt 3: ");
            PowiedzCos(kot);
            Console.WriteLine($"Typ obiektu: {kot.GetType().Name}\n");

            Console.Write("Obiekt 4: ");
            PowiedzCos(waz);
            Console.WriteLine($"Typ obiektu: {waz.GetType().Name}\n");


            Console.WriteLine("=== CZĘŚĆ 2: Pracownicy ===");

            // ZADANIE 10: Obiekt Piekarz
            Piekarz piekarz = new Piekarz();
            piekarz.Pracuj();

            // ZADANIE 11: Próba utworzenia obiektu klasy abstrakcyjnej
            // Pracownik pracownik = new Pracownik(); 
            // POWYŻSZA LINIA SPOWODUJE BŁĄD KOMPILACJI:
            // "Cannot create an instance of the abstract type or interface 'Pracownik'"
            // Klasy abstrakcyjne służą tylko jako baza i nie mogą istnieć samodzielnie.
            Console.WriteLine("(Nie można utworzyć instancji klasy abstrakcyjnej Pracownik)");


            Console.WriteLine("\n=== CZĘŚĆ 3: Kolejność konstruktorów ===");

            Console.WriteLine("--- Tworzenie obiektu A ---");
            A obiektA = new A();

            Console.WriteLine("\n--- Tworzenie obiektu B ---");
            B obiektB = new B();

            Console.WriteLine("\n--- Tworzenie obiektu C ---");
            // ZADANIE 15: Obserwacja kolejności
            C obiektC = new C();

            // Wniosek do Zadania 15:
            // Konstruktory wywoływane są od klasy najbardziej bazowej (najwyżej w hierarchii) w dół.
            // Dla obiektu C kolejność to: A -> B -> C.

            Console.ReadKey();
        }
    }
}
