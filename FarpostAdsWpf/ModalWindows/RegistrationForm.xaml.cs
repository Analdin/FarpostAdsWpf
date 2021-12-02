using System;
using System.Windows;
using FarpostAdsWpf.ServicesClasses;
using FarpostJob.ServicesClasses;
using Notification.Wpf;
using MySql.Data.MySqlClient;

namespace FarpostAdsWpf.ModalWindows
{
    /// <summary>
    /// Класс с формой для регистрации пользователя
    /// </summary>
    public partial class RegistrationForm : Window
    {
        public RegistrationForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Логика регистрации пользователя
        /// </summary>
        /// <param name="sender">Обработчик событий</param>
        /// <param name="e">Передает объект, относящийся к событию</param>
        private void Registration_Process(object sender, RoutedEventArgs e)
        {
            //registeredLogin
            //registeredPassword
            //registeredPasswordRepeat

            var notificationManager = new NotificationManager();

            DbHelper connect = new DbHelper();
            connect.OpenConnection();

            var regesteredLoginUser = registeredLogin.Text;
            var registeredPasswordUser = registeredPassword.Password.ToString();
            var registeredPasswordUeserRepeat = registeredPasswordRepeat.Password.ToString();

            var emailFromDb = String.Empty;
            var loginFromDb = String.Empty;
            var passwordFromDb = String.Empty;
            var rulesFromDb = String.Empty;
            var statusFromDb = String.Empty;

            var result = GetUsersIds.GetAllUsersIds();

            foreach(var id in result)
            {
                var query = $@"SELECT `Email`, `Login`, `Password`, `Rules`, `Status` FROM `UsersInfo` WHERE id={id}";

                var command = new MySqlCommand(query, connect.Connection);
                var reader = command.ExecuteReader();

                if (reader.Read()) emailFromDb = reader.GetString(0);
                if (reader.Read()) loginFromDb = reader.GetString(1);
                if (reader.Read()) passwordFromDb = reader.GetString(2);
                if (reader.Read()) rulesFromDb = reader.GetString(3);
                if (reader.Read()) statusFromDb = reader.GetString(4);

                reader.Close();
                /// <summary>
                /// Проверяем совпадение пароля и повтора пароля
                /// </summary>
                if (registeredPasswordUser != registeredPasswordUeserRepeat)
                {
                    notificationManager.Show(new NotificationContent
                    {
                        Title = "Ошибка",
                        Message = "Пароль и повтор пароля не совпадают! Проверьте правильность ввода",
                        Type = NotificationType.Warning
                    });
                }
                else
                {
                    notificationManager.Show(new NotificationContent
                    {
                        Title = "Уведомление",
                        Message = "Пароль и повтор пароля совпадают.",
                        Type = NotificationType.Information
                    });

                    InsertUserInDataBase.UserAdd(id, regesteredLoginUser, registeredPasswordUser);

                    notificationManager.Show(new NotificationContent
                    {
                        Title = "Уведомление",
                        Message = "Пользователь " + regesteredLoginUser + " добавлен в базу данных",
                        Type = NotificationType.Information
                    });
                }
            }

            // Пользователь ввел логин и пароль => проверяем, верно ли введено подтверждение пароля,
            // если да - выводим сообщение, что все отлично, пользователь занесен в БД
            // если нет - выводим сообщение, что нужно проверить правильность ввода подтверждения

            connect.CloseConnection();
        }
    }
}
