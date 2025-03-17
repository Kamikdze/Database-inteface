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
    public partial class Clients : Form
    {
        private string login;
        NpgsqlConnection con;
        NpgsqlCommand cmd;
        DataTable dt;
        NpgsqlDataReader reader;
        NpgsqlCommand roleCheck;
        string connectionString = "Host=localhost;Username=postgres;Password=1234;Database=Crypto";
        string strSQL;
        string id;
        string permissions;

        public Clients(string login)
        {
            InitializeComponent();
            dataGridView1.SelectionChanged += new EventHandler(dataGridView1_SelectionChanged);
            this.login = login;
            InitData(GetDt());
        }

        private DataTable GetDt()
        {
            return dt;
        }

        private void InitData(DataTable dt)
        {
            try
            {
                con = new NpgsqlConnection(connectionString);
                con.Open();
                cmd = new NpgsqlCommand("SELECT * FROM Coin", con);
                reader = cmd.ExecuteReader();
                dt = new DataTable();
                dt.Load(reader);
                bs1.DataSource = dt;
                bn1.BindingSource = bs1;
                dataGridView1.DataSource = bs1;

                roleCheck = new NpgsqlCommand(($"SELECT role FROM logindata WHERE login='{login}'"), con);
                permissions = Convert.ToString(roleCheck.ExecuteScalar());
                if (permissions != "Admin")
                {
                    changeButton.Enabled = false;
                    insertButton.Enabled = false;
                    deleteButton.Enabled = false;
                }
            }
            catch (NpgsqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                con.Close();
            }
        }
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            int pos = Convert.ToInt32(bn1.PositionItem.ToString());
            if (pos >= 0 && pos < dataGridView1.RowCount)
            {
                DataGridViewRow row = dataGridView1.Rows[pos - 1];
                textBox1.Text = Convert.ToString(row.Cells[0].Value).Trim();
                id = textBox1.Text;
                textBox2.Text = Convert.ToString(row.Cells[1].Value).Trim();
                textBox3.Text = Convert.ToString(row.Cells[2].Value).Trim();
                textBox4.Text = Convert.ToString(row.Cells[3].Value).Trim();
                
            }
        }

        private void insertButton_Click(object sender, EventArgs e)
        {
            dataGridView1.SelectionChanged -= new EventHandler(dataGridView1_SelectionChanged);
            strSQL = string.Format("INSERT INTO Coin(Date,Code,Name,Company) VALUES('{0}', '{1}', '{2}', '{3}')", 
                textBox2.Text, textBox3.Text, textBox4.Text);
            try
            {
                con = new NpgsqlConnection(connectionString);
                con.Open();
                cmd = new NpgsqlCommand(strSQL, con);
                cmd.ExecuteNonQuery();
                InitData(GetDt());
            }
            catch (NpgsqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                con.Close();
                dataGridView1.SelectionChanged += new EventHandler(dataGridView1_SelectionChanged);
            }
        }

        private void changeButton_Click(object sender, EventArgs e)
        {
            dataGridView1.SelectionChanged -= new EventHandler(dataGridView1_SelectionChanged);
            strSQL = string.Format("UPDATE coin SET " +
                "Date='{0}', Code='{1}', Name='{2}', Company='{3}' " +
                "where \"Code\"={4}", textBox2.Text, textBox3.Text, textBox4.Text,  textBox1.Text);
            try
            {
                con = new NpgsqlConnection(connectionString);
                con.Open();
                cmd = new NpgsqlCommand(strSQL, con);
                cmd.ExecuteNonQuery();
                InitData(GetDt());
            }
            catch (NpgsqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                con.Close();
                dataGridView1.SelectionChanged += new
               EventHandler(dataGridView1_SelectionChanged);
            }
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            dataGridView1.SelectionChanged -= new EventHandler(dataGridView1_SelectionChanged);
            strSQL = string.Format("DELETE FROM coin WHERE \"Code\"={0}", textBox2.Text);
            try
            {
                con = new NpgsqlConnection(connectionString);
                con.Open();
                cmd = new NpgsqlCommand(strSQL, con);
                cmd.ExecuteNonQuery();
                InitData(GetDt());
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
                
            }
            catch (NpgsqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                con.Close();
                dataGridView1.SelectionChanged += new EventHandler(dataGridView1_SelectionChanged);
            }
        }

        private void findButton_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
                strSQL = string.Format("select * from Coin where \"Date\"={0}", textBox1.Text);
            else if (textBox2.Text != "")
                strSQL = string.Format("select * from Coin where \"Code\"='{0}'", textBox2.Text);
            else if (textBox3.Text != "")
                strSQL = string.Format("select * from Coin where \"Name\"='{0}'", textBox3.Text);
            else if (textBox4.Text != "")
                strSQL = string.Format("select * from Coin where \"Company\"='{0}'", textBox4.Text);
            if (strSQL != "")
            {
                dataGridView1.SelectionChanged -= new EventHandler(dataGridView1_SelectionChanged);
                try
                {
                    con = new NpgsqlConnection(connectionString);
                    con.Open();
                    cmd = new NpgsqlCommand(strSQL, con);
                    reader = cmd.ExecuteReader();
                    dt = new DataTable();
                    dt.Load(reader);
                    bs1.DataSource = dt;
                    bn1.BindingSource = bs1;
                    dataGridView1.DataSource = bs1;
                }
                catch (NpgsqlException ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    con.Close();
                    dataGridView1.SelectionChanged += new
                   EventHandler(dataGridView1_SelectionChanged);
                }
            }
        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            dataGridView1.SelectionChanged -= new EventHandler(dataGridView1_SelectionChanged);
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            InitData(GetDt());
            dataGridView1.SelectionChanged += new EventHandler(dataGridView1_SelectionChanged);
        }

        private void Clients_Load(object sender, EventArgs e)
        {

        }
    }
}
