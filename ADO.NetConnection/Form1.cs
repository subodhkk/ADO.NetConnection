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
    public partial class Form1 : Form
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;
        public Form1()
        {
            InitializeComponent();
            string strConnection = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            con = new SqlConnection(strConnection);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string str = "insert into Emp_id values(@id,@name,@salary)";
                cmd = new SqlCommand(str, con);
                cmd.Parameters.AddWithValue("@id", Convert.ToInt32(txtEmpid.Text));
                cmd.Parameters.AddWithValue("@name", txtEmpName.Text);
                cmd.Parameters.AddWithValue("@salary", Convert.ToDouble(txtSalary.Text));

                con.Open();

                int result = cmd.ExecuteNonQuery();
                if (result == 1)
                {
                    MessageBox.Show("Record inserted");
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
                string str = "update Emp_id set Name=@name,Salary= @salary where Id=@id";
                cmd = new SqlCommand(str, con);
                cmd.Parameters.AddWithValue("@id", Convert.ToInt32(txtEmpid.Text));
                cmd.Parameters.AddWithValue("@name", txtEmpName.Text);
                cmd.Parameters.AddWithValue("@salary", Convert.ToDouble(txtSalary.Text));

                con.Open();

                int result = cmd.ExecuteNonQuery();
                if (result == 1)
                {
                    MessageBox.Show("Record Updated");
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

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                string str = "delete from Emp_id where Id=@id";
                cmd = new SqlCommand(str, con);
                cmd.Parameters.AddWithValue("@id", Convert.ToInt32(txtEmpid.Text));


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

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string str = "select * from Emp_id where Id=@id";
                cmd = new SqlCommand(str, con);
                cmd.Parameters.AddWithValue("@id", Convert.ToInt32(txtEmpid.Text));


                con.Open();
                dr=cmd.ExecuteReader();
                if(dr.HasRows)
                {
                    while(dr.Read())
                    {
                        txtEmpName.Text = dr["Name"].ToString();
                        txtSalary.Text=dr["Salary"].ToString();
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

        private void btnShowAll_Click(object sender, EventArgs e)
        {
            try
            {
                string str = "select * from Emp_id";
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

