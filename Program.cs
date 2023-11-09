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
using AutoMapper;

namespace HelloWorld
{
   
    internal class Program{
        static void Main(string[] args)
        {
            IConfiguration config = new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json")
                    .Build();
            
            DataContextDapper dapper = new DataContextDapper(config);
          

            string computersJson = File.ReadAllText("ComputersSnake.json");
            // //Console.WriteLine(computersJson);
            // JsonSerializerOptions options = new JsonSerializerOptions()
            // {
            //     PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            // };

            // IEnumerable<Computer>? computers = JsonSerializer.Deserialize<IEnumerable<Computer>>(computersJson,options);
            //IEnumerable<Computer>? computers = JsonConvert.DeserializeObject<IEnumerable<Computer>>(computersJson);

            // if(computers != null)
            // {
            //     foreach(Computer computer in computers)
            //     {
            //        // Console.WriteLine(computer.Motherboard);
            //          string sql = @"INSERT INTO TutorialAppSchema.Computer (
            //         Motherboard,
            //         CPUCores,
            //         HasWifi,
            //         HasLTE,
            //         ReleaseDate,
            //         Price,
            //         VideoCard
            //         ) VALUES ('" +EscapeSingleQuote(computer.Motherboard)
            //         + "','" + computer.CPUCores
            //         + "','" + computer.HasWifi
            //         + "','" + computer.HasLTE
            //         + "','" + computer.ReleaseDate
            //         + "','" + computer.Price
            //         + "','" + EscapeSingleQuote(computer.VideoCard)
            //     +"')\n";
            //     dapper.ExecuteSql(sql);
            //     }
            // }

            // string computersCopyN = JsonConvert.SerializeObject(computers);
            // File.WriteAllText("computersCopyNewtonsoft.txt",computersCopyN);

            // string computersCopyS = System.Text.Json.JsonSerializer.Serialize(computers);
            // File.WriteAllText("computersCopySystem.txt",computersCopyS);

            Mapper mapper = new Mapper(new MapperConfiguration(cfg =>{
                cfg.CreateMap<ComputerSnake,Computer>()
                    .ForMember(destination => destination.ComputerId, options =>
                        options.MapFrom(source => source.computer_id))
                    .ForMember(destination => destination.CPUCores, options =>
                        options.MapFrom(source => source.cpu_cores))
                    .ForMember(destination => destination.Motherboard, options =>
                        options.MapFrom(source => source.motherboard))
                    .ForMember(destination => destination.HasLTE, options =>
                        options.MapFrom(source => source.has_lte))
                    .ForMember(destination => destination.HasWifi, options =>
                        options.MapFrom(source => source.has_wifi))
                    .ForMember(destination => destination.VideoCard, options =>
                        options.MapFrom(source => source.video_card))
                    .ForMember(destination => destination.ReleaseDate, options =>
                        options.MapFrom(source => source.release_date))
                    .ForMember(destination => destination.Price, options =>
                        options.MapFrom(source => source.price));
            }));
            IEnumerable<ComputerSnake>? computersSystem = System.Text.Json.JsonSerializer.Deserialize<IEnumerable<ComputerSnake>>(computersJson);
            if(computersSystem != null)
            {
                IEnumerable<Computer> computerResult = mapper.Map<IEnumerable<Computer>>(computersSystem);

                foreach (Computer c in computerResult)
                {
                    Console.WriteLine(c.Motherboard);
                }
            }

        }
        //SELECT * FROM TutorialAppSchema.Computer WHERE VideoCard = 'Robel-O''Hara';
        static string EscapeSingleQuote(string input){
            string output = input.Replace("'","''");

            return output;
        }
    }
}
