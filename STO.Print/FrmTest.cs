using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ZTO.Print
{
    public partial class FrmTest : Form
    {
        public FrmTest()
        {
            InitializeComponent();
        }
        DataTable dt = new DataTable();
        private void FrmTest_Load(object sender, EventArgs e)
        {
            dt.Columns.Add("ID");
            dt.Columns.Add("Name");
            for (int i = 0; i < 100; i++)
            {
                DataRow row = dt.NewRow();
                row["ID"] = i.ToString();
                row["Name"] = (new Random()).NextDouble().ToString();
                dt.Rows.Add(row);
                System.Threading.Thread.Sleep(1);
            }

            this.repositoryItemSearchLookUpEdit1.DataSource = dt;
            this.gridControl1.DataSource = dt.Clone();
        }

        private void gridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName == "ID")
            {
                string id = e.Value.ToString();
                DataRow[] dr = dt.Select(string.Format("ID = '{0}'", id));
                if (dr != null && dr.Length > 0)
                {
                    DataRow row = dr[0];
                    string name = row["Name"].ToString();
                    gridView1.SetRowCellValue(e.RowHandle, "Name", name);
                }
            }
        }
    }
}
