using Microsoft.Data.SqlClient;
using System.Collections.Generic;

public class Student
{
    public int StudentId { get; set; }
    public string Imie { get; set; } = "";
    public string Nazwisko { get; set; } = "";
    public List<Ocena> Oceny { get; set; } = new();
}

public class Ocena
{
    public int OcenaId { get; set; }
    public double Wartosc { get; set; }
    public string Przedmiot { get; set; } = "";
    public int StudentId { get; set; }
}

public class Program
{
    
    static string connectionString =
        "Data Source=10.200.2.28;" +
        "Initial Catalog=studenci_72233;" +
        "Integrated Security=True;" +
        "Encrypt=True;" +
        "TrustServerCertificate=True";

    public static void Main()
    {
        try
        {
            using SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            Console.WriteLine("Połączono z bazą.");


            //WyswietlStudentow();

            //WyswietlStudentaPoId(1);

            //List<Student> studenci = PobierzStudentowZOcenami();
            //WyswietlStudentowZOcenami(studenci);

            //Student nowy = new Student
            //{
            //    Imie = "Jan",
            //    Nazwisko = "Kowalski"
            //};
            //DodajStudenta(nowy);

            //Ocena ocena = new Ocena
            //{
            //    StudentId = 1,
            //    Przedmiot = "Matematyka",
            //    Wartosc = 4.5
            //};
            //DodajOcene(ocena);

            //UsunOcenyZGeografii();

            //AktualizujOcene(3, 5.0); 
        }
        catch (Exception exc)
        {
            Console.WriteLine("Wystąpił błąd: " + exc.Message);
        }
        
    }
    //zadanie 4
    static void WyswietlStudentow()
    {
        using SqlConnection conn = new(connectionString);
        conn.Open();

        SqlCommand cmd = new(
            "SELECT Student_Id, Imie, Nazwisko FROM student", conn);

        using SqlDataReader reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            Console.WriteLine(
                $"{reader["Student_Id"]} {reader["Imie"]} {reader["Nazwisko"]}");
        }
    }
    //zadanie 5
    static void WyswietlStudentaPoId(int id)
    {
        using SqlConnection conn = new(connectionString);
        conn.Open();

        SqlCommand cmd = new(
            "SELECT Imie, Nazwisko FROM student WHERE Student_Id = @id", conn);
        cmd.Parameters.AddWithValue("@id", id);

        using SqlDataReader reader = cmd.ExecuteReader();

        if (reader.Read())
        {
            Console.WriteLine($"Student: {reader["Imie"]} {reader["Nazwisko"]}");
        }
    }
    //zadanie 6
    static List<Student> PobierzStudentowZOcenami()
    {
        List<Student> lista = new();

        using SqlConnection conn = new(connectionString);
        conn.Open();

        string sql = @"
            SELECT s.Student_Id, s.Imie, s.Nazwisko,
                   o.Ocena_Id, o.Przedmiot, o.Wartosc
            FROM student s
            LEFT JOIN ocena o ON s.Student_Id = o.Student_Id";

        SqlCommand cmd = new(sql, conn);
        using SqlDataReader reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            int id = (int)reader["Student_Id"];

            var student = lista.FirstOrDefault(s => s.StudentId == id);
            if (student == null)
            {
                student = new Student
                {
                    StudentId = id,
                    Imie = reader["Imie"].ToString(),
                    Nazwisko = reader["Nazwisko"].ToString()
                };
                lista.Add(student);
            }

            if (reader["Wartosc"] != DBNull.Value)
            {
                student.Oceny.Add(new Ocena
                {
                    OcenaId = (int)reader["Ocena_Id"],
                    Przedmiot = reader["Przedmiot"].ToString(),
                    Wartosc = (double)reader["Wartosc"],
                    StudentId = id
                });
            }
        }
        return lista;
    }

    static void WyswietlStudentowZOcenami(List<Student> studenci)
    {
        foreach (var s in studenci)
        {
            Console.WriteLine($"{s.Imie} {s.Nazwisko}");
            foreach (var o in s.Oceny)
                Console.WriteLine($"  {o.Przedmiot}: {o.Wartosc}");
        }
    }
    //zadanie 7
    static void DodajStudenta(Student s)
    {
        using SqlConnection conn = new(connectionString);
        conn.Open();

        SqlCommand cmd = new(
            "INSERT INTO student(Imie, Nazwisko) VALUES (@i,@n)", conn);

        cmd.Parameters.AddWithValue("@i", s.Imie);
        cmd.Parameters.AddWithValue("@n", s.Nazwisko);

        cmd.ExecuteNonQuery();
    }
    // zadanie 8 
    static bool CzyPoprawnaOcena(double o)
    {
        return o >= 2 && o <= 5 &&
               (o % 1 == 0 || o % 1 == 0.5) &&
               o != 2.5;
    }

    static void DodajOcene(Ocena o)
    {
        if (!CzyPoprawnaOcena(o.Wartosc))
            throw new ArgumentException("Niepoprawna wartość oceny");

        using SqlConnection conn = new(connectionString);
        conn.Open();

        SqlCommand cmd = new(
            "INSERT INTO ocena(Student_Id, Przedmiot, Wartosc) VALUES (@s,@p,@w)", conn);

        cmd.Parameters.AddWithValue("@s", o.StudentId);
        cmd.Parameters.AddWithValue("@p", o.Przedmiot);
        cmd.Parameters.AddWithValue("@w", o.Wartosc);

        cmd.ExecuteNonQuery();
    }
    //zadanie 9 
    static void UsunOcenyZGeografii()
    {
        using SqlConnection conn = new(connectionString);
        conn.Open();

        SqlCommand cmd = new(
            "DELETE FROM ocena WHERE Przedmiot = 'Geografia'", conn);

        cmd.ExecuteNonQuery();
    }
    //zadanie 10 
    static void AktualizujOcene(int ocenaId, double nowa)
    {
        if (!CzyPoprawnaOcena(nowa))
            throw new ArgumentException("Niepoprawna wartość oceny");

        using SqlConnection conn = new(connectionString);
        conn.Open();

        SqlCommand cmd = new(
            "UPDATE ocena SET Wartosc=@w WHERE Ocena_Id=@id", conn);

        cmd.Parameters.AddWithValue("@w", nowa);
        cmd.Parameters.AddWithValue("@id", ocenaId);

        cmd.ExecuteNonQuery();
    }
}