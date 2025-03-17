using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;

namespace DB_Interface_lab_7
{
    public partial class Procedure : Form
    {
        NpgsqlConnection con;
        NpgsqlCommand cmd;
        string res;
        string connectionString = "Host=localhost;Username=postgres;Password=1234;Database=Crypto";
        string strSQL;

        public Procedure()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox4.Text = "";
            strSQL = string.Format("Select \"email\" From \"owner\" \r\nwhere \"Owner\" = '{0}' ", textBox1.Text);
            try
            {
                con = new NpgsqlConnection(connectionString);
                con.Open();
                cmd = new NpgsqlCommand(strSQL, con);
                object result = cmd.ExecuteScalar();
                if (result != DBNull.Value)
                {
                    res = result?.ToString();
                }
                else
                {
                    res = "Имя не найдено";
                }
                textBox4.Text = res;
            }
            catch (NpgsqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                con.Close();
                //textBox4.Text = res.ToString();
            }
        }
    }
}
