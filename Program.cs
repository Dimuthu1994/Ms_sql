using System;
using System.Data;
using Dapper;
using HelloWorld.Models;
using Microsoft.Data.SqlClient;

namespace HelloWorld
{
   
    internal class Program{
        static void Main(string[] args)
        {
            string connectionString = "Server=localhost;Database=DotNetCourseDatabase;TrustServerCertificate=true;Trusted_Connection=true;";
            IDbConnection dbConnection = new SqlConnection(connectionString);

            string sqlCommand = "SELECT GETDATE()";
            DateTime rightNow = dbConnection.QuerySingle<DateTime>(sqlCommand);
            Console.WriteLine(rightNow);
            
            Computer myComputer = new Computer()
            {
                Motherboard = "z690",
                HasWifi = true,
                HasLTE = false,
                ReleaseDate = DateTime.Now,
                Price = 100.89m,
                VideoCard = "RTX 2060"

            };

            string sql = @"INSERT INTO TutorialAppSchema.Computer (
                Motherboard,
                HasWifi,
                HasLTE,
                ReleaseDate,
                Price,
                VideoCard
            ) VALUES ('" +myComputer.Motherboard
                + "','" + myComputer.HasWifi
                + "','" + myComputer.HasLTE
                + "','" + myComputer.ReleaseDate
                + "','" + myComputer.Price
                + "','" + myComputer.VideoCard
             +"')";

            //return value is number of rows results
            int result = dbConnection.Execute(sql);
            Console.WriteLine(result);

            string sqlSelect = @"SELECT 
                Computer.Motherboard,
                Computer.HasWifi,
                Computer.HasLTE,
                Computer.ReleaseDate,
                Computer.Price,
                Computer.VideoCard
                FROM TutorialAppSchema.Computer";

            //IEnumerable most efficent datastucture
            IEnumerable<Computer> computers = dbConnection.Query<Computer>(sqlSelect);
            //List<Computer> computersa = dbConnection.Query<Computer>(sqlSelect).ToList();

            foreach(Computer c in computers)
            {
                Console.WriteLine(c.Motherboard);
                Console.WriteLine(c.ReleaseDate);
            }
        }
    }
}
