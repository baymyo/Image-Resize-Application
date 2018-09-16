using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ImageResize
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            this.cmbFileType.SelectedIndex = 0;
        }

        private void btnKaynak_Click(object sender, EventArgs e)
        {
            try
            {
                using (FolderBrowserDialog open = new FolderBrowserDialog())
                {
                    switch (open.ShowDialog())
                    {
                        case DialogResult.OK:
                        case DialogResult.Retry:
                        case DialogResult.Yes:
                            txtKaynak.Text = open.SelectedPath;
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Hata");
            }
        }

        private void btnHedef_Click(object sender, EventArgs e)
        {
            try
            {
                using (FolderBrowserDialog open = new FolderBrowserDialog())
                {
                    switch (open.ShowDialog())
                    {
                        case DialogResult.OK:
                        case DialogResult.Retry:
                        case DialogResult.Yes:
                            txtHedef.Text = open.SelectedPath;
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Hata");
            }
        }

        private void btnDirekt_Click(object sender, EventArgs e)
        {
            try
            {
                using (DataTable dt = BAYMYO.UI.FileIO.ReadDirectory(txtKaynak.Text, cmbFileType.Text))
                {
                    if (dt.Rows.Count <= 1)
                    {
                        MessageBox.Show("Belirtilen klasörde " + cmbFileType.Text + " dosya tipinde fotoğraf bulunamadı.", "Uyarı");
                        return;
                    }
                    else
                    {
                        dt.Rows.RemoveAt(0);
                        foreach (DataRow item in dt.Rows)
                            BAYMYO.UI.FileIO.ResizeImage(this.txtKaynak.Text + "\\" + item[0].ToString(), this.txtHedef.Text + "\\" + item[0].ToString(), BAYMYO.UI.Converts.NullToInt32(this.numWidth.Value), this.cmbFileType.Text.Replace("*", ""));
                        MessageBox.Show(dt.Rows.Count + " adet fotoğraf " + numWidth.Value + "px genişliğinde yeniden boyutlandırıldı.", "Bilgi");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Hata");
            }
        }
    }
}
