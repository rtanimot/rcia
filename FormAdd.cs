using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace SemesterProjectApp
{
    public partial class FormAdd : Form
    {
        public int idCustomer;
        public string dirCurrent;
        public string dirData = @"\data.csv";
        public void Clear()
        {
            txt1.Clear();
            txt2.Clear();
            txt3.Clear();
            txt4.Clear();
            txt5.Clear();
            txt6.Clear();
            txt7.Clear();
            txt8.Clear();
            txt9.Clear();
            txt10.Clear();
        }
        public void UpdateCustomerId()
        {
            string dir = dirCurrent + dirData;
            lblId.Text = (File.ReadLines(dir).Count() + 1).ToString();
        }
        public FormAdd()
        {
            InitializeComponent();

            // Directory.SetCurrentDirectory(@"..\..\data");
            dirCurrent = Directory.GetCurrentDirectory();
            txt1.Focus();
            UpdateCustomerId();
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Hide(); // Close the Customer Add form without closing the whole application
        }

        private void FormAdd_Load(object sender, EventArgs e)
        {
            //
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Debug.WriteLine(dirCurrent);
            List<TextBox> lstTxt = new List<TextBox>() { txt1, txt2, txt3, txt4, txt5, txt6, txt7, txt8, txt9, txt10 };

            string strLine = "";
            bool empty = true; // If all boxes are empty, then it will not let the user save the info
            for (int i = 0; i < lstTxt.Count; i++)
            {
                strLine += lstTxt[i].Text;
                if (lstTxt[i].Text.Length > 0)
                {
                    empty = false; // Box is not empty
                }
                if (i != lstTxt.Count - 1) // Adds a comma after each item except for the last one
                {
                    strLine += ',';
                }
                // Debug.WriteLine(lstTxt[i].Text.Length.ToString());
            }

            string dir = dirCurrent + dirData;
            if (!empty)
            {
                using (StreamWriter sw = new StreamWriter(dir, append: true)) // Appends the info to the file (if the file does not exist, then it is created)
                {
                    sw.WriteLine(strLine);
                    sw.Close();
                    Clear();
                    MessageBox.Show("Customer info was successfully added!");
                    txt1.Focus();
                    UpdateCustomerId();
                }
            }
            else
            {
                MessageBox.Show("Please enter the customer information first!"); // If all boxes are empty
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show(text: "Are you sure you would like to clear?", caption: "", buttons: MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                Clear();
            }
        }
    }
}
