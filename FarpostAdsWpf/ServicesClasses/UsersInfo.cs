using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FarpostJob.ServicesClasses;
using MySql.Data.MySqlClient;
using Notification.Wpf;

namespace FarpostWpfJob.ServiceClasses
{
    /// <summary>
    /// Информация о пользователях
    /// </summary>
    public class UsersInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Rules { get; set; }

        /// <summary>
        /// Проверка - успешен ли вход в программу
        /// </summary>
        /// <returns>Возвращает результат: успешный ли вход в программу</returns>
        public static bool ConnectedYes()
        {
            var notificationManager = new NotificationManager();

            bool connected = true;

            if (connected == true)
            {
                try
                {
                    DbHelper dbHelper = new DbHelper();
                    dbHelper.OpenConnection();

                    notificationManager.Show(new NotificationContent
                    {
                        Title = "Уведомление",
                        Message = "Подключились к базе данных",
                        Type = NotificationType.Information
                    });
                }
                catch (Exception ex)
                {
                    notificationManager.Show(new NotificationContent
                    {
                        Title = "Ошибка",
                        Message = "Произошла ошибка при соединении с базой данных",
                        Type = NotificationType.Error
                    });
                }
            }
            else
            {
                try
                {
                    connected = false;
                }
                catch (Exception ex)
                {
                    notificationManager.Show(new NotificationContent
                    {
                        Title = "Ошибка",
                        Message = "Произошла ошибка при соединении с базой данных",
                        Type = NotificationType.Error
                    });
                }
            }

            return connected;
        }
    }

    public class UsersReport
    {
        /// <summary>
        /// Отчет о входе в программу
        /// </summary>
        /// <param name="enterYes">Объект для вставки</param>
        public void GenerateReport()
        {

        }
    }
}
