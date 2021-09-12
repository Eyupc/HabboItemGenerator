
using System.Xml;

namespace HabboItemGenerator.utils
{
    class XMLReader
    {
        private XmlDocument xmlReader;

        public XMLReader()
        {
            this.xmlReader = new XmlDocument();

        }

        public XmlDocument GetReader()
        {
            return this.xmlReader;
        }


    }
}
