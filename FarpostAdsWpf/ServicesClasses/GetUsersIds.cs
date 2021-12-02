using System.Collections.Generic;
using FarpostJob.ServicesClasses;
using MySql.Data.MySqlClient;

namespace FarpostAdsWpf.ServicesClasses
{
    public class GetUsersIds
    {
        public static List<int> GetAllUsersIds()
        {
            DbHelper connectBd = new DbHelper();
            connectBd.OpenConnection();

            var query = $@"SELECT `Id` FROM `UsersInfo`";

            var result = new List<int>();
            var command = new MySqlCommand(query, connectBd.Connection);
            var reader = command.ExecuteReader();

            while (reader.Read())
            {
                result.Add(reader.GetInt32(0));
            }

            reader.Close();
            return result;
        }
    }
}
