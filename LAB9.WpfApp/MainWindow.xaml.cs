using LAB9.BLL;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Reflection;
using System.Xml.Serialization;
using System.Text.Json;
using Microsoft.Win32;

namespace LAB9.WpfApp
{
    public partial class MainWindow : Window
    {
        public List<Student> Students { get; set; }
        public MainWindow()
        {
            Students = new List<Student>();
            InitializeComponent();
            DataGridTable.ItemsSource = Students;
        }
        private void MainRemoveButtonStudentClick(object sender, RoutedEventArgs e)
        {
            if (DataGridTable.SelectedItem is Student std)
            {
                Students.Remove(std);
                DataGridTable.Items.Refresh();
            }
        }
        private void MainAddStudentClick(object sender, RoutedEventArgs e)
        {
            AddStudentWindow addStudentWindow = new AddStudentWindow();
            addStudentWindow.ShowDialog();
            Students.Add(addStudentWindow.Student);
            DataGridTable.Items.Refresh();
        }
        private void MainAddGradeToStudentClick(object sender, RoutedEventArgs e)
        {
            if (DataGridTable.SelectedItem is Student std)
            {
                AddGrade addGrade = new AddGrade();
                addGrade.ShowDialog();
                std.GradesContainer.Add(addGrade.Value);
                DataGridTable.Items.Refresh();
            }
        }

        private async void MainSaveToTxtClick(object sender, RoutedEventArgs e)
        {
            FileStream fs = new FileStream("data.txt", mode: FileMode.OpenOrCreate);
            using (StreamWriter sw = new StreamWriter(fs))
            {
                await sw.Save(Students, DataGridTable);
            }
        }
        private void MainLoadFromTxtClick(object sender, RoutedEventArgs e)
        {
            FileStream fs = new FileStream("data.txt", mode: FileMode.Open);
            using (StreamReader sr = new StreamReader(fs))
            {
                int Count = 0;
                while (!sr.EndOfStream)
                {
                    if (sr.ReadLine() == "[[Student]]")
                    {
                        Count++;
                    }
                    if (Count > 0)
                        break;
                }

                if (Count > 0)
                {
                    sr.DiscardBufferedData();
                    sr.BaseStream.Seek(0, SeekOrigin.Begin);
                    Students.Clear();
                    string FN = string.Empty;
                    string LN = string.Empty;
                    string FAC = string.Empty;
                    string GRD = string.Empty;
                    int IDX = 0;
                    while (!sr.EndOfStream)
                    {
                        GRD = string.Empty;
                        var ln = sr.ReadLine();

                        switch (ln)
                        {
                            case "[FirstName]":
                                ln = sr.ReadLine();
                                FN = ln;
                                break;
                            case "[LastName]":
                                ln = sr.ReadLine();
                                LN = ln;
                                break;
                            case "[Faculty]":
                                ln = sr.ReadLine();
                                FAC = ln;
                                break;
                            case "[Index]":
                                ln = sr.ReadLine();
                                IDX = int.Parse(ln);
                                break;

                            case "[Grades]":
                                ln = sr.ReadLine();
                                GRD = ln;
                                break;
                        }
                        if (GRD != "")
                        {
                            Students.Add(new Student(FN, LN, FAC, IDX)); //proyob po UML 2!!!!
                            string[] grades = GRD.Split(';');
                            foreach (var gr in grades)
                            {
                                Students.FirstOrDefault(s => s.Index == IDX).GradesContainer.Add(double.Parse(gr));
                            }
                        }
                    }
                    DataGridTable.Items.Refresh();
                }
                else
                {
                    MessageBox.Show("No Students!");
                    return;
                }
            }
        }

        private void MainSaveToXMLClick(object sender, RoutedEventArgs e)
        {
            using (FileStream fileStream = new FileStream("datax.xml", FileMode.OpenOrCreate))
            {
                XmlSerializer xmlSer = new XmlSerializer(typeof(List<Student>));    
                xmlSer.Serialize(fileStream, Students);
            }
        }

        private void MainLoadFromXMLClick(object sender, RoutedEventArgs e)
        {
            using (FileStream fileStream = new FileStream("datax.xml", FileMode.OpenOrCreate))
            {
                XmlSerializer xmlSer = new XmlSerializer(typeof(List<Student>));
                if(xmlSer.Deserialize(fileStream) is List<Student> stds && stds.Count > 0)
                {
                    Students = stds;
                    DataGridTable.ItemsSource = Students;
                    DataGridTable.Items.Refresh();
                }
            }
        }

        private void MainSaveToJSONClick(object sender, RoutedEventArgs e)
        {
            string jsonStr = JsonSerializer.Serialize(Students, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText("dataj.json", jsonStr);
        }

        private void MainLoadFromJSONClick(object sender, RoutedEventArgs e)
        {
            string jsonStr = File.ReadAllText("dataj.json");
            Students = JsonSerializer.Deserialize<List<Student>>(jsonStr);
            DataGridTable.ItemsSource = Students;
            DataGridTable.Items.Refresh();
        }
    }
}