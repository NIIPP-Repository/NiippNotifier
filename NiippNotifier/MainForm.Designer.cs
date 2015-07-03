namespace NiippNotifier
{
    partial class MainForm
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.файлToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.закрытьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.автозагрузкаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.установитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.снятьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.видToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.показатьЦветаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.настройкаПоказаСтолбцовToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.неПоказыватьЗавершенныеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.неПоказыватьАктивныеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.действиеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.синхронизацияToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.просмотрToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.статистикаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gbEdit = new System.Windows.Forms.GroupBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.dtpTimeOfDeliveryEl = new System.Windows.Forms.DateTimePicker();
            this.tbNumSL = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.rtbComment = new System.Windows.Forms.RichTextBox();
            this.dtpTimeOfCreationSL = new System.Windows.Forms.DateTimePicker();
            this.cbNamePM = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.gbShow = new System.Windows.Forms.GroupBox();
            this.dgvShow = new System.Windows.Forms.DataGridView();
            this.contextMenuStripDGV = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.удалитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.пометитьКакЗавершенноеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label5 = new System.Windows.Forms.Label();
            this.lblUser = new System.Windows.Forms.Label();
            this.menuStrip.SuspendLayout();
            this.gbEdit.SuspendLayout();
            this.gbShow.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvShow)).BeginInit();
            this.contextMenuStripDGV.SuspendLayout();
            this.SuspendLayout();
            // 
            // notifyIcon
            // 
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "NIIPP Notifier";
            this.notifyIcon.Visible = true;
            this.notifyIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon_MouseDoubleClick);
            // 
            // menuStrip
            // 
            this.menuStrip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.menuStrip.Font = new System.Drawing.Font("Candara", 10.875F);
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.файлToolStripMenuItem,
            this.автозагрузкаToolStripMenuItem,
            this.видToolStripMenuItem,
            this.действиеToolStripMenuItem,
            this.просмотрToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Padding = new System.Windows.Forms.Padding(3, 1, 0, 1);
            this.menuStrip.Size = new System.Drawing.Size(1031, 24);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "menuStrip1";
            // 
            // файлToolStripMenuItem
            // 
            this.файлToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.закрытьToolStripMenuItem});
            this.файлToolStripMenuItem.Name = "файлToolStripMenuItem";
            this.файлToolStripMenuItem.Size = new System.Drawing.Size(52, 22);
            this.файлToolStripMenuItem.Text = "Файл";
            // 
            // закрытьToolStripMenuItem
            // 
            this.закрытьToolStripMenuItem.Name = "закрытьToolStripMenuItem";
            this.закрытьToolStripMenuItem.Size = new System.Drawing.Size(125, 22);
            this.закрытьToolStripMenuItem.Text = "Закрыть";
            this.закрытьToolStripMenuItem.Click += new System.EventHandler(this.закрытьToolStripMenuItem_Click);
            // 
            // автозагрузкаToolStripMenuItem
            // 
            this.автозагрузкаToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.установитьToolStripMenuItem,
            this.снятьToolStripMenuItem});
            this.автозагрузкаToolStripMenuItem.Name = "автозагрузкаToolStripMenuItem";
            this.автозагрузкаToolStripMenuItem.Size = new System.Drawing.Size(99, 22);
            this.автозагрузкаToolStripMenuItem.Text = "Автозагрузка";
            // 
            // установитьToolStripMenuItem
            // 
            this.установитьToolStripMenuItem.Name = "установитьToolStripMenuItem";
            this.установитьToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.установитьToolStripMenuItem.Text = "Установить";
            this.установитьToolStripMenuItem.Click += new System.EventHandler(this.установитьToolStripMenuItem_Click);
            // 
            // снятьToolStripMenuItem
            // 
            this.снятьToolStripMenuItem.Name = "снятьToolStripMenuItem";
            this.снятьToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.снятьToolStripMenuItem.Text = "Снять";
            this.снятьToolStripMenuItem.Click += new System.EventHandler(this.снятьToolStripMenuItem_Click);
            // 
            // видToolStripMenuItem
            // 
            this.видToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.показатьЦветаToolStripMenuItem,
            this.настройкаПоказаСтолбцовToolStripMenuItem,
            this.неПоказыватьЗавершенныеToolStripMenuItem,
            this.неПоказыватьАктивныеToolStripMenuItem});
            this.видToolStripMenuItem.Name = "видToolStripMenuItem";
            this.видToolStripMenuItem.Size = new System.Drawing.Size(44, 22);
            this.видToolStripMenuItem.Text = "Вид";
            // 
            // показатьЦветаToolStripMenuItem
            // 
            this.показатьЦветаToolStripMenuItem.Name = "показатьЦветаToolStripMenuItem";
            this.показатьЦветаToolStripMenuItem.Size = new System.Drawing.Size(250, 22);
            this.показатьЦветаToolStripMenuItem.Text = "Показать цвета";
            this.показатьЦветаToolStripMenuItem.Click += new System.EventHandler(this.показатьЦветаToolStripMenuItem_Click);
            // 
            // настройкаПоказаСтолбцовToolStripMenuItem
            // 
            this.настройкаПоказаСтолбцовToolStripMenuItem.Name = "настройкаПоказаСтолбцовToolStripMenuItem";
            this.настройкаПоказаСтолбцовToolStripMenuItem.Size = new System.Drawing.Size(250, 22);
            this.настройкаПоказаСтолбцовToolStripMenuItem.Text = "Настройка показа столбцов";
            // 
            // неПоказыватьЗавершенныеToolStripMenuItem
            // 
            this.неПоказыватьЗавершенныеToolStripMenuItem.Name = "неПоказыватьЗавершенныеToolStripMenuItem";
            this.неПоказыватьЗавершенныеToolStripMenuItem.Size = new System.Drawing.Size(250, 22);
            this.неПоказыватьЗавершенныеToolStripMenuItem.Text = "Не показывать завершенные";
            this.неПоказыватьЗавершенныеToolStripMenuItem.Click += new System.EventHandler(this.неПоказыватьЗавершенныеToolStripMenuItem_Click);
            // 
            // неПоказыватьАктивныеToolStripMenuItem
            // 
            this.неПоказыватьАктивныеToolStripMenuItem.Name = "неПоказыватьАктивныеToolStripMenuItem";
            this.неПоказыватьАктивныеToolStripMenuItem.Size = new System.Drawing.Size(250, 22);
            this.неПоказыватьАктивныеToolStripMenuItem.Text = "Не показывать активные";
            this.неПоказыватьАктивныеToolStripMenuItem.Click += new System.EventHandler(this.неПоказыватьАктивныеToolStripMenuItem_Click);
            // 
            // действиеToolStripMenuItem
            // 
            this.действиеToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.синхронизацияToolStripMenuItem});
            this.действиеToolStripMenuItem.Name = "действиеToolStripMenuItem";
            this.действиеToolStripMenuItem.Size = new System.Drawing.Size(76, 22);
            this.действиеToolStripMenuItem.Text = "Действие";
            // 
            // синхронизацияToolStripMenuItem
            // 
            this.синхронизацияToolStripMenuItem.Name = "синхронизацияToolStripMenuItem";
            this.синхронизацияToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.синхронизацияToolStripMenuItem.Text = "Синхронизация";
            this.синхронизацияToolStripMenuItem.Click += new System.EventHandler(this.синхронизацияToolStripMenuItem_Click);
            // 
            // просмотрToolStripMenuItem
            // 
            this.просмотрToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.статистикаToolStripMenuItem});
            this.просмотрToolStripMenuItem.Name = "просмотрToolStripMenuItem";
            this.просмотрToolStripMenuItem.Size = new System.Drawing.Size(78, 22);
            this.просмотрToolStripMenuItem.Text = "Просмотр";
            // 
            // статистикаToolStripMenuItem
            // 
            this.статистикаToolStripMenuItem.Name = "статистикаToolStripMenuItem";
            this.статистикаToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.статистикаToolStripMenuItem.Text = "Статистика";
            this.статистикаToolStripMenuItem.Click += new System.EventHandler(this.статистикаToolStripMenuItem_Click);
            // 
            // gbEdit
            // 
            this.gbEdit.Controls.Add(this.lblUser);
            this.gbEdit.Controls.Add(this.label5);
            this.gbEdit.Controls.Add(this.btnSave);
            this.gbEdit.Controls.Add(this.dtpTimeOfDeliveryEl);
            this.gbEdit.Controls.Add(this.tbNumSL);
            this.gbEdit.Controls.Add(this.label2);
            this.gbEdit.Controls.Add(this.rtbComment);
            this.gbEdit.Controls.Add(this.dtpTimeOfCreationSL);
            this.gbEdit.Controls.Add(this.cbNamePM);
            this.gbEdit.Controls.Add(this.label12);
            this.gbEdit.Controls.Add(this.label4);
            this.gbEdit.Controls.Add(this.label3);
            this.gbEdit.Controls.Add(this.label1);
            this.gbEdit.Font = new System.Drawing.Font("Candara", 11.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.gbEdit.Location = new System.Drawing.Point(14, 31);
            this.gbEdit.Margin = new System.Windows.Forms.Padding(2);
            this.gbEdit.Name = "gbEdit";
            this.gbEdit.Padding = new System.Windows.Forms.Padding(2);
            this.gbEdit.Size = new System.Drawing.Size(266, 490);
            this.gbEdit.TabIndex = 1;
            this.gbEdit.TabStop = false;
            this.gbEdit.Text = "Редактирование";
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("Candara", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnSave.Location = new System.Drawing.Point(15, 386);
            this.btnSave.Margin = new System.Windows.Forms.Padding(2);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(235, 30);
            this.btnSave.TabIndex = 11;
            this.btnSave.Text = "Создать запись";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // dtpTimeOfDeliveryEl
            // 
            this.dtpTimeOfDeliveryEl.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.dtpTimeOfDeliveryEl.Location = new System.Drawing.Point(15, 238);
            this.dtpTimeOfDeliveryEl.Margin = new System.Windows.Forms.Padding(2);
            this.dtpTimeOfDeliveryEl.Name = "dtpTimeOfDeliveryEl";
            this.dtpTimeOfDeliveryEl.Size = new System.Drawing.Size(162, 22);
            this.dtpTimeOfDeliveryEl.TabIndex = 4;
            this.dtpTimeOfDeliveryEl.Value = new System.DateTime(2014, 1, 1, 1, 1, 0, 0);
            // 
            // tbNumSL
            // 
            this.tbNumSL.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tbNumSL.Location = new System.Drawing.Point(15, 48);
            this.tbNumSL.Margin = new System.Windows.Forms.Padding(2);
            this.tbNumSL.Name = "tbNumSL";
            this.tbNumSL.Size = new System.Drawing.Size(162, 22);
            this.tbNumSL.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Candara", 11.25F);
            this.label2.Location = new System.Drawing.Point(15, 26);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(187, 18);
            this.label2.TabIndex = 1;
            this.label2.Text = "Номер сопроводного листа";
            // 
            // rtbComment
            // 
            this.rtbComment.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.rtbComment.Location = new System.Drawing.Point(15, 313);
            this.rtbComment.Margin = new System.Windows.Forms.Padding(2);
            this.rtbComment.Name = "rtbComment";
            this.rtbComment.Size = new System.Drawing.Size(235, 50);
            this.rtbComment.TabIndex = 10;
            this.rtbComment.Text = "";
            // 
            // dtpTimeOfCreationSL
            // 
            this.dtpTimeOfCreationSL.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.dtpTimeOfCreationSL.Location = new System.Drawing.Point(15, 167);
            this.dtpTimeOfCreationSL.Margin = new System.Windows.Forms.Padding(2);
            this.dtpTimeOfCreationSL.Name = "dtpTimeOfCreationSL";
            this.dtpTimeOfCreationSL.Size = new System.Drawing.Size(162, 22);
            this.dtpTimeOfCreationSL.TabIndex = 3;
            this.dtpTimeOfCreationSL.Value = new System.DateTime(2014, 1, 1, 1, 1, 0, 0);
            // 
            // cbNamePM
            // 
            this.cbNamePM.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cbNamePM.FormattingEnabled = true;
            this.cbNamePM.Items.AddRange(new object[] {
            "MFR",
            "CORE",
            "TR3",
            "LCX"});
            this.cbNamePM.Location = new System.Drawing.Point(15, 106);
            this.cbNamePM.Margin = new System.Windows.Forms.Padding(2);
            this.cbNamePM.Name = "cbNamePM";
            this.cbNamePM.Size = new System.Drawing.Size(162, 24);
            this.cbNamePM.TabIndex = 2;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Candara", 11.25F);
            this.label12.Location = new System.Drawing.Point(12, 290);
            this.label12.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(89, 18);
            this.label12.TabIndex = 13;
            this.label12.Text = "Примечание";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Candara", 11.25F);
            this.label4.Location = new System.Drawing.Point(12, 218);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(155, 18);
            this.label4.TabIndex = 3;
            this.label4.Text = "Дата выдачи пластины";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Candara", 11.25F);
            this.label3.Location = new System.Drawing.Point(12, 147);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(123, 18);
            this.label3.TabIndex = 2;
            this.label3.Text = "Дата создания СЛ";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Candara", 11.25F);
            this.label1.Location = new System.Drawing.Point(12, 86);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(177, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "Наименование схемы ФШ";
            // 
            // gbShow
            // 
            this.gbShow.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbShow.Controls.Add(this.dgvShow);
            this.gbShow.Font = new System.Drawing.Font("Candara", 11.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.gbShow.Location = new System.Drawing.Point(296, 31);
            this.gbShow.Margin = new System.Windows.Forms.Padding(2);
            this.gbShow.Name = "gbShow";
            this.gbShow.Padding = new System.Windows.Forms.Padding(2);
            this.gbShow.Size = new System.Drawing.Size(724, 490);
            this.gbShow.TabIndex = 2;
            this.gbShow.TabStop = false;
            this.gbShow.Text = "Просмотр";
            // 
            // dgvShow
            // 
            this.dgvShow.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvShow.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvShow.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvShow.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvShow.ContextMenuStrip = this.contextMenuStripDGV;
            this.dgvShow.Location = new System.Drawing.Point(21, 26);
            this.dgvShow.Margin = new System.Windows.Forms.Padding(2);
            this.dgvShow.Name = "dgvShow";
            this.dgvShow.RowTemplate.Height = 33;
            this.dgvShow.Size = new System.Drawing.Size(686, 446);
            this.dgvShow.TabIndex = 0;
            this.dgvShow.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.dgvShow_CellBeginEdit);
            this.dgvShow.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvShow_CellEndEdit);
            // 
            // contextMenuStripDGV
            // 
            this.contextMenuStripDGV.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.удалитьToolStripMenuItem,
            this.пометитьКакЗавершенноеToolStripMenuItem});
            this.contextMenuStripDGV.Name = "contextMenuStripDGV";
            this.contextMenuStripDGV.Size = new System.Drawing.Size(227, 48);
            // 
            // удалитьToolStripMenuItem
            // 
            this.удалитьToolStripMenuItem.Name = "удалитьToolStripMenuItem";
            this.удалитьToolStripMenuItem.Size = new System.Drawing.Size(226, 22);
            this.удалитьToolStripMenuItem.Text = "Удалить выбранную строку";
            this.удалитьToolStripMenuItem.Click += new System.EventHandler(this.удалитьToolStripMenuItem_Click);
            // 
            // пометитьКакЗавершенноеToolStripMenuItem
            // 
            this.пометитьКакЗавершенноеToolStripMenuItem.Name = "пометитьКакЗавершенноеToolStripMenuItem";
            this.пометитьКакЗавершенноеToolStripMenuItem.Size = new System.Drawing.Size(226, 22);
            this.пометитьКакЗавершенноеToolStripMenuItem.Text = "Пометить как завершенная";
            this.пометитьКакЗавершенноеToolStripMenuItem.Click += new System.EventHandler(this.пометитьКакЗавершенноеToolStripMenuItem_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Candara", 11.25F);
            this.label5.Location = new System.Drawing.Point(15, 436);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(106, 18);
            this.label5.TabIndex = 14;
            this.label5.Text = "Пользователь :";
            // 
            // lblUser
            // 
            this.lblUser.AutoSize = true;
            this.lblUser.Font = new System.Drawing.Font("Candara", 11.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblUser.Location = new System.Drawing.Point(15, 460);
            this.lblUser.Name = "lblUser";
            this.lblUser.Size = new System.Drawing.Size(24, 18);
            this.lblUser.TabIndex = 15;
            this.lblUser.Text = "<>";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(1031, 532);
            this.Controls.Add(this.gbShow);
            this.Controls.Add(this.gbEdit);
            this.Controls.Add(this.menuStrip);
            this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "MainForm";
            this.Text = "NIIPP Notifier";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.gbEdit.ResumeLayout(false);
            this.gbEdit.PerformLayout();
            this.gbShow.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvShow)).EndInit();
            this.contextMenuStripDGV.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem файлToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem закрытьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem автозагрузкаToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem установитьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem снятьToolStripMenuItem;
        private System.Windows.Forms.GroupBox gbEdit;
        private System.Windows.Forms.GroupBox gbShow;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox tbNumSL;
        private System.Windows.Forms.ComboBox cbNamePM;
        private System.Windows.Forms.DataGridView dgvShow;
        private System.Windows.Forms.DateTimePicker dtpTimeOfCreationSL;
        private System.Windows.Forms.RichTextBox rtbComment;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.DateTimePicker dtpTimeOfDeliveryEl;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripDGV;
        private System.Windows.Forms.ToolStripMenuItem удалитьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem видToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem показатьЦветаToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem настройкаПоказаСтолбцовToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem пометитьКакЗавершенноеToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem неПоказыватьЗавершенныеToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem неПоказыватьАктивныеToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem действиеToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem синхронизацияToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem просмотрToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem статистикаToolStripMenuItem;
        private System.Windows.Forms.Label lblUser;
        private System.Windows.Forms.Label label5;
    }
}

