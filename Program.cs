using System;
using System.Data;
using System.Text.Json;
using Dapper;
using HelloWorld.Data;
using HelloWorld.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace HelloWorld
{
   
    internal class Program{
        static void Main(string[] args)
        {
            IConfiguration config = new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json")
                    .Build();
            
            DataContextDapper dapper = new DataContextDapper(config);
          

            string computersJson = File.ReadAllText("Computers.json");
            // //Console.WriteLine(computersJson);
            // JsonSerializerOptions options = new JsonSerializerOptions()
            // {
            //     PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            // };

            // IEnumerable<Computer>? computers = JsonSerializer.Deserialize<IEnumerable<Computer>>(computersJson,options);
            IEnumerable<Computer>? computers = JsonConvert.DeserializeObject<IEnumerable<Computer>>(computersJson);

            if(computers != null)
            {
                foreach(Computer computer in computers)
                {
                   // Console.WriteLine(computer.Motherboard);
                     string sql = @"INSERT INTO TutorialAppSchema.Computer (
                    Motherboard,
                    CPUCores,
                    HasWifi,
                    HasLTE,
                    ReleaseDate,
                    Price,
                    VideoCard
                    ) VALUES ('" +EscapeSingleQuote(computer.Motherboard)
                    + "','" + computer.CPUCores
                    + "','" + computer.HasWifi
                    + "','" + computer.HasLTE
                    + "','" + computer.ReleaseDate
                    + "','" + computer.Price
                    + "','" + EscapeSingleQuote(computer.VideoCard)
                +"')\n";
                dapper.ExecuteSql(sql);
                }
            }

            string computersCopyN = JsonConvert.SerializeObject(computers);
            File.WriteAllText("computersCopyNewtonsoft.txt",computersCopyN);

            string computersCopyS = System.Text.Json.JsonSerializer.Serialize(computers);
            File.WriteAllText("computersCopySystem.txt",computersCopyS);

        }
        //SELECT * FROM TutorialAppSchema.Computer WHERE VideoCard = 'Robel-O''Hara';
        static string EscapeSingleQuote(string input){
            string output = input.Replace("'","''");

            return output;
        }
    }
}
