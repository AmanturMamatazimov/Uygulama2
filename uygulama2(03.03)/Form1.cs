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

namespace uygulama2_03._03_
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            listBox1.Items.Add(textBox1.Text);
            textBox1.Clear();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            SqlConnection bag = new SqlConnection(@"server=DESKTOP-DNDEKN8\SQLEXPRESS;initial catalog=uygulama2;integrated security=yes");
            foreach(var item in listBox1.Items)
            {
                SqlCommand komut = new SqlCommand("insert into urunler(urun) values (@ad)", bag);
                komut.Parameters.AddWithValue("@ad",item);
                bag.Open();
                komut.ExecuteNonQuery();
                bag.Close();
            }
            MessageBox.Show("Kayit edildi");
            listBox1.Items.Clear();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SqlConnection bag = new SqlConnection(@"server=DESKTOP-DNDEKN8\SQLEXPRESS;initial catalog=uygulama2;integrated security=yes");
            SqlDataAdapter da = new SqlDataAdapter("select * from urunler",bag);
            DataTable tablo = new DataTable();
            da.Fill(tablo);
            dataGridView1.DataSource = tablo;
            dataGridView1.Columns[0].HeaderText = "SiraNo";
            dataGridView1.Columns[1].HeaderText = "Urun Adi";
            dataGridView1.Columns[2].HeaderText = "Fiyat";

        }

        private void button4_Click(object sender, EventArgs e)
        {
            SqlConnection bag = new SqlConnection(@"server=DESKTOP-DNDEKN8\SQLEXPRESS;initial catalog=uygulama2;integrated security=yes");
            SqlDataAdapter da = new SqlDataAdapter("select urun,fiyat from urunler where urun like '%"+textBox2.Text+"%';", bag);
            DataTable tablo = new DataTable();
            da.Fill(tablo);
            dataGridView2.DataSource = tablo;
            dataGridView2.Columns[0].HeaderText = "Urun Adi";
            dataGridView2.Columns[1].HeaderText = "Fiyat";
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            SqlConnection bag = new SqlConnection(@"server=DESKTOP-DNDEKN8\SQLEXPRESS;initial catalog=uygulama2;integrated security=yes");
            SqlDataAdapter da = new SqlDataAdapter("select urun,fiyat from urunler where urun like '%" + textBox2.Text + "%';", bag);
            DataTable tablo = new DataTable();
            da.Fill(tablo);
            dataGridView2.DataSource = tablo;
            dataGridView2.Columns[0].HeaderText = "Urun Adi";
            dataGridView2.Columns[1].HeaderText = "Fiyat";
        }

        private void button5_Click(object sender, EventArgs e)
        {
            SqlConnection bag = new SqlConnection(@"server=DESKTOP-DNDEKN8\SQLEXPRESS;initial catalog=uygulama2;integrated security=yes");
            SqlCommand komut=new SqlCommand("select * from urunler where id=@id", bag);
            komut.Parameters.AddWithValue("@id", textBox3.Text);
            bag.Open();
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
                textBox4.Text = dr["fiyat"].ToString();
            bag.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            SqlConnection bag = new SqlConnection(@"server=DESKTOP-DNDEKN8\SQLEXPRESS;initial catalog=uygulama2;integrated security=yes");
            SqlCommand komut = new SqlCommand("update urunler set fiyat=@fiyat where id=@id", bag);
            komut.Parameters.AddWithValue("@fiyat", textBox4.Text);
            komut.Parameters.AddWithValue("@id", textBox3.Text);
            bag.Open();
            komut.ExecuteNonQuery();
            bag.Close();
            label4.Text = "Güncellendi";      
        }

        private void button7_Click(object sender, EventArgs e)
        {
            SqlConnection bag = new SqlConnection(@"server=DESKTOP-DNDEKN8\SQLEXPRESS;initial catalog=uygulama2;integrated security=yes");
            SqlCommand komut = new SqlCommand("select * from urunler where id=@id", bag);
            komut.Parameters.AddWithValue("@id", textBox5.Text);
            bag.Open();
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                label8.Text= dr["urun"].ToString();
                label9.Text= dr["fiyat"].ToString();
            }
            bag.Close();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            SqlConnection bag = new SqlConnection(@"server=DESKTOP-DNDEKN8\SQLEXPRESS;initial catalog=uygulama2;integrated security=yes");
            SqlCommand komut = new SqlCommand("delete from urunler where id=@id", bag);
            komut.Parameters.AddWithValue("@id", textBox5.Text);
            bag.Open();
            komut.ExecuteNonQuery();
            bag.Close();
            label10.Text = "Silindi";
        }
    }
}
