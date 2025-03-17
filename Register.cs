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
    public partial class Register : Form
    {
        NpgsqlConnection con;
        NpgsqlCommand cmd;
        string connectionString = "Host=localhost;Username=postgres;Password=1234;Database=Crypto";
        string strSQL;
        string strSQL2;
        public Register()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string login = textBox1.Text, password = textBox2.Text;
                con = new NpgsqlConnection(connectionString);
                con.Open();
                strSQL = string.Format("create user {0} with password '{1}'", login, password);
                cmd = new NpgsqlCommand(strSQL, con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Пользователь добавлен.");

                strSQL2 = string.Format("INSERT INTO logindata VALUES('{0}',{1}, 'User')", login, password);
                cmd = new NpgsqlCommand(strSQL2, con);
                cmd.ExecuteNonQuery();

                textBox1.Text = textBox2.Text = "";
                this.Hide();
                Autorization autorization = new Autorization();
                autorization.Show();
            }
            catch (NpgsqlException ex)
            {
                MessageBox.Show("Невозможно добавить пользователя!");
            }
            finally
            {
                con.Close();
            }
        }

        private void Register_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void roundButton2_Click(object sender, EventArgs e)
        {
            Autorization autoriazation = new Autorization();
            this.Hide();
            autoriazation.Show();
        }
    }
}
