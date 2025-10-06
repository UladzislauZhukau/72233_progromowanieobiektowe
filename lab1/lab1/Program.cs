// See https://aka.ms/new-console-template for more information
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;

void main()
{
    Console.WriteLine("To jest cwiczenie 1");
}

class Zwierze
{
    private string imie;
    private string gotunek;
    private int liczbanog;
    private static int licznik = 0;
    

    
    public static int getLiczba()
    {
        return licznik;
    }
    public int getLiczbanog()
    {
        return liczbanog;
    }
    public string getGotunek()
    {
        return gotunek;
    }
    public string getImie()
    {
        return imie;
    }
    public void setImie(string i)
    {
        imie = i;
    }
    public Zwierze()
    {
        licznik++;
    }
}

