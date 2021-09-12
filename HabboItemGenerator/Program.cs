using System;
using System.IO;
using HabboItemGenerator.utils;

namespace HabboItemGenerator
{
    class Program
    {

        static void Main(string[] args)
        {

         SQLGenerator sQLGenerator = null;
         FurnidataGenerator furnidataGenerator = null;

        Console.WriteLine("STARTED!");

            string[] filePaths = Directory.GetFiles(@"C:\xampp2\htdocs\ms-swf\dcr\hof_furni\test", "*.swf");


            Database databaseConnection = new Database("127.0.0.1", 3306, "root", "hotel", "");
            databaseConnection.connect();
            databaseConnection.getConnection().Open();

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
            
            Console.ReadLine();


            //Need to work on forloop -> will crash



        }
    }
}
