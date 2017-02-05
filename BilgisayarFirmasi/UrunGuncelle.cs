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

namespace BilgisayarFirmasi
{
    public partial class UrunGuncelle : Form
    {
        public UrunGuncelle()
        {
            InitializeComponent();
        }
        public String urunID, UrunSeriNo;
        SqlCommand ekle;
        SqlConnection baglanti = new SqlConnection("Data Source=.; Initial Catalog=BILGISAYAR; Integrated Security=true");

        private void guncelleStok_Click(object sender, EventArgs e)
        {
            try
            {
                baglanti = new SqlConnection("Data Source=.; Initial Catalog=BILGISAYAR; Integrated Security=true");

                if (baglanti.State == ConnectionState.Closed)
                {
                    baglanti.Open();
                }
                SqlCommand guncelle = new SqlCommand("UPDATE STOK SET " +
                   "UrunAd=@UrunAd,Marka=@Marka,Mensei=@Mensei,GarantiSuresi=@GarantiSuresi," +
                   "UrunModel=@UrunModel,UrunAdet=@UrunAdet WHERE UrunID="+urunIdText.Text, baglanti);
                guncelle.Parameters.AddWithValue("@UrunAd", urunAdText.Text);
                guncelle.Parameters.AddWithValue("@Marka", urunMarkaText.Text);
                guncelle.Parameters.AddWithValue("@Mensei", urunMenseiText.Text);
                guncelle.Parameters.AddWithValue("@GarantiSuresi", garantiText.Text);
                guncelle.Parameters.AddWithValue("@UrunModel", urunModelText.Text);
                guncelle.Parameters.AddWithValue("@UrunAdet", urunAdetText.Text);

                guncelle.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("Güncelleme işlemi başarıyla yapılmıştır...");

            }
            catch (SqlException)
            {
                MessageBox.Show(" STOK Veri bulunamıyor yada hatalı giriş yaptınız...");
            }


            /*ÜRÜN GÜNCELLE*/

           
                baglanti = new SqlConnection("Data Source=.; Initial Catalog=BILGISAYAR; Integrated Security=true");

                if (baglanti.State == ConnectionState.Closed)
                {
                    baglanti.Open();
                }
                SqlCommand Urunguncelle = new SqlCommand("UPDATE URUNLER SET " +
                   "UrunID=@UrunID,UrunAd=@UrunAd,Marka=@Marka,Mensei=@Mensei,GarantiSuresi=@GarantiSuresi," +
                   "UrunModel=@UrunModel,KDV=@KDV,BurutFiyat=@BurutFiyat,StokDurum=@StokDurum WHERE SeriNo=" + 
                urunSeriNoText.Text, baglanti);
                Urunguncelle.Parameters.AddWithValue("@UrunID", urunIdText.Text);
                Urunguncelle.Parameters.AddWithValue("@UrunAd", urunAdText.Text);
                Urunguncelle.Parameters.AddWithValue("@Marka", urunMarkaText.Text);
                Urunguncelle.Parameters.AddWithValue("@Mensei", urunMenseiText.Text);
                Urunguncelle.Parameters.AddWithValue("@GarantiSuresi", garantiText.Text);
                Urunguncelle.Parameters.AddWithValue("@UrunModel", urunModelText.Text);
                Urunguncelle.Parameters.AddWithValue("@KDV", KdvText.Text);
                Urunguncelle.Parameters.AddWithValue("@BurutFiyat", BurutText.Text);
                Urunguncelle.Parameters.AddWithValue("@StokDurum", "Var");

                Urunguncelle.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("Güncelleme işlemi başarıyla yapılmıştır...");

           

            /*
                        
             
             */
        }

