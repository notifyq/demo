using demo_1.Model;
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
    /// Логика взаимодействия для EmployeeAddModalWindow.xaml
    /// </summary>
    public partial class EmployeeAddModalWindow : Window
    {
        public static Employee employee;
        db_demo_2024Context dbContext = new db_demo_2024Context();
        bool selected = false;
        public EmployeeAddModalWindow()
        {
            InitializeComponent();
            LoadEmployees();
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Grid grid = (Grid)sender;
            Employee selectedEmployee = (Employee)grid.DataContext; 
            if (selectedEmployee != null)
            {
                employee = selectedEmployee;
                this.Close();
            }
        }

        public void LoadEmployees()
        {
            List<Employee> employees = dbContext.Employees.Where(x => x.EmployeeRole == 2).ToList();
            ListBox_Employees.ItemsSource = employees;
        }
    }
}
