using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;
using Microsoft.Win32;

using NIIPP.DatabaseClient.Library;

namespace NiippNotifier
{
    public partial class MainForm : Form
    {
        DataTable _dtTemplate = null;

        readonly String[] _columnsName = {
            "Последнее изменение",
            "Изменил(а)",
            "Номер СЛ", 
            "Дата создания СЛ", 
            "Схема ФШ", 
            "Номер ячейки",
            "Дата выдачи 1", 
            "Межоперационные измерения", 
            "Дата выдачи 2",
            "Измерение 1", 
            "Измерение 2", 
            "% выхода",
            "Кол-во годных",
            "Комментарии",
            "Завершено"
        };
        Settings _currSettings;
        String _currentAuthor;

        

        public MainForm()
        {
            InitializeComponent();
        }

        //private void CheckSingleInstance()
        //{
        //    Process current = Process.GetCurrentProcess();
        //    Process[] processes = Process.GetProcessesByName(current.ProcessName);
        //    if (processes.Count() >= 2)
        //    {
        //        MessageBox.Show("Программа уже запущена", "Ошибка", MessageBoxButtons.OK);
        //        Close();
        //    }
        //} 

        [Serializable]
        public class Settings
        {
            private String[] _columnsName;
            private readonly List<String> _setOfVisColumns = new List<String>();
            public bool ShowColor,
                        DontShowCompleted,
                        DontShowActive;

            public void SetColumnsName(String[] columnsName)
            {
                _columnsName = (String[])columnsName.Clone();
            }

            public void SetDefaultSettings()
            {
                foreach (String name in _columnsName)
                {
                    _setOfVisColumns.Add(name);
                }
                ShowColor = true;
                DontShowCompleted = true;
                DontShowActive = false;
            }

            public bool IsVisibleColumn(int index)
            {
                return _setOfVisColumns.Contains(_columnsName[index]);
            }

            public void AddNewVisible(String columnName)
            {
                _setOfVisColumns.Add(columnName);
            }

            public void AddNewInvisible(String columnName)
            {
                _setOfVisColumns.Remove(columnName);
            }
        }

        int FindIndexOfColumn(String name)
        {
            int res = -1;
            for (int i = 0; i < _columnsName.Length; i++)
            {
                if (_columnsName[i] == name)
                {
                    res = i;
                    break;
                }
            }

            return res;
        }

        void SetNewValue(int countOfRow, String fieldName, String newValue)
        {
            dgvShow.Rows[countOfRow].Cells[FindIndexOfColumn(fieldName)].Value = newValue;
        }

        String GetValue(int countOfRow, String fieldName)
        {
            String res;
            try
            {
                res = dgvShow.Rows[countOfRow].Cells[FindIndexOfColumn(fieldName)].Value.ToString();
            }
            catch
            {
                res = "";
            }
            return res;
        }

        String GetValueInSqlData(int countOfRow, String fieldName)
        {
            try
            {
                String str = dgvShow.Rows[countOfRow].Cells[FindIndexOfColumn(fieldName)].Value.ToString();
                var dt = DateTime.Parse(str);
                return "'" + dt.ToString("yyyy-MM-dd") + "'";
            }
            catch
            {
                return "NULL";
            }
        }

        String GetValueInSqlInt(int countOfRow, String fieldName)
        {
            try
            {
                String str = dgvShow.Rows[countOfRow].Cells[FindIndexOfColumn(fieldName)].Value.ToString();
                Int32.Parse(str);
                return "'" + str + "'";
            }
            catch
            {
                return "NULL";
            }
        }

        String GetValueInSqlDouble(int countOfRow, String fieldName)
        {
            try
            {
                String str = dgvShow.Rows[countOfRow].Cells[FindIndexOfColumn(fieldName)].Value.ToString();
                str = str.Replace(',', '.');
                Double.Parse(str, CultureInfo.InvariantCulture);
                return "'" + str + "'";
            }
            catch
            {
                return "NULL";
            }
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                Hide();
            }
        }

