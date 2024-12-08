using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using static System.Net.Mime.MediaTypeNames;

namespace SemesterProjectApp
{
    public partial class FormUpdate : Form
    {
        public string dirCurrent;
        public string dirData = @"\data.csv";
        public string dir;
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
            txtId.Clear();
        }
        public int GetLastCustomerId()
        {
            string dir = dirCurrent + dirData;
            return File.ReadLines(dir).Count() + 1;
        }
        public FormUpdate()
        {
            InitializeComponent();

            dirCurrent = Directory.GetCurrentDirectory();
            dir = dirCurrent + dirData;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            int cId = Convert.ToInt32(txtId.Text);
            if (GetLastCustomerId() <= cId)
            {
                MessageBox.Show("Please enter a valid customer ID.");
            }
            else
            {
                var result = MessageBox.Show(text: "Are you sure you would like to delete this customer? This action cannot be undone.", caption: "", buttons: MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    string dirTemp = Path.GetTempFileName();
                    using (StreamReader sr = new StreamReader(dir))
                    using (StreamWriter sw = new StreamWriter(dirTemp))
                    {
                        int i = 0;
                        while (!sr.EndOfStream)
                        {
                            string line = sr.ReadLine();
                            if (i + 1 != cId)
                            {
                                sw.WriteLine(line);
                            }
                            i++;
                        }
                    }
                    File.Delete(dir);
                    File.Move(dirTemp, dir);
                    Clear();
                    MessageBox.Show("Customer info was successfully deleted!");
                    txtId.Focus();
                }
            }
        }
        private void btnGet_Click(object sender, EventArgs e)
        {
            try {
                int cId = Convert.ToInt32(txtId.Text);
                if (GetLastCustomerId() <= cId)
                {
                    MessageBox.Show("Please enter a valid customer ID.");
                }
                else
                {
                    List<TextBox> lstTxt = new List<TextBox>() { txt1, txt2, txt3, txt4, txt5, txt6, txt7, txt8, txt9, txt10 };
                    string[] line = new string[lstTxt.Count];
                    using (StreamReader sr = new StreamReader(dir))
                    {
                        int i = 0;
                        while (!sr.EndOfStream)
                        {
                            line = sr.ReadLine().Split(',');
                            if (i + 1 == cId)
                            {
                                break;
                            }
                            i++;
                        }
                    }
                    for (int i = 0; i < lstTxt.Count; i++)
                    {
                        lstTxt[i].Text = line[i];
                    }
                }
            }
            catch
            {
                MessageBox.Show("Please enter a valid customer ID.");
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            int cId = Convert.ToInt32(txtId.Text);
            if (GetLastCustomerId() <= cId)
            {
                MessageBox.Show("Please enter a valid customer ID.");
            }
            else
            {
                var result = MessageBox.Show(text: "Are you sure you would like to permanently update this customer info? This action cannot be undone.", caption: "", buttons: MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    List<TextBox> lstTxt = new List<TextBox>() { txt1, txt2, txt3, txt4, txt5, txt6, txt7, txt8, txt9, txt10 };
                    string lineNew = "";
                    bool empty = true; // If all boxes are empty, then it will not let the user save the info
                    for (int i = 0; i < lstTxt.Count; i++)
                    {
                        lineNew += lstTxt[i].Text;
                        if (lstTxt[i].Text.Length > 0)
                        {
                            empty = false; // Box is not empty
                        }
                        if (i != lstTxt.Count - 1) // Adds a comma after each item except for the last one
                        {
                            lineNew += ',';
                        }
                        // Debug.WriteLine(lstTxt[i].Text.Length.ToString());
                    }

                    string dir = dirCurrent + dirData;
                    if (!empty)
                    {
                        string dirTemp = Path.GetTempFileName();
                        using (StreamReader sr = new StreamReader(dir))
                        using (StreamWriter sw = new StreamWriter(dirTemp))
                        {
                            int i = 0;
                            while (!sr.EndOfStream)
                            {
                                string line = sr.ReadLine();
                                if (i + 1 != cId)
                                {
                                    sw.WriteLine(line);
                                }
                                else
                                {
                                    sw.WriteLine(lineNew);
                                }
                                i++;
                            }
                        }
                        File.Delete(dir);
                        File.Move(dirTemp, dir);
                        Clear();
                        MessageBox.Show("Customer info was successfully updated!");
                        txtId.Focus();
                    }
                    else
                    {
                        MessageBox.Show("Please enter the customer information first!"); // If all boxes are empty
                    }
                }
            }
        }
    }
}
