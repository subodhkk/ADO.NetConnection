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
using System.Configuration;


namespace ADO.NetConnection
{
    public partial class Form2 : Form
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;
        public Form2()
        {
            InitializeComponent();
            string strConnection = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            con = new SqlConnection(strConnection);
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string str = "insert into Course_Details values(@name,@fees)";
                cmd = new SqlCommand(str, con);
                //cmd.Parameters.AddWithValue("@id", Convert.ToInt32(txtCourseId.Text));
                cmd.Parameters.AddWithValue("@name", txtCourseNm.Text);
                cmd.Parameters.AddWithValue("@fees", Convert.ToInt32(txtFees.Text));

                con.Open();

                int result = cmd.ExecuteNonQuery();
                if (result == 1)
                {
                    MessageBox.Show("Record inserted");
                    txtCourseId.Clear();
                    txtCourseNm.Clear();
                    txtFees.Clear();
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            finally
            {
                con.Close();
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                string str = "update Course_Details set CourseName=@name,Fees=@fees where Id=@id";
                cmd = new SqlCommand(str, con);
                cmd.Parameters.AddWithValue("@id", Convert.ToInt32(txtCourseId.Text));
                cmd.Parameters.AddWithValue("@name", txtCourseNm.Text);
                cmd.Parameters.AddWithValue("@fees", Convert.ToInt32(txtFees.Text));

                con.Open();

                int result = cmd.ExecuteNonQuery();
                if (result == 1)
                {
                    MessageBox.Show("Record updated");
                    txtCourseId.Clear();
                    txtCourseNm.Clear();
                    txtFees.Clear();
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            finally
            {
                con.Close();
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string str = "select * from Course_Details where Id=@id";
                cmd = new SqlCommand(str, con);
                cmd.Parameters.AddWithValue("@id", Convert.ToInt32(txtCourseId.Text));
                

                con.Open();
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        txtCourseNm.Text = dr["CourseName"].ToString();
                        txtFees.Text = dr["Fees"].ToString();
                    }
                }
                else
                    MessageBox.Show("Record Not Found");



            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            finally
            {
                con.Close();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                string str = "delete from Course_Details where Id=@id";
                cmd = new SqlCommand(str, con);
                cmd.Parameters.AddWithValue("@id", Convert.ToInt32(txtCourseId.Text));


                con.Open();

                int result = cmd.ExecuteNonQuery();
                if (result == 1)
                {
                    MessageBox.Show("Record Deleted");
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            finally
            {
                con.Close();
            }
        }

        private void btnshowall_Click(object sender, EventArgs e)
        {
            try
            {
                string str = "select * from Course_Details";
                cmd = new SqlCommand(str, con);



                con.Open();
                dr = cmd.ExecuteReader();

                DataTable dt = new DataTable();
                dt.Load(dr);
                dataGridView1.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            finally
            {
                con.Close();
            }
        }
    }
}
