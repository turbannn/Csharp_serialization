using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAB9.BLL
{
    public class Student
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Faculty { get; set; }
        public int Index { get; set; }
        public List<double> GradesContainer { get; set; }
        public string Grades
        {
            get
            {
                if (GradesContainer != null)
                    return string.Join("; ", GradesContainer);

                return "No Grades";
            }
        }
        public Student(string FName, string LName, string fac, int id)
        {
            FirstName = FName;
            LastName = LName;
            Faculty = fac;
            Index = id;
            GradesContainer = new List<double>();
        }
        public Student()
        {
            GradesContainer = new List<double>();
        }
    }
}
