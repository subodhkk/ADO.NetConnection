using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;


namespace ADO.NetConnection
{
    public partial class Form3 : Form
    {
        SqlConnection con;
        SqlDataAdapter da;
        DataSet ds;
        SqlCommandBuilder scb;

        public Form3()
        {
            InitializeComponent();
            string strConnection = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            con = new SqlConnection(strConnection);
        }
        private DataSet GetEmpData()
        {
            da = new SqlDataAdapter("select * from Emp_id", con);
            
            da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
            scb = new SqlCommandBuilder(da);
            ds = new DataSet();
           
            da.Fill(ds, "emp"); 
            return ds;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                ds = GetEmpData();
                DataRow row = ds.Tables["emp"].NewRow();
                row["Name"] = txtEmpName.Text;
                row["Salary"] = txtSalary.Text;
                ds.Tables["emp"].Rows.Add(row);
               
                int result = da.Update(ds.Tables["emp"]);
                if (result == 1)
                {
                    MessageBox.Show("Record inserted");
                    txtEmpid.Clear();
                    txtEmpName.Clear();
                    txtSalary.Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                ds = GetEmpData();
                DataRow row = ds.Tables["emp"].Rows.Find(txtEmpid.Text);
                if (row != null)
                {
                    row["Name"] = txtEmpName.Text;
                    row["Salary"] = txtSalary.Text;
                    int result = da.Update(ds.Tables["emp"]);
                    if (result == 1)
                    {
                        MessageBox.Show("Record updated");
                        txtEmpid.Clear();
                        txtEmpName.Clear();
                        txtSalary.Clear();
                    }

                }
                else
                {
                    MessageBox.Show("Id does not exists to update");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                ds = GetEmpData();
                DataRow row = ds.Tables["emp"].Rows.Find(txtEmpid.Text);
                if (row != null)
                {
                    row.Delete();
                    int result = da.Update(ds.Tables["emp"]);
                    if (result == 1)
                    {
                        MessageBox.Show("Record Deleted");
                        txtEmpid.Clear();
                        txtEmpName.Clear();
                        txtSalary.Clear();
                    }

                }
                else
                {
                    MessageBox.Show("Id does not exists to delete");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                ds = GetEmpData();
                DataRow row = ds.Tables["emp"].Rows.Find(txtEmpid.Text);
                if (row != null)
                {
                    txtEmpName.Text = row["Name"].ToString();
                    txtSalary.Text = row["Salary"].ToString();
                }
                else
                {
                    MessageBox.Show("Record does not exists");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btnShowAll_Click(object sender, EventArgs e)
        {
            ds = GetEmpData();
            dataGridView1.DataSource = ds.Tables["emp"];
            

        }
    }
}
