using System;
using System.Collections.Generic;
using System.Windows;
using FarpostAdsWpf.ModalWindows;
using FarpostJob.ServicesClasses;
using Notification.Wpf;
using MySql.Data.MySqlClient;
using FarpostAdsWpf.ServicesClasses;

namespace FarpostAdsWpf
{
    /// <summary>
    /// Форма для ввода логина и пароля
    /// </summary>
    public partial class StartWindow : Window
    {
        public StartWindow()
        {
            InitializeComponent();
        }

        ///<summary>
        /// Запрос к БД, чтобы проверить условия работы с пользователем
        ///</summary>

        // c# sql параметры?
        private void EnterTheFpUp(object sender, RoutedEventArgs e)
        {
            DbHelper connect = new DbHelper();
            connect.OpenConnection();

            var notificationManager = new NotificationManager();

            var loginText = LoginField.Text;
            var passwordText = PasswordField.Password.ToString();

            var loginFromDb = String.Empty;
            var passwordFromDb = String.Empty;

            //Лучше
            if (LoginField.Text.Length > 0)
            {
                if (PasswordField.Password.Length > 0)
                {
                    List<int> userIds = GetUsersIds.GetAllUsersIds();

                    foreach (var userId in userIds)
                    {
                        var command = new MySqlCommand($@"SELECT * FROM `UsersInfo` WHERE Login = '" + LoginField.Text + "' AND Password = '" + PasswordField.Password + "'", connect.Connection);
                        command.ExecuteNonQuery();

                        MySqlDataReader dt = command.ExecuteReader();
                        dt.Read(); 

                        if  ((string)dt["Login"] != "")
                        {
                            notificationManager.Show(new NotificationContent
                            {
                                Title = "Уведомление",
                                Message = "Пользователь авторизовался",
                                Type = NotificationType.Success
                            });

                            //вызов основного окна программы
                        }
                        else
                        {
                            notificationManager.Show(new NotificationContent
                            {
                                Title = "Ошибка",
                                Message = "Пользователь не найден в базе данных, зарегистрируйтесь",
                                Type = NotificationType.Warning
                            });
                        }
                        dt.Close();

                        RegistrationForm Window = new RegistrationForm();
                        Window.Show();
                    }
                }
                else
                {
                    notificationManager.Show(new NotificationContent
                    {
                        Title = "Ошибка",
                        Message = "Введите пароль",
                        Type = NotificationType.Warning
                    });
                }
            }
            else
            {
                notificationManager.Show(new NotificationContent
                {
                    Title = "Ошибка",
                    Message = "Введите логин",
                    Type = NotificationType.Warning
                });
            }

            connect.CloseConnection();
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            RegistrationForm Window = new RegistrationForm();
            Window.Show();
        }
    }
}
