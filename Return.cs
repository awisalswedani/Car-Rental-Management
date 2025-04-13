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
    public partial class Return : Form
    {
        public Return()
        {
            InitializeComponent();
        }
        SqlConnection Cnn = new SqlConnection(@Settings.Default["ConStr"].ToString());

        private void populate()
        {
            Cnn.Open();
            string query = "Select * from Rental";
            SqlDataAdapter Da = new SqlDataAdapter(query, Cnn);
            SqlCommandBuilder builder = new SqlCommandBuilder(Da);
            var ds = new DataSet();
            Da.Fill(ds);
            RentDGV.DataSource = ds.Tables[0];

            Cnn.Close();

        }
      
        private void populateRet()
        {
            Cnn.Open();
            string query = "Select * from ReturnCar";
            SqlDataAdapter Da = new SqlDataAdapter(query, Cnn);
            SqlCommandBuilder builder = new SqlCommandBuilder(Da);
            var ds = new DataSet();
            Da.Fill(ds);
            ReturnDGV.DataSource = ds.Tables[0];
            Cnn.Close();

        }
        private void DeleteOnReturn()
        {
            int RentID;


            RentID = Convert.ToInt32(RentDGV.CurrentRow.Cells[0].Value.ToString());

            Cnn.Open();
            string query = "Delete from Rental where Rental_id=" + RentID + ";";
            SqlCommand cmd = new SqlCommand(query, Cnn);
            cmd.ExecuteNonQuery();
            Cnn.Close();
            populate();

        }

        private void UpdateOnRentDelete(string regNo)
        {
            Cnn.Open();
            string query = "update Cars Set Avialable='" + "Yes" + "'where RegNum='" + regNo + "';";
            SqlCommand cmd = new SqlCommand(query, Cnn);
            // MessageBox.Show("Successfully Updated");
            cmd.ExecuteNonQuery();
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

        private void Return_Load(object sender, EventArgs e)
        {

            populate();
            populateRet();
        }

            private void DelayTb_TextChanged(object sender, EventArgs e)
        {

        }

        private void ReturnDate_ValueChanged(object sender, EventArgs e)
        {

        }

        private void customerNameTb_TextChanged(object sender, EventArgs e)
        {

        }

        private void CarRegTb_TextChanged(object sender, EventArgs e)
        {

        }

        private void RidTb_TextChanged(object sender, EventArgs e)
        {

        }

        private void fineTb_TextChanged(object sender, EventArgs e)
        {

        }

        private void RentDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (RidTb.Text == "" || customerNameTb.Text == "" || fineTb.Text == "" || DelayTb.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    Cnn.Open();
                    string query = "Insert into ReturnCar values(" + RidTb.Text + ",'" + CarRegTb.Text + "','" + customerNameTb.Text + "',CONVERT(date,'" + ReturnDate.Value.Date.ToString("yyyy-MM-dd") + "'),'" + DelayTb.Text + "','" + fineTb.Text + "')";
                    SqlCommand cmd = new SqlCommand(query, Cnn);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Car Returned Successfully");
                    Cnn.Close();
                    UpdateOnRentDelete(CarRegTb.Text);
                    DeleteOnReturn();
                    populateRet();
                    
                }
                catch (Exception Myex)
                {
                    MessageBox.Show(Myex.Message);
                }

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void RentDGV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = RentDGV.Rows[e.RowIndex];
                if (row.Cells[0].Value != null && !string.IsNullOrEmpty(row.Cells[0].Value.ToString()))
                {
                    CarRegTb.Text = row.Cells[0].Value.ToString();
                    customerNameTb.Text = row.Cells[1].Value.ToString();
                    ReturnDate.Value = Convert.ToDateTime(row.Cells[5].Value);
                    DateTime d1 = ReturnDate.Value.Date;
                    DateTime d2 = DateTime.Now;
                    TimeSpan t = d2 - d1;
                    int NumOfDays = Convert.ToInt32(t.TotalDays);
                    if (NumOfDays <= 0)
                    {
                        DelayTb.Text = "No Delay";
                        fineTb.Text = "0";
                    }
                    else
                    {
                        DelayTb.Text = "" + NumOfDays;
                        fineTb.Text = "" + (NumOfDays * 300);
                    }
                }
            }
        }
    }
    }
