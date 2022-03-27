using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Tema
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        DataTable table = new DataTable();
        int index;

        private void Form1_Load(object sender, EventArgs e)
        {
            table.Columns.Add("Movie Title", typeof(string));
            table.Columns.Add("Gender", typeof (string));
            table.Columns.Add("My rating", typeof (double));
            table.Columns.Add("About", typeof(string));

            dataGridView1.DataSource = table;

            string[] lines = File.ReadAllLines(@"C:\Users\Eu\OneDrive\Desktop\Tema\Tema\bin\Debug\fisier.txt");
            string[] values;

            for(int i = 0; i < lines.Length; i++)
            {
                values = lines[i].ToString().Split(',');
                string[] row = new string[values.Length];

                for(int j=0; j < values.Length; j++)
                {
                    row[j] = values[j].Trim();
                }

                table.Rows.Add(row);
            }
            
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            double i;
            if(titleBox.Text != "" && genreBox.Text != "" && double.TryParse(ratingBox.Text, out i) && aboutBox.Text !="" )
            {
               table.Rows.Add(titleBox.Text, genreBox.Text, ratingBox.Text, aboutBox.Text);
                clearFields();
            } else
            {
                MessageBox.Show("Invalid input");
            }
            
        }
          private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            index = e.RowIndex;
            DataGridViewRow row = dataGridView1.Rows[index];
            
            titleBox.Text = row.Cells[0].Value.ToString();
            genreBox.Text = row.Cells[1].Value.ToString();
            ratingBox.Text = row.Cells[2].Value.ToString();
            aboutBox.Text = row.Cells[3].Value.ToString();

        }
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            double i;
            DataGridViewRow newdata = dataGridView1.Rows[index];
            if (titleBox.Text != "" && genreBox.Text != "" && double.TryParse(ratingBox.Text, out i) && aboutBox.Text != "")
            {
              newdata.Cells[0].Value = titleBox.Text;
              newdata.Cells[1].Value = genreBox.Text;
              newdata.Cells[2].Value = ratingBox.Text;
              newdata.Cells[3].Value = aboutBox.Text;
              clearFields();
            }else
            {
                MessageBox.Show("You didnt select a movie");
            }
                
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            string message = "Do you want to delete this movie";
            string title = "Delete the list";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result = MessageBox.Show(message, title, buttons);
            if (result == DialogResult.Yes)
            {
                index = dataGridView1.CurrentCell.RowIndex;
            dataGridView1.Rows.RemoveAt(index);
            clearFields();
            }
            
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            string message = "Do you want to delete the entire list";
            string title = "Delete the list";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result = MessageBox.Show(message, title, buttons);
            if (result == DialogResult.Yes)
            {
                table.Clear();
            }
            
        }

       
        public void clearFields()
        {
            titleBox.Text=" ";
            genreBox.Text = "";
            ratingBox.Text = "";
            aboutBox.Text = "";
        }

       
        private void pictureBox1_Click(object sender, EventArgs e)
        {

            string message = "Do you want to exit?";
            string title = "Delete the list";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result = MessageBox.Show(message, title, buttons);
            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            StreamWriter writer = new StreamWriter(@"C:\Users\Eu\OneDrive\Desktop\Tema\Tema\bin\Debug\fisier.txt");
            
            for(int i = 0; i< dataGridView1.Rows.Count-1; i++)
            {
                writer.Write( dataGridView1.Rows[i].Cells[0].Value.ToString() + ',');
                writer.Write( dataGridView1.Rows[i].Cells[1].Value.ToString() + ',');
                writer.Write( dataGridView1.Rows[i].Cells[2].Value.ToString() + ',');
                writer.WriteLine( dataGridView1.Rows[i].Cells[3].Value.ToString() );

            }
            writer.Close();
        }
    }
   
}
