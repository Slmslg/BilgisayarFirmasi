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
using System.Data.Sql;
using System.Collections;

namespace BilgisayarFirmasi
{
    public partial class UrunEkle : Form
    {
        public UrunEkle()
        {
            InitializeComponent();
        }
        public String urunID, UrunSeriNo;
        SqlCommand ekle;
        SqlConnection baglanti = new SqlConnection("Data Source=.; Initial Catalog=BILGISAYAR; Integrated Security=true");

        private void UrunEkle_Load(object sender, EventArgs e)
        {
            urunSeriNoText.Text = UrunSeriNo;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {

                if (baglanti.State == ConnectionState.Closed)
                {
                    baglanti.Open();
                }
                ekle = new SqlCommand("INSERT INTO URUNLER(UrunID,UrunAd,Marka,Mensei,GarantiSuresi,UrunModel,KDV,BurutFiyat,Fiyat,StokDurum) VALUES (@UrunAd,@Marka,@Mensei,@GarantiSuresi,@UrunModel,@KDV,@BurutFiyat,@StokDurum)", baglanti);
                ekle.Parameters.AddWithValue("@UrunAd", UrunSeriNo.ToString());
                ekle.Parameters.AddWithValue("@Marka", ramKapasiteText.Text);
                ekle.Parameters.AddWithValue("@Mensei", ramTipText.Text);
                ekle.Parameters.AddWithValue("@GarantiSuresi", textBox11.Text);
                ekle.Parameters.AddWithValue("@UrunModel", hddMarkaText.Text);
                ekle.Parameters.AddWithValue("@KDV", textBox11.Text);
                ekle.Parameters.AddWithValue("@BurutFiyat", hddKapasiteText.Text);
                ekle.Parameters.AddWithValue("@StokDurum", hddTurText.Text);

                ekle.ExecuteNonQuery();

                MessageBox.Show("Ekleme işlemi başarıyla yapılmıştır.");
                baglanti.Close();
            }
            catch (SqlException)
            {
                MessageBox.Show("URUNLER VERİTABANI HATASI... !!!");
            }

        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            /* STOK TABLOSUNA VERİ EKLEME İŞLEMİ */
            urunIdText.Text = (Convert.ToInt16(urunID) + 1).ToString();
            try
            {
                if (baglanti.State == ConnectionState.Closed)
                {
                    baglanti.Open();
                }
                ekle = new SqlCommand("INSERT INTO STOK(UrunAd,Marka,Mensei,GarantiSuresi,UrunModel,StokDurum,UrunAdet) VALUES (@UrunAd,@Marka,@Mensei,@GarantiSuresi,@UrunModel,@StokDurum,@UrunAdet)", baglanti);
                ekle.Parameters.AddWithValue("@UrunAd", urunAdText.Text);
                ekle.Parameters.AddWithValue("@Marka", urunMarkaText.Text);
                ekle.Parameters.AddWithValue("@Mensei", urunMenseiText.Text);
                ekle.Parameters.AddWithValue("@GarantiSuresi", garantiText.Text);
                ekle.Parameters.AddWithValue("@UrunModel", urunModelText.Text);
                ekle.Parameters.AddWithValue("@StokDurum", "Var");
                ekle.Parameters.AddWithValue("@UrunAdet", urunAdetText.Text);

                ekle.ExecuteNonQuery();

                MessageBox.Show("Stok'a Ürün Ekleme işlemi başarıyla yapılmıştır.");

            }
            catch (SqlException)
            {
                MessageBox.Show("STOK VERİ TABANI HATASI... !!!");
            }
            baglanti.Close();
           
            /*ÜRÜNLER TABLOSUNA VERİ EKLEME İŞLEMİ */
            if (baglanti.State == ConnectionState.Closed)
                {
                    baglanti.Open();
                }
            int kdv = 0;
                ekle = new SqlCommand("INSERT INTO URUNLER(UrunID,UrunAd,Marka,Mensei,GarantiSuresi,UrunModel,KDV,BurutFiyat,StokDurum) VALUES(@UrunID,@UrunAd,@Marka,@Mensei,@GarantiSuresi,@UrunModel,@KDV,@BurutFiyat,@StokDurum)", baglanti);   
                ekle.Parameters.AddWithValue("@UrunID", urunIdText.Text);
                ekle.Parameters.AddWithValue("@UrunAd", urunAdText.Text);
                ekle.Parameters.AddWithValue("@Marka", urunMarkaText.Text);
                ekle.Parameters.AddWithValue("@Mensei", urunMenseiText.Text);
                ekle.Parameters.AddWithValue("@GarantiSuresi", garantiText.Text);
                ekle.Parameters.AddWithValue("@UrunModel", urunModelText.Text);
                ekle.Parameters.AddWithValue("@KDV",kdv.ToString());
                ekle.Parameters.AddWithValue("@BurutFiyat", BurutText.Text);
                ekle.Parameters.AddWithValue("@StokDurum", "Var");              
               
                ekle.ExecuteNonQuery();

                MessageBox.Show("Ürün Ekleme işlemi başarıyla yapılmıştır.");
                baglanti.Close();
           
            baglanti.Close();
            groupBox2.Enabled = true;
            groupBox1.Enabled = false;
            /*BİTİŞ */
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
                if (baglanti.State == ConnectionState.Closed)
                {
                    baglanti.Open();
                }
                SqlCommand ekle = new SqlCommand("INSERT INTO CPU(CPUNo,CPUTeknoloji,CPUMarka,CekirdekSayisi,CPUHizi,Cache) VALUES (@CPUNo,@CPUTeknoloji,@CPUMarka,@CekirdekSayisi,@CPUHizi,@Cache)", baglanti);
               
                ekle.Parameters.AddWithValue("@CPUNo", CpuNoText.Text);
                ekle.Parameters.AddWithValue("@CPUTeknoloji", CpuTeknoText.Text);
                ekle.Parameters.AddWithValue("@CPUMarka", CpuMarkaText.Text);
                ekle.Parameters.AddWithValue("@CekirdekSayisi", CpuSayiText.Text);
                ekle.Parameters.AddWithValue("@CPUHizi", CpuHiziText.Text);
                ekle.Parameters.AddWithValue("@Cache", cacheText.Text);

                ekle.ExecuteNonQuery();

                MessageBox.Show("İşlemci Özellikleri Ekleme işlemi başarıyla yapılmıştır.");
                baglanti.Close();
                groupBox3.Enabled = true;
                groupBox2.Enabled = false;
           

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (baglanti.State == ConnectionState.Closed)
                {
                    baglanti.Open();
                }
                SqlCommand ekle = new SqlCommand("INSERT INTO BELLEK(RamKapasite,RAMTipi,RAMMarka,HDDKapasite,HDDTuru,HDDMarka,USBTuru,IsletimSistemi) VALUES (@RamKapasite,@RAMTipi,@RAMMarka,@HDDKapasite,@HDDTuru,@HDDMarka,@USBTuru,@IsletimSistemi)", baglanti);
                
                ekle.Parameters.AddWithValue("@RamKapasite", ramKapasiteText.Text);
                ekle.Parameters.AddWithValue("@RAMTipi", ramTipText.Text);
                ekle.Parameters.AddWithValue("@RAMMarka", ramMarkaText.Text);
                ekle.Parameters.AddWithValue("@HDDKapasite", hddKapasiteText.Text);
                ekle.Parameters.AddWithValue("@HDDTuru", hddTurText.Text);
                ekle.Parameters.AddWithValue("@HDDMarka", hddMarkaText.Text);
                ekle.Parameters.AddWithValue("@USBTuru", usbText.Text);
                ekle.Parameters.AddWithValue("@IsletimSistemi", usbText.Text);

                ekle.ExecuteNonQuery();

                MessageBox.Show("Ram Özellikleri Ekleme işlemi başarıyla yapılmıştır.");
                baglanti.Close();
            }
            catch (SqlException)
            {
                MessageBox.Show("RAM Veri Tabanı Hatası... !!!");
            }
            groupBox4.Enabled = true;
            groupBox3.Enabled = false;
        }

