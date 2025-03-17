using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace DB_Interface_lab_7
{
    public partial class Form1 : Form
    {
        private string login;
        public Form1(string login)
        {
            InitializeComponent();
            
            label1.Parent = pictureBox2;
            label1.BackColor = Color.Transparent;
            
            label3.Parent = pictureBox2;
            label3.BackColor = Color.LightYellow;
            label4.Parent = pictureBox2;
            label4.BackColor = Color.LightYellow;
            radioButton1.BackColor = Color.LightYellow;
            radioButton2.BackColor = Color.LightYellow;
            radioButton3.BackColor = Color.LightYellow;
            radioButton4.BackColor = Color.LightYellow;
            radioButton5.BackColor = Color.LightYellow;
            radioButton6.BackColor = Color.LightYellow;
            this.login = login;
        }

        private void DbButton_Click(object sender, EventArgs e)
        {
            Clients clients = new Clients(login);
            Workers workers = new Workers(login);
            Material material = new Material(login);
            Product product = new Product(login);
            Order order = new Order(login);
            Admin admin = new Admin();
            if (radioButton1.Checked)
                clients.Show();
            else if (radioButton2.Checked)
                workers.Show();
            else if (radioButton3.Checked)
                material.Show();
            else if (radioButton4.Checked)
                product.Show();
            else if (radioButton6.Checked)
                admin.Show();
            else
                order.Show();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            label4.Text = login;
            if (label4.Text != "postgres")
            {
                radioButton6.Visible = false;
            }
        }

        private void quitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void aboutButton_Click(object sender, EventArgs e)
        {
            InfoForm infoform = new InfoForm();
            infoform.Show();
        }

        private void procedureButton_Click(object sender, EventArgs e)
        {
            Procedure procedure = new Procedure();
            procedure.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Autorization autorization = new Autorization();
            this.Hide();
            autorization.Show();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void radioButton6_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
