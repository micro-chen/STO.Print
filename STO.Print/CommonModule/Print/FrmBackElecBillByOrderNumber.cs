//-------------------------------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2015 , STO , Ltd .
//-------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using STO.Print.AddBillForm;


namespace STO.Print
{
    using DevExpress.XtraEditors;
    using DotNet.Utilities;
    using STO.Print.Manager;
    using STO.Print.Model;
    using STO.Print.Utilities;

    /// <summary>
    /// 回收申通电子面单（根据订单编号来回收）
    ///
    /// 修改纪录
    ///
    ///	    2015-12-17  版本：1.0 YangHengLian 创建主键，注意命名空间的排序。
    /// 
    /// 版本：1.0
    ///
    /// <author>
    ///		<name>YangHengLian</name>
    ///		<date>2015-12-17</date>
    /// </author>
    /// </summary>
    public partial class FrmBackElecBillByOrderNumber : BaseForm
    {
        /// <summary>
        /// 是否更新备份库的订单数据
        /// </summary>
        public bool IsUpdateBackUpData = false;

        private List<ZtoPrintBillEntity> _printBillEntities;

        public FrmBackElecBillByOrderNumber()
        {
            InitializeComponent();
        }

        public FrmBackElecBillByOrderNumber(bool isUpdateBackUpData = false)
        {
            InitializeComponent();
            IsUpdateBackUpData = isUpdateBackUpData;
        }

        public FrmBackElecBillByOrderNumber(List<ZtoPrintBillEntity> printBillEntities, bool isUpdateBackUpData = false)
        {
            InitializeComponent();
            IsUpdateBackUpData = isUpdateBackUpData;
            if (printBillEntities.Any())
            {
                _printBillEntities = printBillEntities;
                var cancelCount = 0;
                foreach (ZtoPrintBillEntity s in printBillEntities)
                {
                    if (!string.IsNullOrEmpty(s.OrderNumber))
                    {
                        txtOrderNumbers.Text = txtOrderNumbers.Text + s.OrderNumber.Replace(" ","").Replace("\t","") + Environment.NewLine;
                        ++cancelCount;
                    }
                }
                lblTotalBillNum.Text = cancelCount.ToString();
            }
            else
            {
                lblTotalBillNum.Text = "0";
            }
        }

        private void FrmBackElecBillByOrderNumber_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 取消订单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBackBill_Click(object sender, EventArgs e)
        {
            ZtoElecUserInfoEntity elecUserInfoEntity = BillPrintHelper.GetElecUserInfoEntity();
            if (elecUserInfoEntity != null)
            {
                List<object> stateList = new List<object>();
                //输入单号字符串集合
                var orderNumberList = DotNet.Utilities.StringUtil.SplitMobile(txtOrderNumbers.Text, false).ToList();
                if (!orderNumberList.Any())
                {
                    XtraMessageBox.Show("没有任何的订单信息，请在左侧填写订单号进行回收");
                    return;
                }
                ZtoPrintCancelManager cancelManager = new ZtoPrintCancelManager(BillPrintHelper.DbHelper);
                foreach (var orderNumber in orderNumberList)
                {
                    string msg = "";
                    var result = ZtoElecBillHelper.BackZtoElecBill(orderNumber, elecUserInfoEntity, ref msg);
                    stateList.Add(new
                    {
                        订单号 = orderNumber,
                        状态 = msg
                    });
                    // 取消成功了可以把订单号和单号都清空一下
                    if (result)
                    {
                        string commandText = string.Format("UPDATE ZTO_PRINT_BILL SET ORDER_NUMBER = '', BILL_CODE = '' WHERE ORDER_NUMBER = '" + orderNumber + "'");
                        if (IsUpdateBackUpData)
                        {
                            // 同时把单号的订单取消掉,备份库的订单号和单号也要取消掉
                            BillPrintHelper.BackupDbHelper.ExecuteNonQuery(commandText);
                        }
                        else
                        {
                            // 同时把单号的订单取消掉，打印数据的取消掉就行了
                            BillPrintHelper.DbHelper.ExecuteNonQuery(commandText);
                        }
                        var temp = _printBillEntities.Find(p => p.OrderNumber.Replace(" ","").Replace("\t","") == orderNumber.Replace(" ",""));
                        if (temp != null)
                        {
                            cancelManager.Add(new ZtoPrintCancelEntity()
                            {
                                OrderNumber = temp.OrderNumber,
                                BillCode= temp.BillCode,
                                SendMan = temp.SendMan,
                                SendPhone=temp.SendPhone,
                                SendAddress= temp.SendAddress,
                                ReceiveMan= temp.ReceiveMan,
                                ReceivePhone = temp.ReceivePhone,
                                ReceiveAddress= temp.ReceiveAddress,
                                Remark = "取消时间点是：" + DateTime.Now.ToString(BaseSystemInfo.DateTimeFormat)
                            });
                        }
                        else
                        {
                            cancelManager.Add(new ZtoPrintCancelEntity()
                            {
                                OrderNumber = orderNumber,
                                Remark="取消时间点是："+DateTime.Now.ToString(BaseSystemInfo.DateTimeFormat)
                            });
                        }
                    }
                }
                if (stateList.Any())
                {
                    gcStatus.DataSource = stateList;
                }
            }
            else
            {
                // 获取系统是否有默认发件人，如果有救修改，如果没有就新增
                ZtoUserManager userManager = new ZtoUserManager(BillPrintHelper.DbHelper);
                var userList = userManager.GetList<ZtoUserEntity>(new KeyValuePair<string, object>(ZtoUserEntity.FieldIsDefault, 1), new KeyValuePair<string, object>(ZtoUserEntity.FieldIssendorreceive, 1));
                if (!userList.Any())
                {
                    XtraMessageBox.Show("系统未绑定默认发件人和商家ID，请进行绑定", AppMessage.MSG0000, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    FrmAddSendMan addSendMan = new FrmAddSendMan();
                    addSendMan.ShowDialog();
                    addSendMan.Dispose();
                }
                else
                {
                    XtraMessageBox.Show("默认发件人未绑定商家ID，请进行绑定", AppMessage.MSG0000, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    FrmAddSendMan frmSendMan = new FrmAddSendMan { Id = userList.First().Id.ToString() };
                    frmSendMan.ShowDialog();
                    frmSendMan.Dispose();
                }
            }
        }

        /// <summary>
        /// 导出回收结果
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExportToExcel_Click(object sender, EventArgs e)
        {
            STO.Print.Utilities.ExportHelper.Export(ExportEnum.Xlsx, gcStatus, gvStatus);
        }

        private void FrmBackElecBillByOrderNumber_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult = DialogResult.OK;
        }
    }
}
