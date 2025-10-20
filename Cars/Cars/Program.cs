using System;

namespace MyApplication {
    class Program
    {
        static void Main(string[] args)
        {
            Car MyCar = new Car();
            MyCar.model = "ABC";
            MyCar.color = "blue";
            MyCar.year = 1956;

            Console.WriteLine($"Model is: {MyCar.model} \ncolor is: {MyCar.color} \nyear is: {MyCar.year}");

        }
    }

}
