using System;

namespace Laboratorium3
{
    // ZADANIE 2: Interfejs IModular
    public interface IModular
    {
        double Module();
    }

    // ZADANIE 1: Klasa ComplexNumber
    // Implementuje ICloneable (kopiowanie), IEquatable (porównywanie) i IModular (zadanie 3)
    public class ComplexNumber : ICloneable, IEquatable<ComplexNumber>, IModular
    {
        // Pola prywatne
        private double re;
        private double im;

        // Publiczne właściwości
        public double Re
        {
            get { return re; }
            set { re = value; }
        }

        public double Im
        {
            get { return im; }
            set { im = value; }
        }

        // Konstruktor
        public ComplexNumber(double re, double im)
        {
            this.re = re;
            this.im = im;
        }

        // Przeciążenie ToString()
        public override string ToString()
        {
            // Formatowanie, aby obsłużyć znak minus (np. "6 - 7i" zamiast "6 + -7i")
            if (im < 0)
                return $"{re} - {-im}i";
            else
                return $"{re} + {im}i";
        }

        // Przeciążenie operatora dodawania (+)
        public static ComplexNumber operator +(ComplexNumber a, ComplexNumber b)
        {
            return new ComplexNumber(a.re + b.re, a.im + b.im);
        }

        // Przeciążenie operatora odejmowania (-)
        public static ComplexNumber operator -(ComplexNumber a, ComplexNumber b)
        {
            return new ComplexNumber(a.re - b.re, a.im - b.im);
        }

        // Przeciążenie operatora mnożenia (*)
        // Wzór: (a+bi)(c+di) = (ac−bd)+(ad+bc)i
        public static ComplexNumber operator *(ComplexNumber a, ComplexNumber b)
        {
            double newRe = (a.re * b.re) - (a.im * b.im);
            double newIm = (a.re * b.im) + (a.im * b.re);
            return new ComplexNumber(newRe, newIm);
        }

        // Przeciążenie operatora unarnego (-) realizującego sprzężenie
        // Treść zadania: -(a+bi) = a-bi
        public static ComplexNumber operator -(ComplexNumber a)
        {
            return new ComplexNumber(a.re, -a.im);
        }

        // Implementacja ICloneable
        public object Clone()
        {
            // Tworzymy płytką kopię (dla typów prostych double jest to wystarczające)
            return new ComplexNumber(this.re, this.im);
        }

        // ZADANIE 3: Implementacja metody Module z interfejsu IModular
        // |Z| = sqrt(Re^2 + Im^2)
        public double Module()
        {
            return Math.Sqrt(re * re + im * im);
        }

        // Implementacja IEquatable<ComplexNumber>
        public bool Equals(ComplexNumber other)
        {
            if (other == null) return false;
            // Porównujemy wartości z pewną tolerancją dla liczb zmiennoprzecinkowych, 
            // choć proste == dla double też często wystarcza w zadaniach akademickich.
            return this.re == other.re && this.im == other.im;
        }

        // Nadpisanie Equals(object) dla spójności
        public override bool Equals(object obj)
        {
            if (obj is ComplexNumber other)
            {
                return Equals(other);
            }
            return false;
        }

        // Nadpisanie GetHashCode (wymagane przy nadpisywaniu Equals)
        public override int GetHashCode()
        {
            // Prosty sposób łączenia hashcodów pól
            return HashCode.Combine(re, im);
        }

        // Przeciążenie operatorów porównania == i !=
        public static bool operator ==(ComplexNumber a, ComplexNumber b)
        {
            if (ReferenceEquals(a, null))
            {
                return ReferenceEquals(b, null);
            }
            return a.Equals(b);
        }

        public static bool operator !=(ComplexNumber a, ComplexNumber b)
        {
            return !(a == b);
        }
    }

    class Program
    {
        // ZADANIE 4: Punkt wejścia
        static void Main(string[] args)
        {
            Console.WriteLine("=== Liczby Zespolone - Testy ===");

            // 1. Tworzenie liczb
            ComplexNumber z1 = new ComplexNumber(3, 4);  // 3 + 4i
            ComplexNumber z2 = new ComplexNumber(1, -2); // 1 - 2i

            Console.WriteLine($"z1 = {z1}");
            Console.WriteLine($"z2 = {z2}");

            // 2. Dodawanie
            ComplexNumber suma = z1 + z2;
            Console.WriteLine($"\nDodawanie (z1 + z2): {suma}"); // Oczekiwane: 4 + 2i

            // 3. Odejmowanie
            ComplexNumber roznica = z1 - z2;
            Console.WriteLine($"Odejmowanie (z1 - z2): {roznica}"); // Oczekiwane: 2 + 6i

            // 4. Mnożenie
            ComplexNumber iloczyn = z1 * z2;
            Console.WriteLine($"Mnożenie (z1 * z2): {iloczyn}");
            // (3+4i)(1-2i) = (3 - (-8)) + (-6 + 4)i = 11 - 2i

            // 5. Sprzężenie (operator unarny - zgodnie z zadaniem)
            ComplexNumber sprzezenie = -z1;
            Console.WriteLine($"\nSprzężenie (-z1): {sprzezenie}"); // Oczekiwane: 3 - 4i

            // 6. Moduł
            Console.WriteLine($"\nModuł z1 (|z1|): {z1.Module()}"); // sqrt(9+16) = 5
            Console.WriteLine($"Moduł z2 (|z2|): {z2.Module():F2}"); // sqrt(1+4) = sqrt(5) ~= 2.24

            // 7. Klonowanie
            ComplexNumber klon = (ComplexNumber)z1.Clone();
            Console.WriteLine($"\nKlon z1: {klon}");

            // Modyfikacja klona nie powinna wpływać na oryginał
            klon.Re = 100;
            Console.WriteLine($"Klon po zmianie: {klon}");
            Console.WriteLine($"Oryginał z1: {z1}");

            // 8. Porównywanie (Equals, ==, !=)
            Console.WriteLine("\nPorównywanie:");
            ComplexNumber z3 = new ComplexNumber(3, 4);
            Console.WriteLine($"z1 == z3 (taka sama wartość): {z1 == z3}"); // True
            Console.WriteLine($"z1 == z2 (inna wartość): {z1 == z2}"); // False
            Console.WriteLine($"z1.Equals(z3): {z1.Equals(z3)}"); // True

            // Zadanie 6 (Informacyjnie): Klasy wewnętrzne
            // W C# można definiować klasy wewnątrz innych klas. 
            // Przykład (zakomentowany):
            // public class Zewnetrzna {
            //     private class Wewnetrzna { }
            // }
            // Służy to głównie do enkapsulacji logiki pomocniczej, która nie powinna być widoczna na zewnątrz.

            Console.ReadKey();
        }
    }
}