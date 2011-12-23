using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SubgraphViewer
{
    partial class CellProperty : Form
    {
        public CellProperty()
        {
            InitializeComponent();
        }

        public CellProperty(ViewerResultGraph vqg)
        {
            InitializeComponent();
            InitializeLayout();
            CellProperTable cpt = new CellProperTable(vqg);
            FillData(cpt);
        }

        private void InitializeLayout()
        {
            this.ClientSize = new Size(ViewerConfig.CellPropertyWidth, ViewerConfig.CellPropertyHeight);
            dataGridView1.Location = new Point(0, 0);
            dataGridView1.Width = ViewerConfig.CellPropertyWidth;
            dataGridView1.Height = ViewerConfig.CellPropertyHeight;
            dataGridView1.BackgroundColor = this.BackColor;
            dataGridView1.ReadOnly = true;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void FillData(CellProperTable cpt)
        {
            if (cpt.KeyValueTable.Count == 0)
            {
                return;
            }
            DataTable dt = new DataTable();
            List<string> keyList = cpt.KeyValueTable.Keys.ToList();
            
            for(int i = 0; i < keyList.Count; ++i)
            {
                string key = keyList[i];
                dt.Columns.Add(key);
            }
            for (int i = 0; i < cpt.Row; ++i)
            {
                DataRow dr = dt.NewRow();
                for (int j = 0; j < cpt.Col; ++j)
                {
                    dr[keyList[j]] = cpt.KeyValueTable[keyList[j]][i];
                }
                dt.Rows.Add(dr);
            }
            dataGridView1.DataSource = dt;
        }
    }
}
