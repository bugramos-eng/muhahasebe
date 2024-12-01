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
    public partial class ürünler : Form
    {
        public ürünler()
        {
            InitializeComponent();
        }
        SqlConnection conn = new SqlConnection("Data Source=PC10402\\SQLEXPRESS;Initial Catalog=muhasebe;Integrated Security=True;TrustServerCertificate=True");
        private void ürünler_Load(object sender, EventArgs e)
        {
            doldur();
        }

        private void doldur()
        {
            string cumle = "Select * from kategoriler";
            SqlDataAdapter da = new SqlDataAdapter(cumle,conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                string cumle = "insert into kategoriler (kategoriad) values (@kategoriad)";
                SqlCommand cmd = new SqlCommand(cumle,conn);
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@kategoriad", textBox1.Text);
                cmd.ExecuteNonQuery();
                MessageBox.Show("kaydınız eklenmiştir");

                conn.Close();
                doldur();

            }
            catch (Exception ex)
            {

                MessageBox.Show("hata oluştu:"+ex.Message);
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int satir = dataGridView1.CurrentRow.Index;
            textBox1.Text = dataGridView1.Rows[satir].Cells[1].Value.ToString();
            textBox2.Text = dataGridView1.Rows[satir].Cells[0].Value.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult cevap = MessageBox.Show("bu kaydı silmek istiyormusunuz","silme", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (cevap == DialogResult.Yes)
                {
                    conn.Open();
                    string cumle = "delete from kategoriler where kategoriId=@Id";
                    SqlCommand cmd = new SqlCommand(cumle, conn);
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@Id", textBox2.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("kaydınız silinmiştir");

                    conn.Close();
                    doldur();
                }


            }
            catch (Exception ex)
            {

                MessageBox.Show("hata oluştu:" + ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                string cumle = "update kategoriler set kategoriad=@kategoriAd where kategoriId=@Id";
                SqlCommand cmd = new SqlCommand(cumle, conn);
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@kategoriAd", textBox1.Text);
                cmd.ExecuteNonQuery();
                MessageBox.Show("kaydınız silinmiştir");

                conn.Close();
                doldur();

            }
            catch (Exception ex)
            {

                MessageBox.Show("güncellenmiştir:" + ex.Message);
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox3.Text))
            {
                MessageBox.Show("arama kutusu boş olamaz");
                textBox3.Focus();
                textBox3.BackColor = Color.BlueViolet;
            }
            else
            {
                string cumle = "Select * from kategoriler where kategoriad like '%" + textBox3.Text + "%'";
                SqlDataAdapter da = new SqlDataAdapter(cumle,conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
                if (dt.Rows.Count ==0)
                {
                    MessageBox.Show("kayıt bulunmadı");
                }
            }
        }
    }
}
