using System;
using System.Transactions;

namespace Test
{
    class Program
    {
        static void Main(string[] args) 
        {
            Console.WriteLine("Hello world ");
            //comment 
            /* multi-line
            comments*/
            Console.WriteLine("Enter your user name:");
            //string username = Console.ReadLine();
            //Console.WriteLine("user name is " + username);
            Console.WriteLine("Enter your age:");
            //int age = Convert.ToInt32(Console.ReadLine());
            //Console.WriteLine("Your age is: " + age);
            /*
             logical and &&
             logical or ||
             logical not !
             */
            string txt = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            Console.WriteLine("The length of the txt string is: " + txt.Length);
            string firstName = "Uladzislau";
            string seconfName = "Zhukau";
            Console.WriteLine($"My full name is {firstName} {seconfName}");
            string MyString = "In geometry, a tesseract or 4-cube \n is a four-dimensional hypercube, analogous to a two-dimensional square and a three-dimensional cube.[1] Just as the perimeter of the";
            Console.WriteLine(MyString.IndexOf("e"));
            int i = 0;
            while (i <= 5)
            {
                Console.WriteLine(i);
                i++;
            }
            for (int i1 = 0; i1 < 5; i1++)
            {
                Console.WriteLine(i1);
            }
            // Outer loop
            for (int i2 = 1; i2 <= 2; ++i2)
            {
                Console.WriteLine("Outer: " + i2);  // Executes 2 times

                // Inner loop
                for (int j = 1; j <= 3; j++)
                {
                    Console.WriteLine(" Inner: " + j); // Executes 6 times (2 * 3)
                }
            }
            string[] cars = { "Volvo", "BMW", "Ford", "Mazda" };
            foreach (string car in cars)
            {
                Console.WriteLine(i);
            }
            string[] cars1 = { "Volvo", "BMW", "Ford", "Mazda" };
            for (int i3 = 0; i3 < cars1.Length; i3++)
            {
                Console.WriteLine(cars[i]);
            }
        }
    }
    class Car
    {
        string model;
        string color;
        int year;

        static void Main(string[] args)
        {
            Car Ford = new Car();
            Ford.model = "Mustang";
            Ford.color = "red";
            Ford.year = 1969;

            Car Opel = new Car();
            Opel.model = "Astra";
            Opel.color = "white";
            Opel.year = 2005;

            Console.WriteLine(Ford.model);
            Console.WriteLine(Opel.model);
        }
    }
    class Car1
    {
        public string model;
        public string color;
        public int year;

        // Create a class constructor with multiple parameters
        public Car1(string modelName, string modelColor, int modelYear)
        {
            model = modelName;
            color = modelColor;
            year = modelYear;
        }

        static void Main(string[] args)
        {
            Car1 Ford = new Car1("Mustang", "Red", 1969);
            Console.WriteLine(Ford.color + " " + Ford.year + " " + Ford.model);
        }
    }

}
