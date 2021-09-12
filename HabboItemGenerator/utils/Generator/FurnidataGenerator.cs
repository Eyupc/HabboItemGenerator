using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HabboItemGenerator.utils
{
    class FurnidataGenerator
    {
        private int itemID;
        private string itemName;
        private static StringBuilder furnidata = new();

        
        public FurnidataGenerator(int itemID,Dictionary<string, string> itemDetails)
        {
            this.itemID = itemID;
            this.itemName = itemDetails["name"];

            this.generate();
        }

        public void generate()
        {
            furnidata.AppendLine("<furnitype id=\"" + itemID.ToString() + "\" classname=\"" + itemName + "\">");
            furnidata.AppendLine("  <revision>0</revision>");
            furnidata.AppendLine("  <defaultdir>0</defaultdir>");
            furnidata.AppendLine("  <xdim>1</xdim>");
            furnidata.AppendLine("  <ydim>1</ydim>");
            furnidata.AppendLine("  <partcolors />");
            furnidata.AppendLine("  <name>" + itemName + "</name>");
            furnidata.AppendLine("  <description>" + itemName+ " desc" + "</description>");
            furnidata.AppendLine("  <adurl />");
            furnidata.AppendLine("  <offerid>" + itemID + "</offerid>");
            furnidata.AppendLine("  <buyout>1</buyout>");
            furnidata.AppendLine("  <rentofferid>-1</rentofferid>");
            furnidata.AppendLine("  <rentbuyout>0</rentbuyout>");
            furnidata.AppendLine("  <bc>0</bc>");
            furnidata.AppendLine("  <excludeddynamic>0</excludeddynamic>");
            furnidata.AppendLine("  <customparams>0</customparams>");
            furnidata.AppendLine("  <specialtype>1</specialtype>");
            furnidata.AppendLine("  <canstandon>0</canstandon>");
            furnidata.AppendLine("  <cansiton>0</cansiton>");
            furnidata.AppendLine("  <canlayon>0</canlayon>");
            furnidata.AppendLine("</furnitype>");
            furnidata.AppendLine("");

        }

        public string getFurnidata()
        {
            return furnidata.ToString();
        }

    }
}
