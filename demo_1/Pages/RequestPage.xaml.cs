using demo_1.Model;
using demo_1.Windows;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace demo_1.Pages
{
    /// <summary>
    /// Логика взаимодействия для RequestPage.xaml
    /// </summary>
    public partial class RequestPage : Page
    {
        db_demo_2024Context dbContext = new db_demo_2024Context();
        Request request;
        public RequestPage(Request request)
        {
            InitializeComponent();
            
            this.request = dbContext.Requests.Find(request.RequestId);

            if (UserInfo.Employee.EmployeeRole != 1)
            {
                Button_AddEmployee.Visibility = Visibility.Collapsed;
                Button_AddEquipment.Visibility = Visibility.Collapsed;
                
            }


            LoadStatuses();
            LoadStatusesHistory();
            LoadDesignatedEmployees();
            LoadComments();
            LoadEquipment();


            TextBlock_ClientName.Text = request.ClientName;
            TextBlock_ClientSurname.Text = request.ClientSurname;
            TextBlock_ClientPatronymic.Text = request.ClientPatronymic;

            TextBlock_RequestDate.Text = "Дата запроса: " + request.RequestDate.ToString();

            TextBox_Description.Text = request.RequestDescription;
            TextBlock_RequestDate.Text = $"Дата заявки: {request.RequestDate}";

            if (dbContext.RequestStatuses.Where(x => x.RequestId == request.RequestId).ToList().Count > 0)
            {
                ComboBox_RequestStatus.SelectedValue = dbContext.RequestStatuses
                                .Include(x => x.Status)
                                .Where(x => x.RequestId == request.RequestId)
                                .OrderByDescending(x => x.ChangeDate).ToList()[0].Status.StatusId;
                isInitialLoad = false;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }

        private void Button_SendComment_Click(object sender, RoutedEventArgs e)
        {
            if (TextBox_Comment.Text != "")
            {
                AddComment(TextBox_Comment.Text);
            }
        }

        private bool isInitialLoad = true;

        private void ComboBox_RequestStatus_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (isInitialLoad)
            {
                isInitialLoad = false;
                return;
            }
            int statusId = int.Parse(ComboBox_RequestStatus.SelectedValue.ToString());
            dbContext.RequestStatuses.Add(new RequestStatus
            {
                StatusId = statusId,
                RequestId = request.RequestId,
                ChangeDate = DateTime.Now,
        });
            dbContext.SaveChanges();
        }

        private void MenuItem_DeleteEmployee_Click(object sender, RoutedEventArgs e)
        {
            if (UserInfo.Employee.EmployeeRole != 1)
            {
                return;
            }
            MenuItem menuItem = (MenuItem)sender;
            RequestDesignatedEmployee designatedEmployee = (RequestDesignatedEmployee)menuItem.DataContext;
            dbContext.RequestDesignatedEmployees.Remove(designatedEmployee);
            dbContext.SaveChanges();
            LoadDesignatedEmployees();
        }

        public void LoadStatuses()
        {
            List<Status> statusList = dbContext.Statuses.ToList();
            if (statusList != null)
            {
                ComboBox_RequestStatus.ItemsSource = statusList;
            }           
        }
        public void LoadStatusesHistory()
        {
            List<RequestStatus> requestStatuses = dbContext.RequestStatuses.Include(x => x.Status).Where(x => x.RequestId == request.RequestId).OrderByDescending(x => x.ChangeDate).ToList();

            if (requestStatuses != null)
            {
                ListBox_StatusInfo.ItemsSource = requestStatuses;
            }
        }
        public void LoadDesignatedEmployees()
        {
            List<RequestDesignatedEmployee> designatedEmployees = dbContext.RequestDesignatedEmployees.Include(x => x.Employee).Where(x => x.RequestId == request.RequestId).ToList();

            if (designatedEmployees != null)
            {
                ListBox_Employees.ItemsSource = designatedEmployees;
            }
        }
        public void LoadComments()
        {
            List<RequestComment> requestComments = dbContext.RequestComments.Where(x => x.RequestId == request.RequestId).ToList();
            if (requestComments != null)
            {
                ListBox_Comments.ItemsSource = requestComments;
            }
        }

        public void LoadEquipment()
        {
            List<RequestEquipmentList> equipmentList = dbContext.RequestEquipmentLists
                .Include(x => x.Equipment).ThenInclude(d => d.DefectType)
                .Where(x => x.RequestId == request.RequestId).ToList();
            if (equipmentList != null)
            {
                ListBox_Equipment.ItemsSource = equipmentList;
            }
        }
        public void AddComment(string message)
        {
            dbContext.RequestComments.Add(new RequestComment { CommentContent = message, RequestId = request.RequestId });
            dbContext.SaveChanges();
            LoadComments();
        }

        private void TextBox_Description_TextChanged(object sender, TextChangedEventArgs e)
        {
           if (TextBox_Description.Text.Length > 0)
           {
                Request requestq = dbContext.Requests.FirstOrDefault(x => x.RequestId == request.RequestId);
                requestq.RequestDescription = TextBox_Description.Text;
                dbContext.Update(requestq);
           }
        }

        private void Button_AddEmployee_Click(object sender, RoutedEventArgs e)
        {
            EmployeeAddModalWindow modalWindow = new EmployeeAddModalWindow();
            modalWindow.ShowDialog();

            Employee employee = EmployeeAddModalWindow.employee;
            if (employee != null)
            {
                dbContext.RequestDesignatedEmployees.Add(new RequestDesignatedEmployee
                {
                    RequestId = request.RequestId,
                    EmployeeId = employee.EmployeeId,
                });
                dbContext.SaveChanges();
            }
            LoadDesignatedEmployees();
            EmployeeAddModalWindow.employee = null;
        }

        private void Button_AddEquipment_Click(object sender, RoutedEventArgs e)
        {
            EquipmentAddModalWindow modalWindow = new EquipmentAddModalWindow();
            modalWindow.ShowDialog();

            Equipment equipment = EquipmentAddModalWindow.equipment;
            if (equipment.DefectTypeId == 0 )
            {
                return;
            }
            if (equipment != null)
            {
                dbContext.Equipment.Add(equipment);
                dbContext.SaveChanges();
            }
            else
            {
                return;
            }
            dbContext.RequestEquipmentLists.Add(new RequestEquipmentList
            {
                RequestId = request.RequestId,
                EquipmentId = equipment.EquipmentId,
            });
            dbContext.SaveChanges();
            LoadEquipment();
            EquipmentAddModalWindow.equipment = new Equipment();
        }

        private void MenuItem_DeleteEquipment_Click(object sender, RoutedEventArgs e)
        {
            if (UserInfo.Employee.EmployeeRole != 1)
            {
                return;
            }
            MenuItem menuItem = (MenuItem)sender;
            RequestEquipmentList equipment = (RequestEquipmentList)menuItem.DataContext;
            dbContext.RequestEquipmentLists.Remove(equipment);
            dbContext.SaveChanges();
            LoadEquipment();
        }
    }
}
