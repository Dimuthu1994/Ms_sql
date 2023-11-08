using System;
using System.Data;
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
             +"')\n";

            //overwrite but for a log file we don't want overwrite
            //File.WriteAllText("log.txt",sql);
            

            //now append same text at the end
            using StreamWriter openFile = new("log.txt",append:true);
            openFile.WriteLine(sql);
          
        }
    }
}
