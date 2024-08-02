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
    public partial class FrmKategoriler : Form
    {
        public FrmKategoriler()
        {
            InitializeComponent();
        }

        SqlConnection baglanti = new SqlConnection(@"Data Source=FatihKurekci\SQLEXPRESS;Initial Catalog=SatisVT;Integrated Security=True;TrustServerCertificate=True");

        private void btnListele_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("Select * From KATEGORI", baglanti);
            SqlDataAdapter da = new SqlDataAdapter(komut);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            baglanti.Close();
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut2 = new SqlCommand("Insert Into KATEGORI (KATEGORIAD) Values (@p1)", baglanti);
            komut2.Parameters.AddWithValue("@p1", txtKategoriAd.Text);
            komut2.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Kategori kaydetme işlemi başarıyla gerçekleşti.");
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtKategoriID.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtKategoriAd.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut3 = new SqlCommand("Delete From KATEGORI Where KATEGORIID=@p1", baglanti);
            komut3.Parameters.AddWithValue("@p1", txtKategoriID.Text);
            komut3.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Kategori silme işlemi gerçekleşti.");
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut4 = new SqlCommand("Update KATEGORI Set KATEGORIAD=@p1 Where KATEGORIID=@p2", baglanti);
            komut4.Parameters.AddWithValue("@p1", txtKategoriAd.Text);
            komut4.Parameters.AddWithValue("@p2", txtKategoriID.Text);
            komut4.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Kategori güncelleme işlemi gerçekleşti.");
        }
    }
}
