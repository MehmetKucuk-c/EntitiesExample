using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace EntitiesExample
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        readonly DbSinavOgrenciEntities db = new DbSinavOgrenciEntities();

        private void BtnDesListesi_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = db.TBL_Dersler.ToList();
        }

        private void BtnOgrListele_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = db.TBL_Ogrenci.ToList();
            dataGridView1.Columns[3].Visible = false;
            dataGridView1.Columns[4].Visible = false;
        }

        private void BtnNotListesi_Click(object sender, EventArgs e)
        {
            //dataGridView1.DataSource = db.TBL_Ogrenci.ToList();
            var query = from item in db.TBL_Notlar
                        select new
                        {
                            item.NotId,
                            item.TBL_Ogrenci.OgrAdi,
                            item.TBL_Ogrenci.OgrSoyadi,
                            item.TBL_Dersler.DersAdi,
                            item.Sinav1,
                            item.Sinav2,
                            item.Sinav3,
                            item.Ortalama,
                            item.Durum
                        };
            dataGridView1.DataSource = query.ToList();
        }

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            TBL_Ogrenci t = new TBL_Ogrenci
            {
                OgrAdi = TxtAd.Text,
                OgrSoyadi = TxtSoyad.Text,
            };

            db.TBL_Ogrenci.Add(t);
            db.SaveChanges();
            MessageBox.Show("Öğrenci Listeye klenmiştir");
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            //int id = Convert.ToInt32(TxtOgrId.Text);
            //var x = db.TBL_Ogrenci.Find(id);
            db.TBL_Ogrenci.Remove(db.TBL_Ogrenci.Find(Convert.ToInt32(TxtOgrId.Text)));
            db.SaveChanges();
            MessageBox.Show("Öğrenci Sistemden silindi");
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            //int id = Convert.ToInt32(TxtOgrId.Text);
            var x = db.TBL_Ogrenci.Find(Convert.ToInt32(TxtOgrId.Text));
            x.OgrAdi = TxtAd.Text;
            x.OgrSoyadi = TxtSoyad.Text;
            x.Fotograf = TxtFoto.Text;
            db.SaveChanges();
            MessageBox.Show("Öğrenci Başarıyla Güncelendi");
        }

        private void BtnBul_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = db.TBL_Ogrenci.Where(x => x.OgrAdi.Equals(TxtAd.Text) | x.OgrSoyadi.Equals(TxtSoyad.Text)).ToList();
        }

        private void BtnProsedur_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = db.NotListesi();
        }

        private void TxtAd_TextChanged(object sender, EventArgs e)
        {
            //string aranaDeger = TxtAd.Text;
            var degerBulunan = from item in db.TBL_Ogrenci
                               where item.OgrAdi.Contains(TxtAd.Text)
                               select item;
            dataGridView1.DataSource = degerBulunan.ToList();
        }

        private void BtnLinqEntities_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                List<TBL_Ogrenci> tblOgrencis = db.TBL_Ogrenci.OrderBy(p => p.OgrAdi).ToList();
                dataGridView1.DataSource = tblOgrencis;
            }
            else if (radioButton2.Checked)
            {
                List<TBL_Ogrenci> ogrencis = db.TBL_Ogrenci.OrderByDescending(p => p.OgrAdi).ToList();
                dataGridView1.DataSource = ogrencis;
            }
            else if (radioButton3.Checked)
            {
                List<TBL_Ogrenci> ogrencis = db.TBL_Ogrenci.OrderBy(p => p.OgrAdi).Take(3).ToList();
                dataGridView1.DataSource = ogrencis;
            }
            else if (radioButton4.Checked)
            {
                List<TBL_Ogrenci> ogrencis = db.TBL_Ogrenci.Where(p => p.Id == 5).ToList();
                dataGridView1.DataSource = ogrencis;
            }
            else if (radioButton5.Checked)
            {
                List<TBL_Ogrenci> ogrencis = db.TBL_Ogrenci.Where(p => p.OgrAdi.StartsWith("a")).ToList();
                dataGridView1.DataSource = ogrencis;
            }
            else if (radioButton6.Checked)
            {
                List<TBL_Ogrenci> ogrencis = db.TBL_Ogrenci.Where(p => p.OgrAdi.EndsWith("a")).ToList();
                dataGridView1.DataSource = ogrencis;
            }
            else if (radioButton7.Checked)
            {
                bool ogrencis = db.TBL_Kulupler.Any();
                MessageBox.Show(ogrencis ? "Yok" : "Var", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (radioButton8.Checked)
            {
                MessageBox.Show("Toplam Ögrenci Sayısı:" + db.TBL_Ogrenci.Count().ToString(), "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (radioButton9.Checked)
            {
                var toplam = db.TBL_Notlar.Sum(p => p.Sinav1);
                MessageBox.Show("Sınav 1 Toplamı: " + toplam.ToString(), "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (radioButton10.Checked)
            {
                var avg = db.TBL_Notlar.Average(p => p.Sinav1);
                MessageBox.Show("Sınav 1 Toplamı: " + avg.ToString(), "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (radioButton11.Checked)
            {
                var avg = db.TBL_Notlar.Average(p => p.Sinav1);
                var sonuclar = db.TBL_Notlar.Where(p => p.Sinav1 > avg);
                //var sonuclar = db.TBL_Notlar.Where(p => p.Sinav1 > db.TBL_Notlar.Average(x => x.Sinav1));
                dataGridView1.DataSource = sonuclar.ToList();
                MessageBox.Show("Sınav 1 Ortalaması: " + avg.ToString(), "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (radioButton12.Checked)
            {
                var avg = db.TBL_Notlar.Max(p => p.Sinav1);
                MessageBox.Show("Sınav 1 En Yüksek Notu: " + avg.ToString(), "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (radioButton13.Checked)
            {
                var avg = db.TBL_Notlar.Min(p => p.Sinav1);
                MessageBox.Show("Sınav 1 En Düşük Notu: " + avg.ToString(), "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (radioButton14.Checked)
            {
                //var sonuclar = db.TBL_Notlar.Where(p => p.Sinav1 == db.TBL_Notlar.Max(x => x.Sinav1));
                var joinQuery = from not in db.TBL_Notlar.Where(p => p.Sinav1 == db.TBL_Notlar.Max(x => x.Sinav1))
                                join ogr in db.TBL_Ogrenci
                                    on not.Ogr equals ogr.Id
                                join ders in db.TBL_Dersler
                                    on not.Ders equals ders.DersId
                                select new
                                {
                                    not.NotId,
                                    NameSurname = ogr.OgrAdi + " " + ogr.OgrSoyadi,
                                    Class = ders.DersAdi,
                                    Quiz1 = not.Sinav1,
                                    Quiz2 = not.Sinav2,
                                    Quiz3 = not.Sinav3,
                                    Averange = not.Ortalama,
                                    Status = not.Durum
                                };
                var innerJoinResult = db.TBL_Ogrenci.Join(// outer sequence 
                    db.TBL_Ogrenci,  // inner sequence 
                    str1 => str1,    // outerKeySelector
                    str2 => str2,  // innerKeySelector
                    (str1, str2) => str1);

                dataGridView1.DataSource = joinQuery.ToList();
            }
            else if (radioButton15.Checked)
            {
                //var sonuclar = db.TBL_Notlar.Where(p => p.Sinav1 == db.TBL_Notlar.Max(x => x.Sinav1));
                var joinQuery = from not in db.TBL_Notlar
                                join ogr in db.TBL_Ogrenci
                                    on not.Ogr equals ogr.Id
                                join ders in db.TBL_Dersler
                                    on not.Ders equals ders.DersId
                                where not.Sinav1 == db.TBL_Notlar.Min(x => x.Sinav1)
                                select new
                                {
                                    not.NotId,
                                    NameSurname = ogr.OgrAdi + " " + ogr.OgrSoyadi,
                                    Class = ders.DersAdi,
                                    Quiz1 = not.Sinav1,
                                    Quiz2 = not.Sinav2,
                                    Quiz3 = not.Sinav3,
                                    Averange = not.Ortalama,
                                    Status = not.Durum
                                };
                dataGridView1.DataSource = joinQuery.ToList();
            }
        }

        private void BtnJoin_Click(object sender, EventArgs e)
        {
            var query = from d1 in db.TBL_Notlar
                        join d2 in db.TBL_Ogrenci
                            on d1.Ogr equals d2.Id
                        join d3 in db.TBL_Dersler
                            on d1.Ders equals d3.DersId
                        select new
                        {
                            Öğrenci = d2.OgrAdi + " " + d2.OgrSoyadi,
                            Ders = d3.DersAdi,
                            Sınav1 = d1.Sinav1,
                            Sınav2 = d1.Sinav2,
                            Sınav3 = d1.Sinav3,
                            d1.Ortalama
                        };
            dataGridView1.DataSource = query.ToList();
        }
    }
}