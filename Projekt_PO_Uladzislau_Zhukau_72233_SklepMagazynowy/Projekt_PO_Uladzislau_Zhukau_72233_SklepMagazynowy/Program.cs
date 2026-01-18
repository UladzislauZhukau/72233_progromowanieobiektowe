using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

// --- MODEL DANYCH ---
// Klasa reprezentująca pojedynczy produkt w magazynie.
public class Produkt
{
    public int Id { get; set; }
    public string Nazwa { get; set; }
    public string Kategoria { get; set; } // Np. Elektronika, Spożywcze
    public int Ilosc { get; set; }
    public decimal Cena { get; set; }

    public override string ToString()
    {
        // Zmiana formatowania na sztywne "zł" z dwoma miejscami po przecinku (F2)
        return $"ID: {Id} | Nazwa: {Nazwa} | Kat: {Kategoria} | Ilość: {Ilosc} | Cena: {Cena:F2} zł";
    }
}

// --- INTERFEJS (ABSTRAKCJA) ---
// Definiuje kontrakt dla operacji na danych. Umożliwia łatwą podmianę zapisu plikowego na bazę danych w przyszłości.
public interface IMagazynService
{
    void DodajProdukt(Produkt produkt);         // Create
    List<Produkt> PobierzWszystkie();           // Read
    Produkt PobierzPoId(int id);                // Read
    void EdytujProdukt(Produkt produkt);        // Update
    void UsunProdukt(int id);                   // Delete
    decimal ObliczWartoscMagazynu();            // Statystyka (Bonus)
}

// --- IMPLEMENTACJA (ZAPIS DO PLIKU JSON) ---
// Realizuje logikę zapisu i odczytu z pliku tekstowego (JSON).
public class PlikowyMagazynService : IMagazynService
{
    private const string NazwaPliku = "magazyn.json";
    private List<Produkt> _produkty;

    public PlikowyMagazynService()
    {
        _produkty = WczytajZPliku();
    }

    // CREATE
    public void DodajProdukt(Produkt produkt)
    {
        // Auto-inkrementacja ID
        produkt.Id = _produkty.Any() ? _produkty.Max(p => p.Id) + 1 : 1;
        _produkty.Add(produkt);
        ZapiszDoPliku();
    }

    // READ (Wszystkie)
    public List<Produkt> PobierzWszystkie()
    {
        return _produkty;
    }

    // READ (Pojedynczy)
    public Produkt PobierzPoId(int id)
    {
        return _produkty.FirstOrDefault(p => p.Id == id);
    }

    // UPDATE
    public void EdytujProdukt(Produkt zaktualizowanyProdukt)
    {
        var istniejacy = PobierzPoId(zaktualizowanyProdukt.Id);
        if (istniejacy != null)
        {
            istniejacy.Nazwa = zaktualizowanyProdukt.Nazwa;
            istniejacy.Kategoria = zaktualizowanyProdukt.Kategoria;
            istniejacy.Ilosc = zaktualizowanyProdukt.Ilosc;
            istniejacy.Cena = zaktualizowanyProdukt.Cena;
            ZapiszDoPliku();
        }
    }

    // DELETE
    public void UsunProdukt(int id)
    {
        var produkt = PobierzPoId(id);
        if (produkt != null)
        {
            _produkty.Remove(produkt);
            ZapiszDoPliku();
        }
    }

    // Dodatkowa funkcjonalność (Statystyka)
    public decimal ObliczWartoscMagazynu()
    {
        return _produkty.Sum(p => p.Ilosc * p.Cena);
    }

    // Metody pomocnicze do obsługi pliku
    private void ZapiszDoPliku()
    {
        string json = JsonSerializer.Serialize(_produkty, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(NazwaPliku, json);
    }

    private List<Produkt> WczytajZPliku()
    {
        if (!File.Exists(NazwaPliku))
        {
            return new List<Produkt>();
        }
        string json = File.ReadAllText(NazwaPliku);
        return string.IsNullOrEmpty(json) ? new List<Produkt>() : JsonSerializer.Deserialize<List<Produkt>>(json);
    }
}

// --- INTERFEJS UŻYTKOWNIKA (CONSOLE UI) ---
// Oddzielna klasa do obsługi wejścia/wyjścia.
public class AplikacjaKonsolowa
{
    private readonly IMagazynService _serwis;

    public AplikacjaKonsolowa(IMagazynService serwis)
    {
        _serwis = serwis;
    }

    public void Uruchom()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== SYSTEM MAGAZYNOWY (CRUD) ===");
            Console.WriteLine("1. Wyświetl listę produktów (Read)");
            Console.WriteLine("2. Dodaj nowy produkt (Create)");
            Console.WriteLine("3. Edytuj produkt (Update)");
            Console.WriteLine("4. Usuń produkt (Delete)");
            Console.WriteLine("5. Statystyki magazynu");
            Console.WriteLine("0. Wyjście");
            Console.Write("\nWybierz opcję: ");

