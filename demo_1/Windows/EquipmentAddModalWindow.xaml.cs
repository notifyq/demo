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
    /// Логика взаимодействия для EquipmentAddModalWindow.xaml
    /// </summary>
    public partial class EquipmentAddModalWindow : Window
    {
        public static Equipment equipment = new Equipment();
        db_demo_2024Context dbContext = new db_demo_2024Context();
        public EquipmentAddModalWindow()
        {
            InitializeComponent();
            LoadDefectTypes();
        }

        private void Button_Add_Click(object sender, RoutedEventArgs e)
        {
            if (ComboBox_DefectType.SelectedIndex == -1)
            {
                return;
            }
            string name = TextBox_EquipmentName.Text;
            string serial_number = TextBox_EquipmentSerialNumber.Text;
            equipment.DefectTypeId = int.Parse(ComboBox_DefectType.SelectedValue.ToString());  
            if (name.Length > 0 && serial_number.Length > 0)
            {
                equipment.EquipmentName = name;
                equipment.EquipmentSerialNumber = serial_number;
                equipment.DefectTypeId = int.Parse(ComboBox_DefectType.SelectedValue.ToString());
                this.Close();
            }
            
        }

        public void LoadDefectTypes()
        {
            List<DefectType> defectTypes = dbContext.DefectTypes.ToList();
            ComboBox_DefectType.ItemsSource = defectTypes;
        }
    }
}