        private void Ara_Click(object sender, EventArgs e)
        {
            try
            {
                baglanti = new SqlConnection("Data Source=.; Initial Catalog=BILGISAYAR; Integrated Security=true");
                if (baglanti.State == ConnectionState.Closed)
                {
                    baglanti.Open();
                }
                ekle = new SqlCommand("SELECT * FROM STOK WHERE UrunID=" + urunIdText.Text + "", baglanti);
                ekle.ExecuteNonQuery();
                SqlDataReader dr = ekle.ExecuteReader();
                if (dr.Read())
                {
                    urunAdText.Text = dr["UrunAd"].ToString();
                    urunMarkaText.Text = dr["Marka"].ToString();
                    urunMenseiText.Text = dr["Mensei"].ToString();
                    garantiText.Text =dr["GarantiSuresi"].ToString();
                    urunModelText.Text = dr["UrunModel"].ToString();
                    urunAdetText.Text = dr["UrunAdet"].ToString();
                }
                baglanti.Dispose();
                baglanti.Close();
                dr.Close();
            }
            catch (SqlException)
            {
                MessageBox.Show("Güncelleme Hatası...");
            }
            /* ÜRÜN FİYAT*/
            try
            {
                baglanti = new SqlConnection("Data Source=.; Initial Catalog=BILGISAYAR; Integrated Security=true");
                if (baglanti.State == ConnectionState.Closed)
                {
                    baglanti.Open();
                }
                ekle = new SqlCommand("SELECT * FROM URUNLER WHERE SeriNo=" + urunSeriNoText.Text + "", baglanti);
                ekle.ExecuteNonQuery();
                SqlDataReader dr = ekle.ExecuteReader();
                if (dr.Read())
                {
                    BurutText.Text = dr["BurutFiyat"].ToString();
                    KdvText.Text = dr["KDV"].ToString();
                  
                }
                baglanti.Dispose();
                baglanti.Close();
                dr.Close();
            }
            catch (SqlException)
            {
                MessageBox.Show("Güncelleme Hatası...");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
           
                baglanti = new SqlConnection("Data Source=.; Initial Catalog=BILGISAYAR; Integrated Security=true");
                if (baglanti.State == ConnectionState.Closed)
                {
                    baglanti.Open();
                }
                ekle = new SqlCommand("SELECT * FROM CPU WHERE SeriNo=" + urunSeriNoText.Text + "", baglanti);
                ekle.ExecuteNonQuery();
                SqlDataReader dr = ekle.ExecuteReader();
                if (dr.Read())
                {
                    CpuNoText.Text = dr["CPUNo"].ToString();
                    CpuHiziText.Text = dr["CPUHizi"].ToString();
                    cacheText.Text = dr["Cache"].ToString();
                    CpuTeknoText.Text = dr["CPUTeknoloji"].ToString();
                    CpuMarkaText.Text = dr["CPUMarka"].ToString();
                    CpuSayiText.Text = dr["CekirdekSayisi"].ToString();
                }
                baglanti.Dispose();
                baglanti.Close();
                dr.Close();
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
                baglanti = new SqlConnection("Data Source=.; Initial Catalog=BILGISAYAR; Integrated Security=true");

                if (baglanti.State == ConnectionState.Closed)
                {
                    baglanti.Open();
                }
                SqlCommand guncelle = new SqlCommand("UPDATE CPU SET " +
                   "CPUNo=@CPUNo,CPUHizi=@CPUHizi,Cache=@Cache,CPUTeknoloji=@CPUTeknoloji,CPUMarka=@CPUMarka,CekirdekSayisi=@CekirdekSayisi WHERE SeriNo=" + urunSeriNoText.Text, baglanti);
                guncelle.Parameters.AddWithValue("@CPUNo", CpuNoText.Text);
                guncelle.Parameters.AddWithValue("@CPUHizi", CpuHiziText.Text);
                guncelle.Parameters.AddWithValue("@Cache", cacheText.Text);
                guncelle.Parameters.AddWithValue("@CPUTeknoloji", CpuTeknoText.Text);
                guncelle.Parameters.AddWithValue("@CPUMarka", CpuMarkaText.Text);
                guncelle.Parameters.AddWithValue("@CekirdekSayisi", CpuSayiText.Text);

                guncelle.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("Güncelleme işlemi başarıyla yapılmıştır...");

           
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            baglanti = new SqlConnection("Data Source=.; Initial Catalog=BILGISAYAR; Integrated Security=true");
            if (baglanti.State == ConnectionState.Closed)
            {
                baglanti.Open();
            }
            ekle = new SqlCommand("SELECT * FROM BELLEK WHERE SeriNo=" + urunSeriNoText.Text + "", baglanti);
            ekle.ExecuteNonQuery();
            SqlDataReader dr = ekle.ExecuteReader();
            if (dr.Read())
            {
                ramKapasiteText.Text = dr["RamKapasite"].ToString();
                ramTipText.Text = dr["RAMTipi"].ToString();
                ramMarkaText.Text = dr["RAMMarka"].ToString();
                hddKapasiteText.Text = dr["HDDKapasite"].ToString();
                hddTurText.Text = dr["HDDTuru"].ToString();
                hddMarkaText.Text = dr["HDDMarka"].ToString();
                usbText.Text = dr["USBTuru"].ToString();
                isletimSistemiText.Text = dr["IsletimSistemi"].ToString();
            }
            baglanti.Dispose();
            baglanti.Close();
            dr.Close();
           

        }

        private void BellekEkle_Click(object sender, EventArgs e)
        {
           
                baglanti = new SqlConnection("Data Source=.; Initial Catalog=BILGISAYAR; Integrated Security=true");

                if (baglanti.State == ConnectionState.Closed)
                {
                    baglanti.Open();
                }
                SqlCommand guncelle = new SqlCommand("UPDATE BELLEK SET " +
                   "RamKapasite=@RamKapasite,RAMTipi=@RAMTipi,RAMMarka=@RAMMarka,HDDKapasite=@HDDKapasite,HDDTuru=@HDDTuru,HDDMarka=@HDDMarka,USBTuru=@USBTuru,IsletimSistemi=@IsletimSistemi WHERE SeriNo=" + urunSeriNoText.Text, baglanti);
                guncelle.Parameters.AddWithValue("@RamKapasite", ramKapasiteText.Text);
                guncelle.Parameters.AddWithValue("@RAMTipi", ramTipText.Text);
                guncelle.Parameters.AddWithValue("@RAMMarka", ramMarkaText.Text);
                guncelle.Parameters.AddWithValue("@HDDKapasite", hddKapasiteText.Text);
                guncelle.Parameters.AddWithValue("@HDDTuru", hddTurText.Text);
                guncelle.Parameters.AddWithValue("@HDDMarka", hddMarkaText.Text);
                guncelle.Parameters.AddWithValue("@USBTuru", usbText.Text);
                guncelle.Parameters.AddWithValue("@IsletimSistemi", isletimSistemiText.Text);

                guncelle.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("Güncelleme işlemi başarıyla yapılmıştır...");

          
        }

        private void button2_Click(object sender, EventArgs e)
        {
            baglanti = new SqlConnection("Data Source=.; Initial Catalog=BILGISAYAR; Integrated Security=true");

            if (baglanti.State == ConnectionState.Closed)
            {
                baglanti.Open();
            }
            SqlCommand guncelle = new SqlCommand("UPDATE GPU SET " +
               "EkranKartiTipi=@EkranKartiTipi,EkranKartiMarka=@EkranKartiMarka,EkranKartiKapasite=@EkranKartiKapasite,Chipset=@Chipset,Cozunurluk=@Cozunurluk,KartOkuyucu=@KartOkuyucu,Kamera=@Kamera WHERE SeriNo=" + urunSeriNoText.Text, baglanti);
            guncelle.Parameters.AddWithValue("@EkranKartiTipi", EkranKartTip.Text);
            guncelle.Parameters.AddWithValue("@EkranKartiMarka", EkranKartMarkaText.Text);
            guncelle.Parameters.AddWithValue("@EkranKartiKapasite", EkranKartKapasiteText.Text);
            guncelle.Parameters.AddWithValue("@Chipset", ChipsetText.Text);
            guncelle.Parameters.AddWithValue("@Cozunurluk", CozunurlukText.Text);
            guncelle.Parameters.AddWithValue("@KartOkuyucu", KartCombo.Text);
            guncelle.Parameters.AddWithValue("@Kamera", KameraCombo.Text);

            guncelle.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Güncelleme işlemi başarıyla yapılmıştır...");
        }

        private void button4_Click(object sender, EventArgs e)
        {
             baglanti = new SqlConnection("Data Source=.; Initial Catalog=BILGISAYAR; Integrated Security=true");
            if (baglanti.State == ConnectionState.Closed)
            {
                baglanti.Open();
            }
            ekle = new SqlCommand("SELECT * FROM GPU WHERE SeriNo=" + urunSeriNoText.Text + "", baglanti);
            ekle.ExecuteNonQuery();
            SqlDataReader dr = ekle.ExecuteReader();
            if (dr.Read())
            {
                EkranKartTip.Text = dr["EkranKartiTipi"].ToString();
                EkranKartMarkaText.Text = dr["EkranKartiMarka"].ToString();
                EkranKartKapasiteText.Text = dr["EkranKartiKapasite"].ToString();
                ChipsetText.Text = dr["Chipset"].ToString();
                CozunurlukText.Text = dr["Cozunurluk"].ToString();
                KartCombo.Text = dr["KartOkuyucu"].ToString();
                KameraCombo.Text = dr["Kamera"].ToString();
            }
            baglanti.Dispose();
            baglanti.Close();
        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }
    }
}
