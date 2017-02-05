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
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;

namespace BilgisayarFirmasi
{
    public partial class Admin : Form
    {
        public Admin()
        {
            InitializeComponent();
        }
        public SqlConnection baglanti;
        public string urunid, serinu;

        private void Admin_Load(object sender, EventArgs e)
        {      
   
             try
            {
                baglanti = new SqlConnection("Data Source =.;Initial Catalog = BILGISAYAR;Integrated Security=true");
                if (baglanti.State == ConnectionState.Closed)
                {
                    baglanti.Open();
                }
                     /* ÜRÜNLER  */
                SqlCommand tumVeriler = new SqlCommand("SELECT * FROM URUNLER", baglanti);
                SqlDataAdapter adapter = new SqlDataAdapter(tumVeriler);
                DataTable veriTablosu = new DataTable();
                adapter.Fill(veriTablosu);                 
                SqlDataReader dr;
                dr = tumVeriler.ExecuteReader();
                while (dr.Read())
                {
                    ListViewItem item = new ListViewItem(dr["SeriNo"].ToString());
                    seriNumarasi.Text = dr["SeriNo"].ToString();
                    item.SubItems.Add(dr["UrunID"].ToString());
                    item.SubItems.Add(dr["UrunAd"].ToString());
                    item.SubItems.Add(dr["Marka"].ToString());
                    item.SubItems.Add(dr["Mensei"].ToString());
                    item.SubItems.Add(dr["GarantiSuresi"].ToString());
                    item.SubItems.Add(dr["UrunModel"].ToString());
                    item.SubItems.Add(dr["KDV"].ToString());
                    item.SubItems.Add(dr["BurutFiyat"].ToString());
                    item.SubItems.Add(dr["Fiyat"].ToString());
                    item.SubItems.Add(dr["StokDurum"].ToString());
                    listView5.Items.Add(item);
                }
                dr.Close();

                 /* SİLİNMİŞ ÜRÜNLER */

                tumVeriler = new SqlCommand("SELECT * FROM SILINMIS_URUN_KAYITLAR", baglanti);
                adapter = new SqlDataAdapter(tumVeriler);
                veriTablosu = new DataTable();
                adapter.Fill(veriTablosu);
                dr = tumVeriler.ExecuteReader();
                while (dr.Read())
                {
                    ListViewItem item = new ListViewItem(dr["SeriNo"].ToString());
                    item.SubItems.Add(dr["UrunID"].ToString());  
                    item.SubItems.Add(dr["UrunAd"].ToString());
                    item.SubItems.Add(dr["Marka"].ToString());
                    item.SubItems.Add(dr["Mensei"].ToString());
                    item.SubItems.Add(dr["GarantiSuresi"].ToString());
                    item.SubItems.Add(dr["UrunModel"].ToString());
                    item.SubItems.Add(dr["KDV"].ToString());
                    item.SubItems.Add(dr["Fiyat"].ToString());
                    listView1.Items.Add(item);
                }
                dr.Close();

                tumVeriler = new SqlCommand("SELECT * FROM SERVIS", baglanti);
                adapter = new SqlDataAdapter(tumVeriler);
                veriTablosu = new DataTable();
                adapter.Fill(veriTablosu);
                dr = tumVeriler.ExecuteReader();
                while (dr.Read())
                {
                    ListViewItem item = new ListViewItem(dr["MusteriID"].ToString());
                    item.SubItems.Add(dr["SeriNo"].ToString());
                    item.SubItems.Add(dr["GarantiSuresi"].ToString());
                    listView2.Items.Add(item);
                }
                dr.Close();

                tumVeriler = new SqlCommand("SELECT * FROM MUSTERI", baglanti);
                adapter = new SqlDataAdapter(tumVeriler);
                veriTablosu = new DataTable();
                adapter.Fill(veriTablosu);
                dr = tumVeriler.ExecuteReader();
                while (dr.Read())
                {
                    ListViewItem item = new ListViewItem(dr["MusteriID"].ToString());
                    item.SubItems.Add(dr["MusteriAd"].ToString());
                    item.SubItems.Add(dr["MusteriSoyad"].ToString());
                    item.SubItems.Add(dr["MusteriTelNo"].ToString());
                    item.SubItems.Add(dr["MusteriAdres"].ToString());
                    item.SubItems.Add(dr["MusteriSehir"].ToString());
                    item.SubItems.Add(dr["MusteriKartNo"].ToString());
                    listView3.Items.Add(item);
                }
                dr.Close();

                tumVeriler = new SqlCommand("SELECT * FROM STOK", baglanti);
                adapter = new SqlDataAdapter(tumVeriler);
                veriTablosu = new DataTable();
                adapter.Fill(veriTablosu);
                dr = tumVeriler.ExecuteReader();
                while (dr.Read())
                {
                    ListViewItem item = new ListViewItem(dr["UrunID"].ToString());
                    urunNum.Text = dr["UrunID"].ToString();
                    item.SubItems.Add(dr["UrunAd"].ToString());
                    item.SubItems.Add(dr["Marka"].ToString());
                    item.SubItems.Add(dr["Mensei"].ToString());
                    item.SubItems.Add(dr["GarantiSuresi"].ToString());
                    item.SubItems.Add(dr["UrunModel"].ToString());
                    item.SubItems.Add(dr["UrunAdet"].ToString());
                    listView4.Items.Add(item);
                }
                dr.Close();


                baglanti.Close();
            }
            catch (SqlException)
            {
                MessageBox.Show("Veri Tabanında hata oluştu.");
            }
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {
            
        }

        private void listView1_ItemActivate(object sender, EventArgs e)
        {
        }

        private void listView5_SelectedIndexChanged(object sender, EventArgs e)
        {
            /* URUN BİLGİLERİ VIEW'İNDEN VERİ ÇEKME*/
            SqlDataReader oku;
            try
            {
                baglanti = new SqlConnection("Data Source =.;Initial Catalog = BILGISAYAR;Integrated Security=true");
                if (baglanti.State == ConnectionState.Closed)
                {
                    baglanti.Open();
                }               
                if (listView5.SelectedItems.Count > 0)
                {
                    ListViewItem item = listView5.SelectedItems[0];
                    seriNumarasi.Text = item.SubItems[0].Text;
                }
                SqlParameter urunSeri = new SqlParameter("@urunSeri", seriNumarasi.Text);
                String giris = "SELECT * FROM URUN_BILGILERI WHERE SeriNo = @urunSeri";
                SqlCommand komut = new SqlCommand(giris, baglanti);
                komut.Parameters.Add(urunSeri);
                SqlDataAdapter adapter = new SqlDataAdapter(komut);
                DataTable veriTablosu = new DataTable();
                adapter.Fill(veriTablosu);
                if (veriTablosu.Rows.Count > 0)
                {
                    komut.ExecuteNonQuery();
                    oku = komut.ExecuteReader();
                    if (oku.Read())
                    {
                        seriNumarasi.Text = oku["SeriNo"].ToString();
                        serinu = seriNumarasi.Text;
                        urunid = oku["UrunID"].ToString();
                        urunAdText.Text = oku["UrunAd"].ToString();
                        urunModelText.Text = oku["UrunModel"].ToString();
                        islemciHız.Text = oku["CPUHizi"].ToString();
                        cache.Text = oku["Cache"].ToString();
                        islemciTeknoloji.Text = oku["CPUTeknoloji"].ToString();
                        islemciMarka.Text = oku["CPUMarka"].ToString();
                        cekirdekSayisi.Text = oku["CekirdekSayisi"].ToString();
                        ramTip.Text = oku["RAMTipi"].ToString();
                        ramKapasite.Text = oku["RamKapasite"].ToString();
                        ramMarka.Text = oku["RAMMarka"].ToString();
                        hddTur.Text = oku["HDDTuru"].ToString();
                        hddMarka.Text = oku["HDDMarka"].ToString();
                        usb.Text = oku["USBTuru"].ToString();
                        isletimSistemi.Text = oku["IsletimSistemi"].ToString();
                        ekranKartTip.Text = oku["EkranKartiTipi"].ToString();
                        ekranMarka.Text = oku["EkranKartiMarka"].ToString();
                        chipset.Text = oku["Chipset"].ToString();
                        kapasite.Text = oku["EkranKArtiKapasite"].ToString();
                        cozunurluk.Text = oku["Cozunurluk"].ToString();
                        kartOkuyucu.Text = oku["KartOkuyucu"].ToString();
                        kamera.Text = oku["Kamera"].ToString();
                        garantiSure.Text = oku["GarantiSuresi"].ToString();
                        urunFiyat.Text = oku["Fiyat"].ToString();

                    }
                    baglanti.Close();
                }              
                
                /*  */
                
            }
            catch (SqlException)
            {
                MessageBox.Show("Veri Seçilirken Hata Oluştu ! ");
            }

        }

        private void listView5_MouseClick(object sender, MouseEventArgs e)
        {
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                baglanti = new SqlConnection("Data Source=.; Initial Catalog=BILGISAYAR; Integrated Security=true");
                if (baglanti.State == ConnectionState.Closed)
                {
                    baglanti.Open();
                }
                SqlCommand sil = new SqlCommand("DELETE FROM URUNLER WHERE SeriNo =  " + seriNumarasi.Text,baglanti);
                sil.ExecuteNonQuery();
                MessageBox.Show("Silme İşlemi Başarıyla Yapılmıştır.");
                baglanti.Close();

            }
            catch (SqlException)
            {
                MessageBox.Show("Silme işlemi yapılırken bir hata oluştu. Lütfen ürünün olup olmadığını kontrol ediniz...");
            }
        }
        
