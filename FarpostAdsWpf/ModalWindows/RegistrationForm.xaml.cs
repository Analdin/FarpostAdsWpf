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

            if (regesteredLoginUser.Length > 0)
            {
                string[] dataLogin = regesteredLoginUser.Split('@'); //делим строку на две части

                if(dataLogin.Length == 2) //проверка, есть ли у нас две части
                {
                    string[] data2Login = dataLogin[1].Split('.');

                    if(data2Login.Length == 2)
                    {

                    }
                    else
                    {
                        notificationManager.Show(new NotificationContent
                        {
                            Title = "Уведомление",
                            Message = "Укажите логин в формате x@x.x",
                            Type = NotificationType.Notification
                        });
                    }
                }
                else
                {
                    notificationManager.Show(new NotificationContent
                    {
                        Title = "Уведомление",
                        Message = "Укажите логин в формате x@x.x",
                        Type = NotificationType.Notification
                    });
                }

                if (registeredPasswordUser.Length > 6)
                {
                    bool en = true;

                    bool symbol = false;

                    bool number = false;

                    //перебираем символы
                    for(int i = 0; i < registeredPasswordUser.Length; i++)
                    {
                        if (registeredPasswordUser[i] >= 'А' && registeredPasswordUser[i] <= 'Я') en = false;
                        if (registeredPasswordUser[i] >= '0' && registeredPasswordUser[i] <= '9') number = true;
                        if (registeredPasswordUser[i] >= '_' || registeredPasswordUser[i] <= '-' || registeredPasswordUser.Length == '!') symbol = true;
                    }

                    if(!en)
                    {
                        notificationManager.Show(new NotificationContent
                        {
                            Title = "Уведомление",
                            Message = "Доступна только английская раскладка",
                            Type = NotificationType.Notification
                        });
                    }
                    else if (!symbol)
                    {
                        notificationManager.Show(new NotificationContent
                        {
                            Title = "Уведомление",
                            Message = "Добавьте один из следующих символов: _ - !",
                            Type = NotificationType.Notification
                        });
                    }
                    else if (!number)
                    {
                        notificationManager.Show(new NotificationContent
                        {
                            Title = "Уведомление",
                            Message = "Добавьте хотя бы одну цифру",
                            Type = NotificationType.Notification
                        });
                    }

                    if(en && symbol && number)
                    {
                    }

                    if (registeredPasswordUeserRepeat.Length > 6)
                    {
                        for (int i = 0; i < registeredPasswordUser.Length; i++)
                        {
                            if (registeredPasswordUeserRepeat[i] >= 'А' && registeredPasswordUeserRepeat[i] <= 'Я') en = false;
                            if (registeredPasswordUeserRepeat[i] >= '0' && registeredPasswordUeserRepeat[i] <= '9') number = true;
                            if (registeredPasswordUeserRepeat[i] >= '_' || registeredPasswordUeserRepeat[i] <= '-' || registeredPasswordUser.Length == '!') symbol = true;
                        }

                        if (!en)
                        {
                            notificationManager.Show(new NotificationContent
                            {
                                Title = "Уведомление",
                                Message = "Доступна только английская раскладка",
                                Type = NotificationType.Notification
                            });
                        }
                        else if (!symbol)
                        {
                            notificationManager.Show(new NotificationContent
                            {
                                Title = "Уведомление",
                                Message = "Добавьте один из следующих символов: _ - !",
                                Type = NotificationType.Notification
                            });
                        }
                        else if (!number)
                        {
                            notificationManager.Show(new NotificationContent
                            {
                                Title = "Уведомление",
                                Message = "Добавьте хотя бы одну цифру",
                                Type = NotificationType.Notification
                            });
                        }

                        if (en && symbol && number)
                        {
                        }
                    }
                    else
                    {
                        notificationManager.Show(new NotificationContent
                        {
                            Title = "Уведомление",
                            Message = "Пароль должен быть больше 6 символов. Повторите пароль",
                            Type = NotificationType.Notification
                        });
                    }
                }
                else
                {
                    notificationManager.Show(new NotificationContent
                    {
                        Title = "Уведомление",
                        Message = "Повтор пароля должен быть больше 6 символов и совпадать с первым паролем. Повторите пароль.",
                        Type = NotificationType.Notification
                    });
                }

                if(registeredPasswordUser == registeredPasswordUeserRepeat)
                {
                    notificationManager.Show(new NotificationContent
                    {
                        Title = "Уведомление",
                        Message = "Пользователь зарегистрирован",
                        Type = NotificationType.Success
                    });

                    InsertUserInDataBase.UserAdd(regesteredLoginUser, registeredPasswordUser);
                }
                else
                {
                    notificationManager.Show(new NotificationContent
                    {
                        Title = "Ошибка",
                        Message = "Пароли не совпадают",
                        Type = NotificationType.Success
                    });
                }
            }
            else
            {
                notificationManager.Show(new NotificationContent
                {
                    Title = "Уведомление",
                    Message = "Укажите логин",
                    Type = NotificationType.Notification
                });
            }

            connect.CloseConnection();
        }
    }
}
