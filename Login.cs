using Car_Rental_Management_System.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Car_Rental_Management_System
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void Login_Load(object sender, EventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string message = "Do you want to close this Application?";
            string title = "Close Application";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result = MessageBox.Show(message, title, buttons);
            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
            else
            {
                
            } 

        }

   

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox1.Focus();

        }
        
        SqlConnection Cnn = new SqlConnection(@Settings.Default["ConStr"].ToString());
        
        private void button3_Click(object sender, EventArgs e)
        {
            //
            Cnn.Open();
            string query = "select * from UserTb where Uname='"+textBox1.Text+"' and Upassword='"+textBox2.Text+"'";
            SqlDataAdapter Da = new SqlDataAdapter(query, Cnn);
            SqlCommandBuilder builder = new SqlCommandBuilder(Da);
            var ds = new DataSet();
            Da.Fill(ds);
            DataTable dt=ds.Tables[0];
            Cnn.Close();

            if (dt!=null&&dt.Rows.Count>0)
            {

                //foreach (DataRow row in dt.Rows)
                //{
                //    string Uname = row["Uname"].ToString();
                //    string Upassword = row["Upassword"].ToString();
                //}
                if (textBox1.Text == dt.Rows[0]["Uname"].ToString() && textBox2.Text == dt.Rows[0]["Upassword"].ToString())
                {
                    Main_Menu temp = new Main_Menu();
                    this.Hide();
                    temp.Show();
                }
            }
            else
            {
                string message = "Your Password was not corect";
                string title = "Wrong Password!";
                MessageBoxButtons buttons = MessageBoxButtons.RetryCancel;
                DialogResult result = MessageBox.Show(message, title, buttons);
                if (result == DialogResult.Cancel)
                {
                    Application.Exit();
                }
                else
                {
                    textBox1.Text = "";
                    textBox2.Text = "";
                    textBox1.Focus();
                }
            }
        }
    }
}
