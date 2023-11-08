using System;
using System.Data;
using System.Text.Json;
using Dapper;
using HelloWorld.Data;
using HelloWorld.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace HelloWorld
{
   
    internal class Program{
        static void Main(string[] args)
        {
            IConfiguration config = new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json")
                    .Build();
            
            DataContextDapper dapper = new DataContextDapper(config);
            // string sql = @"INSERT INTO TutorialAppSchema.Computer (
            //     Motherboard,
            //     CPUCores,
            //     HasWifi,
            //     HasLTE,
            //     ReleaseDate,
            //     Price,
            //     VideoCard
            // ) VALUES ('" +myComputer.Motherboard
            //     + "','" + myComputer.CPUCores
            //     + "','" + myComputer.HasWifi
            //     + "','" + myComputer.HasLTE
            //     + "','" + myComputer.ReleaseDate
            //     + "','" + myComputer.Price
            //     + "','" + myComputer.VideoCard
            //  +"')\n";

            string computersJson = File.ReadAllText("Computers.json");
            //Console.WriteLine(computersJson);
            JsonSerializerOptions options = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            IEnumerable<Computer>? computers = JsonSerializer.Deserialize<IEnumerable<Computer>>(computersJson,options);
            if(computers != null)
            {
                foreach(Computer computer in computers)
                {
                    Console.WriteLine(computer.Motherboard);
                }
            }
        }
    }
}