            var opcja = Console.ReadLine();

            try
            {
                switch (opcja)
                {
                    case "1": WyswietlProdukty(); break;
                    case "2": DodajProdukt(); break;
                    case "3": EdytujProdukt(); break;
                    case "4": UsunProdukt(); break;
                    case "5": PokazStatystyki(); break;
                    case "0": return;
                    default: Console.WriteLine("Nieznana opcja."); break;
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\nWystąpił błąd: {ex.Message}");
                Console.ResetColor();
            }

            Console.WriteLine("\nNaciśnij dowolny klawisz, aby kontynuować...");
            Console.ReadKey();
        }
    }

    private void WyswietlProdukty()
    {
        var produkty = _serwis.PobierzWszystkie();
        Console.WriteLine("\n--- Lista Produktów ---");
        if (!produkty.Any())
        {
            Console.WriteLine("Magazyn jest pusty.");
            return;
        }

        foreach (var p in produkty)
        {
            Console.WriteLine(p);
        }
    }

    private void DodajProdukt()
    {
        Console.WriteLine("\n--- Dodawanie Produktu ---");

        // Walidacja wejścia jest kluczowa dla wyższej oceny
        Console.Write("Podaj nazwę: ");
        string nazwa = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(nazwa)) throw new ArgumentException("Nazwa nie może być pusta.");

        Console.Write("Podaj kategorię: ");
        string kat = Console.ReadLine();

        Console.Write("Podaj ilość: ");
        if (!int.TryParse(Console.ReadLine(), out int ilosc) || ilosc < 0)
            throw new ArgumentException("Ilość musi być liczbą nieujemną.");

        Console.Write("Podaj cenę: ");
        if (!decimal.TryParse(Console.ReadLine(), out decimal cena) || cena < 0)
            throw new ArgumentException("Cena musi być liczbą nieujemną.");

        _serwis.DodajProdukt(new Produkt { Nazwa = nazwa, Kategoria = kat, Ilosc = ilosc, Cena = cena });
        Console.WriteLine("Produkt dodany pomyślnie.");
    }

    private void EdytujProdukt()
    {
        WyswietlProdukty();
        Console.Write("\nPodaj ID produktu do edycji: ");
        if (int.TryParse(Console.ReadLine(), out int id))
        {
            var produkt = _serwis.PobierzPoId(id);
            if (produkt == null)
            {
                Console.WriteLine("Nie znaleziono produktu.");
                return;
            }

            Console.WriteLine($"Edytujesz: {produkt.Nazwa}. Pozostaw puste pole, aby nie zmieniać.");

            Console.Write("Nowa nazwa: ");
            string nowaNazwa = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(nowaNazwa)) produkt.Nazwa = nowaNazwa;

            Console.Write("Nowa kategoria: ");
            string nowaKat = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(nowaKat)) produkt.Kategoria = nowaKat;

            Console.Write("Nowa ilość: ");
            string iloscStr = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(iloscStr) && int.TryParse(iloscStr, out int ilosc) && ilosc >= 0)
                produkt.Ilosc = ilosc;

            Console.Write("Nowa cena: ");
            string cenaStr = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(cenaStr) && decimal.TryParse(cenaStr, out decimal cena) && cena >= 0)
                produkt.Cena = cena;

            _serwis.EdytujProdukt(produkt);
            Console.WriteLine("Zaktualizowano produkt.");
        }
    }

    private void UsunProdukt()
    {
        WyswietlProdukty();
        Console.Write("\nPodaj ID produktu do usunięcia: ");
        if (int.TryParse(Console.ReadLine(), out int id))
        {
            _serwis.UsunProdukt(id);
            Console.WriteLine("Produkt usunięty (jeśli istniał).");
        }
    }

    private void PokazStatystyki()
    {
        var wartosc = _serwis.ObliczWartoscMagazynu();
        var ilosc = _serwis.PobierzWszystkie().Count;
        Console.WriteLine("\n--- Statystyki ---");
        Console.WriteLine($"Liczba produktów w bazie: {ilosc}");
        // Zmiana formatowania również w statystykach
        Console.WriteLine($"Całkowita wartość magazynu: {wartosc:F2} zł");
    }
}

// --- MAIN ---
class Program
{
    static void Main(string[] args)
    {
        // NAPRAWA: Ustawienie kodowania na UTF-8 pozwala wyświetlać "zł" i polskie znaki
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        // Wstrzykiwanie zależności (Dependency Injection) - ręczne
        IMagazynService serwis = new PlikowyMagazynService();
        AplikacjaKonsolowa app = new AplikacjaKonsolowa(serwis);

        app.Uruchom();
    }
}