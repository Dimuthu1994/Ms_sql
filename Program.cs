using System;
using System.Data;
using Dapper;
using HelloWorld.Data;
using HelloWorld.Models;
using Microsoft.Data.SqlClient;

namespace HelloWorld
{
   
    internal class Program{
        static void Main(string[] args)
        {
            DataContextDapper dapper = new DataContextDapper();

            DateTime rightNow = dapper.LoadDataSingle<DateTime>("SELECT GETDATE()");
            Console.WriteLine(rightNow);
            
            Computer myComputer = new Computer()
            {
                Motherboard = "z690",
                HasWifi = true,
                HasLTE = false,
                ReleaseDate = DateTime.Now,
                Price = 100.89m

            };

            string sql = @"INSERT INTO TutorialAppSchema.Computer (
                Motherboard,
                CPUCores,
                HasWifi,
                HasLTE,
                ReleaseDate,
                Price,
                VideoCard
            ) VALUES ('" +myComputer.Motherboard
                + "','" + myComputer.CPUCores
                + "','" + myComputer.HasWifi
                + "','" + myComputer.HasLTE
                + "','" + myComputer.ReleaseDate
                + "','" + myComputer.Price
                + "','" + myComputer.VideoCard
             +"')";

            bool result = dapper.ExecuteSql(sql);
            Console.WriteLine(result);

            string sqlSelect = @"SELECT 
                Computer.ComputerId,
                Computer.Motherboard,
                Computer.CPUCores,
                Computer.HasWifi,
                Computer.HasLTE,
                Computer.ReleaseDate,
                Computer.Price,
                Computer.VideoCard
                FROM TutorialAppSchema.Computer";

            //IEnumerable most efficent datastucture
            IEnumerable<Computer> computers = dapper.LoadData<Computer>(sqlSelect);
           

            foreach(Computer c in computers)
            {
                Console.WriteLine(c.ComputerId);
                Console.WriteLine(c.ReleaseDate);
            }
        }
    }
}
