using System;

namespace HelloWorld
{
    public class Computer
    {
        public string Motherboard{get; set;} = "";
        public int CPUCores{get; set;}
        public bool HasWifi{get; set;}
        public bool HasLTE{get; set;}
        public DateTime ReleaseDate{get; set;}
        public decimal Price{get; set;}
        public string? VideoCard{get; set;} = "";

    }
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
