using Capstone.DAL;
using Capstone.Models;
using Capstone.Views;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;

namespace Capstone
{
    public class Program
    {
        static void Main(string[] args)
        {
            // Get the connection string from the appsettings.json file
            IConfigurationBuilder builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            IConfigurationRoot configuration = builder.Build();

            string connectionString = configuration.GetConnectionString("Project");



            IParkSqlDAO parkDAO = new ParkSQLDAO(connectionString);
            // Create a menu and run it
            Park park = new Park();
            MainMenu menu = new MainMenu(parkDAO);
            menu.Run();
        }
    }
}
