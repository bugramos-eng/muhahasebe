using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace muhahasebe
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection conn = new SqlConnection("Data Source=PC10402\\SQLEXPRESS;Initial Catalog=muhasebe;Integrated Security=True;TrustServerCertificate=True");
        private void Form1_Load(object sender, EventArgs e)
        {
            doldur();
        }
        private void doldur()
        {
            string cumle = "select * from firma";
            SqlDataAdapter adapter = new SqlDataAdapter(cumle, conn);
            DataTable tb = new DataTable();
            adapter.Fill(tb);
            dataGridView1.DataSource = tb;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            conn.Open();
            string cumle = "insert into firma (Ad,VergiNo,Adres,Tel) values (@Ad,@VergiNo,@Adres,@Tel)";
            SqlCommand cmd=new SqlCommand(cumle, conn);
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@Ad",textBox1.Text);
            cmd.Parameters.AddWithValue("@VergiNo", textBox2.Text);
            cmd.Parameters.AddWithValue("@Adres", textBox3.Text);
            cmd.Parameters.AddWithValue("@Tel", textBox4.Text);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Kayıt Eklendi");
            doldur();

            conn.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int satir = dataGridView1.CurrentRow.Index;
            textBox1.Text = dataGridView1.Rows[satir].Cells[1].Value.ToString();
            textBox2.Text = dataGridView1.Rows[satir].Cells[2].Value.ToString();
            textBox3.Text = dataGridView1.Rows[satir].Cells[3].Value.ToString();
            textBox4.Text = dataGridView1.Rows[satir].Cells[4].Value.ToString();
            textBox5.Text = dataGridView1.Rows[satir].Cells[0].Value.ToString();

        }
    }
}
