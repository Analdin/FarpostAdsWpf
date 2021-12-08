using FarpostJob.ServicesClasses;
using MySql.Data.MySqlClient;

namespace FarpostAdsWpf.ServicesClasses
{
    /// <summary>
    /// Добавление пользователя в базу данных
    /// </summary>
    public class InsertUserInDataBase
    {
        /// <summary>
        /// Добавить пользователя
        /// </summary>
        /// <param name="Id"> Идентификатор</param>
        /// <param name="Email"> Почта</param>
        /// <param name="Login"> Логин в системе</param>
        /// <param name="Password"> Пароль пользователя</param>
        /// <param name="Rules"> Администратор или пользователь?</param>
        /// <param name="Status"> Статус в системе (active или inactive)</param>
        public static void UserAdd(string Login, string Password)
        {
            DbHelper connect = new DbHelper();
            connect.OpenConnection();

            var query = $@"INSERT INTO `UsersInfo` (`Login`, `Password`) VALUES ('{Login}', '{Password}');";

            var command = new MySqlCommand(query, connect.Connection);

            connect.CloseConnection();
        }
    }
}
