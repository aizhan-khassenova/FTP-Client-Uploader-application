using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormFtpUploader
{
    public partial class Form1 : Form
    {
        FtpWebRequest ftpRequest;
        string PathAndFilename = "";
        string Filename = "";
        public Form1()
        {
            InitializeComponent();
        }

        public void UploadFile()
        {
            FileStream uploadedFile = new FileStream(PathAndFilename, FileMode.Open, FileAccess.Read);

            ftpRequest = (FtpWebRequest)WebRequest.Create(textBox1.Text + Filename);
            ftpRequest.Credentials = new NetworkCredential(textBox3.Text, textBox4.Text);
            //ftpRequest.EnableSsl = _UseSSL;
            ftpRequest.Method = WebRequestMethods.Ftp.UploadFile;

            //Буфер для загружаемых данных
            byte[] file_to_bytes = new byte[uploadedFile.Length];
            //Считываем данные в буфер
            uploadedFile.Read(file_to_bytes, 0, file_to_bytes.Length);

            uploadedFile.Close();

            //Поток для загрузки файла 
            Stream writer = ftpRequest.GetRequestStream();

            writer.Write(file_to_bytes, 0, file_to_bytes.Length);
            writer.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            // получаем выбранный файл
            PathAndFilename = openFileDialog1.FileName;
            Filename = openFileDialog1.SafeFileName;
            textBox2.Text = PathAndFilename;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox1.Text == "" | textBox2.Text== "")
            {
                MessageBox.Show("Проверьте заполнение полей Путь Ftp сервера и Путь к файлу");
            }
            else
            {
                UploadFile();
            }
        }
    }

}
