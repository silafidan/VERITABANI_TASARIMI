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



namespace WindowsFormsApp16
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

      
        private void button2_Click(object sender, EventArgs e)
        {
            SqlConnection baglanti = new SqlConnection("Data Source=localhost;Initial Catalog=Kitap2025;Integrated Security=True;TrustServerCertificate=True;");
            
            baglanti.Open();
            SqlCommand komut = new SqlCommand($"INSERT INTO TblKitaplar(KitapAd, Yazar, Sayfa, Fiyat, YayinEvi, Tur) VALUES ('{txtKitapAd.Text}', '{txtYazar.Text}', {txtSayfa.Text}, {txtFiyat.Text}," +
                $" '{txtYayinEvi.Text}', {cmbKitapTuru.SelectedValue})", baglanti);

            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Kayıt Eklendi");
            Listele();
        }

        void Listele()
        {
            SqlConnection baglanti = new SqlConnection("Data Source=localhost;Initial Catalog=Kitap2025;Integrated Security=True;TrustServerCertificate=True;");
            SqlDataAdapter adtr = new SqlDataAdapter("SELECT * FROM TblKitaplar", baglanti);
            DataTable dt = new DataTable();
            adtr.Fill(dt);
            dataGridView1.DataSource = dt;


        }

        private void btnkayitlistele_Click(object sender, EventArgs e)
        {
            Listele();
        }

        private void btnguncelle_Click(object sender, EventArgs e)
        {

             if (string.IsNullOrWhiteSpace(textBox1.Text))
             {
                 MessageBox.Show("Güncellenecek Kaydı Seçin");
                 return;
             }

             try
             {
                 int Kitapid = Convert.ToInt32(textBox1.Text);
                
                 SqlConnection baglanti = new SqlConnection("Data Source=localhost;Initial Catalog=Kitap2025;Integrated Security=True;TrustServerCertificate=True;");

                 baglanti.Open();
                 SqlCommand komut = new SqlCommand($"UPDATE TblKitaplar SET KitapAd='{txtKitapAd.Text}', Yazar='{txtYazar.Text}', Sayfa={txtSayfa.Text}, Fiyat={txtFiyat.Text}," +
                     $" YayinEvi='{txtYayinEvi.Text}', Tur='{cmbKitapTuru.SelectedValue}' WHERE Kitapid={Kitapid}", baglanti);
                 komut.ExecuteNonQuery();
                 baglanti.Close();

                 MessageBox.Show("Kayıt Güncellendi");
                 Listele();
             }
             catch (FormatException)
             {
                 MessageBox.Show("Kitap ID Değeri Geçerli Bir Sayı Olmalı");
             }
             catch (Exception )
             {
                 MessageBox.Show("Hata Oluştu  Lütfen tekrar deneyin");
             }
         }


       private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow satir = dataGridView1.Rows[e.RowIndex];

                textBox1.Text = satir.Cells["Kitapid"].Value?.ToString();
                txtKitapAd.Text = satir.Cells["KitapAd"].Value?.ToString();
                txtYazar.Text = satir.Cells["Yazar"].Value?.ToString();
                txtSayfa.Text = satir.Cells["Sayfa"].Value?.ToString();
                txtFiyat.Text = satir.Cells["Fiyat"].Value?.ToString();
                txtYayinEvi.Text = satir.Cells["YayinEvi"].Value?.ToString();
                cmbKitapTuru.SelectedValue = satir.Cells["Tur"].Value?.ToString();
            }
        }

        private void btnsil_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Silinecek Kaydı Seçin");
                return;
            }

            SqlConnection baglanti = new SqlConnection("Data Source=localhost;Initial Catalog=Kitap2025;Integrated Security=True;TrustServerCertificate=True;");
            if (MessageBox.Show("Kayıt Silinecek Onaylıyor Musunuz?", "Uyarı",MessageBoxButtons.YesNo,MessageBoxIcon.Question)==DialogResult.Yes)
            {
                int Kitapid = Convert.ToInt32(textBox1.Text);
                baglanti.Open();

                SqlCommand komut = new SqlCommand($"DELETE FROM TblKitaplar WHERE Kitapid={Kitapid}", baglanti);
                komut.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("Kayıt Silindi");
                Listele();

            }
           
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           
            SqlConnection baglanti = new SqlConnection("Data Source=localhost;Initial Catalog=Kitap2025;Integrated Security=True;TrustServerCertificate=True;");
            SqlDataAdapter da = new SqlDataAdapter("SELECT Turid, TurAd FROM TblTurler", baglanti);
            DataTable dt = new DataTable();
            da.Fill(dt);

            cmbKitapTuru.DisplayMember = "TurAd";  
            cmbKitapTuru.ValueMember = "Turid";     
            cmbKitapTuru.DataSource = dt;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}

