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

namespace automobil
{
    public partial class Form1 : Form
    {
        void sacredRefresh()
        {
            if (podaci.Rows.Count == 0)
            {
                button5.Enabled = false;
                button6.Enabled = false;
                button4.Enabled = false;
                button7.Enabled = false;
                button3.Enabled = false;
                button2.Enabled = false;
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
                textBox5.Text = "";
            }
            else
            {
                textBox1.Text = podaci.Rows[gde]["id"].ToString();
                textBox2.Text = podaci.Rows[gde]["br_sasije"].ToString();
                textBox3.Text = podaci.Rows[gde]["br_motora"].ToString();
                textBox4.Text = podaci.Rows[gde]["marka"].ToString();
                textBox5.Text = podaci.Rows[gde]["boja"].ToString();

                button6.Enabled = (gde != podaci.Rows.Count - 1);
                button7.Enabled = (gde != podaci.Rows.Count - 1);
                button5.Enabled = (gde != 0);
                button4.Enabled = (gde != 0);
            }

        }
        string cs = "Data source = DESKTOP-3586HO0\\SQLEXPRESS; Initial catalog = Automobil; Integrated security = true";
        int gde = 0;
        DataTable podaci = new DataTable();
        SqlConnection veza;
        string br_sasije, br_motora, marka, boja;
        SqlDataAdapter adapter;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            veza = new SqlConnection(cs);
            adapter = new SqlDataAdapter("select * from automobil", veza);
            adapter.Fill(podaci);
            textBox1.Enabled = false;
            sacredRefresh();
        }

        private void idText_TextChanged(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (gde + 1 <= podaci.Rows.Count)
            {
                gde++;
                sacredRefresh();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            veza = new SqlConnection(cs);
            SqlCommand naredba = new SqlCommand(String.Format($"delete from automobil where id={textBox1.Text}"), veza);
            veza.Open();
            naredba.ExecuteNonQuery();
            veza.Close();
            podaci.Clear();
            adapter = new SqlDataAdapter("select * from automobil", veza);
            adapter.Fill(podaci);
            gde = 0;
            sacredRefresh();
        }

        private void button2_Click(object sender, EventArgs e)
        {

            veza = new SqlConnection(cs);
            br_sasije = textBox2.Text;
            br_motora = textBox3.Text;
            marka = textBox4.Text;
            boja = textBox5.Text;
            if (br_sasije == "" && br_motora == "" && marka == "" && boja == "")
                MessageBox.Show("Unesite makar jedan podatak za updateovanje");
            veza.Open();
            SqlCommand naredba = new SqlCommand($"update automobil set br_sasije = '{br_sasije}', br_motora = '{br_motora}', marka = '{marka}', boja = '{boja}' where id = {textBox1.Text}", veza);
            naredba.ExecuteNonQuery();
            veza.Close();
            podaci.Clear();
            adapter = new SqlDataAdapter("select * from automobil", veza);
            adapter.Fill(podaci);
            sacredRefresh();

        }

        private void button8_Click(object sender, EventArgs e)
        {
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (gde - 1 >= 0)
            {
                gde--;
                sacredRefresh();
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            gde = podaci.Rows.Count - 1;
            sacredRefresh();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            gde = 0;
            sacredRefresh();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            veza = new SqlConnection(cs);
            br_sasije = textBox2.Text;
            br_motora = textBox3.Text;
            marka = textBox4.Text;
            boja = textBox5.Text;

            veza.Open();
            SqlCommand naredba = new SqlCommand($"insert into automobil values('{br_sasije}','{br_motora}','{marka}','{boja}')", veza);
            naredba.ExecuteNonQuery();
            veza.Close();
            podaci.Clear();
            adapter = new SqlDataAdapter("select * from automobil", veza);
            adapter.Fill(podaci);
            gde = podaci.Rows.Count - 1;
            sacredRefresh();
        }
    }
}           
 