        private void button2_Click_2(object sender, EventArgs e)
        {
            try
            {


                if (baglanti.State == ConnectionState.Closed)
                {
                    baglanti.Open();
                }
                SqlCommand ekle = new SqlCommand("INSERT INTO GPU(EkranKartiTipi,EkranKartiMarka,EkranKartiKapasite,Chipset,Cozunurluk,KartOkuyucu,Kamera) VALUES (@EkranKartiTipi,@EkranKartiMarka,@EkranKartiKapasite,@Chipset,@Cozunurluk,@KartOkuyucu,@Kamera)", baglanti);
               
                ekle.Parameters.AddWithValue("@EkranKartiTipi", GPUTipText.Text);
                ekle.Parameters.AddWithValue("@EkranKartiMarka", GPUMarkaText.Text);
                ekle.Parameters.AddWithValue("@EkranKartiKapasite", kapasiteText.Text);
                ekle.Parameters.AddWithValue("@Chipset", chipsetText.Text);
                ekle.Parameters.AddWithValue("@Cozunurluk", cozunurlukText.Text);
                ekle.Parameters.AddWithValue("@KartOkuyucu", kartOkuyucuText.Text);
                ekle.Parameters.AddWithValue("@Kamera", kameraText.Text);

                ekle.ExecuteNonQuery();

                MessageBox.Show("Grafik Ekleme işlemi başarıyla yapılmıştır.");
                MessageBox.Show("Ekleme İşlemi Bitmiştir...");
                baglanti.Close();
            }
            catch (SqlException)
            {
                MessageBox.Show("Grafik Veri Tabanı HATASI... !!!");
            }
            this.Close();
        }
    }

}
