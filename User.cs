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
    public partial class User : Form
    {
        public User()
        {
            InitializeComponent();
        }

        SqlConnection Cnn = new SqlConnection(@Settings.Default["ConStr"].ToString());



        private void label5_Click(object sender, EventArgs e)
        {

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

        private void User_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'car_Rental_SystemDataSet.UserTable' table. You can move, or remove it, as needed.
            //this.userTableTableAdapter.Fill(this.car_Rental_SystemDataSet.UserTable);
            populate();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            Main_Menu temp = new Main_Menu();
            this.Close();
            temp.Show();
        }
        private void populate()
        {
            Cnn.Open();
            string query = "Select * from UserTb";
            SqlDataAdapter Da = new SqlDataAdapter(query, Cnn);
            SqlCommandBuilder builder = new SqlCommandBuilder(Da);
            var ds = new DataSet();
            Da.Fill(ds);
            UserDGV.DataSource = ds.Tables[0];

            Cnn.Close();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Uid.Text == "" || Uname.Text == "" || Upasswordtxt.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    Cnn.Open();
                    string query = "Insert into  UserTb Values(" + Uid.Text + ",'" + Uname.Text + "','" + Upasswordtxt.Text + "','Admin')";
                    SqlCommand cmd = new SqlCommand(query, Cnn);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("User Successfully Added");
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
            if (Uid.Text == "")
            {
                MessageBox.Show("You have not provied any ID Which you want to delete");
            }
            else
            {
                try
                {
                    Cnn.Open();
                    string query = "Delete from UserTb where ID='" + Uid.Text + "';";
                    SqlCommand cmd = new SqlCommand(query, Cnn);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("User Delete Successfully");
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

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (Uid.Text == "" || Uname.Text == "" || Upasswordtxt.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {  
                    
                    Cnn.Open();
                    string query = "update UserTb Set Uname='" + Uname.Text + "' ,Upassword='" + Upasswordtxt.Text + "' where ID='" + Uid.Text + "';";
                    SqlCommand cmd = new SqlCommand(query, Cnn);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("User Successfully Updated");
                    Cnn.Close();
                    populate();
                }
                catch (Exception Myex)
                {
                    MessageBox.Show(Myex.Message);
                }finally
                {
                    Cnn.Close();
                }

            }
        }
        

        private void UserDGV_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void UserDGV_SelectionChanged(object sender, EventArgs e)
        {

            

        }

        private void UserDGV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = UserDGV.Rows[e.RowIndex];
                Uid.Text = row.Cells[0].Value.ToString();
                Uname.Text = row.Cells[1].Value.ToString();
                Upasswordtxt.Text = row.Cells[2].Value.ToString();
            }

            //Uid.Text = UserDGV.SelectedRows[0].Cells[0].Value.ToString();
            //Uname.Text = UserDGV.SelectedRows[0].Cells[1].Value.ToString();
            //Upassword.Text = UserDGV.SelectedRows[0].Cells[2].Value.ToString();
        }
    }
}
