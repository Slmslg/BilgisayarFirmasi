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
    public partial class User : Form
    {
        public User()
        {
            InitializeComponent();
        }
        public String secilenUrun;
        SqlConnection baglanti = new SqlConnection("Data Source =.;Initial Catalog = BILGISAYAR;Integrated Security=true");
        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void checkBox22_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void User_Load(object sender, EventArgs e)
        {

            try
            {

                if (baglanti.State == ConnectionState.Closed)
                {
                    baglanti.Open();
                }
                /* ÜRÜNLER  */
                SqlCommand satis = new SqlCommand("SELECT * FROM SATIS WHERE MusteriID=" + this.Text + "", baglanti);
                SqlDataAdapter adapter = new SqlDataAdapter(satis);
                DataTable veriTablosu = new DataTable();
                adapter.Fill(veriTablosu);
                SqlDataReader dr;
                dr = satis.ExecuteReader();
                while (dr.Read())
                {
                    ListViewItem item = new ListViewItem(dr["SeriNo"].ToString());
                    item.SubItems.Add(dr["UrunID"].ToString());
                    item.SubItems.Add(dr["UrunAd"].ToString());
                    item.SubItems.Add(dr["Marka"].ToString());
                    item.SubItems.Add(dr["Mensei"].ToString());
                    item.SubItems.Add(dr["GarantiSuresi"].ToString());
                    item.SubItems.Add(dr["UrunModel"].ToString());
                    item.SubItems.Add(dr["UrunAdet"].ToString());
                    item.SubItems.Add(dr["SatisTuru"].ToString());
                    urunlerim.Items.Add(item);
                }
                dr.Close();
            }
            catch (SqlException)
            {

            }

                /****************************************************************/
                

                if (baglanti.State == ConnectionState.Closed)
                {
                    baglanti.Open();
                }
                     /* ÜRÜNLER  */
                SqlCommand tumVeriler = new SqlCommand("SELECT * FROM URUNLER", baglanti);
                SqlDataAdapter adapter2 = new SqlDataAdapter(tumVeriler);
                DataTable veriTablosu2 = new DataTable();
                adapter2.Fill(veriTablosu2);                 
                SqlDataReader dr2;
                dr2 = tumVeriler.ExecuteReader();
                while (dr2.Read())
                {
                    ListViewItem item = new ListViewItem(dr2["SeriNo"].ToString());
                    item.SubItems.Add(dr2["UrunID"].ToString());
                    item.SubItems.Add(dr2["UrunAd"].ToString());
                    item.SubItems.Add(dr2["Marka"].ToString());
                    item.SubItems.Add(dr2["Mensei"].ToString());
                    item.SubItems.Add(dr2["GarantiSuresi"].ToString());                   
                    item.SubItems.Add(dr2["UrunModel"].ToString());
                    item.SubItems.Add(dr2["KDV"].ToString());
                    item.SubItems.Add(dr2["BurutFiyat"].ToString());
                    item.SubItems.Add(dr2["Fiyat"].ToString());
                    item.SubItems.Add(dr2["StokDurum"].ToString());
                    urunInceleList.Items.Add(item);
                }
                dr2.Close();
         
                /***********************************************************************/

              
                    if (baglanti.State == ConnectionState.Closed)
                    {
                        baglanti.Open();
                    }
                    /* ÜRÜNLER  */
                    SqlParameter p1 = new SqlParameter("@MusteriID",this.Text);
                    SqlParameter p2 = new SqlParameter("@SatisTur", "Sepette");
                    SqlCommand sepet = new SqlCommand("SELECT * FROM SATIS WHERE MusteriID=@MusteriID AND SatisTuru=@SatisTur", baglanti);                    
                    sepet.Parameters.Add(p1);
                    sepet.Parameters.Add(p2);
                    SqlDataAdapter adapterSepet = new SqlDataAdapter(sepet);
                    DataTable veriTablosuSepet = new DataTable();
                    adapterSepet.Fill(veriTablosuSepet);
                    SqlDataReader drSepet;
                    drSepet = sepet.ExecuteReader();
                    while (drSepet.Read())
                    {
                        ListViewItem item = new ListViewItem(drSepet["SeriNo"].ToString());
                        item.SubItems.Add(drSepet["UrunID"].ToString());
                        item.SubItems.Add(drSepet["UrunAd"].ToString());
                        item.SubItems.Add(drSepet["Marka"].ToString());
                        item.SubItems.Add(drSepet["Mensei"].ToString());
                        item.SubItems.Add(drSepet["GarantiSuresi"].ToString());
                        item.SubItems.Add(drSepet["UrunAdet"].ToString());
                        item.SubItems.Add(drSepet["SatisTuru"].ToString());
                        sepetList.Items.Add(item);
                    }
                    drSepet.Close();
              

            /***********************************************************************/
          
        }

        private void satınAlToolStripMenuItem_Click(object sender, EventArgs e)
        {
            /*SATIN ALMA İŞLEMİ */
            
            if (baglanti.State == ConnectionState.Closed)
            {
                baglanti.Open();
            }
            SqlCommand sil = new SqlCommand("DELETE FROM SATIS WHERE MusteriID =  " + this.Text+"AND SatisTuru=@sepet", baglanti);
            sil.Parameters.Add("@sepet","Sepette");
            sil.ExecuteNonQuery();
            baglanti.Close();
            /******************************/
            /*SATIN ALMA İŞLEMİ */
            if (baglanti.State == ConnectionState.Closed)
            {
                baglanti.Open();
            }
            ListViewItem item = urunlerim.SelectedItems[0];
            SqlCommand ekle = new SqlCommand("INSERT INTO SATIS(UrunID,MusteriID,SeriNo,UrunAd,Marka,Mensei,GarantiSuresi,UrunModel,UrunAdet,SatisTuru) VALUES (@UrunID,@MusteriID,@SeriNo,@UrunAd,@Marka,@Mensei,@GarantiSuresi,@UrunModel,@UrunAdet,@SatisTuru)", baglanti);
            ekle.Parameters.AddWithValue("@UrunId", item.SubItems[1].Text);
            ekle.Parameters.AddWithValue("@MusteriID", this.Text);
            ekle.Parameters.AddWithValue("@SeriNo", item.SubItems[0].Text);
            ekle.Parameters.AddWithValue("@UrunAd", item.SubItems[2].Text);
            ekle.Parameters.AddWithValue("@Marka", item.SubItems[3].Text);
            ekle.Parameters.AddWithValue("@Mensei", item.SubItems[4].Text);
            ekle.Parameters.AddWithValue("@GarantiSuresi", item.SubItems[5].Text);
            ekle.Parameters.AddWithValue("@UrunModel", item.SubItems[6].Text);
            ekle.Parameters.AddWithValue("@UrunAdet", comboBox1.Text);
            ekle.Parameters.AddWithValue("@Satis_Tarih", DateTime.Now.ToShortDateString());
            ekle.Parameters.AddWithValue("@SatisTuru", "Satıldı");

            ekle.ExecuteNonQuery();
            MessageBox.Show("Satın Alındı...");

            /*                    -------------------------                    */
        }

        private void urunlerim_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (urunlerim.SelectedItems.Count > 0)
            {
                ListViewItem item = urunlerim.SelectedItems[0];
                secilenUrun = item.SubItems[0].Text;
            }
        }

        private void sepeteEkleToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (baglanti.State == ConnectionState.Closed)
            {
                baglanti.Open();
            }
            ListViewItem item = urunlerim.SelectedItems[0];
            SqlCommand ekle = new SqlCommand("INSERT INTO SATIS(UrunID,MusteriID,SeriNo,UrunAd,Marka,Mensei,GarantiSuresi,UrunModel,UrunAdet,SatisTuru) VALUES (@UrunID,@MusteriID,@SeriNo,@UrunAd,@Marka,@Mensei,@GarantiSuresi,@UrunModel,@UrunAdet,@SatisTuru)", baglanti);
            ekle.Parameters.AddWithValue("@UrunId", item.SubItems[1].Text);
            ekle.Parameters.AddWithValue("@MusteriID", this.Text);
            ekle.Parameters.AddWithValue("@SeriNo", item.SubItems[0].Text);
            ekle.Parameters.AddWithValue("@UrunAd", item.SubItems[2].Text);
            ekle.Parameters.AddWithValue("@Marka", item.SubItems[3].Text);
            ekle.Parameters.AddWithValue("@Mensei", item.SubItems[4].Text);
            ekle.Parameters.AddWithValue("@GarantiSuresi", item.SubItems[5].Text);
            ekle.Parameters.AddWithValue("@UrunModel", item.SubItems[6].Text);
            ekle.Parameters.AddWithValue("@UrunAdet", "1");
            ekle.Parameters.AddWithValue("@SatisTuru", "Sepette");

            ekle.ExecuteNonQuery();
            MessageBox.Show("Sepete Eklendi...");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            /* ARA */
            /****************************************************************/
            if (fiyatSiralaCombo.Text == "Artan")
            {
                if (baglanti.State == ConnectionState.Closed)
                {
                    baglanti.Open();
                }
                urunInceleList.Items.Clear();
                SqlCommand artan = new SqlCommand("SELECT * FROM URUNLER ORDER BY Fiyat ASC", baglanti);
                SqlDataAdapter artanAdapter = new SqlDataAdapter(artan);
                DataTable veriArtan = new DataTable();
                artanAdapter.Fill(veriArtan);
                SqlDataReader artanReader;
                artanReader = artan.ExecuteReader();
                while (artanReader.Read())
                {                    
                    ListViewItem item = new ListViewItem(artanReader["SeriNo"].ToString());
                    item.SubItems.Add(artanReader["UrunID"].ToString());
                    item.SubItems.Add(artanReader["UrunAd"].ToString());
                    item.SubItems.Add(artanReader["Marka"].ToString());
                    item.SubItems.Add(artanReader["Mensei"].ToString());
                    item.SubItems.Add(artanReader["GarantiSuresi"].ToString());
                    item.SubItems.Add(artanReader["UrunModel"].ToString());
                    item.SubItems.Add(artanReader["KDV"].ToString());
                    item.SubItems.Add(artanReader["BurutFiyat"].ToString());
                    item.SubItems.Add(artanReader["Fiyat"].ToString());
                    item.SubItems.Add(artanReader["StokDurum"].ToString());
                    urunInceleList.Items.Add(item);
                }
                artanReader.Close();   
            }
            else if (fiyatSiralaCombo.Text == "Azalan")            
            {
                urunInceleList.Items.Clear();
                if (baglanti.State == ConnectionState.Closed)
                {
                    baglanti.Open();
                }
                SqlCommand azalan = new SqlCommand("SELECT * FROM URUNLER ORDER BY Fiyat DESC", baglanti);
                SqlDataAdapter azalanAdapter = new SqlDataAdapter(azalan);
                DataTable veriAzalan = new DataTable();
                azalanAdapter.Fill(veriAzalan);
                SqlDataReader azalanReader;
                azalanReader = azalan.ExecuteReader();
                while (azalanReader.Read())
                {
                    ListViewItem item = new ListViewItem(azalanReader["SeriNo"].ToString());
                    item.SubItems.Add(azalanReader["UrunID"].ToString());
                    item.SubItems.Add(azalanReader["UrunAd"].ToString());
                    item.SubItems.Add(azalanReader["Marka"].ToString());
                    item.SubItems.Add(azalanReader["Mensei"].ToString());
                    item.SubItems.Add(azalanReader["GarantiSuresi"].ToString());
                    item.SubItems.Add(azalanReader["UrunModel"].ToString());
                    item.SubItems.Add(azalanReader["KDV"].ToString());
                    item.SubItems.Add(azalanReader["BurutFiyat"].ToString());
                    item.SubItems.Add(azalanReader["Fiyat"].ToString());
                    item.SubItems.Add(azalanReader["StokDurum"].ToString());
                    urunInceleList.Items.Add(item);
                }
                azalanReader.Close();  
            }

           

            /***********************************************************************/

            /**/
        }

        private void button2_Click(object sender, EventArgs e)
        {
            /* ------------------------------------------------------------ */
            urunInceleList.Items.Clear();
            if (baglanti.State == ConnectionState.Closed)
            {
                baglanti.Open();
            }
            SqlParameter markaAdi = new SqlParameter("@Marka", markaSiralaCombo.Text);
            SqlCommand markaKomut = new SqlCommand("SELECT * FROM URUNLER WHERE Marka=@Marka", baglanti);
            markaKomut.Parameters.Add(markaAdi);
            SqlDataAdapter markaAdapter = new SqlDataAdapter(markaKomut);
            DataTable markaVeri = new DataTable();
            markaAdapter.Fill(markaVeri);
            SqlDataReader markaReader;
            markaReader = markaKomut.ExecuteReader();
            while (markaReader.Read())
            {
                ListViewItem item = new ListViewItem(markaReader["SeriNo"].ToString());
                item.SubItems.Add(markaReader["UrunID"].ToString());
                item.SubItems.Add(markaReader["UrunAd"].ToString());
                item.SubItems.Add(markaReader["Marka"].ToString());
                item.SubItems.Add(markaReader["Mensei"].ToString());
                item.SubItems.Add(markaReader["GarantiSuresi"].ToString());
                item.SubItems.Add(markaReader["UrunModel"].ToString());
                item.SubItems.Add(markaReader["KDV"].ToString());
                item.SubItems.Add(markaReader["BurutFiyat"].ToString());
                item.SubItems.Add(markaReader["Fiyat"].ToString());
                item.SubItems.Add(markaReader["StokDurum"].ToString());
                urunInceleList.Items.Add(item);
            }
            markaReader.Close();   
            /*---------------------------------------------------------------*/
        }

        private void button3_Click(object sender, EventArgs e)
        {
            /* -------------------------------------------------------------------- */
            urunInceleList.Items.Clear();
            if (baglanti.State == ConnectionState.Closed)
            {
                baglanti.Open();
            }
            SqlParameter markaAdi = new SqlParameter("@İslemciTeknoloji", islemciSiralaCombo.Text);
            SqlCommand markaKomut = new SqlCommand("SELECT * FROM URUN_BILGILERI WHERE CPUTeknoloji=@İslemciTeknoloji", baglanti);
            markaKomut.Parameters.Add(markaAdi);
            SqlDataAdapter markaAdapter = new SqlDataAdapter(markaKomut);
            DataTable markaVeri = new DataTable();
            markaAdapter.Fill(markaVeri);
            SqlDataReader markaReader;
            markaReader = markaKomut.ExecuteReader();
            
            while (markaReader.Read())
            {
                ListViewItem item = new ListViewItem(markaReader["SeriNo"].ToString());
                item.SubItems.Add(markaReader["UrunID"].ToString());
                item.SubItems.Add(markaReader["UrunAd"].ToString());
                item.SubItems.Add(markaReader["Marka"].ToString());
                item.SubItems.Add(markaReader["Mensei"].ToString());
                item.SubItems.Add(markaReader["GarantiSuresi"].ToString());
                item.SubItems.Add(markaReader["UrunModel"].ToString());
                item.SubItems.Add(markaReader["KDV"].ToString());
                item.SubItems.Add(markaReader["BurutFiyat"].ToString());
                item.SubItems.Add(markaReader["Fiyat"].ToString());
                item.SubItems.Add(markaReader["StokDurum"].ToString());
                item.SubItems.Add(markaReader["CPUTeknoloji"].ToString());
                urunInceleList.Items.Add(item);
            }
            urunInceleList.Columns.Add("Cpu Teknolojisi");
            markaReader.Close();   
            /*----------------------------------------------------------------------*/
              
	     }

        private void tabPage3_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            /* -------------------------------------------------------------------- */
            urunInceleList.Items.Clear();
            if (baglanti.State == ConnectionState.Closed)
            {
                baglanti.Open();
            }
            SqlParameter markaAdi = new SqlParameter("@CPUHizi", islemciHiziCombo.Text);
            SqlCommand markaKomut = new SqlCommand("SELECT * FROM URUN_BILGILERI WHERE CPUHizi=@CPUHizi", baglanti);
            markaKomut.Parameters.Add(markaAdi);
            SqlDataAdapter markaAdapter = new SqlDataAdapter(markaKomut);
            DataTable markaVeri = new DataTable();
            markaAdapter.Fill(markaVeri);
            SqlDataReader markaReader;
            urunInceleList.Columns.Add("CPU Hızı");
            markaReader = markaKomut.ExecuteReader();
            while (markaReader.Read())
            {
                ListViewItem item = new ListViewItem(markaReader["SeriNo"].ToString());
                item.SubItems.Add(markaReader["UrunID"].ToString());
                item.SubItems.Add(markaReader["UrunAd"].ToString());
                item.SubItems.Add(markaReader["Marka"].ToString());
                item.SubItems.Add(markaReader["Mensei"].ToString());
                item.SubItems.Add(markaReader["GarantiSuresi"].ToString());
                item.SubItems.Add(markaReader["UrunModel"].ToString());
                item.SubItems.Add(markaReader["KDV"].ToString());
                item.SubItems.Add(markaReader["BurutFiyat"].ToString());
                item.SubItems.Add(markaReader["Fiyat"].ToString());
                item.SubItems.Add(markaReader["StokDurum"].ToString());
                item.SubItems.Add(markaReader["CPUHizi"].ToString());
                urunInceleList.Items.Add(item);
            }
            markaReader.Close();
            /*----------------------------------------------------------------------*/
              
        }          
                        
        }
    }

