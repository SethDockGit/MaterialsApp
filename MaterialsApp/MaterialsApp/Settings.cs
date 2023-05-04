using System;
using System.Collections.Generic;
using System.Text;
using MaterialsApp.Data;
using Microsoft.Extensions.Configuration;

namespace MaterialsApp
{
    public class Settings
    {

        public static string FilePath { get; set; }
        public static IDataSource DataSource { get; set; }

        static Settings()
        {

            var config = new ConfigurationBuilder().AddJsonFile
            ("appsettings.json", true, true).Build();

            string dataSource = config.GetSection("Settings:DataMode").Value;
            SetDataSource(dataSource);

            string path = @"C:\Data\MaterialsApp\data.txt";
            SetFilePath(path);
        }

        static void SetFilePath(string path)
        {
            FilePath = path;
        }
        static void SetDataSource(string mode)
        {

            switch (mode)
            {
                case "InMemory":
                    DataSource = new InMemoryDataSource();
                    break;
                case "TxtData":
                    DataSource = new TxtDataSource();
                    break;
                default:
                    throw new Exception("Data mode could not be configured");
            }
        }
    }
}
