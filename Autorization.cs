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

    public partial class Autorization : Form
    {
        NpgsqlConnection con;
        NpgsqlCommand cmd;
        string connectionString = "Host=localhost;Username=postgres;Password=1234;Database=Crypto";
        string strSQL;
        string login;
        public Autorization()
        {
            InitializeComponent();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Register register = new Register();
            register.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            login = textBox1.Text;
            int password = Convert.ToInt32(textBox2.Text);
            con = new NpgsqlConnection(connectionString);
            con.Open();
            strSQL = string.Format("SELECT COUNT(*) FROM logindata WHERE login = '{0}' AND password = {1}", login, password);
            cmd = new NpgsqlCommand(strSQL, con);
            int count = Convert.ToInt32(cmd.ExecuteScalar());
            if (count > 0)
            {
                this.Hide();
                Form1 form1 = new Form1(login);
                form1.Show();
            }
            else
            {
                MessageBox.Show("Invalid login or password.");
            }

        }

        private void Autorization_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
