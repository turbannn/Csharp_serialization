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
    /// Логика взаимодействия для AddGrade.xaml
    /// </summary>
    public partial class AddGrade : Window
    {
        public double Value { get; set; }
        public AddGrade()
        {
            InitializeComponent();
        }
        public void AddButtonGradeClick(object sender, RoutedEventArgs e)
        {
            if (!Regex.IsMatch(input: AddGradeTB.Text, pattern: @"^\d+\,\d{2,}$"))
            {
                MessageBox.Show("Invalid input data");
                return;
            }
            Value = double.Parse(AddGradeTB.Text);
            Value = Math.Truncate(Value * 100) / 100;
            DialogResult = true;
        }
    }
}
