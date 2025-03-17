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
    public partial class Admin : Form
    {
        NpgsqlConnection con;
        NpgsqlCommand cmd;
        NpgsqlDataReader reader;
        string connectionString = "Host = localhost; Username = postgres; Password = 1234; Database = Crypto";
        string strSQL;
        DataTable dt;
        public Admin()
        {
            InitializeComponent();
            dataGridView1.SelectionChanged += new EventHandler(dataGridView1_SelectionChanged);
            InitData();
        }
        private void InitData()
        {
            try
            {
                con = new NpgsqlConnection(connectionString);
                con.Open();
                cmd = new NpgsqlCommand("SELECT * FROM logindata", con);
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
            }
        }
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            int pos = Convert.ToInt32(bn1.PositionItem.ToString());
            if (pos >= 0 && pos < dataGridView1.RowCount)
            {
                DataGridViewRow row = dataGridView1.Rows[pos - 1];
                textBox1.Text = Convert.ToString(row.Cells[0].Value).Trim();
                textBox2.Text = Convert.ToString(row.Cells[1].Value).Trim();
                comboBox1.Text = Convert.ToString(row.Cells[2].Value).Trim();
            }
        }

        private void changeButton_Click(object sender, EventArgs e)
        {
            dataGridView1.SelectionChanged -= new EventHandler(dataGridView1_SelectionChanged);
            strSQL = string.Format("UPDATE logindata SET " +
                "role='{0}' WHERE login='{1}' AND password={2}", comboBox1.Text, textBox1.Text, textBox2.Text);
            try
            {
                con = new NpgsqlConnection(connectionString);
                con.Open();
                cmd = new NpgsqlCommand(strSQL, con);
                cmd.ExecuteNonQuery();
                InitData();
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
            strSQL = string.Format("DELETE FROM logindata WHERE login='{0}' AND password={1}", textBox1.Text, textBox2.Text);
            try
            {
                con = new NpgsqlConnection(connectionString);
                con.Open();
                cmd = new NpgsqlCommand(strSQL, con);
                cmd.ExecuteNonQuery();
                InitData();
                textBox1.Text = "";
                textBox2.Text = "";
                comboBox1.Text = "";
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
                strSQL = string.Format("select * from logindata where login='{0}'", textBox1.Text);
            else if (textBox2.Text != "")
                strSQL = string.Format("select * from logindata where password='{0}'", textBox2.Text);
            else if (comboBox1.Text != "")
                strSQL = string.Format("select * from logindata where role ='{0}'", comboBox1.Text);
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
            comboBox1.Text = "";
            InitData();
            dataGridView1.SelectionChanged += new EventHandler(dataGridView1_SelectionChanged);
        }
    }
}
