using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics; // For debugging
using System.IO;
using System.Windows.Forms;

namespace SemesterProjectApp
{
    public partial class FormMain : Form
    {
        public string dirCurrent;
        public string dirData = @"\data.csv";
        public FormAdd formAdd;
        public FormUpdate formUpdate;
        public FormMain()
        {
            InitializeComponent();

            Directory.SetCurrentDirectory(@"..\..\data");
            dirCurrent = Directory.GetCurrentDirectory();
            formAdd = new FormAdd();
            formUpdate = new FormUpdate();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                formAdd.Show();
            }
            catch // In case the user presses X button on FormAdd instead of Exit button, which deletes it instead of hiding it
            {
                formAdd = new FormAdd();
                formAdd.Show();
            }
        }

        private void btnInfo_Click(object sender, EventArgs e)
        {
            // Debug.WriteLine(dirCurrent);

            string dir = dirCurrent + dirData;
            DataTable dt = new DataTable();
            List<string> lstCol = new List<string>() { "ID", "Company", "Addr. Line 1", "Addr. Line 2", "State", "Postal Code", "Country", "Contact Name", "Contact Position", "Contact Email", "Contact Phone #" };

            foreach (string col in lstCol)
            {
                dt.Columns.Add(col);
            }

            using (StreamReader sr = new StreamReader(dir))
            {
                int i = 0;
                while (!sr.EndOfStream)
                {
                    string[] line = sr.ReadLine().Split(',');
                    DataRow dr = dt.NewRow();
                    dr[0] = Convert.ToString(i + 1);
                    for (int j = 0; j < line.Length; j++)
                    {
                        dr[j + 1] = line[j];
                    }
                    dt.Rows.Add(dr);
                    i++;
                }
                dgv.DataSource = dt;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                formUpdate.Show();
            }
            catch // In case the user presses X button on FormAdd instead of Exit button, which deletes it instead of hiding it
            {
                formUpdate = new FormUpdate();
                formUpdate.Show();
            }
        }
    }
}
