using System;
using MySql.Data.MySqlClient;

namespace FarpostJob.ServicesClasses
{
    public class DbHelper
    {
        private const string Server = "37.140.192.191";
        private const string DatabaseName = "FarpostBd";
        private const string UserName = "u1486803_admin";
        private const string Password = "pR5iQ4zF7ovX6l";

        public readonly MySqlConnection Connection;

        public DbHelper(MySqlConnection connection)
        {
            this.Connection = connection ?? throw new ArgumentNullException(nameof(connection));
        }

        public DbHelper()
            : this(new MySqlConnection($"Server={Server}; database={DatabaseName}; UID={UserName}; password={Password};CharSet=UTF8;"))
        {
        }

        public void OpenConnection()
        {
            this.Connection.Open();
        }

        public void CloseConnection()
        {
            this.Connection.Close();
        }

        // For Insert, Update or Delete queries
        public void ExecuteNonQuery(string query)
        {
            using (var cmd = new MySqlCommand(query, this.Connection))
            {
                cmd.ExecuteNonQuery();
            }
        }

        public int ExecuteSimpleQueryAsInt(string query)
        {
            return int.Parse(this.ExecuteSimpleQueryAsString(query));
        }

        public string ExecuteSimpleQueryAsString(string query)
        {
            return this.ExecuteSimpleQuery(query).ToString();
        }

        private object ExecuteSimpleQuery(string query)
        {
            using (var cmd = new MySqlCommand(query, this.Connection))
            {
                cmd.ExecuteNonQuery();

                var reader = cmd.ExecuteReader();

                if (!reader.Read())
                    throw new Exception("Incorrect command?");

                return reader.GetValue(0);
            }
        }
    }
}
