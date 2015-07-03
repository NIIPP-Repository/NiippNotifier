namespace NiippNotifier
{
    partial class FormStatistics
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormStatistics));
            this.gbMaingInfo = new System.Windows.Forms.GroupBox();
            this.dgvShowStatistic = new System.Windows.Forms.DataGridView();
            this.gbMaingInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvShowStatistic)).BeginInit();
            this.SuspendLayout();
            // 
            // gbMaingInfo
            // 
            this.gbMaingInfo.Controls.Add(this.dgvShowStatistic);
            this.gbMaingInfo.Font = new System.Drawing.Font("Candara", 11.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.gbMaingInfo.Location = new System.Drawing.Point(12, 12);
            this.gbMaingInfo.Name = "gbMaingInfo";
            this.gbMaingInfo.Size = new System.Drawing.Size(612, 348);
            this.gbMaingInfo.TabIndex = 0;
            this.gbMaingInfo.TabStop = false;
            this.gbMaingInfo.Text = "Основная информация";
            // 
            // dgvShowStatistic
            // 
            this.dgvShowStatistic.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvShowStatistic.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvShowStatistic.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvShowStatistic.Location = new System.Drawing.Point(6, 19);
            this.dgvShowStatistic.Name = "dgvShowStatistic";
            this.dgvShowStatistic.Size = new System.Drawing.Size(590, 322);
            this.dgvShowStatistic.TabIndex = 0;
            // 
            // FormStatistics
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(637, 381);
            this.Controls.Add(this.gbMaingInfo);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormStatistics";
            this.Text = "FormStatistics";
            this.Load += new System.EventHandler(this.FormStatistics_Load);
            this.gbMaingInfo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvShowStatistic)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbMaingInfo;
        private System.Windows.Forms.DataGridView dgvShowStatistic;
    }
}