        private void notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Show();
            WindowState = FormWindowState.Normal;
        }

        bool IsStartupItem()
        {
            // The path to the key where Windows looks for startup applications
            RegistryKey rkApp = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

            if (rkApp.GetValue("NIIPP Notifier") == null)
                // The value doesn't exist, the application is not set to run at startup
                return false;
            else
                // The value exists, the application is set to run at startup
                return true;
        }

        void SetAutorun()
        {
            // The path to the key where Windows looks for startup applications
            RegistryKey rkApp = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

            if (!IsStartupItem())
                // Add the value in the registry so that the application runs at startup
                rkApp.SetValue("NIIPP Notifier", Application.ExecutablePath.ToString());
        }

        void RemoveAutorun()
        {
            // The path to the key where Windows looks for startup applications
            RegistryKey rkApp = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

            if (IsStartupItem())
                // Remove the value from the registry so that the application doesn't start
                rkApp.DeleteValue("NIIPP Notifier", false);
        }

        private void закрытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void установитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetAutorun();
        }

        private void снятьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RemoveAutorun();
        }

        void SaveSettingsInBinary()
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (var fStream = new FileStream(_currentAuthor + " settings.dat", FileMode.Create, FileAccess.Write, FileShare.None))
            {
                formatter.Serialize(fStream, _currSettings);
            }
        }

        void RecoverySettings()
        {
            BinaryFormatter formatter = new BinaryFormatter();
            try
            {
                using (var fStream = File.OpenRead(_currentAuthor + " settings.dat"))
                {
                    _currSettings = (Settings)formatter.Deserialize(fStream);
                }
            }
            catch
            {
                _currSettings = new Settings();
                _currSettings.SetColumnsName(_columnsName);
                _currSettings.SetDefaultSettings();
            }
        }

        void InitializeControls()
        {
            // узнаем автора
            _currentAuthor = ClientLibrary.GetAuthorOfComputer();
            lblUser.Text = _currentAuthor;

            // восстановление настроек
            RecoverySettings();

            dgvShow.ColumnCount = _columnsName.Length;
            for (int i = 0; i < _columnsName.Length; i++)
            {
                dgvShow.Columns[i].Name = _columnsName[i];
            }
            dgvShow.Font = new Font("Microsoft Sans Serif", 9);
            dgvShow.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 9, FontStyle.Bold);
            dgvShow.BackgroundColor = SystemColors.Control;
            dgvShow.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.ColumnHeader;
            dgvShow.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvShow.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvShow.AutoResizeColumns();

            int k = 0;
            foreach (String name in _columnsName)
            {
                настройкаПоказаСтолбцовToolStripMenuItem.DropDownItems.Add(name);
                настройкаПоказаСтолбцовToolStripMenuItem.DropDownItems[k].Click += SetColumnsVisibleItem_Click;
                k++;
            }

            int num = 0;
            foreach (ToolStripMenuItem item in настройкаПоказаСтолбцовToolStripMenuItem.DropDownItems)
            {
                item.Checked = _currSettings.IsVisibleColumn(num);
                num++;
            }

            показатьЦветаToolStripMenuItem.Checked = _currSettings.ShowColor;
            неПоказыватьЗавершенныеToolStripMenuItem.Checked = _currSettings.DontShowCompleted;
            неПоказыватьАктивныеToolStripMenuItem.Checked = _currSettings.DontShowActive;

            dgvShow.SortCompare += dgvShow_SortCompare;
        }

        void SetColumnsVisibleItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem tsmi = (ToolStripMenuItem)sender;
            tsmi.Checked = !tsmi.Checked;

            int ind = FindIndexOfColumn(tsmi.Text);
            if (tsmi.Checked)
                _currSettings.AddNewVisible(tsmi.Text);
            else
                _currSettings.AddNewInvisible(tsmi.Text);
            SaveSettingsInBinary();

            RefreshTable();

            видToolStripMenuItem.ShowDropDown();
            настройкаПоказаСтолбцовToolStripMenuItem.ShowDropDown();
        }

        void MakeReadOnlyColumnsAndRows()
        {
            dgvShow.Columns[FindIndexOfColumn("Последнее изменение")].ReadOnly = true;
            dgvShow.Columns[FindIndexOfColumn("Изменил(а)")].ReadOnly = true;
            dgvShow.Columns[FindIndexOfColumn("Номер СЛ")].ReadOnly = true;

            DataTable settingsDt = SqlQuery.Execute("SELECT * FROM `" + "tb_settings" + "`");
            if (settingsDt.Rows[0].ItemArray[1].ToString() != "YES")
            {
                dgvShow.Columns[FindIndexOfColumn("Дата создания СЛ")].ReadOnly = true;
                dgvShow.Columns[FindIndexOfColumn("Схема ФШ")].ReadOnly = true;
            }

            dgvShow.Rows[dgvShow.Rows.Count - 1].ReadOnly = true;
        }

        void RefreshTable()
        // обновляем локальную таблицу облачной таблицей
        // метод требует строгого соответствия структуры облачной и локальной таблицы
        {
            // считывание текущей активной ячейки
            Point curCell = new Point();
            if (dgvShow.CurrentCell != null)
            {
                curCell.X = dgvShow.CurrentCell.ColumnIndex;
                curCell.Y = dgvShow.CurrentCell.RowIndex;
            }
            else
            {
                curCell.X = -1;
                curCell.Y = -1;
            }

            DataTable dt = SqlQuery.Execute("SELECT * FROM `tb_inter_control`");
            dgvShow.Rows.Clear();
            foreach (DataRow dr in dt.Rows)
            {
                if (dr[FindIndexOfColumn("Номер СЛ")].ToString().IndexOf("_DELETED") != -1)
                        continue;

                dgvShow.RowCount++;
                int currCol = 0;
                foreach (Object str in dr.ItemArray)
                {
                    String next = str.ToString();

                    // урезаем строку со временем
                    if (next.IndexOf(" 0:00:00") != -1)
                        next = next.Substring(0, 10);

                    dgvShow.Rows[dgvShow.RowCount - 2].Cells[currCol].Value = next;
                    currCol++;
                }
            }

            // в зависимости от ситуации ставим некоторые столбцы недоступными для редактирования
            MakeReadOnlyColumnsAndRows();

            // красим ячейки
            DrawCells();

            // настройка показа столбцов
            SetColumnsVisible();

            // настройка показа строк
            SetRowsVisible();

            try
            {
                // возвращаем текущую активную ячейку
                if (curCell.X != -1 && curCell.Y != -1)
                {
                    dgvShow.CurrentCell = dgvShow[curCell.X, curCell.Y];
                }
                else
                {
                    dgvShow.CurrentCell = null;
                }
            }
            catch (Exception ex)
            {
            }

            dgvShow.AutoResizeColumns();
        }

        void DrawCells(int numberOfRow)
        {
            if (!_currSettings.ShowColor)
                return;

            // есть дата выдачи
            String timeOfIssue = GetValue(numberOfRow, "Дата выдачи 1");
            if (timeOfIssue != "")
            {
                String timeOfMeasStart = GetValue(numberOfRow, "Межоперационные измерения");
                // не определена дата начала измерений
                if (timeOfMeasStart == "")
                {
                    dgvShow.Rows[numberOfRow].Cells[FindIndexOfColumn("Последнее изменение")].Style.BackColor = ChooseColorFromDateDiff(timeOfIssue);
                }
                if (timeOfMeasStart != "")
                {
                    String timeOfIssueMKI = GetValue(numberOfRow, "Дата выдачи 2");
                    if (timeOfIssueMKI != "")
                    {
                        String timeOfFirstMeas = GetValue(numberOfRow, "Измерение 1");
                        if (timeOfFirstMeas == "")
                        {
                            dgvShow.Rows[numberOfRow].Cells[FindIndexOfColumn("Последнее изменение")].Style.BackColor = ChooseColorFromDateDiff(timeOfIssueMKI);
                        }
                    }
                }
            }

            if (GetValue(numberOfRow, "Завершено") == "YES")
            {
                dgvShow.Rows[numberOfRow].Cells[FindIndexOfColumn("Последнее изменение")].Style.BackColor = Color.LightGreen;
            }
        }

        void DrawCells()
        {
            for (int i = 0; i < dgvShow.Rows.Count - 1; i++)
            {
                DrawCells(i);
            }
        }

        Color ChooseColorFromDateDiff(String str)
        {
            Color res = Color.White;

            DateTime past;
            try
            {
                past = DateTime.Parse(str);
            }
            catch
            {
                return res;
            }

            TimeSpan diff = DateTime.Now - past;
            if (diff.Days > 7 && diff.Days <= 14)
                res = Color.FromArgb(255, 255, 153);
            if (diff.Days > 14 && diff.Days <= 21)
                res = Color.FromArgb(255, 204, 0);
            if (diff.Days > 21)
                res = Color.FromArgb(255, 102, 0);
            return res;
        }

        String GetMacAddress()
        {
            String res = "";
            IPGlobalProperties computerProperties = IPGlobalProperties.GetIPGlobalProperties();
            NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();

            foreach (NetworkInterface adapter in nics)
            {
                if (adapter.OperationalStatus == OperationalStatus.Up)
                {
                    PhysicalAddress address = adapter.GetPhysicalAddress();
                    byte[] bytes = address.GetAddressBytes();
                    for (int i = 0; i < bytes.Length; i++)
                    {
                        res += bytes[i].ToString("X2");
                        if (i != bytes.Length - 1)
                            res += "-";
                    }
                    break;
                }
            }

            if (res != "")
                return res;
            else
                return "undefined";
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            InitializeControls();

            RefreshTable();

            _dtTemplate = SqlQuery.Execute("SELECT * FROM `" + "tb_version_control" + "`");
            Timer checkOut = new Timer();
            checkOut.Tick += checkOut_Tick;
            checkOut.Interval = 3000;
            checkOut.Start();
        }

        void SetColumnsVisible()
        {
            for (int i = 0; i < dgvShow.Columns.Count; i++)
            {
                dgvShow.Columns[i].Visible = _currSettings.IsVisibleColumn(i);
            }
        }

        void SetRowsVisible()
        {
            for (int i = 0; i < dgvShow.Rows.Count - 1; i++)
            {
                SetRowsVisible(i);
            }
        }

        void SetRowsVisible(int rowNumber)
        {
            if (_currSettings.DontShowCompleted && GetValue(rowNumber, "Завершено") == "YES")
                dgvShow.Rows[rowNumber].Visible = false;
            if (_currSettings.DontShowActive && GetValue(rowNumber, "Завершено") == "NO")
                dgvShow.Rows[rowNumber].Visible = false;
        }

        bool EqualDT(DataTable dt1, DataTable dt2)
        {
            String s1 = dt1.Rows[0].ItemArray[1].ToString(),
                   s2 = dt2.Rows[0].ItemArray[1].ToString();
            String author = dt2.Rows[0].ItemArray[2].ToString();

            if (s1 == s2 || author == _currentAuthor)
            {
                return true;
            }
            else
                return false;
        }

        void checkOut_Tick(object sender, EventArgs e)
        {
            DataTable dtCurrent = SqlQuery.Execute("SELECT * FROM `" + "tb_version_control" + "`");
            if (!EqualDT(_dtTemplate, dtCurrent))
            {
                RefreshTable();

                _dtTemplate = dtCurrent;
                notifyIcon.BalloonTipTitle = "Произошли изменения!";
                notifyIcon.BalloonTipText = "Откройте программу, что бы выяснить что случилось ;)";
                notifyIcon.ShowBalloonTip(10000);
            }
        }

        String GetDateInMySQLFormat(DateTimePicker dtp)
        {
            String res = "'" + dtp.Value.ToString("yyyy-MM-dd") + "'";
            if (res == "'2014-01-01'")
                res = "NULL";
            return res;
        }
        
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (tbNumSL.Text != "" && cbNamePM.Text != "" && dtpTimeOfCreationSL.Value.ToString("yyyy-MM-dd") != "2014-01-01"
                && dtpTimeOfDeliveryEl.Value.ToString("yyyy-MM-dd") != "2014-01-01")
            {
                SqlQuery.Execute("INSERT INTO `tb_inter_control`(`number_of_sheet`) VALUES('" + tbNumSL.Text + "')");

                SqlQuery.Execute("UPDATE `" + "tb_inter_control" + "` SET " +
                   "`last_update` = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', " +
                   "`author` = '" + _currentAuthor + "', " +
                   "`name_of_scheme` = '" + cbNamePM.Text + "', " +
                   "`sheet_date` = " + GetDateInMySQLFormat(dtpTimeOfCreationSL) + ", " +
                   "`el_date_of_issue` = " + GetDateInMySQLFormat(dtpTimeOfDeliveryEl) + ", " +
                   "`note` = '" + rtbComment.Text + "' " +
                   "WHERE `number_of_sheet` = '" + tbNumSL.Text + "'");

              RefreshTable();
            }
            else
            {
                MessageBox.Show("Пожалуйста, заполните все поля!", "Предупреждение");
            } 
        }

        bool IsValidData(String value, int columnNumber)
        {
            bool res = true;
            String nameOfColumn = _columnsName[columnNumber];

            if (nameOfColumn == "Дата создания СЛ" || nameOfColumn == "Дата выдачи 1" || nameOfColumn == "Межоперационные измерения" ||
                nameOfColumn == "Дата выдачи 2" || nameOfColumn == "Измерение 1" || nameOfColumn == "Измерение 2")
            {
                try
                {
                    DateTime dt = DateTime.Parse(value);
                }
                catch
                {
                    res = false;
                }
            }

            if (nameOfColumn == "% выхода")
            {
                try
                {
                    Double d = Double.Parse(value.Replace(',', '.'), CultureInfo.InvariantCulture);
                }
                catch
                {
                    res = false;
                }
            }

            if (nameOfColumn == "Кол-во годных" || nameOfColumn == "Номер ячейки")
            {
                try
                {
                    int d = Int32.Parse(value);
                }
                catch
                {
                    res = false;
                }
            }

            if (nameOfColumn == "Завершено")
            {
                if (value != "YES" && value != "NO")
                    res = false;
            }

            return res;
        }

        private void dgvShow_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            int rowNumber = e.RowIndex,
                columnNumber = e.ColumnIndex;
            String id = GetId(rowNumber);
            String newValue;

            if (dgvShow.Rows[rowNumber].Cells[columnNumber].Value != null)
            {
                newValue = dgvShow.Rows[rowNumber].Cells[columnNumber].Value.ToString();
                if (!IsValidData(newValue, columnNumber) && newValue != "")
                {
                    MessageBox.Show("Введенные данные не соответствуют формату! \n" +
                        "Данные не сохранены в базе данных!");
                    return;
                }
            }
            else
                newValue = "";

            if (newValue != currValue)
            {
                SqlQuery.Execute("UPDATE `" + "tb_inter_control" + "` SET " +
                   "`last_update` = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', " +
                   "`author` = '" + _currentAuthor + "', " +
                   "`sheet_date` = " + GetValueInSqlData(rowNumber, "Дата создания СЛ") + ", " +
                   "`name_of_scheme` = '" + GetValue(rowNumber, "Схема ФШ") + "', " +
                   "`cell` = " + GetValueInSqlInt(rowNumber, "Номер ячейки") + ", " +
                   "`el_date_of_issue` = " + GetValueInSqlData(rowNumber, "Дата выдачи 1") + ", " +
                   "`start_of_meas` = " + GetValueInSqlData(rowNumber, "Межоперационные измерения") + ", " +
                   "`dev_date_of_issue` = " + GetValueInSqlData(rowNumber, "Дата выдачи 2") + ", " +
                   "`meas1` = " + GetValueInSqlData(rowNumber, "Измерение 1") + ", " +
                   "`meas2` = " + GetValueInSqlData(rowNumber, "Измерение 2") + ", " +
                   "`note` = '" + GetValue(rowNumber, "Комментарии") + "', " +
                   "`procent_of_good` = " + GetValueInSqlDouble(rowNumber, "% выхода") + ", " +
                   "`count_of_good` = " + GetValueInSqlInt(rowNumber, "Кол-во годных") + ", " +
                   "`completed` = '" + GetValue(rowNumber, "Завершено") + "' " +
                   "WHERE `number_of_sheet` = '" + id + "'");

                SetNewValue(rowNumber, "Изменил(а)", _currentAuthor);
                SetNewValue(rowNumber, "Последнее изменение", DateTime.Now.ToString());
                SetRowsVisible(rowNumber);
                DrawCells(rowNumber);

                dgvShow.AutoResizeColumn(FindIndexOfColumn("Изменил(а)"));
                dgvShow.AutoResizeColumn(columnNumber);
            }
        }

        String GetId(int x)
        {
            return GetValue(x, "Номер СЛ");
        }

        private void удалитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgvShow.CurrentRow != null)
            {
                String id = GetId(dgvShow.CurrentRow.Index);
                DialogResult dr = MessageBox.Show("Вы действительно хотите удалить запись с сопроводным листом '" + id + "' ?", "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    SqlQuery.Execute("UPDATE `" + "tb_inter_control" + "` SET " +
                        "`last_update` = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', " +
                        "`number_of_sheet` = '" + id + "_DELETED_" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' " +
                        "WHERE `number_of_sheet` = '" + id + "'");
                }
                RefreshTable();
            }
        }

        int currRow = 0,
            currCol = 0;
        String currValue = "";

        private void показатьЦветаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ((ToolStripMenuItem)sender).Checked = !_currSettings.ShowColor;
            _currSettings.ShowColor = !_currSettings.ShowColor;
            SaveSettingsInBinary();

            RefreshTable();
        }

        private bool IsDateColumn(int columnIndex)
        {
            bool res = false;
            switch (_columnsName[columnIndex])
            {
                case "Дата создания СЛ":
                    res = true;
                    break;
                case "Дата выдачи 1":
                    res = true;
                    break;
                case "Дата выдачи 2":
                    res = true;
                    break;
                case "Измерение 1":
                    res = true;
                    break;
                case "Измерение 2":
                    res = true;
                    break;
                case "Межоперационные измерения":
                    res = true;
                    break;
            }

            return res;
        }

        private void dgvShow_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            currRow = e.RowIndex;
            currCol = e.ColumnIndex;
            if (currRow >= 0 && currRow < dgvShow.Rows.Count - 1 && currCol >= 0 && currCol < dgvShow.Columns.Count)
            {
                if (IsDateColumn(currCol) && string.IsNullOrEmpty( (string) dgvShow.Rows[currRow].Cells[currCol].Value))
                    dgvShow.Rows[currRow].Cells[currCol].Value = DateTime.Now.ToString("dd.MM.yyyy");
                currValue = dgvShow.Rows[currRow].Cells[currCol].Value.ToString();
            }
        }

        private void пометитьКакЗавершенноеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgvShow.CurrentRow != null)
            {
                String id = GetId(dgvShow.CurrentRow.Index);
                SqlQuery.Execute("UPDATE `" + "tb_inter_control" + "` SET " +
                        "`last_update` = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', " +
                        "`completed` = 'YES' " +
                        "WHERE `number_of_sheet` = '" + id + "'");
            }
        }

        private void неПоказыватьЗавершенныеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            неПоказыватьЗавершенныеToolStripMenuItem.Checked = !неПоказыватьЗавершенныеToolStripMenuItem.Checked;
            _currSettings.DontShowCompleted = неПоказыватьЗавершенныеToolStripMenuItem.Checked;
            SaveSettingsInBinary();
           
            RefreshTable();
        }

        int GetCostOfSlName(String str)
        {
            int res = 0, year = 0;

            try
            {
                int temp = str.IndexOf('/');
                if (temp != -1)
                {
                    year = Int32.Parse(str.Substring(temp + 1));
                    res = year * 10000 + Int32.Parse(str.Substring(0, temp));
                }
                else
                    res = Int32.Parse(str);
            }
            catch
            {
                res = 0;
            }

            return res;
        }

        int CompareDateTime(String str1, String str2)
        {
            int res = 0;
            try
            {
                DateTime dt1 = DateTime.Parse(str1);
                DateTime dt2 = DateTime.Parse(str2);
                res = -DateTime.Compare(dt1, dt2);
            }
            catch
            {
                res = 0;
            }
            return res;
        }

        int CompareNumbers(String str1, String str2)
        {
            int res = 0;
            try
            {
                int a = Int32.Parse(str1);
                int b = Int32.Parse(str2);
                if (a < b)
                    res = -1;
                if (a > b)
                    res = 1;
            }
            catch
            {
                res = 0;
            }
            return res;
        }

        int CompareSL(String str1, String str2)
        {
            int res = 0;
            int a = GetCostOfSlName(str1);
            int b = GetCostOfSlName(str2);
            if (a == b)
                res = 0;
            else
            {
                res = (a > b) ? -1 : 1;
            }

            return res;
        }

        private void dgvShow_SortCompare(object sender, DataGridViewSortCompareEventArgs e)
        {
            String str1 = e.CellValue1.ToString(),
                   str2 = e.CellValue2.ToString();
            if (str1 == "" || str2 == "")
            {
                if (str1 == "" && str2 == "")
                    e.SortResult = 0;
                else
                {
                    e.SortResult = (str1 == "") ? 1 : -1;
                }
            }
            else
            {
                switch (e.Column.Name)
                {
                    case "Номер СЛ":
                        e.SortResult = CompareSL(str1, str2);
                        break;
                    case "Последнее изменение":
                        e.SortResult = CompareDateTime(str1, str2);
                        break;
                    case "Дата создания СЛ":
                        e.SortResult = CompareDateTime(str1, str2);
                        break;
                    case "Дата выдачи 1":
                        e.SortResult = CompareDateTime(str1, str2);
                        break;
                    case "Межоперационные измерения":
                        e.SortResult = CompareDateTime(str1, str2);
                        break;
                    case "Дата выдачи 2":
                        e.SortResult = CompareDateTime(str1, str2);
                        break;
                    case "Измерение 1":
                        e.SortResult = CompareDateTime(str1, str2);
                        break;
                    case "Измерение 2":
                        e.SortResult = CompareDateTime(str1, str2);
                        break;
                    case "Номер ячейки":
                        e.SortResult = CompareNumbers(str1, str2);
                        break;
                    default:
                        e.SortResult = String.CompareOrdinal(str1, str2);
                        break;
                }
            }

            e.Handled = true;
        }

        private void неПоказыватьАктивныеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            неПоказыватьАктивныеToolStripMenuItem.Checked = !неПоказыватьАктивныеToolStripMenuItem.Checked;
            _currSettings.DontShowActive = неПоказыватьАктивныеToolStripMenuItem.Checked;
            SaveSettingsInBinary();

            RefreshTable();
        }

        private void синхронизацияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RefreshTable();
        }

        private void статистикаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormStatistics formStatistics = new FormStatistics();
            formStatistics.Show();
        }
    }
}
