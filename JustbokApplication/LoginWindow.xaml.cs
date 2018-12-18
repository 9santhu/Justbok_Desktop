using JustbokApplication.Data;
using JustbokApplication.Models;
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

namespace JustbokApplication
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            CheckLogin();
        }

        private void CheckLogin()
        {
            try
            {
                LoaderControl loader = new LoaderControl();
                loader.Show();
                loader.Activate();
                User objUser = new LoginDao().Login(UserName.Text, Password.Password);

                if (objUser != null)
                {
                    SessionManager.UserId = objUser.UserId;
                    SessionManager.FirstName = objUser.FirstName;
                    SessionManager.LastName = objUser.LastName;
                    SessionManager.UserRole = objUser.RoleName;

                    loader.Close();
                    MainWindow mainWindow = new MainWindow();
                    mainWindow.Show();
                    this.Close();
                    mainWindow.Activate();
                }
                else
                {
                    message.Text = "Invalid Username/Password.";
                    loader.Show();
                    loader.Close();
                }
            }
            catch (Exception)
            {
            }

        }
    }
}
