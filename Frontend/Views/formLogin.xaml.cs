using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Backend.BusinessLogic;
using Backend.DataAccess;

namespace Frontend.Views
{
    /// <summary>
    /// Окно авторизации пользователей в системе.
    /// Проверяет учетные данные и предоставляет доступ в зависимости от типа пользователя.
    /// </summary>
    public partial class formLogin : Window
    {
        private readonly Login _login = new Login();
        private readonly LoginData _loginData = new LoginData();

        /// <summary>
        /// Статическое поле для хранения логина авторизованного пользователя
        /// </summary>
        public static string LoggedInUser;

        /// <summary>
        /// Инициализирует новый экземпляр окна авторизации.
        /// </summary>
        public formLogin()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Обработчик нажатия кнопки закрытия окна.
        /// </summary>
        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Обработчик перемещения окна при зажатии левой кнопки мыши.
        /// </summary>
        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        /// <summary>
        /// Обработчик нажатия кнопки входа в систему.
        /// Проверяет учетные данные и авторизует пользователя.
        /// </summary>
        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!ValidateInput())
                    return;

                _login.Username = txtUsername.Text.Trim();
                _login.Password = txtPassword.Password.Trim();
                _login.UserType = cmbUserType.Text.Trim();

                bool isAuthenticated = _loginData.LoginCheck(_login);

                if (isAuthenticated)
                {
                    LoggedInUser = _login.Username;
                    OpenDashboardWindow();
                    this.Close();
                }
                else
                {
                    ShowAuthError();
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage($"Произошла ошибка: {ex.Message}");
            }
        }

        /// <summary>
        /// Проверяет корректность введенных данных.
        /// </summary>
        /// <returns>True если данные валидны, иначе False</returns>
        private bool ValidateInput()
        {
            if (string.IsNullOrEmpty(txtUsername.Text))
            {
                ShowErrorMessage("Введите логин");
                return false;
            }

            if (string.IsNullOrEmpty(txtPassword.Password))
            {
                ShowErrorMessage("Введите пароль");
                return false;
            }

            if (string.IsNullOrEmpty(cmbUserType.Text))
            {
                ShowErrorMessage("Выберите тип пользователя");
                return false;
            }

            return true;
        }

        /// <summary>
        /// Открывает соответствующее окно в зависимости от типа пользователя.
        /// </summary>
        private void OpenDashboardWindow()
        {
            Window dashboard = _login.UserType == "Admin"
                ? new formAdminDashboard(_login)
                : new formUserDashboard(_login);

            dashboard.Show();
        }

        /// <summary>
        /// Показывает сообщение об ошибке авторизации.
        /// </summary>
        private void ShowAuthError()
        {
            MessageBox.Show("Неверный логин, пароль или тип пользователя",
                "Ошибка авторизации",
                MessageBoxButton.OK,
                MessageBoxImage.Error);
        }

        /// <summary>
        /// Показывает стандартное сообщение об ошибке.
        /// </summary>
        /// <param name="message">Текст сообщения</param>
        private void ShowErrorMessage(string message)
        {
            MessageBox.Show(message, "Ошибка",
                MessageBoxButton.OK,
                MessageBoxImage.Error);
        }

        /// <summary>
        /// Обработчик изменения выбранного типа пользователя.
        /// </summary>
        private void cmbUserType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Реализация может быть добавлена при необходимости
        }
    }
}