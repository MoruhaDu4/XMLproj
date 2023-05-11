using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Xml;
namespace WindowsFormsApp16
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
            LoadEmployees();
        }

        public string none = "(none)";
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("Fill in all fields", "Error");
            }
            else
            {
                int n = dataGridView1.Rows.Add();
                dataGridView1.Rows[n].Cells[0].Value = textBox1.Text;
                dataGridView1.Rows[n].Cells[1].Value = numericUpDown1.Value;
                dataGridView1.Rows[n].Cells[2].Value = comboBox1.Text;
              
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int n = dataGridView1.SelectedRows[0].Index;
                dataGridView1.Rows[n].Cells[0].Value = textBox1.Text;
                dataGridView1.Rows[n].Cells[1].Value = numericUpDown1.Value;
                dataGridView1.Rows[n].Cells[2].Value = comboBox1.Text;
                
            }
            else
            {
                MessageBox.Show("Select a term to edit.", "Error.");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                dataGridView1.Rows.RemoveAt(dataGridView1.SelectedRows[0].Index);
            }
            else
            {
                MessageBox.Show("Select a term to delete.", "Error.");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                dt.TableName = "Employee";
                dt.Columns.Add("Name");
                dt.Columns.Add("Age");          
                ds.Tables.Add(dt);

                foreach (DataGridViewRow r in dataGridView1.Rows)
                {
                    DataRow row = ds.Tables["Employee"].NewRow();

                    row["Name"] = r.Cells[0].Value;
                    row["Age"] = r.Cells[1].Value;
                    row["Sex"] = r.Cells[2].Value;                   
                    ds.Tables["Employee"].Rows.Add(row);
                }
                ds.WriteXml("C:\\Users\\DU4\\Downloads\\Data1.xml");
                MessageBox.Show("XML file saved successfully.", "Completed.");
            }
            catch
            {
                MessageBox.Show("Unable to save XML file.", "Error.");
            }


        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                textBox1.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                int n = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[1].Value);
                numericUpDown1.Value = n;
                comboBox1.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            }
            catch { }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                dataGridView1.Rows.Clear();
            }
            else
            {
                MessageBox.Show("Table is empty.", "Error.");
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = "C:\\Users\\Acer\\Downloads\\";
            openFileDialog1.Filter = "Text Files (*.xml)|*.xml|All Files (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;
            if (dataGridView1.Rows.Count > 0)
            {
                button5_Click(sender, e);
            }

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                DataSet ds = new DataSet();
                ds.ReadXml(openFileDialog1.FileName);
                foreach (DataRow item in ds.Tables["Employee"].Rows)
                {
                    int n = dataGridView1.Rows.Add();
                    dataGridView1.Rows[n].Cells[0].Value = item["Name"];
                    dataGridView1.Rows[n].Cells[1].Value = item["Age"];
                    dataGridView1.Rows[n].Cells[2].Value = item["Sex"];
                    dataGridView1.Rows[n].Cells[3].Value = item["Programmer"];
                    dataGridView1.Rows[n].Cells[4].Value = item["Employment in"];
                }
            }
            else
                MessageBox.Show("XML file not found", "Error.");
        }

       

        
    }

}
