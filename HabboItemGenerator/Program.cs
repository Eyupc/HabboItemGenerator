using System;
using System.IO;
using HabboItemGenerator.utils;
using MySql.Data.MySqlClient;

namespace HabboItemGenerator
{
    class Program
    {

        static void Main(string[] args)
        {

            SQLGenerator sQLGenerator = null;
            FurnidataGenerator furnidataGenerator = null;
            Configuration configuration = new();
           
            Console.WriteLine("STARTED!");

            string[] filePaths = null;
            try
            {
               filePaths = Directory.GetFiles(configuration.getFurniPath(), "*.swf");
            }catch(DirectoryNotFoundException e)
            {
                Console.WriteLine(configuration.getFurniPath() + "    THIS PATH doesn't exist!");
                Console.ReadLine();
                return;
            }
           
            Database databaseConnection = new Database(configuration.getDBConfig()["db.host"], int.Parse(configuration.getDBConfig()["db.port"]), configuration.getDBConfig()["db.user"], configuration.getDBConfig()["db.database"], configuration.getDBConfig()["db.password"]);

            try
            {
                databaseConnection.connect();
                databaseConnection.getConnection().Open();
            }
            catch(MySqlException e) {
                Console.WriteLine("Failed to connect to database.");
                Console.ReadLine();
                return;
            };

            for (int i = 0; i <= filePaths.Length - 1; i++) {
                    Stream stream = File.Open(filePaths[i], FileMode.Open, FileAccess.Read);
                    DefineBinaryData defineBinaryData = new DefineBinaryData(stream, Path.GetFileName(filePaths[i]).Substring(0, Path.GetFileName(filePaths[i]).IndexOf(".swf")));
                    sQLGenerator = new SQLGenerator(databaseConnection, defineBinaryData.getItemDetails());
                    furnidataGenerator = new FurnidataGenerator(SQLGenerator.itemID, defineBinaryData.getItemDetails());
            }

           var export =  new ExportFiles(sQLGenerator, furnidataGenerator);
            export.CatalogItems();
            export.ItemsBase();
            export.Furnidata();

            databaseConnection.getConnection().Close();

            Console.WriteLine("Successfully generated");
            Console.ReadLine();


            //Need to work on forloop -> will crash



        }
    }
}
