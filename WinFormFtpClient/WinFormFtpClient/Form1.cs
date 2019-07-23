using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormFtpClient
{
    public partial class Form1 : Form
    {
        string Addr = "";
        string filename = "";
        public Form1()
        {
            InitializeComponent();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox3.Text == "" | textBox6.Text == "")
            {
                MessageBox.Show("Поля адреса и именя файла не должно быть пустым");
            }
            else
            {
                GetFtpFileClass GFFC = new GetFtpFileClass();
                GFFC.Addres = fullpath();
                GFFC.Login = textBox2.Text;
                GFFC.Password = textBox4.Text;
                GFFC.FileName = filename;
                GFFC.Btn = button1;
                GFFC.GetFtpFileMethod();
            }
        }
        
        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            Addr = textBox3.Text.Replace("ftp://", "");
            label5.Text = fullpath();

        }

        public string fullpath()
        {
            return "ftp://" + Addr + filename;
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            filename = textBox6.Text;
            label5.Text = fullpath();
        }
    }
    public class GetFtpFileClass
    {
        public string Addres { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string FileName { get; set; }
        public Button Btn { get; set; }

        public void GetFtpFileMethod()
        {
            try
            {
                Btn.Enabled = false;
                FtpWebRequest frpWebRequest = (FtpWebRequest)FtpWebRequest.Create(Addres);
                frpWebRequest.Credentials = new NetworkCredential(Login, Password);
                frpWebRequest.KeepAlive = true;
                frpWebRequest.UsePassive = true;
                frpWebRequest.UseBinary = true;
                frpWebRequest.Method = WebRequestMethods.Ftp.DownloadFile;
                FtpWebResponse response = (FtpWebResponse)frpWebRequest.GetResponse();
                Stream stream = response.GetResponseStream();
                List<byte> list = new List<byte>();
                int b;
                while ((b = stream.ReadByte()) != -1)
                    list.Add((byte)b);
                File.WriteAllBytes(@"" + FileName, list.ToArray());
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Возникла ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Btn.Enabled = true;
            }
        }
    }

}
