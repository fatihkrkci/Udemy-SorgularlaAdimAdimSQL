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
using System.Data.SqlClient;

namespace Proje_SQL_DB
{
    public partial class FrmAnaForm : Form
    {
        public FrmAnaForm()
        {
            InitializeComponent();
        }
        
        SqlConnection baglanti = new SqlConnection(@"Data Source=FatihKurekci\SQLEXPRESS;Initial Catalog=SatisVT;Integrated Security=True;TrustServerCertificate=True");

        private void btnKategoriler_Click(object sender, EventArgs e)
        {
            FrmKategoriler frmKategoriler = new FrmKategoriler();
            frmKategoriler.Show();
        }

        private void btnCikisYap_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnMusteriler_Click(object sender, EventArgs e)
        {
            FrmMusteri frmMusteri = new FrmMusteri();
            frmMusteri.Show();
        }

        private void FrmAnaForm_Load(object sender, EventArgs e)
        {
            KritikSeviyeUrunleriGetir();
            GrafigeVeriCekmeKategoriler();
            GrafigeVeriCekmeSehirler();
        }

        void KritikSeviyeUrunleriGetir()
        {
            SqlCommand komut = new SqlCommand("Execute Test4", baglanti);
            SqlDataAdapter da = new SqlDataAdapter(komut);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        void GrafigeVeriCekmeKategoriler()
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("Select KATEGORIAD,COUNT(*) FROM KATEGORI INNER JOIN URUNLER ON KATEGORI.KATEGORIID=URUNLER.KATEGORI GROUP BY KATEGORIAD", baglanti);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                chart1.Series["Kategoriler"].Points.AddXY(dr[0], dr[1]);
            }
            baglanti.Close();
        }

        void GrafigeVeriCekmeSehirler()
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("SELECT MUSTERISEHIR, COUNT(*) FROM MUSTERI GROUP BY MUSTERISEHIR", baglanti);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                chart2.Series["Sehirler"].Points.AddXY(dr[0], dr[1]);
            }
            baglanti.Close();
        }
    }
}
