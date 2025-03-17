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
    public partial class Order : Form
    {
        private string login;
        NpgsqlConnection con;
        NpgsqlCommand cmd;
        NpgsqlDataReader reader;
        NpgsqlCommand roleCheck;
        string connectionString = "Host=localhost;Username=postgres;Password=1234;Database=Crypto";
        string strSQL;
        DataTable dt;
        string id;
        string permissions;

        public Order(string login)
        {
            InitializeComponent();
            dataGridView1.SelectionChanged += new EventHandler(dataGridView1_SelectionChanged);
            this.login = login;
            InitData();
        }

        private void InitData()
        {
            try
            {
                con = new NpgsqlConnection(connectionString);
                con.Open();
                cmd = new NpgsqlCommand("SELECT * FROM creator", con);
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
                textBox5.Text = Convert.ToString(row.Cells[4].Value).Trim();
                
            }
        }

        private void insertButton_Click(object sender, EventArgs e)
        {
            dataGridView1.SelectionChanged -= new EventHandler(dataGridView1_SelectionChanged);
            strSQL = string.Format("INSERT INTO Заказ(Размер,\"Метров изделия\",Количество,Итоговая_цена," +
                                   "\"Дата заказа\",\"ID материала\",\"ID изделия\",\"ID клиента\",\"ID работника\") " +
                                   "VALUES('{0}', '{1}', '{2}', '{3}', '{4}'::date, {5}, {6}, {7}, {8})",
                                   textBox2.Text, textBox3.Text, textBox4.Text, textBox5.Text
                                   );
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
                dataGridView1.SelectionChanged += new EventHandler(dataGridView1_SelectionChanged);
            }
        }

        private void changeButton_Click(object sender, EventArgs e)
        {
            dataGridView1.SelectionChanged -= new EventHandler(dataGridView1_SelectionChanged);
            strSQL = string.Format("UPDATE Заказ SET " +
                                   "Размер={0},\"Метров изделия\"={1},Количество={2},Итоговая_цена={3}," +
                                   "\"Дата заказа\"={4}::date,\"ID материала\"={5},\"ID изделия\"={6},\"ID клиента\"={7},\"ID работника\"={8}" +
                                   "where \"ID заказа\"={9}", textBox2.Text, textBox3.Text, textBox4.Text, textBox5.Text);
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
            strSQL = string.Format("DELETE FROM Заказ WHERE \"ID заказа\"={0}", textBox1.Text);
            try
            {
                con = new NpgsqlConnection(connectionString);
                con.Open();
                cmd = new NpgsqlCommand(strSQL, con);
                cmd.ExecuteNonQuery();
                InitData();
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
                textBox5.Text = "";
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
                strSQL = string.Format("select * from Заказ where \"ID заказа\"={0}", textBox1.Text);
            else if (textBox2.Text != "")
                strSQL = string.Format("select * from Заказ where Размер={0}", textBox2.Text);
            else if (textBox3.Text != "")
                strSQL = string.Format("select * from Заказ where \"Метров изделия\"='{0}'", textBox3.Text);
            else if (textBox4.Text != "")
                strSQL = string.Format("select * from Заказ where Количество='{0}'", textBox4.Text);
            else if (textBox5.Text != "")
                strSQL = string.Format("select * from Заказ where Итоговая_цена='{0}'", textBox5.Text);
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
            textBox5.Text = "";
            InitData();
            dataGridView1.SelectionChanged += new EventHandler(dataGridView1_SelectionChanged);
        }
    }
}
