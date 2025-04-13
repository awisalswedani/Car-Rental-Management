using Car_Rental_Management_System.Properties;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Car_Rental_Management_System
{
    public partial class Dashboard : Form
    {
        public Dashboard()
        {
            InitializeComponent();
        }
        SqlConnection Cnn = new SqlConnection(@Settings.Default["ConStr"].ToString());


        private void label2_Click(object sender, EventArgs e)
        {
            string message = "Do you want to close this window?";
            string title = "Close Window";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result = MessageBox.Show(message, title, buttons);
            if (result == DialogResult.Yes)
            {
                Main_Menu temp = new Main_Menu();
                this.Close();
                temp.Show();
            }
            else
            {

            }  
        }

        private void Dashboard_Load(object sender, EventArgs e)
        {
            string queryCar = "select count(*)  from Cars";
            SqlDataAdapter sda = new SqlDataAdapter(queryCar, Cnn);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            Carlab.Text = dt.Rows[0][0].ToString();

            string queryCustomer = "select count(*)  from Customer";
            SqlDataAdapter sda1 = new SqlDataAdapter(queryCustomer, Cnn);
            DataTable dt1 = new DataTable();
            sda1.Fill(dt1);
            Customerlab.Text = dt1.Rows[0][0].ToString();

            string queryUser = "select count(*)  from UserTb";
            SqlDataAdapter sda2 = new SqlDataAdapter(queryUser, Cnn);
            DataTable dt2 = new DataTable();
            sda2.Fill(dt2);
           
            Userlb.Text = dt2.Rows[0][0].ToString();

        
        
    }

        private void label7_Click(object sender, EventArgs e)
        {

        }
    }
    }

