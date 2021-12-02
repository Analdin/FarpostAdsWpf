using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using FarpostAdsWpf.ModalWindows;
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

            DbHelper connect = new DbHelper();
            connect.OpenConnection();

            var regesteredLoginUser = registeredLogin.Text;
            var registeredPasswordUser = registeredPassword.Password.ToString();
            var registeredPasswordUeserRepeat = registeredPasswordRepeat.Password.ToString();

            var loginFromDb = String.Empty;
            var passwordFromDb = String.Empty;

            // Пользователь ввел логин и пароль => проверяем, верно ли введено подтверждение пароля,
            // если да - выводим сообщение, что все отлично, пользователь занесен в БД
            // если нет - выводим сообщение, что нужно проверить правильность ввода подтверждения
        }
    }
}
