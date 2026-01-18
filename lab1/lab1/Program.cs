using System;
using System.Collections.Generic;

namespace Lab1
{
    // ZADANIE 19: Utwórz drugą klasę – Zwierze
    public class Zwierze
    {
        // POLA PRYWATNE
        // Zadanie 22: Pole nazwy jest obsługiwane przez właściwość (Property)
        private string _nazwa;

        // Pola wymagane w zadaniu 19
        private string gatunek;
        private int liczbaNog;

        // Pole statyczne do zliczania liczby zwierząt
        private static int liczbaZwierzat = 0;

        // WŁAŚCIWOŚCI (ZADANIE 22)
        // Zamiana getterów i setterów pola Nazwa na właściwości C#
        public string Nazwa
        {
            get { return _nazwa; }
            set { _nazwa = value; }
        }

        // GETTERY (ZADANIE 19)
        // Dla gatunku i liczby nóg pozostawiamy gettery (według treści zadania tylko Nazwa miała być zmieniona na Property, ale w praktyce w C# robi się Properties dla wszystkiego)
        public string GetGatunek()
        {
            return gatunek;
        }

        public int GetLiczbaNog()
        {
            return liczbaNog;
        }

        // METODA STATYCZNA (ZADANIE 19)
        // Zwraca aktualną liczbę zwierząt
        public static int DajLiczbeZwierzat()
        {
            return liczbaZwierzat;
        }

        // KONSTRUKTORY (ZADANIE 19)

        // 1. Konstruktor bezparametrowy (domyślny)
        public Zwierze()
        {
            this._nazwa = "Burek";
            this.gatunek = "Pies";
            this.liczbaNog = 4;
            liczbaZwierzat++; // Inkrementacja licznika
        }

        // 2. Konstruktor parametryczny
        public Zwierze(string nazwa, string gatunek, int liczbaNog)
        {
            this._nazwa = nazwa;
            this.gatunek = gatunek;
            this.liczbaNog = liczbaNog;
            liczbaZwierzat++;
        }

        // 3. Konstruktor kopiujący
        public Zwierze(Zwierze inneZwierze)
        {
            this._nazwa = inneZwierze._nazwa;
            this.gatunek = inneZwierze.gatunek;
            this.liczbaNog = inneZwierze.liczbaNog;
            liczbaZwierzat++; // Kopia to nowy obiekt, więc zwiększamy licznik
        }

        // METODA DAJ_GLOS (ZADANIE 19)
        public void DajGlos()
        {
            string dzwiek = "Nieznany dźwięk";
            string g = gatunek.ToLower(); // dla ułatwienia porównania

            if (g.Contains("pies")) dzwiek = "Hau hau!";
            else if (g.Contains("kot")) dzwiek = "Miau!";
            else if (g.Contains("krowa")) dzwiek = "Muuu!";
            else if (g.Contains("wąż")) dzwiek = "Ssssyk!";

            Console.WriteLine($"{_nazwa} ({gatunek}) mówi: {dzwiek}");
        }

        // Metoda pomocnicza do wyświetlania stanu obiektu
        public void PokazInfo()
        {
            Console.WriteLine($"[Info] Imię: {Nazwa}, Gatunek: {gatunek}, Nogi: {liczbaNog}");
        }

        // DESTRUKTOR (ZADANIE 23)
        // W .NET destruktory są wywoływane przez Garbage Collector.
        // Nie mamy gwarancji, kiedy dokładnie komunikat się pojawi.
        ~Zwierze()
        {
            // Uwaga: Wypisywanie w konsoli z destruktora może nie zawsze być widoczne przy zamykaniu aplikacji,
            // ale służy tu celom edukacyjnym zgodnie z zadaniem.
            System.Diagnostics.Debug.WriteLine($"Zwierzę {this._nazwa} jest usuwane z pamięci.");
        }
    }

    // ZADANIE 18: Klasa Program
    class Program
    {
        static void Main(string[] args)
        {
            // Zadanie 18: Tekst powitalny
            Console.WriteLine("To jest cwiczenie 1");
            Console.WriteLine("-------------------");

            // Lista do przechowywania obiektów (dla porządku)
            List<Zwierze> listaZwierzat = new List<Zwierze>();

            // ZADANIE 21: Pętla pytająca o 3 zwierzęta
            for (int i = 1; i <= 3; i++)
            {
                Console.WriteLine($"\n--- Tworzenie zwierzęcia nr {i} ---");

                Console.Write("Podaj nazwę: ");
                string nazwa = Console.ReadLine();

                Console.Write("Podaj gatunek (pies/kot/krowa/inne): ");
                string gatunek = Console.ReadLine();

                Console.Write("Podaj liczbę nóg: ");
                int nogi;
                // Zabezpieczenie przed wpisaniem tekstu zamiast liczby
                while (!int.TryParse(Console.ReadLine(), out nogi))
                {
                    Console.Write("To nie jest liczba. Podaj liczbę nóg: ");
                }

                // Tworzenie obiektu konstruktorem z parametrami
                Zwierze noweZwierze = new Zwierze(nazwa, gatunek, nogi);
                listaZwierzat.Add(noweZwierze);
            }

            Console.WriteLine("\n--- Test konstruktora kopiującego (Klonowanie) ---");
            // ZADANIE 21 cd.: Klonowanie i zmiana imienia
            if (listaZwierzat.Count > 0)
            {
                Zwierze oryginal = listaZwierzat[0];
                Zwierze klon = new Zwierze(oryginal); // Użycie konstruktora kopiującego

                Console.WriteLine($"Sklonowano zwierzę: {oryginal.Nazwa}");

                // Zmiana imienia klona
                klon.Nazwa = "Klon_" + original.Nazwa;
                Console.WriteLine($"Nowe imię klona: {klon.Nazwa}");

                listaZwierzat.Add(klon);
            }

            Console.WriteLine("\n--- Prezentacja zwierząt ---");
            // ZADANIE 21 cd.: Wypisanie info i daj_glos dla wszystkich
            foreach (var z in listaZwierzat)
            {
                z.PokazInfo();
                z.DajGlos();
            }

            Console.WriteLine("\n-------------------");
            // ZADANIE 21 cd.: Wywołanie statycznej funkcji
            Console.WriteLine($"Liczba utworzonych obiektów Zwierze: {Zwierze.DajLiczbeZwierzat()}");

            // Zatrzymanie programu, aby zobaczyć wyniki
            Console.WriteLine("\nNaciśnij dowolny klawisz, aby zakończyć (i wywołać destruktory)...");
            Console.ReadKey();

            // Po zamknięciu programu Garbage Collector zacznie sprzątać obiekty,
            // co teoretycznie uruchomi destruktory (~Zwierze).
        }
    }
}