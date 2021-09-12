using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HabboItemGenerator.utils
{
    class ExportFiles
    {
        private string catalog_items, items_base, furnidata;
        public ExportFiles(SQLGenerator db, FurnidataGenerator furnidata)
        {
            this.catalog_items = db.getCatalogItems();
            this.items_base = db.getItemsBase();
            this.furnidata = furnidata.getFurnidata();
            Directory.CreateDirectory("Generated");

        }

        public void CatalogItems()
        {
            using (StreamWriter sw = File.CreateText("Generated/catalog_items.txt"))
            {
                sw.Write(catalog_items);
            }
        }

        public void ItemsBase()
        {
            using (StreamWriter sw = File.CreateText("Generated/items_base.txt"))
            {
                sw.Write(items_base);
            }
        }

        public void Furnidata()
        {
            using (StreamWriter sw = File.CreateText("Generated/furnidata.xml"))
            {
                sw.Write(furnidata);
            }
        }


    }
}
