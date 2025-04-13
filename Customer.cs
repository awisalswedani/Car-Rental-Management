using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using Car_Rental_Management_System.Properties;

namespace Car_Rental_Management_System
{
    public partial class Customer : Form
    {
        public Customer()
        {
            InitializeComponent();
        }
        SqlConnection Cnn = new SqlConnection(@Settings.Default["ConStr"].ToString());

        private void populate()
        {
            Cnn.Open();
            string query = "Select * from Customer";
            SqlDataAdapter Da = new SqlDataAdapter(query, Cnn);
            SqlCommandBuilder builder = new SqlCommandBuilder(Da);
            var ds = new DataSet();
            Da.Fill(ds);
            CustomerDGV.DataSource = ds.Tables[0];
            Cnn.Close();

        }

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

        private void button4_Click(object sender, EventArgs e)
        {
            Main_Menu temp = new Main_Menu();
            this.Close();
            temp.Show();
        }

        private void Customer_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'car_Rental_SystemDataSet2.Customer' table. You can move, or remove it, as needed.
            //this.customerTableAdapter.Fill(this.car_Rental_SystemDataSet2.Customer);
            populate();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (ID.Text == "" || Cname.Text == "" || Address.Text == "" || Phone.Text == "" )
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    Cnn.Open();
                    string query = "Insert into  Customer Values(" + ID.Text + ",'" + Cname.Text + "','" + Address.Text + "','" + Phone.Text + "')";
                    SqlCommand cmd = new SqlCommand(query, Cnn);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Customer Successfully Added");
                    Cnn.Close();
                    populate();

                }
                catch (Exception Myex)
                {
                    MessageBox.Show(Myex.Message);
                }
                finally
                {
                    Cnn.Close();
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (ID.Text == "")
            {
                MessageBox.Show("You have not provied any Customer id Which you want to delete");
            }
            else
            {
                try
                {
                    Cnn.Open();
                    string query = "Delete from Customer where C_id='" + ID.Text + "';";
                    SqlCommand cmd = new SqlCommand(query, Cnn);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Customer Delete Successfully");
                    Cnn.Close();
                    populate();

                }
                catch (Exception Myex)
                {
                    MessageBox.Show(Myex.Message);
                }
                finally
                {
                    Cnn.Close();
                }
            }
        }

        private void CustomerDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (ID.Text == "" || Cname.Text == "" || Address.Text == "" || Phone.Text == "" )
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {

                    Cnn.Open();
                    string query = "update Customer Set Cname='" + Cname.Text + "' ,C_address='" + Address.Text + "',C_phone='" + Phone.Text +  "'where C_id='" + ID.Text + "';";
                    SqlCommand cmd = new SqlCommand(query, Cnn);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Customer Successfully Updated");
                    Cnn.Close();
                    populate();
                }
                catch (Exception Myex)
                {
                    MessageBox.Show(Myex.Message);
                }
                finally
                {
                    Cnn.Close();
                }
            }
        }

        private void CustomerDGV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = CustomerDGV.Rows[e.RowIndex];
                ID.Text = row.Cells[0].Value.ToString();
                Cname.Text = row.Cells[1].Value.ToString();
                Address.Text = row.Cells[2].Value.ToString();
                Phone.Text = row.Cells[3].Value.ToString();

            }
            /*
             
            ID.Text = CustomerDGV.SelectedRows[0].Cells[0].Value.ToString();
            Cname.Text = CustomerDGV.SelectedRows[0].Cells[1].Value.ToString();
            Address.Text = CustomerDGV.SelectedRows[0].Cells[2].Value.ToString();
            Phone.Text = CustomerDGV.SelectedRows[0].Cells[3].Value.ToString();      */

        }
    }
}
