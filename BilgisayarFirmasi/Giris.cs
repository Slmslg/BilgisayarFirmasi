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
    public partial class Giris : Form
    {
        public Giris()
        {
            InitializeComponent();
        }
        public SqlConnection baglanti;
        public string kullaniciAdi;
        private void Form1_Load(object sender, EventArgs e)
        {
            baglanti = new SqlConnection("Data Source=.;Initial Catalog=BILGISAYAR;Integrated Security=true");           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                 if (baglanti.State == ConnectionState.Closed)
            {
                 baglanti.Open();
            }
                 SqlParameter kAdi = new SqlParameter("@kadi",kadiText.Text);
                 SqlParameter kSifre = new SqlParameter("@ksifre", kSifreText.Text);
                 String giris = "SELECT * FROM KULLANICI WHERE KullaniciAdi = @kadi AND KullaniciSifre = @ksifre";
                 SqlCommand komut = new SqlCommand(giris,baglanti);
                 komut.Parameters.Add(kAdi);
                 komut.Parameters.Add(kSifre);
                 SqlDataAdapter adapter = new SqlDataAdapter(komut);
                 DataTable veriTablosu = new DataTable();
                 adapter.Fill(veriTablosu);
                 if (veriTablosu.Rows.Count > 0 )
                 {
                     komut.ExecuteNonQuery();
                     SqlDataReader oku = komut.ExecuteReader();
                     if (oku.Read())
                     {
                         kullaniciAdi = oku["KullaniciTuru"].ToString();
                         if (kullaniciAdi == "Admin")
                         {
                             Admin adminGirisi = new Admin();
                             adminGirisi.Show();
                             this.Hide();
                         }
                         else if(kullaniciAdi == "User")
                         {
                             User userGirisi = new User();
                             userGirisi.Text = oku["MusteriID"].ToString();
                             userGirisi.Show();
                             this.Hide();
                         }
                        //MessageBox.Show("Giris Başarılı"+kullaniciAdi);                       
                     }  
                 }
                 else
                 {
                     MessageBox.Show("Hatalı kullanıcı girişi");
                 }
            }
            catch (SqlException)
            {
                MessageBox.Show("Veri Tabanında bir hata oluştu...");
            }           
        }
    }
}
