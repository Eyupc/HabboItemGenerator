using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using SwfLib;

namespace HabboItemGenerator.utils
{
    class DefineBinaryData
    {
        private SwfFile swf;
        private XMLReader xmlReader;
        private Dictionary<String, string> ItemDetails = new();

        private List<byte[]> data = new List<byte[]>();

        private decimal X, Y, Z;
        private short interaction_modes_count;
        private String filename;
        public DefineBinaryData(Stream stream,String filename)
        {

            this.swf = SwfFile.ReadFrom(stream);
            this.xmlReader = new XMLReader();
            this.filename = filename;

            this.data.Clear();
            this.ItemDetails.Clear();

           this.getBinaryData();
           this.getFurniLogic();
        }

        private List<byte[]> getBinaryData()
        {             
            foreach (var x in this.swf.Tags)
            {
                if (x.TagType == SwfLib.Tags.SwfTagType.DefineBinaryData)
                {
                    this.data.Add(x.RestData);
                    
                }
          //    try
          //    {
          //        Console.WriteLine(Encoding.UTF8.GetString(x.RestData));
          //    }
          //    catch (Exception e) { }
            }

            foreach (var s in this.data)
            {
                var sx = Encoding.UTF8.GetString(s);
                //Console.WriteLine(sx);
            }
            return this.data; 
        }


        private void getFurniLogic()
        {
            foreach (byte[] xml in this.data)
            {

                try {
                    int index = Encoding.UTF8.GetString(xml).IndexOf("<?xml");
                    string _xml = Encoding.UTF8.GetString(xml).Substring(index);



                    xmlReader.GetReader().LoadXml(_xml.Replace("\"x", "\" x").Replace("\"y", "\" y"));

                    if (xmlReader.GetReader().GetElementsByTagName("dimensions") != null)
                    {
                        if (xmlReader.GetReader().GetElementsByTagName("dimensions").Count > 0)
                        {

                            XmlNodeList node = xmlReader.GetReader().GetElementsByTagName("dimensions");

                            this.X = decimal.Parse(node[0].Attributes["x"].Value.Replace(".", ","));
                            this.Y = decimal.Parse(node[0].Attributes["y"].Value.Replace(".", ","));
                            this.Z = decimal.Parse(node[0].Attributes["z"].Value.Replace(".", ","));

                            ItemDetails.Add("name", this.filename);
                            ItemDetails.Add("X", this.X.ToString());ItemDetails.Add("Y", this.Y.ToString());ItemDetails.Add("Z", this.Z.ToString());

                            //  Console.WriteLine(_xml);

                            Console.WriteLine((this.filename + "    X: " + this.X + " " + "Y: " + this.Y + " " +"Z: "+  this.Z));
                        }else if(xmlReader.GetReader().GetElementsByTagName("animations").Count > 0)
                        {
                            XmlNodeList animation = xmlReader.GetReader().GetElementsByTagName("animation");
                            this.interaction_modes_count = (short)animation.Count;
                            ItemDetails.Add("interaction_modes_count", this.interaction_modes_count.ToString());


                        }
                    }

                }
                catch (Exception e) {
                    var s = Encoding.UTF8.GetString(xml);
                }


            }
        }

        public decimal getX()
        {
            return this.X;
        }

        public decimal getY()
        {
            return this.Y;
        }

        public decimal getZ()
        {
          return  this.Z;
        }

        public short getInteractionCount()
        {
            return this.interaction_modes_count;
        }

        public Dictionary<string,string> getItemDetails()
        {
            return this.ItemDetails;
        }

    }
}
