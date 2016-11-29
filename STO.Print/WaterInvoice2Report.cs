using DevExpress.XtraReports.UI;
using System.Collections.Generic;
using System.Data;

namespace ZTO.Print
{
    public partial class WaterInvoice2Report : DevExpress.XtraReports.UI.XtraReport
    {
        public WaterInvoice2Report()
        {
            InitializeComponent();
            SetData();
        }
        private void SetData()
        {
            //DataSet ds = new DataSet();
            //using (WapQueryServiceClient client = new WapQueryServiceClient())
            //{
            //    QueryParameter[] @params = { 
            //            new QueryParameter() {
            //            ParameterName = "@FEEIDS",
            //            DbType = DbType.String,
            //            Value =string.IsNullOrEmpty(paramArr["FEEIDS"])? null:paramArr["FEEIDS"]
            //        },                   
            //        new QueryParameter() {
            //            ParameterName = "@UserId",
            //            DbType = DbType.String,                        
            //            Value = string.IsNullOrEmpty(paramArr["UserId"])? paramArr["UserId"]:null
            //        }
            //    };
            //    ds = client.QueryStoredProcedureWithParams("Print_GuiTaiFP", @params);
            //}

           // DBCore.DBAccess dba = new DBCore.DBAccess(SH3H.CSM.Common.Const.ConnectionStringNameConst.CSM);
           // string sql = string.Empty;
           // Dictionary<string, object> paramArr = new Dictionary<string, object>();
           // sql = "exec Print_GuiTaiFP @UserId,@FEEIDS";
            
           
           // paramArr.Add("@UserId", "123");

           // paramArr.Add("@FEEIDS", "456");

           // DataSet ds1 = new DataSet();
           // ds1 = dba.ExecuteDataSet(sql, paramArr);
           // //因涉及到修改DataSet的内部属性，建议创建副本进行操作。
           // DataSet ds = ds1.Copy();//创建副本

           // //重要！！！给组(GroupHeader)绑定主键字段
           // //本报表是按业务单号分组
           // GroupField gf = new GroupField("FeeId", XRColumnSortOrder.Ascending);
           // //GroupHeader1.GroupFields.Add(gf);
           // GroupHeader1.GroupFields.Add(gf);
           
           // //GroupHeader1.PageBreak = PageBreak.BeforeBand;

           // //ds.Tables[0].TableName = "t1";
           // //ds.Tables[0].TableName = "t1";

           // //给数据集建立主外键关系
           // DataColumn parentColumn = ds.Tables["Table"].Columns["FeeId"];
           // DataColumn childColumn = ds.Tables["Table1"].Columns["FeeId"];
           // DataRelation R1 = new DataRelation("R1", parentColumn, childColumn);
           // ds.Relations.Add(R1);

           // //绑定主表的数据源
           // this.DataMember = "Table";
           // this.DataSource = ds;

          

           // //绑定明细表的数据源
           // this.DetailReport.DataMember = "R1";
           // this.DetailReport.DataSource = ds;
           // this.DetailReport.PageBreak = PageBreak.AfterBand;

           // //自动绑定明细表XRLabel的数据源
           // XRTableCellCollection cc = ((XRTable)(this.FindControl("tableDT", true))).Rows[0].Cells;
           //this.BindingFields(ds, cc, "R1.");

           // //this.columnYongShuiLeiBie.DataBindings.Add("Text", ds, "R1.YongShuiLeiBie");
           //this.lblShuiBiaoBH.DataBindings.Add("Text", ds, "Table.ShuiBiaoBH");
           //this.lblAddress.DataBindings.Add("Text", ds, "Table.DiZhi");
           //this.lblYongShuiNianYue.DataBindings.Add("Text", ds, "Table.YongShuiNianYue");
        }

         private void BindingFields(DataSet ds, XRTableCellCollection cc,string relationName)
        {
            for (int i = 0; i < ds.Tables[1].Columns.Count-1; i++)
           {
                cc[i].DataBindings.Add("Text", ds, relationName + ds.Tables[1].Columns[i+1].Caption);
            }
        }

    }
}
