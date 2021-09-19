using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PeanutButter.INI;

namespace HabboItemGenerator.utils
{
    class Configuration
    {
        private INIFile config;
        private Dictionary<string, string> dbconfig = new Dictionary<string, string>();
      public Configuration()
        {
            config = new INIFile("config.ini");

            this.DBConfig();
        }

        private void DBConfig()
        {
            this.dbconfig.Add("db.host",config.GetValue("database", "host"));
            this.dbconfig.Add("db.port",config.GetValue("database", "port"));
            this.dbconfig.Add("db.user",config.GetValue("database", "user"));
            this.dbconfig.Add("db.password",config.GetValue("database", "password"));
            this.dbconfig.Add("db.database",config.GetValue("database", "database"));
        }

        public String  getFurniPath()
        {
            return config.GetValue("config","furniPath");
        }

        public Dictionary<string,string> getDBConfig()
        {
            return this.dbconfig;
        }



    }
}
