using System;
using System.Collections.Generic; // Do obsługi List, HashSet, Dictionary
using System.Linq; // Niezbędne do metod rozszerzających: OrderBy, Where, Min, Max, ElementAt

namespace Laboratorium3
{
    // ZADANIE 1 (cd.): Interfejs IModular
    public interface IModular
    {
        double Module();
    }

    // ZADANIE 1: Klasa ComplexNumber
    // Dodano: IComparable<ComplexNumber> do porównywania obiektów (potrzebne do sortowania)
    public class ComplexNumber : ICloneable, IEquatable<ComplexNumber>, IModular, IComparable<ComplexNumber>
    {
        private double re;
        private double im;

        // Używamy zapisu lambda (expression-bodied members) zgodnie z Twoim przykładem
        public double Re { get => re; set => re = value; }
        public double Im { get => im; set => im = value; }

        public ComplexNumber(double re, double im)
        {
            this.re = re;
            this.im = im;
        }

        public override string ToString()
        {
            // Formatowanie wyświetlania
            string sign = im >= 0 ? "+" : "-";
            return $"{re} {sign} {Math.Abs(im)}i";
        }

        // Operatory
        public static ComplexNumber operator +(ComplexNumber a, ComplexNumber b)
            => new ComplexNumber(a.re + b.re, a.im + b.im);

        public static ComplexNumber operator -(ComplexNumber a, ComplexNumber b)
            => new ComplexNumber(a.re - b.re, a.im - b.im);

        public static ComplexNumber operator *(ComplexNumber a, ComplexNumber b)
            => new ComplexNumber(a.re * b.re - a.im * b.im, a.re * b.im + a.im * b.re);

        public static ComplexNumber operator -(ComplexNumber a)
            => new ComplexNumber(a.re, -a.im);

        // Klonowanie
        public object Clone() => new ComplexNumber(re, im);

        // Równość (IEquatable)
        public bool Equals(ComplexNumber other)
        {
            if (other is null) return false;
            return re == other.re && im == other.im;
        }

        public override bool Equals(object obj)
            => obj is ComplexNumber other && Equals(other);

        // Ważne dla HashSet i Dictionary!
        public override int GetHashCode()
        {
            // Zmieniono HashCode.Combine na wersję klasyczną (działa w starszych .NET Framework)
            unchecked // Pozwala na przekręcenie licznika (overflow) bez błędu
            {
                int hash = 17;
                hash = hash * 23 + re.GetHashCode();
                hash = hash * 23 + im.GetHashCode();
                return hash;
            }
        }

        public static bool operator ==(ComplexNumber a, ComplexNumber b)
            => a?.Equals(b) ?? b is null;

        public static bool operator !=(ComplexNumber a, ComplexNumber b)
            => !(a == b);

        public double Module()
            => Math.Sqrt(re * re + im * im);

