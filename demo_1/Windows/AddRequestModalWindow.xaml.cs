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
    /// Логика взаимодействия для AddRequestModalWindow.xaml
    /// </summary>
    public partial class AddRequestModalWindow : Window
    {
        db_demo_2024Context dbContext = new db_demo_2024Context();
        List<Equipment> equipmentList = new List<Equipment>(0);
        List<Employee> employeeList = new List<Employee>(0);

        public AddRequestModalWindow()
        {
            InitializeComponent();
        }

        private void MenuItem_DeleteEquipment_Click(object sender, RoutedEventArgs e)
        {
            MenuItem menuItem = (MenuItem)sender;
            Equipment equipment = (Equipment)menuItem.DataContext;

            equipmentList.Remove(equipment);
            ListBox_Equipment.Items.Remove(equipment);
        }

        private void MenuItem_DeleteEmployee_Click(object sender, RoutedEventArgs e)
        {
            MenuItem menuItem = (MenuItem)sender;
            Employee employee = (Employee)menuItem.DataContext;

            employeeList.Remove(employee);
            ListBox_Employees.Items.Remove(employee);
        }

        private void Button_AddEmployee_Click(object sender, RoutedEventArgs e)
        {
            EmployeeAddModalWindow modalWindow = new EmployeeAddModalWindow();
            modalWindow.ShowDialog();

            Employee employee = EmployeeAddModalWindow.employee;
            if (employee != null)
            {
                employeeList.Add(employee);
                ListBox_Employees.Items.Add(employee);
            }
            EmployeeAddModalWindow.employee = null;
        }

        private void Button_AddEquipment_Click(object sender, RoutedEventArgs e)
        {
            EquipmentAddModalWindow modalWindow = new EquipmentAddModalWindow();
            modalWindow.ShowDialog();

            Equipment equipment = EquipmentAddModalWindow.equipment;
            if (equipment.DefectTypeId == 0)
            {
                return;
            }
            if (equipment != null)
            {
                equipmentList.Add(equipment);
                ListBox_Equipment.Items.Add(equipment);
            }
            
            EquipmentAddModalWindow.equipment = new Equipment();
        }

        private void Button_Add_Click(object sender, RoutedEventArgs e)
        {

            if (TextBox_ClientName.Text.Length > 0 && TextBox_Description.Text.Length > 0 && employeeList.Count > 0 && equipmentList.Count > 0)
            {
                Request request = new Request()
                {
                    ClientName = TextBox_ClientName.Text,
                    ClientSurname = TextBox_ClientSurname.Text,
                    ClientPatronymic = TextBox_ClientPatronymic.Text,
                    RequestDate = DateOnly.FromDateTime(DateTime.Now),
                    RequestDescription = TextBox_Description.Text,
                };

                dbContext.Requests.Add(request);
                dbContext.SaveChanges();

                foreach (Equipment item in equipmentList)
                {
                    dbContext.Equipment.Add(item);
                    dbContext.SaveChanges();
                    dbContext.RequestEquipmentLists.Add(new RequestEquipmentList
                    {
                        RequestId = request.RequestId,
                        EquipmentId = item.EquipmentId,
                    });
                }
                dbContext.SaveChanges();
                foreach (Employee item in employeeList)
                {
                    dbContext.RequestDesignatedEmployees.Add(new RequestDesignatedEmployee()
                    {
                        RequestId = request.RequestId,
                        EmployeeId = item.EmployeeId,
                    });
                }
                dbContext.SaveChanges();
                dbContext.RequestStatuses.Add(new RequestStatus()
                {
                    StatusId = 1,
                    ChangeDate = DateTime.Now,
                    RequestId = request.RequestId,
                });
                dbContext.SaveChanges();
                this.Close();
            }
            else
            {
                MessageBox.Show("Заполните все поля");
            }
            
        }

        private void TextBox_Description_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
