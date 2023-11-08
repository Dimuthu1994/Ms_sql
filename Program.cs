using System;
using HelloWorld.Models;

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
                Price = 100.89m,
                VideoCard = "RTX 2060"

            };
        }
    }
}
