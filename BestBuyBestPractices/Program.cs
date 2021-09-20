using System;
using System.Data;
using System.IO;
using System.Linq;
using MySql.Data.MySqlClient;
using Microsoft.Extensions.Configuration;


namespace BestBuyBestPractices
{
    class Program
    {
        static void Main(string[] args)
        {

            var connection = GetConnection();
            //ExerciseOne(connection);
            ExerciseTwo(connection);

        }

        static IDbConnection GetConnection()
        {

            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            string connString = config.GetConnectionString("DefaultConnection");
            return new MySqlConnection(connString);

        }
      
        static void ExerciseOne(IDbConnection connection)
        {
            // create a variable that represents the DepartmentRepository
            var departmentsRepo = new DapperDepartmentRepository(connection);

            // foreach dept in (Select *  From Deparments) WriteLine (dept.Name)
            departmentsRepo.GetAllDepartments().ToList().ForEach(x => Console.WriteLine(x.Name));

            // ask for a name for a new dept to insert to table
            Console.WriteLine("Enter the name of the new department.");

            // insert the department into the Table
            departmentsRepo.InsertDepartment(Console.ReadLine());

            // display all departments again.
            departmentsRepo.GetAllDepartments().ToList().ForEach(x => Console.WriteLine(x.Name));
        }

        static void ExerciseTwo(IDbConnection connection)
        {

            var productsRepo = new DapperProductRepository(connection);
            var maxLength = productsRepo.GetAllProducts().ToList().Max(x => x.Name.Length);
            productsRepo.GetAllProducts().ToList().ForEach((x => Console.WriteLine($"{x.Name.PadRight(maxLength)}  |  Price: {x.Price}")));
            Console.ReadKey();
            productsRepo.UpdateProduct("Diablo II", 59.99);

            Console.Clear();

            Console.WriteLine("Now we will pull \"Diablo II\" by it's name.");
            productsRepo.GetSpecificProduct("Diablo II").ToList().ForEach(x => Console.WriteLine(x.Name + " - Price: " + x.Price));
            Console.WriteLine("Wait... Lets change the price on that.");
            productsRepo.UpdateProduct("Diablo II", 1.99);
            productsRepo.GetSpecificProduct("Diablo II").ToList().ForEach(x => Console.WriteLine(x.Name + " - Price: " + x.Price));
            Console.WriteLine("There, now I can afford it.");

            Console.ReadKey();
            Console.Clear();
            Console.WriteLine("Now I will delete the Animal Crossing game.");
            productsRepo.GetSpecificProduct("Animal Crossing").ToList().ForEach(x => Console.WriteLine(x.Name + " - Price: " + x.Price));
            productsRepo.DeleteProduct(316);             
            Console.WriteLine("Now it should be gone.");
            productsRepo.GetSpecificProduct("Animal Crossing").ToList().ForEach(x => Console.WriteLine(x.Name + " - Price: " + x.Price));
            Console.ReadKey();



        }


     

    }
}
