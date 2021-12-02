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

        private void EnterTheFpUp(object sender, RoutedEventArgs e)
        {
            DbHelper connect = new DbHelper();
            connect.OpenConnection();

            var notificationManager = new NotificationManager();

            var loginText = LoginField.Text;
            var passwordText = PasswordField.Password.ToString();

            var loginFromDb = String.Empty;
            var passwordFromDb = String.Empty;

            var query = $@"SELECT `Name`, `Login` FROM `UsersInfo` WHERE 1";

            if (String.IsNullOrWhiteSpace(query))
            {
                notificationManager.Show(new NotificationContent
                {
                    Title = "Уведомление",
                    Message = "При первом запуске программы - необходимо зарегистрировать одного пользователя",
                    Type = NotificationType.Warning
                });

                RegistrationForm Window = new RegistrationForm();
                Window.Show();
            }

            if (!String.IsNullOrWhiteSpace(query))
            {
                List<int> allIds = new List<int>();

                var result = GetUsersIds.GetAllUsersIds();
                allIds.AddRange(result);

                var query2 = $@"SELECT `Login`, `Password` FROM `UsersInfo` WHERE 1";

                var command = new MySqlCommand(query2, connect.Connection);
                var reader = command.ExecuteReader();

                foreach (var userId in allIds)
                {
                    if (reader.Read()) loginFromDb = reader.GetString(0);
                    if (reader.Read()) passwordFromDb = reader.GetString(1);

                    if (loginText.Trim().ToLower() == loginFromDb.Trim().ToLower() && passwordText == passwordFromDb)
                    {
                        notificationManager.Show(new NotificationContent
                        {
                            Title = "Уведомление",
                            Message = "Логин и пароль найдены в базе - вошли в программу",
                            Type = NotificationType.Information
                        });

                        // добавить открытие основного окна программы
                    }
                    else if (loginText.Trim().ToLower() != loginFromDb.Trim().ToLower())
                    {
                        notificationManager.Show(new NotificationContent
                        {
                            Title = "Ошибка",
                            Message = "Логин не совпадает с указанным в базе, проверьте правильность ввода",
                            Type = NotificationType.Error
                        });
                    }
                    else if (passwordText != passwordFromDb)
                    {
                        notificationManager.Show(new NotificationContent
                        {
                            Title = "Ошибка",
                            Message = "Пароль не совпадает с указанным в базе, проверьте правильность ввода",
                            Type = NotificationType.Error
                        });
                    }
                }

                reader.Close();
            }

            //Что может быть при нажатии?

            // ** Самый первый вход (нет пользователей в БД) => сказать, что нужно зарегистрироваться перед использованием проги

            // 1. Не верно введен текст => вывести сообщение об ошибке и сказать, что нужно проверить ввод текста
            // 2. Верно введен текст и пользователь есть в БД  => перекинуть на форму программы
            // 3. Текст введен верно, но пользователя нет в БД => перекинуть на форму регистрации
            // и сказать, что нужно зарегистрироваться перед использованием
            // 4. Пользователь уже есть в БД => сказать, что пользовательн уже существует и задать
            // вопрос - нужно ли войти под его учеткой?
            // 5. Пользователь введен верно, но пароль не верно => сказать, что пароль не верный 
            // и попросить ввести его заново

            connect.CloseConnection();
        }
        private void Register_Click(object sender, RoutedEventArgs e)
        {
            RegistrationForm Window = new RegistrationForm();
            Window.Show();
        }
    }
}
