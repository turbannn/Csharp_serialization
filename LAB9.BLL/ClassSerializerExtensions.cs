using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Text.Json;
using Microsoft.Win32;
using System.Runtime.CompilerServices;
using System.IO;
using System.Windows.Controls;
using LAB9.WpfApp;

namespace LAB9.BLL
{
    public static class ClassSerializerExtensions
    {
        public static async Task Save(this StreamWriter streamWriter, List<Student> students, DataGrid DataGridTable)
        {
            for (int i = 0; i < students.Count; i++)
            {
                await streamWriter.WriteLineAsync("[[Student]]");
                foreach (var column in DataGridTable.Columns)
                {
                    await streamWriter.WriteLineAsync("[" + column.Header.ToString() + "]");
#pragma warning disable CS8602
                    var sss = students[i].GetType().GetProperties().FirstOrDefault(p => p.Name == column.Header.ToString()).GetValue(students[i]).ToString();
#pragma warning restore CS8602
                    await streamWriter.WriteLineAsync(sss);
                }
            }
        }

    }
}
