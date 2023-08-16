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

namespace login_register
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
      

        private void button1_Click(object sender, EventArgs e)
        {
           
            string kullanici=txtNick.Text;
            string password = txtPassword.Text;
            SqlConnection conn = null;

            try
               
            {
                if (!string.IsNullOrEmpty(kullanici) || !string.IsNullOrEmpty(password))
                {   
                    conn = new SqlConnection(@"Data Source=DESKTOP-9VTPEG5;Initial Catalog=kayit;Integrated Security=True");
                    conn.Open();

                SqlCommand cmd = new SqlCommand("SELECT * FROM users where nickname='" + kullanici + "'and password='" + password + "'",conn);
                SqlDataReader oku = cmd.ExecuteReader();
                if (oku.Read())
                {
                    MessageBox.Show("Hoşgeldiniz:" + kullanici);
                }
                else
                {
                    MessageBox.Show("Hatalı Kullnıcı adı veya şifre");  
                }

                }
                else
                {
                    MessageBox.Show("Kullanıcı Adı veya Şifre boş geçilemez!");
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Sql bağlantısında hata oluştu!" + ex.ToString());
            }
            finally
            {
                if(conn != null)
                    conn.Close();
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string kullanici = txtNick.Text;
            string password = txtPassword.Text;
            SqlConnection conn=null;

            try
            {
                if (!string.IsNullOrEmpty(kullanici) || !string.IsNullOrEmpty(password))
                {
                    conn=new SqlConnection(@"Data Source=DESKTOP-9VTPEG5;Initial Catalog=kayit;Integrated Security=True");
                    conn.Open();
                    SqlCommand cmd=new SqlCommand("SELECT COUNT(*) FROM users where nickname='"+kullanici+"'",conn);
                    int kullaniciSayi = (int)cmd.ExecuteScalar();//eşleşen kayıt sayısını) döndürdüğünden, bu sonucu bir int türüne dönüştürür.

                    if (kullaniciSayi > 0)
                    {
                        MessageBox.Show("Bu kullanıcı adı zaten mevcut!!");
                    }
                    else
                    {
                        SqlCommand c =new SqlCommand("INSERT INTO users  (nickname,password) VALUES('"+kullanici+"','"+password+"')",conn);
                        c.ExecuteNonQuery(); //bir değişiklik yapıldığı zaman kullanılır.
                        MessageBox.Show("Kaydınız Başarıyla Oluşturuldu.");
                    }
                }
                else
                {
                    MessageBox.Show("Boş geçilemez!!");
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Sql Bağlantısı olmadı!" + ex.ToString());
            }
            finally {if(conn!=null)
                    conn.Close();
                        }
        }
    }
}
