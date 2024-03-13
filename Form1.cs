using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FolderListener
{
    public partial class Form1 : Form
    {

        string IndirilenlerDosyaYolu = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Shell Folders").GetValue("{374DE290-123F-4565-9164-39C4925E467B}").ToString();
        string HedefDosyaYolu = Path.Combine("F:\\Downloads");

        public Form1()
        {
            InitializeComponent();

            textBox1.Text = IndirilenlerDosyaYolu;
            textBox2.Text = HedefDosyaYolu;

            string DinlenecekDizin = Path.Combine(textBox1.Text);

            if (!Directory.Exists(DinlenecekDizin))
            {
                listBox1.Items.Add("Dinlenecek dizin bulunamadı! " + DinlenecekDizin);
                return;
            }
            else
            {
                listBox1.Items.Add("Dinlenecek dizin: " + DinlenecekDizin);
            }

            string HedefDizin = Path.Combine(textBox2.Text);
            if (!Directory.Exists(HedefDizin))
            {
                listBox1.Items.Add("Hedef dizin bulunamadı! " + HedefDizin);
                return;
            }
            else
            {
                listBox1.Items.Add("Hedef dizin: " + HedefDizin);
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //notifyIcon1.ShowBalloonTip(1000, "Dosya Ekleme Bildirimi", "Dizin dinlemesi başlatıldı!", ToolTipIcon.Info);

        }

        private void fileSystemWatcher1_Created(object sender, System.IO.FileSystemEventArgs e)
        {
            string filePath = e.FullPath;
            // Dosya bilgilerini alın.
            FileInfo fileInfo = new FileInfo(filePath);
            string fileName = fileInfo.Name;
            string extension = fileInfo.Extension;
            long fileSize = fileInfo.Length;
            string targetPath = Path.Combine(textBox2.Text, extension);

            if (fileSize <= 10000)
            {
                if (Directory.Exists(targetPath))
                {
                    notifyIcon1.ShowBalloonTip(1000, "Dosya Ekleme Bildirimi", fileName + " isimli dosya " + targetPath + " dizinine taşındı.", ToolTipIcon.Info);
                    string newFilePath = targetPath + "\\" + fileInfo.Name;
                    listBox1.Items.Add(fileName + " isimli dosya " + targetPath + " dizinine taşındı.");
                    File.Move(filePath, newFilePath);
                }
                else
                {
                    Directory.CreateDirectory(targetPath);

                    listBox1.Items.Add("Hedef dizin olmadığı için dizin oluşturuldu:" + extension);
                    notifyIcon1.ShowBalloonTip(1000, "Dosya Ekleme Bildirimi", "Hedef dizin olmadığı için dizin oluşturuldu:" + extension, ToolTipIcon.Info);
                }

            }

        }

        private void gosterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
        }

        private void kapatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void notifyIcon1_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
        }

        private void listenFolderSelect_Button_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog DilenecekKlasorDizini = new FolderBrowserDialog();
            if (DilenecekKlasorDizini.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = DilenecekKlasorDizini.SelectedPath;
            }
            else
            {
                listBox1.Items.Add("Dinlenecek dizin için bir klasör seçilmedi");
            }



        }

        private void targetFolderSelect_Button_Click(object sender, EventArgs e)
        {

            FolderBrowserDialog HedefKlasorDizini = new FolderBrowserDialog();
            if (HedefKlasorDizini.ShowDialog() == DialogResult.OK)
            {
                textBox2.Text = HedefKlasorDizini.SelectedPath;
            }
            else
            {
                listBox1.Items.Add("Hedef dizin için bir klasör seçilmedi");
            }

        }


        private void notifyIcon1_BalloonTipClicked(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
        }



    }
}
