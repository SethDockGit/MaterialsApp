using MaterialsApp.Data;
using MaterialsApp.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MaterialsApp
{
    class Program
    {
        static void Main(string[] args)
        {

            IConfiguration configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", true, true)
                .Build();


            string dataMode = configuration.GetSection("Settings:DataMode").Value;

            IDataSource dataSource = ConfigureDataMode(dataMode);

            IO io = new IO(dataSource);
            io.Run();
        }
        static IDataSource ConfigureDataMode(string mode)
        {
            switch(mode)
            {
                case "InMemory":
                    return new InMemoryDataSource();
                case "TxtData":
                    return new TxtDataSource();
                default:
                    throw new Exception("Data mode could not be configured");
            }
        }
        public static IConfiguration GetConfig()
        {
            return new ConfigurationBuilder().AddJsonFile
            ("appsettings.json", true, true).Build();
        }
    }
}
