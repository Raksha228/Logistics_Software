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
using System;
using Backend.Interfaces;
using Backend.BusinessLogic;
using Backend.DataAccess;


namespace Frontend.Views
{
    public partial class formLogin : Window
    {
        public formLogin()
        {
            InitializeComponent();
        }

        Login login = new Login();
        LoginData loginData = new LoginData();
        public static string loggedIn;

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                login.Username = txtUsername.Text.Trim();
                login.Password = txtPassword.Password.Trim();
                login.UserType = cmbUserType.Text.Trim();

                //Проверка, есть ли такой пользователь с такими правами
                bool sucess = loginData.loginCheck(login);

                

                // Проверяем введенные данные
                if (string.IsNullOrEmpty(login.Username))
                {
                    MessageBox.Show("Введите логин", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (string.IsNullOrEmpty(login.Password))
                {
                    MessageBox.Show("Введите пароль", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (string.IsNullOrEmpty(login.UserType))
                {
                    MessageBox.Show("Выберите тип пользователя", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }


                if (sucess == true)
                {
                    // Определяем какое окно открывать в зависимости от типа пользователя
                    if (login.UserType == "Admin")
                    {
                        formAdminDashboard adminDashboard = new formAdminDashboard(login);
                        adminDashboard.Show();
                    }
                    else
                    {
                        formUserDashboard userDashboard = new formUserDashboard(login);
                        userDashboard.Show();
                    }

                    this.Close();
                }
                else
                {
                    MessageBox.Show("Неверный логин, пароль или тип пользователя", "Ошибка авторизации",
                                  MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}", "Ошибка",
                              MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // Закрываем приложение при закрытии окна авторизации
            //Application.Current.Shutdown();
        }
    }
}