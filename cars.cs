using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;
using Car_Rental_Management_System.Properties;

namespace Car_Rental_Management_System
{
    public partial class cars : Form
    {
        public cars()
        {
            InitializeComponent();
        }
       
        SqlConnection Cnn = new SqlConnection(@Settings.Default["ConStr"].ToString());
       
        private void populate()
        {
            Cnn.Open();
            string query = "Select * from Cars";
            SqlDataAdapter Da = new SqlDataAdapter(query, Cnn);
            SqlCommandBuilder builder = new SqlCommandBuilder(Da);
            var ds = new DataSet();
            Da.Fill(ds);
            CarsDGV.DataSource = ds.Tables[0];

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

        private void cars_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'car_Rental_SystemDataSet1.Cars' table. You can move, or remove it, as needed.
            //this.carsTableAdapter.Fill(this.car_Rental_SystemDataSet1.Cars);
            populate();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (RegNo.Text == "" || Brand.Text == "" || Model.Text == "" || Price.Text == "" || AvailableCb.SelectedItem.ToString() == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    Cnn.Open();
                    string query = "Insert into  Cars Values('" + RegNo.Text + "','" + Brand.Text + "','" + Model.Text + "','" + Price.Text + "','" + AvailableCb.SelectedItem.ToString() + "')"; 
                    SqlCommand cmd = new SqlCommand(query, Cnn);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Car Successfully Added");
                    Cnn.Close();
                    populate();

                }
                catch (Exception Myex)
                {
                    MessageBox.Show(Myex.Message);
                }

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (RegNo.Text == "")
            {
                MessageBox.Show("You have not provied any Car Registation No Which you want to delete");
            }
            else
            {
                try
                {
                    Cnn.Open();
                    string query = "Delete from Cars where RegNum='" + RegNo.Text + "';";
                    SqlCommand cmd = new SqlCommand(query, Cnn);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Car Delete Successfully");
                    Cnn.Close();
                    populate();

                }
                catch (Exception Myex)
                {
                    MessageBox.Show(Myex.Message);
                }
            }
        }

        private void CarsDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {


            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (RegNo.Text == "" || Brand.Text == "" || Model.Text == "" || Price.Text == "" || AvailableCb.SelectedItem.ToString() == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {

                    Cnn.Open();
                    string query = "update Cars Set Brand='" + Brand.Text + "' ,Model='" + Model.Text + "',Price='" + Price.Text + "' ,Avialable='" + AvailableCb.SelectedItem.ToString() + "'where RegNum='" + RegNo.Text + "';";
                    SqlCommand cmd = new SqlCommand(query, Cnn);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Car Successfully Updated");
                    Cnn.Close();
                    populate();
                }
                catch (Exception Myex)
                {
                    MessageBox.Show(Myex.Message);
                }

            }
        }

        private void CarsDGV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = CarsDGV.Rows[e.RowIndex];
                RegNo.Text = row.Cells[0].Value.ToString();
                Brand.Text = row.Cells[1].Value.ToString();
                Model.Text = row.Cells[2].Value.ToString();
                Price.Text = row.Cells[3].Value.ToString();
                AvailableCb.SelectedItem = row.Cells[4].Value.ToString();
            }
            /*RegNo.Text = CarsDGV.SelectedRows[0].Cells[0].Value.ToString();
            Brand.Text = CarsDGV.SelectedRows[0].Cells[1].Value.ToString();
            Model.Text = CarsDGV.SelectedRows[0].Cells[2].Value.ToString();
            Price.Text = CarsDGV.SelectedRows[0].Cells[3].Value.ToString();
            AvailableCb.SelectedItem = CarsDGV.SelectedRows[0].Cells[4].Value.ToString(); */
        }
    }
}
