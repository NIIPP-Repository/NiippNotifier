using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

using NIIPP.DatabaseClient.Library;
using NIIPP.DatabaseClient.DataStorage;

namespace NiippNotifier
{
    public partial class FormStatistics : Form
    {
        private readonly string[] _nameOfColumns =
        {
            "Год",
            "Количество пластин",
            "Количество завершенных",
            "Количество активных"
        };

        public FormStatistics()
        {
            InitializeComponent();
        }

        private void InitDGV()
        {
            dgvShowStatistic.ColumnCount = _nameOfColumns.Length;
            for (int i = 0; i < _nameOfColumns.Length; i++)
                dgvShowStatistic.Columns[i].Name = _nameOfColumns[i];

            dgvShowStatistic.ReadOnly = true;
            dgvShowStatistic.Font = new Font("Microsoft Sans Serif", 9);
            dgvShowStatistic.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 9, FontStyle.Bold);
            dgvShowStatistic.BackgroundColor = SystemColors.Control;
            dgvShowStatistic.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvShowStatistic.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvShowStatistic.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgvShowStatistic.AutoResizeColumns();
        }

        private void RetrieveStatisticInfo()
        {
            SqlTable tbInterControl = new SqlTable(TbInterControl.Name);
            SqlSelect select = tbInterControl.GetSelect();
            select.RetrieveData();
            DataTable dt = select.DataTable;

            Dictionary<int, int> yearsStat = new Dictionary<int, int>();
            Dictionary<int, int> yearsStatActive = new Dictionary<int, int>();
            Dictionary<int, int> yearsStatFinished = new Dictionary<int, int>();

            foreach (DataRow row in dt.Rows)
            {
                int pos = dt.Columns.IndexOf(TbInterControl.SheetDate);
                DateTime currTime;
                if ( row.ItemArray[pos].ToString() != "")
                    currTime = (DateTime) row.ItemArray[pos];
                else
                    continue;
                int year = currTime.Year;

                if (!yearsStat.Keys.Contains(year))
                {
                    yearsStat.Add(year, 0);
                    yearsStatActive.Add(year, 0);
                    yearsStatFinished.Add(year, 0);
                }
                yearsStat[year]++;

                int posStatus = dt.Columns.IndexOf(TbInterControl.IsCompleted);
                string status = (string) row.ItemArray[posStatus];
                if (status == "YES")
                    yearsStatFinished[year]++;
                else
                    yearsStatActive[year]++;
            }

            dgvShowStatistic.RowCount = yearsStat.Count + 2;

            int n = 0;
            foreach (var kvp in yearsStat)
            {
                dgvShowStatistic.Rows[n].Cells[0].Value = kvp.Key.ToString();
                dgvShowStatistic.Rows[n].Cells[1].Value = kvp.Value.ToString();
                dgvShowStatistic.Rows[n].Cells[2].Value = yearsStatFinished[kvp.Key].ToString();
                dgvShowStatistic.Rows[n].Cells[3].Value = yearsStatActive[kvp.Key].ToString();
                n++;
            }

            int totalCountOfFinished = 0,
                totalCountOfActive = 0;
            foreach (DataRow row in dt.Rows)
            {
                int posStatus = dt.Columns.IndexOf(TbInterControl.IsCompleted);
                string status = (string)row.ItemArray[posStatus];
                if (status == "YES")
                    totalCountOfFinished++;
                if (status == "NO")
                    totalCountOfActive++;
            }
            dgvShowStatistic.Rows[n].Cells[0].Value = "Суммарно";
            dgvShowStatistic.Rows[n].Cells[1].Value = dt.Rows.Count;
            dgvShowStatistic.Rows[n].Cells[2].Value = totalCountOfFinished.ToString();
            dgvShowStatistic.Rows[n].Cells[3].Value = totalCountOfActive.ToString();

            dgvShowStatistic.AutoResizeColumns();

        }

        private void FormStatistics_Load(object sender, EventArgs e)
        {
            InitDGV();

            RetrieveStatisticInfo();
        }
    }
}
