using StokTakip.BLL;
using System;
using System.Data;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace StokTakip.UI
{
    public partial class ReportForm : Form
    {
        public ReportForm()
        {
            InitializeComponent();
        }

        RaporManager rManager = new RaporManager();

        void RaporlariGetir()
        {
            try
            {
                decimal ciro = rManager.CiroHesapla();
                decimal kar = rManager.KarHesapla();
                lblCiro.Text = ciro.ToString("C2");
                lblKar.Text = kar.ToString("C2");

                DataTable dtKritik = rManager.KritikStokListesi();
                DataTable dtEnCokSatan = rManager.EnCokSatanlariGetir();
                DataTable dtAylikSatis = rManager.AylikSatisGetir();
                DataTable dtMusteriCiro = rManager.MusteriCiroGetir();

             
                dgvKritik.DataSource = dtKritik;
                if (dgvKritik.Columns["Geçen Gün"] != null)
                {
                    dgvKritik.Columns["Geçen Gün"].Visible = false;
                }
                dgvEnCokSatan.DataSource = dtEnCokSatan;
                if (dgvEnCokSatan.Columns["Toplam Kar"] != null)
                {
                    dgvEnCokSatan.Columns["Toplam Kar"].Visible = false;
                }
                dgvAylikSatis.DataSource = dtAylikSatis;
                if (dgvAylikSatis.Columns["Aylık Kar"] != null)
                {
                    dgvAylikSatis.Columns["Aylık Kar"].Visible = false;
                }
                dgvMusteriCiro.DataSource = dtMusteriCiro;
                if (dtEnCokSatan.Rows.Count > 0)
                {
                    GrafikOlustur(
                        chartEnCokSatan,
                        dtEnCokSatan,
                        "Ürün Adı",
                        "Toplam Kar",
                        "En Çok Satanların Karlılık Durumu",
                        SeriesChartType.Column
                    );
                    if (chartEnCokSatan.Series.Count > 0)
                    {
                        chartEnCokSatan.Series[0].LabelFormat = "C2";
                    }
                }
                if (dtMusteriCiro.Rows.Count > 0)
                {
                    GrafikOlustur(
                        chartMusteriCiro, dtMusteriCiro,
                        "Müşteri", "Toplam Alışveriş",
                        "Müşteri Ciro Dağılımı",
                        SeriesChartType.Pie
                    );
                }
                if (dtAylikSatis.Rows.Count > 0)
                {
                    GrafikOlustur(
                        chartAylikSatis,
                        dtAylikSatis,
                        "Dönem",
                        "Aylık Kar",
                        "Aylık Net Kar Grafiği",
                        SeriesChartType.Column
                    );
                    if (chartAylikSatis.Series.Count > 0)
                        chartAylikSatis.Series[0].LabelFormat = "C2";
                }
                if (dtKritik.Rows.Count > 0)
                {
                    GrafikOlustur(
                        chartKritikStok, dtKritik,
                        "Ürün Adı", "Kalan Stok",
                        "Kritik Seviyedeki Ürünler",
                        SeriesChartType.Bar
                    );
                    chartKritikStok.Palette = ChartColorPalette.Fire;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Raporlar yüklenirken hata: " + ex.Message);
            }
        }

        private void GrafikOlustur(Chart chart, DataTable dt, string xKolon, string yKolon, string baslik, SeriesChartType tur)
        {
            chart.Series.Clear();
            chart.Titles.Clear();
            Title title = new Title(baslik);
            title.Font = new System.Drawing.Font("Segoe UI", 12, System.Drawing.FontStyle.Bold);
            title.ForeColor = System.Drawing.Color.DimGray;
            chart.Titles.Add(title);
            string seriAdi = "Veriler";
            Series series = new Series(seriAdi);
            series.ChartType = tur;
            if (chart.ChartAreas.Count > 0)
            {
                series.ChartArea = chart.ChartAreas[0].Name;
            }
            foreach (DataRow row in dt.Rows)
            {
                if (row[xKolon] != DBNull.Value && row[yKolon] != DBNull.Value)
                {
                    series.Points.AddXY(row[xKolon], row[yKolon]);
                }
            }
            series.IsValueShownAsLabel = true;
            series.LabelForeColor = System.Drawing.Color.Black;
            series.ToolTip = "#VALX: #VALY";
            if (tur == SeriesChartType.Pie)
            {
                series.Label = "#VALX \n(#PERCENT)";
            }
            else
            {
                series.Label = "#VALY";
                chart.Legends.Clear(); 
            }
            chart.Series.Add(series);
        }

        private void btnYenile_Click(object sender, EventArgs e)
        {
            RaporlariGetir();
        }

        private void ReportForm_Load_1(object sender, EventArgs e)
        {
            
            RaporlariGetir();
            cmbRaporTuru.Items.Clear();
            cmbRaporTuru.Items.Add("Rapor Seçiniz...");
            cmbRaporTuru.Items.Add("Kritik Stok Listesi");
            cmbRaporTuru.Items.Add("En Çok Satan Ürünler");
            cmbRaporTuru.Items.Add("Aylık Satış Grafiği");
            cmbRaporTuru.Items.Add("Müşteri Ciro Raporu");
            cmbRaporTuru.SelectedIndex = 0;
        }

        private void cmbRaporTuru_SelectedIndexChanged(object sender, EventArgs e)
        {
            PanelleriSifirla();

            if (cmbRaporTuru.SelectedItem == null) return;
            string secim = cmbRaporTuru.SelectedItem.ToString();
            if (secim == "Rapor Seçiniz...")
            {
                tableLayoutPanel1.Visible = false;
                return;
            }

            tableLayoutPanel1.Visible = false;
            GroupBox secilenGrup = null;

            if (secim == "Kritik Stok Listesi") secilenGrup = groupBox2;
            else if (secim == "En Çok Satan Ürünler") secilenGrup = groupBox3;
            else if (secim == "Aylık Satış Grafiği") secilenGrup = groupBox4;
            else if (secim == "Müşteri Ciro Raporu") secilenGrup = groupBox5;
            if (secilenGrup != null)
            {
                secilenGrup.Parent = this;
                secilenGrup.Dock = DockStyle.Fill;
                secilenGrup.BringToFront();
                secilenGrup.Visible = true;
            }
        }

        private void PanelleriSifirla()
        {
            tableLayoutPanel1.Visible = true;
            groupBox4.Parent = tableLayoutPanel1;
            tableLayoutPanel1.SetCellPosition(groupBox4, new TableLayoutPanelCellPosition(0, 0));
            groupBox4.Dock = DockStyle.Fill;
            groupBox4.Visible = true;
            groupBox3.Parent = tableLayoutPanel1;
            tableLayoutPanel1.SetCellPosition(groupBox3, new TableLayoutPanelCellPosition(1, 0));
            groupBox3.Dock = DockStyle.Fill;
            groupBox3.Visible = true;
            groupBox2.Parent = tableLayoutPanel1;
            tableLayoutPanel1.SetCellPosition(groupBox2, new TableLayoutPanelCellPosition(0, 1));
            groupBox2.Dock = DockStyle.Fill;
            groupBox2.Visible = true;
            groupBox5.Parent = tableLayoutPanel1;
            tableLayoutPanel1.SetCellPosition(groupBox5, new TableLayoutPanelCellPosition(1, 1));
            groupBox5.Dock = DockStyle.Fill;
            groupBox5.Visible = true;
        }
    }
}