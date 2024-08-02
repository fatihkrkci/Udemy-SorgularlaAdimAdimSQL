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

namespace Proje_SQL_DB
{
    public partial class FrmMusteri : Form
    {
        public FrmMusteri()
        {
            InitializeComponent();
        }

        SqlConnection baglanti = new SqlConnection(@"Data Source=FatihKurekci\SQLEXPRESS;Initial Catalog=SatisVT;Integrated Security=True;TrustServerCertificate=True");

        void Listele()
        {
            SqlCommand komut = new SqlCommand("Select * From MUSTERI", baglanti);
            SqlDataAdapter da = new SqlDataAdapter(komut);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        void Sehirler()
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("Select * From SEHIR", baglanti);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                cmbSehir.Items.Add(dr["SehirAd"]);
            }
            baglanti.Close();
        }

        private void FrmMusteri_Load(object sender, EventArgs e)
        {
            Listele();
            Sehirler();
        }

        private void btnListele_Click(object sender, EventArgs e)
        {
            Listele();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtMusteriID.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtMusteriAd.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtMusteriSoyad.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            cmbSehir.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            txtMusteriBakiye.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("Insert Into MUSTERI (MUSTERIAD,MUSTERISOYAD,MUSTERISEHIR,MUSTERIBAKIYE) values (@p1, @p2,@p3, @p4)", baglanti);
            komut.Parameters.AddWithValue("@p1", txtMusteriAd.Text);
            komut.Parameters.AddWithValue("@p2", txtMusteriSoyad.Text);
            komut.Parameters.AddWithValue("@p3", cmbSehir.Text);
            komut.Parameters.AddWithValue("@p4", decimal.Parse(txtMusteriBakiye.Text));
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Müşteri Sisteme Kaydedildi");
            Listele();
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("Delete From MUSTERI WHERE MUSTERIID=@p1", baglanti);
            komut.Parameters.AddWithValue("@p1", txtMusteriID.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Müşteri Silindi");
            Listele();
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("Update MUSTERI Set MUSTERIAD=@p1,MUSTERISOYAD=@p2,MUSTERISEHIR=@p3,MUSTERIBAKIYE=@p4 WHERE MUSTERIID=@p5", baglanti);
            komut.Parameters.AddWithValue("@p1", txtMusteriAd.Text);
            komut.Parameters.AddWithValue("@p2", txtMusteriSoyad.Text);
            komut.Parameters.AddWithValue("@p3", cmbSehir.Text);
            komut.Parameters.AddWithValue("@p4", decimal.Parse(txtMusteriBakiye.Text));
            komut.Parameters.AddWithValue("@p5", txtMusteriID.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Müşteri Güncellendi");
            Listele();
        }

        private void btnAra_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("Select * FROM MUSTERI WHERE MUSTERIAD=@p1", baglanti);
            komut.Parameters.AddWithValue("@p1", txtMusteriAd.Text);
            SqlDataAdapter da = new SqlDataAdapter(komut);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }
    }
}
