using demo_1.Model;
using demo_1.Windows;
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
    public partial class RequestsPage : Page
    {
        List<Request> requestList = new List<Request>(0);
        db_demo_2024Context Context = new db_demo_2024Context();
        public RequestsPage()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddRequestModalWindow modalWindow = new AddRequestModalWindow();
            modalWindow.ShowDialog();
            LoadRequests();

        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Grid grid = (Grid)sender;
            Request request = grid.DataContext as Request;
            if (request != null)
            {
                this.NavigationService.Navigate(new RequestPage(request));
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            TextBlock_Role.Text = "Должность: " + UserInfo.Employee.EmployeeRoleNavigation.RoleName;
            TextBlock_UserFIO.Text = UserInfo.Employee.EmployeeFIO;

            if (UserInfo.Employee.EmployeeRole != 1)
            {
                Button_Add.Visibility = Visibility.Collapsed;
            }
            LoadRequests();   
        }

        public void LoadRequests()
        {
            if (ListView_Requests.Items.Count > 0)
            {
                ListView_Requests.Items.Clear();
            }
            if (UserInfo.Employee.EmployeeRole == 1)
            {
                requestList = Context.Requests.ToList();
            }
            else if (UserInfo.Employee.EmployeeRole == 2)
            {
                requestList = Context.RequestDesignatedEmployees.Where(x => x.EmployeeId == UserInfo.Employee.EmployeeId).Select(x => x.Request).ToList();
            }
            if (TextBox_Search.Text.Length > 0)
            {
                requestList = requestList.Where(x => x.RequestDescription.Contains(TextBox_Search.Text)).ToList();
            }
            foreach (Request item in requestList)
            {
                ListView_Requests.Items.Add(item);
            }
            ListView_Requests.Items.Refresh();

        }

        private void TextBox_Search_TextChanged(object sender, TextChangedEventArgs e)
        {
            LoadRequests();
        }
    }
}
