namespace StokTakip.UI
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panelMenu = new System.Windows.Forms.Panel();
            this.btnCikis = new FontAwesome.Sharp.IconButton();
            this.btnRaporlar = new FontAwesome.Sharp.IconButton();
            this.btnSatis = new FontAwesome.Sharp.IconButton();
            this.btnMusteriler = new FontAwesome.Sharp.IconButton();
            this.btnUrunler = new FontAwesome.Sharp.IconButton();
            this.panelHeader = new System.Windows.Forms.Panel();
            this.panelDesktop = new System.Windows.Forms.Panel();
            this.panelMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelMenu
            // 
            this.panelMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(55)))), ((int)(((byte)(82)))));
            this.panelMenu.Controls.Add(this.btnCikis);
            this.panelMenu.Controls.Add(this.btnRaporlar);
            this.panelMenu.Controls.Add(this.btnSatis);
            this.panelMenu.Controls.Add(this.btnMusteriler);
            this.panelMenu.Controls.Add(this.btnUrunler);
            this.panelMenu.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelMenu.Location = new System.Drawing.Point(0, 0);
            this.panelMenu.Name = "panelMenu";
            this.panelMenu.Size = new System.Drawing.Size(220, 673);
            this.panelMenu.TabIndex = 5;
            // 
            // btnCikis
            // 
            this.btnCikis.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCikis.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCikis.ForeColor = System.Drawing.Color.White;
            this.btnCikis.IconChar = FontAwesome.Sharp.IconChar.SignOut;
            this.btnCikis.IconColor = System.Drawing.Color.White;
            this.btnCikis.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnCikis.IconSize = 32;
            this.btnCikis.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCikis.Location = new System.Drawing.Point(0, 280);
            this.btnCikis.Name = "btnCikis";
            this.btnCikis.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.btnCikis.Size = new System.Drawing.Size(220, 63);
            this.btnCikis.TabIndex = 4;
            this.btnCikis.Text = "Çıkış";
            this.btnCikis.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCikis.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnCikis.UseVisualStyleBackColor = true;
            this.btnCikis.Click += new System.EventHandler(this.btnCikis_Click);
            // 
            // btnRaporlar
            // 
            this.btnRaporlar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRaporlar.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRaporlar.ForeColor = System.Drawing.Color.White;
            this.btnRaporlar.IconChar = FontAwesome.Sharp.IconChar.ChartBar;
            this.btnRaporlar.IconColor = System.Drawing.Color.White;
            this.btnRaporlar.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnRaporlar.IconSize = 32;
            this.btnRaporlar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRaporlar.Location = new System.Drawing.Point(0, 225);
            this.btnRaporlar.Name = "btnRaporlar";
            this.btnRaporlar.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.btnRaporlar.Size = new System.Drawing.Size(220, 63);
            this.btnRaporlar.TabIndex = 3;
            this.btnRaporlar.Text = "Rapolar";
            this.btnRaporlar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRaporlar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnRaporlar.UseVisualStyleBackColor = true;
            this.btnRaporlar.Click += new System.EventHandler(this.btnRaporlar_Click);
            // 
            // btnSatis
            // 
            this.btnSatis.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSatis.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSatis.ForeColor = System.Drawing.Color.White;
            this.btnSatis.IconChar = FontAwesome.Sharp.IconChar.Shopify;
            this.btnSatis.IconColor = System.Drawing.Color.White;
            this.btnSatis.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnSatis.IconSize = 32;
            this.btnSatis.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSatis.Location = new System.Drawing.Point(0, 170);
            this.btnSatis.Name = "btnSatis";
            this.btnSatis.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.btnSatis.Size = new System.Drawing.Size(220, 63);
            this.btnSatis.TabIndex = 2;
            this.btnSatis.Text = "Satış Yap";
            this.btnSatis.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSatis.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSatis.UseVisualStyleBackColor = true;
            this.btnSatis.Click += new System.EventHandler(this.btnSatis_Click);
            // 
            // btnMusteriler
            // 
            this.btnMusteriler.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMusteriler.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMusteriler.ForeColor = System.Drawing.Color.White;
            this.btnMusteriler.IconChar = FontAwesome.Sharp.IconChar.Users;
            this.btnMusteriler.IconColor = System.Drawing.Color.White;
            this.btnMusteriler.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnMusteriler.IconSize = 32;
            this.btnMusteriler.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnMusteriler.Location = new System.Drawing.Point(0, 115);
            this.btnMusteriler.Name = "btnMusteriler";
            this.btnMusteriler.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.btnMusteriler.Size = new System.Drawing.Size(220, 63);
            this.btnMusteriler.TabIndex = 1;
            this.btnMusteriler.Text = "Müşteriler";
            this.btnMusteriler.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnMusteriler.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnMusteriler.UseVisualStyleBackColor = true;
            this.btnMusteriler.Click += new System.EventHandler(this.btnMusteriler_Click);
            // 
            // btnUrunler
            // 
            this.btnUrunler.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUrunler.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUrunler.ForeColor = System.Drawing.Color.White;
            this.btnUrunler.IconChar = FontAwesome.Sharp.IconChar.Box;
            this.btnUrunler.IconColor = System.Drawing.Color.White;
            this.btnUrunler.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnUrunler.IconSize = 32;
            this.btnUrunler.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnUrunler.Location = new System.Drawing.Point(0, 60);
            this.btnUrunler.Name = "btnUrunler";
            this.btnUrunler.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.btnUrunler.Size = new System.Drawing.Size(220, 63);
            this.btnUrunler.TabIndex = 0;
            this.btnUrunler.Text = "Ürünler";
            this.btnUrunler.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnUrunler.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnUrunler.UseVisualStyleBackColor = true;
            this.btnUrunler.Click += new System.EventHandler(this.btnUrunler_Click);
            // 
            // panelHeader
            // 
            this.panelHeader.BackColor = System.Drawing.Color.White;
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(220, 0);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(1042, 60);
            this.panelHeader.TabIndex = 6;
            // 
            // panelDesktop
            // 
            this.panelDesktop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelDesktop.Location = new System.Drawing.Point(220, 60);
            this.panelDesktop.Name = "panelDesktop";
            this.panelDesktop.Size = new System.Drawing.Size(1042, 613);
            this.panelDesktop.TabIndex = 7;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(246)))), ((int)(((byte)(249)))));
            this.ClientSize = new System.Drawing.Size(1262, 673);
            this.Controls.Add(this.panelDesktop);
            this.Controls.Add(this.panelHeader);
            this.Controls.Add(this.panelMenu);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "StokveSatışTakip";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form1_Load_1);
            this.panelMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panelMenu;
        private FontAwesome.Sharp.IconButton btnUrunler;
        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Panel panelDesktop;
        private FontAwesome.Sharp.IconButton btnCikis;
        private FontAwesome.Sharp.IconButton btnRaporlar;
        private FontAwesome.Sharp.IconButton btnSatis;
        private FontAwesome.Sharp.IconButton btnMusteriler;
    }
}