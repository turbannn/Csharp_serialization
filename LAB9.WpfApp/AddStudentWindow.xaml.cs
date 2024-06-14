using LAB9.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace LAB9.WpfApp
{
    /// <summary>
    /// Логика взаимодействия для AddStudentWindow.xaml
    /// </summary>
    public partial class AddStudentWindow : Window
    {
        public Student Student { get; set; }
        public AddStudentWindow(Student std = null)
        {
            InitializeComponent();
            if (std != null)
            {
                FnBox.Text = std.FirstName;
                LnBox.Text = std.LastName;
                FacBox.Text = std.Faculty;
                IdxBox.Text = std.Index.ToString();
            }
            Student = std ?? new Student(); //if null set => new Student()
        }
        private void AddButtonStudentClick(object sender, RoutedEventArgs e)
        {
            if (!Regex.IsMatch(input: FnBox.Text, pattern: @"^\p{L}{1,12}$") ||
                !Regex.IsMatch(input: LnBox.Text, pattern: @"^\p{L}{1,12}$") ||
                !Regex.IsMatch(input: FacBox.Text, pattern: @"^\p{L}{1,12}$") ||
                !Regex.IsMatch(input: IdxBox.Text, pattern: @"[0-9]{4,10}$"))
            {
                MessageBox.Show("Invalid input data");
                return;
            }
            Student.FirstName = FnBox.Text;
            Student.LastName = LnBox.Text;
            Student.Faculty = FacBox.Text;
            Student.Index = int.Parse(IdxBox.Text);
            DialogResult = true;
        }
    }
}