        // ZADANIE 1: Implementacja IComparable
        // Pozwala sortować obiekty (np. Array.Sort, list.Sort)
        // Porównujemy wg modułu liczby zespolonej.
        public int CompareTo(ComplexNumber other)
        {
            if (other == null) return 1; // Obiekt istniejący jest "większy" od nulla
            return this.Module().CompareTo(other.Module());
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Przygotowanie danych testowych
            ComplexNumber z1 = new ComplexNumber(6, 7);
            ComplexNumber z2 = new ComplexNumber(1, 2);
            ComplexNumber z3 = new ComplexNumber(6, 7); // To samo co z1
            ComplexNumber z4 = new ComplexNumber(1, -2);
            ComplexNumber z5 = new ComplexNumber(-5, 9);

            // ==========================================
            // ZADANIE 2: TABLICE
            // ==========================================
            Console.WriteLine("=== ZADANIE 2: TABLICE ===");
            ComplexNumber[] tablica = { z1, z2, z4, z5, new ComplexNumber(0, 0) };

            // 2a. Wypisanie pętlą foreach
            Console.WriteLine("Tablica oryginalna:");
            foreach (var num in tablica) Console.Write($"[{num}] ");
            Console.WriteLine();

            // 2b. Sortowanie w oparciu o moduł (dzięki IComparable)
            Array.Sort(tablica);
            Console.WriteLine("\nTablica posortowana (wg modułu):");
            foreach (var num in tablica) Console.Write($"[{num} (mod:{num.Module():F2})] ");
            Console.WriteLine();

            // 2c. Minimum i Maksimum tablicy (dzięki IComparable działa Max() i Min())
            Console.WriteLine($"\nMinimum (wg modułu): {tablica.Min()}");
            Console.WriteLine($"Maksimum (wg modułu): {tablica.Max()}");

            // 2d. Filtrowanie (część urojona ujemna) - używamy LINQ Where
            Console.WriteLine("\nLiczby z ujemną częścią urojoną:");
            var ujemneIm = tablica.Where(n => n.Im < 0);
            foreach (var num in ujemneIm) Console.Write($"[{num}] ");
            Console.WriteLine("\n");


            // ==========================================
            // ZADANIE 3: LISTY
            // ==========================================
            Console.WriteLine("=== ZADANIE 3: LISTY ===");
            List<ComplexNumber> lista = new List<ComplexNumber> { z1, z2, z4, z5, new ComplexNumber(10, 10) };

            Console.WriteLine("Lista początkowa:");
            lista.ForEach(n => Console.Write($"[{n}] ")); // Metoda pomocnicza Listy
            Console.WriteLine();

            // 3a. Usuń drugi element (indeks 1)
            if (lista.Count > 1)
            {
                lista.RemoveAt(1);
                Console.WriteLine("\nPo usunięciu 2. elementu:");
                lista.ForEach(n => Console.Write($"[{n}] "));
            }

            // 3b. Usuń najmniejszy element
            if (lista.Count > 0)
            {
                ComplexNumber minVal = lista.Min(); // Znajdujemy najmniejszy (dzięki IComparable)
                lista.Remove(minVal);               // Usuwamy go
                Console.WriteLine($"\n\nPo usunięciu najmniejszego ({minVal}):");
                lista.ForEach(n => Console.Write($"[{n}] "));
            }

            // 3c. Wyczyść listę
            lista.Clear();
            Console.WriteLine($"\n\nRozmiar listy po Clear(): {lista.Count}");
            Console.WriteLine();


            // ==========================================
            // ZADANIE 4: HASHSET (ZBIÓR)
            // ==========================================
            Console.WriteLine("=== ZADANIE 4: HASHSET ===");
            // HashSet przechowuje UNIKALNE wartości. 
            // Dzięki poprawnemu GetHashCode i Equals, z1 i z3 zostaną potraktowane jako jeden element.
            HashSet<ComplexNumber> zbior = new HashSet<ComplexNumber>();
            zbior.Add(z1); // 6 + 7i
            zbior.Add(z2); // 1 + 2i
            zbior.Add(z3); // 6 + 7i (Duplikat wartości z1 - nie zostanie dodany!)
            zbior.Add(z4); // 1 - 2i
            zbior.Add(z5); // -5 + 9i

            // 4a. Sprawdź zawartość
            Console.WriteLine($"Elementy w zbiorze ({zbior.Count}):");
            foreach (var item in zbior) Console.Write($"[{item}] ");
            Console.WriteLine("\n(Zauważ, że z3 nie zostało dodane jako osobny element, bo jest identyczne jak z1)");

            // 4b. Operacje na zbiorze (Min, Max, Filtrowanie działają przez LINQ)
            // Sortowanie zbioru nie ma sensu (HashSet jest nieuporządkowany), 
            // ale można pobrać posortowane wyniki do wyświetlenia.
            Console.WriteLine($"Max w zbiorze: {zbior.Max()}");
            Console.WriteLine($"Min w zbiorze: {zbior.Min()}");
            Console.WriteLine("Posortowany widok zbioru:");
            foreach (var item in zbior.OrderBy(x => x)) Console.Write($"[{item}] "); // OrderBy używa IComparable
            Console.WriteLine("\n");


            // ==========================================
            // ZADANIE 5: SŁOWNIK (DICTIONARY)
            // ==========================================
            Console.WriteLine("=== ZADANIE 5: SŁOWNIK ===");
            Dictionary<string, ComplexNumber> slownik = new Dictionary<string, ComplexNumber>
            {
                { "z1", z1 },
                { "z2", z2 },
                { "z3", z3 }, // Tutaj klucz jest inny ("z3"), więc wartość może być taka sama jak z1
                { "z4", z4 },
                { "z5", z5 }
            };

            // 5a. Wypisz (Klucz, Wartość)
            Console.WriteLine("Zawartość słownika:");
            foreach (KeyValuePair<string, ComplexNumber> kvp in slownik)
            {
                Console.WriteLine($"Klucz: {kvp.Key}, Wartość: {kvp.Value}");
            }

            // 5b. Osobno klucze i wartości
            Console.Write("\nKlucze: ");
            foreach (var k in slownik.Keys) Console.Write(k + " ");
            Console.Write("\nWartości: ");
            foreach (var v in slownik.Values) Console.Write(v + " ");
            Console.WriteLine();

            // 5c. Czy istnieje klucz "z6"
            bool hasZ6 = slownik.ContainsKey("z6");
            Console.WriteLine($"\nCzy słownik zawiera klucz 'z6'? {hasZ6}");

            // 5d. Zadania 2c i 2d na wartościach słownika
            Console.WriteLine($"Maksimum ze słownika (wg Values): {slownik.Values.Max()}");
            Console.Write("Filtrowanie słownika (tylko ujemne urojone): ");
            foreach (var v in slownik.Values.Where(c => c.Im < 0)) Console.Write(v + " ");
            Console.WriteLine();

            // 5e. Usuń element o kluczu "z3"
            slownik.Remove("z3");
            Console.WriteLine("\nUsunięto klucz 'z3'.");

            // 5f. Usuń drugi element ze słownika
            // Słowniki teoretycznie nie mają indeksów, ale możemy użyć LINQ ElementAt
            if (slownik.Count >= 2)
            {
                var kluczDoUsuniecia = slownik.Keys.ElementAt(1); // Pobieramy klucz drugiego elementu
                slownik.Remove(kluczDoUsuniecia);
                Console.WriteLine($"Usunięto drugi element (klucz: {kluczDoUsuniecia}).");
            }

            // 5g. Wyczyść słownik
            slownik.Clear();
            Console.WriteLine($"Rozmiar słownika po Clear(): {slownik.Count}");

            Console.WriteLine("\nNaciśnij dowolny klawisz...");
            Console.ReadKey();
        }
    }
}