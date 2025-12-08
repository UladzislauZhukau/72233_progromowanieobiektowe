using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Xml.Serialization;

public class Student
{
    public string Imie { get; set; }
    public string Nazwisko { get; set; }
    public List<int> Oceny { get; set; }
}

class Program
{
    static void Main()
    {
        // Wywołuj funkcje według potrzeb, np.:

        //ZapisTekstuDoPliku();
        //OdczytZPliku();
        //DopiszDoPliku();
        //ZapisStudentowJSON();
        //OdczytStudentowJSON();
        //ZapisStudentowXML();
        //OdczytStudentowXML();
        //OdczytCSV();
        //SrednieKolumnCSV();
        //FiltrujIrisDoCSV();
    }

    // ------------------- ZADANIE 2 -------------------
    static void ZapisTekstuDoPliku()
    {
        Console.WriteLine("Ile linii tekstu chcesz wpisać?");
        int n = int.Parse(Console.ReadLine());

        using StreamWriter sw = new StreamWriter("dane.txt");

        for (int i = 0; i < n; i++)
        {
            Console.Write("Podaj tekst: ");
            string tekst = Console.ReadLine();
            sw.WriteLine(tekst);
        }

        Console.WriteLine("Zapisano dane do pliku dane.txt");
    }

    // ------------------- ZADANIE 3 -------------------
    static void OdczytZPliku()
    {
        if (!File.Exists("dane.txt"))
        {
            Console.WriteLine("Brak pliku dane.txt");
            return;
        }

        string[] linie = File.ReadAllLines("dane.txt");

        foreach (string linia in linie)
            Console.WriteLine(linia);
    }
    // ------------------- ZADANIE 4 -------------------
    static void DopiszDoPliku()
    {
        using StreamWriter sw = new StreamWriter("dane.txt", append: true);

        Console.Write("Ile linii chcesz dopisać? ");
        int n = int.Parse(Console.ReadLine());

        for (int i = 0; i < n; i++)
        {
            Console.Write("Podaj tekst: ");
            string tekst = Console.ReadLine();
            sw.WriteLine(tekst);
        }

        Console.WriteLine("Dopisano dane do pliku dane.txt");
    }

    // ------------------- ZADANIE 6 -------------------
    static void ZapisStudentowJSON()
    {
        List<Student> studenci = new()
        {
            new Student { Imie="Jan", Nazwisko="Kowalski", Oceny=new List<int>{5,4,3}},
            new Student { Imie="Anna", Nazwisko="Nowak", Oceny=new List<int>{4,5,5}},
            new Student { Imie="Piotr", Nazwisko="Zieliński", Oceny=new List<int>{3,3,4}}
        };

        string json = JsonSerializer.Serialize(studenci, new JsonSerializerOptions { WriteIndented = true });

        File.WriteAllText("studenci.json", json);

        Console.WriteLine("Zapisano dane studentów do studenci.json");
    }

    // ------------------- ZADANIE 7 -------------------
    static void OdczytStudentowJSON()
    {
        if (!File.Exists("studenci.json"))
        {
            Console.WriteLine("Brak pliku studenci.json");
            return;
        }

        string json = File.ReadAllText("studenci.json");

        var studenci = JsonSerializer.Deserialize<List<Student>>(json);

        foreach (var s in studenci)
        {
            Console.WriteLine($"{s.Imie} {s.Nazwisko} — Oceny: {string.Join(",", s.Oceny)}");
        }
    }
  
    //-------------------- ZADANIE 8 -------------------
    static void ZapisStudentowXML()
    {
        List<Student> studenci = new()
        {
            new Student { Imie="Jan", Nazwisko="Kowalski", Oceny=new List<int>{5,4,3}},
            new Student { Imie="Anna", Nazwisko="Nowak", Oceny=new List<int>{4,5,5}},
            new Student { Imie="Piotr", Nazwisko="Zieliński", Oceny=new List<int>{3,3,4}}
        };

        XmlSerializer xs = new(typeof(List<Student>));
        using FileStream fs = new("studenci.xml", FileMode.Create);

        xs.Serialize(fs, studenci);

        Console.WriteLine("Zapisano studentów do studenci.xml");
    }

    //-------------------- ZADANIE 9 -------------------
    static void OdczytStudentowXML()
    {
        if (!File.Exists("studenci.xml"))
        {
            Console.WriteLine("Brak pliku studenci.xml");
            return;
        }

        XmlSerializer xs = new(typeof(List<Student>));
        using FileStream fs = new("studenci.xml", FileMode.Open);

        var studenci = (List<Student>)xs.Deserialize(fs);

        foreach (var s in studenci)
        {
            Console.WriteLine($"{s.Imie} {s.Nazwisko} — Oceny: {string.Join(",", s.Oceny)}");
        }
    }

    //-------------------- ZADANIA 10–12 -------------------
    static void OdczytCSV()
    {
        if (!File.Exists("iris.csv"))
        {
            Console.WriteLine("Brak pliku iris.csv");
            return;
        }

        var linie = File.ReadAllLines("iris.csv");

        foreach (var l in linie)
            Console.WriteLine(l);
    }

    static void SrednieKolumnCSV()
    {
        if (!File.Exists("iris.csv"))
        {
            Console.WriteLine("Brak pliku iris.csv");
            return;
        }

        var linie = File.ReadAllLines("iris.csv");
        var naglowki = linie[0].Split(',');

        double[] suma = new double[naglowki.Length - 1];
        int licznik = 0;

        for (int i = 1; i < linie.Length; i++)
        {
            var pola = linie[i].Split(',');

            for (int j = 0; j < pola.Length - 1; j++)
                suma[j] += double.Parse(pola[j]);

            licznik++;
        }

        Console.WriteLine("Średnie wartości kolumn:");
        for (int i = 0; i < suma.Length; i++)
            Console.WriteLine($"{naglowki[i]} = {suma[i] / licznik:F2}");
    }

    static void FiltrujIrisDoCSV()
    {
        if (!File.Exists("iris.csv"))
        {
            Console.WriteLine("Brak pliku iris.csv");
            return;
        }

        var linie = File.ReadAllLines("iris.csv");
        var naglowki = linie[0].Split(',');

        int idxSepalLength = Array.IndexOf(naglowki, "sepal_length");
        int idxSepalWidth = Array.IndexOf(naglowki, "sepal_width");
        int idxClass = Array.IndexOf(naglowki, "class");

        List<string> wynik = new();
        wynik.Add("sepal_length,sepal_width,class");

        for (int i = 1; i < linie.Length; i++)
        {
            var pola = linie[i].Split(',');

            double sl = double.Parse(pola[idxSepalLength]);

            if (sl < 5)
            {
                string nowaLinia =
                    $"{pola[idxSepalLength]},{pola[idxSepalWidth]},{pola[idxClass]}";
                wynik.Add(nowaLinia);
            }
        }

        File.WriteAllLines("iris_filtered.csv", wynik);

        Console.WriteLine("Zapisano iris_filtered.csv");
    }
}

