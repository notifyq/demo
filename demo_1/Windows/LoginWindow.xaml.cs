using demo_1.Model;
using Microsoft.EntityFrameworkCore;
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

namespace demo_1.Windows
{
    /// <summary>
    /// Логика взаимодействия для LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        db_demo_2024Context dbContext = new db_demo_2024Context();
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void ButtonLogin_Click(object sender, RoutedEventArgs e)
        {
            Employee user = GetUser(TextBox_Login.Text, PasswordBox_Password.Password);
            if (user != null)
            {
                UserWindow userWindow = new UserWindow();
                UserInfo.Employee = user;
                userWindow.Show();
                this.Close();
            }
        }

        public Employee GetUser(string login, string password)
        {
            Employee employee = dbContext.Employees.Include(x => x.EmployeeRoleNavigation).FirstOrDefault(x => x.EmployeeLogin == login && x.EmployeePassword == password);

            return employee;
        }
    }
}