        private void button2_Click(object sender, EventArgs e)
        {
            //if (baglanti.State == ConnectionState.Closed)
            //{
            //    baglanti.Open();
            //}
            //SqlCommand veri = new SqlCommand("Select MAX(SeriNo) From URUN ", baglanti);
            //SqlDataAdapter adapter = new SqlDataAdapter(veri);
            //DataTable veriTablosu = new DataTable();
            //adapter.Fill(veriTablosu);
            //SqlDataReader dr = veri.ExecuteReader();
            //while (dr.Read())
            //{
            //    UrunEkle urunE = new UrunEkle();
            //    urunE.urunSeriNoText.Text = dr["SeriNo"].ToString();
            //}
            //dr.Close();
            UrunEkle ekle = new UrunEkle();
            ekle.urunID = urunNum.Text;
            ekle.UrunSeriNo = (Convert.ToInt16(seriNumarasi.Text)+1).ToString();
            ekle.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            UrunGuncelle guncelleEkrani = new UrunGuncelle();
            guncelleEkrani.urunIdText.Text = urunNum.Text;
            guncelleEkrani.urunSeriNoText.Text = seriNumarasi.Text;
            guncelleEkrani.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            /*-------------------------------------*/

            iTextSharp.text.Document raporum = new iTextSharp.text.Document();

            // PDF oluşturması ve konumun belirlenmesi
            Random r = new Random();
            
            PdfWriter.GetInstance(raporum, new FileStream("C:Raporum"+r.Next()+".pdf", FileMode.Create));

            //PDF yi yazan özelliğine eklenecek

            raporum.AddAuthor("Selim Silgu"); // PDF Oluşturma Tarihi Ekle

            raporum.AddCreationDate(); // PDF Oluşturma Tarihi

            // PDF oluşturan kişi özelliğine yazılacak

            raporum.AddCreator("Selim Silgu");

            if (raporum.IsOpen() == false)
            {

                raporum.Open();

            }

            raporum.Add(new Paragraph("Ürün Fiyatı : "+urunFiyat.Text+" TL"));
            raporum.Add(new Paragraph("Seri Numarası : "+seriNumarasi.Text));
            raporum.Add(new Paragraph("Ürün ID : "+urunid.ToString()));
            raporum.Add(new Paragraph("Ürün Adı : "+urunAdText.Text));
            raporum.Add(new Paragraph("Ürün Modeli : "+UrunModel.Text));
            raporum.Add(new Paragraph("Islemci Hızı : "+islemciHız.Text));
            raporum.Add(new Paragraph("Cache Bellek : "+cache.Text));
            raporum.Add(new Paragraph("Islemci Teknolojisi : "+islemciTeknoloji.Text));
            raporum.Add(new Paragraph("Islemci Markası : "+islemciMarka.Text));
            raporum.Add(new Paragraph("Islemci Hızı : "+islemciHız.Text));
            raporum.Add(new Paragraph("Çekirdek Sayısı : "+cekirdekSayisi.Text));
            raporum.Add(new Paragraph("RAM Tipi :"+ramTip.Text));
            raporum.Add(new Paragraph("RAM Kapasitesi : "+ramKapasite.Text));
            raporum.Add(new Paragraph("RAM Markası : "+ramMarka.Text));
            raporum.Add(new Paragraph("Disk Türü : "+hddTur.Text));
            raporum.Add(new Paragraph("Disk Markası : "+hddMarka.Text));
            raporum.Add(new Paragraph("USB : "+usb.Text));
            raporum.Add(new Paragraph("Isletim Sistemi : "+isletimSistemi.Text));
            raporum.Add(new Paragraph("Ekran Kartı Tipi : "+ekranKartTip.Text));
            raporum.Add(new Paragraph("Ekran Kartı Markası : "+ekranMarka.Text));
            raporum.Add(new Paragraph("Ekran Kartı Chipset : "+chipset.Text));
            raporum.Add(new Paragraph("Ekran Kartı Kapasitesi : "+kapasite.Text));
            raporum.Add(new Paragraph("Çözünürlük : "+cozunurluk.Text));
            raporum.Add(new Paragraph("Kart Okuyucu : "+kartOkuyucu.Text));
            raporum.Add(new Paragraph("Kamera : "+kamera.Text));
            raporum.Add(new Paragraph("Garanti Süresi : "+GarantiSuresi.Text));
            MessageBox.Show("PDF Dosyanız Oluşmuştur.");

            raporum.Close();

            /*-------------------------------------*/
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Application.Exit();    
        }

        private void SecilenUrun_Enter(object sender, EventArgs e)
        {

        }
      
    }
}
