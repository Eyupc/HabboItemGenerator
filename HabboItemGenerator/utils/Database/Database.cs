using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HabboItemGenerator.utils
{
    class Database
    {
        private MySqlConnection connection;
        private MySqlCommand command = new();
        private MySqlDataReader mysqlDataReader;

        private string server, user, database, password;
        private int port;


        public Database(string server, int port, string user, string database, string password)
        {
            this.server = server;
            this.port = port;
            this.user = user;
            this.database = database;
            this.password = password;

        }

        public void connect()
        {
            this.connection =  new MySqlConnection("server =" + this.server + ";user =" + this.user + "; database = " + this.database + "; port =+" + this.port.ToString() + "; password =" + this.password + ";SslMode=none");
            this.command.Connection = this.connection;

        }

        
        public MySqlConnection getConnection()
        {
            return this.connection;
        }

        public MySqlDataReader getDataReader()
        {
            return this.mysqlDataReader;
        }
        public MySqlCommand getCommand()
        {
            return this.command;
        }

    }
}
