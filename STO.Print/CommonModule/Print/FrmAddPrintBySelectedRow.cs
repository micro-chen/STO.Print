//-------------------------------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2015 , STO , Ltd .
//-------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace STO.Print
{
    using STO.Print.AddBillForm;
    using STO.Print.Manager;
    using STO.Print.Model;
    using STO.Print.Utilities;

    /// <summary>
    /// 复制选中记录新增适合【一票多件】
    /// 
    /// 修改纪录
    /// 
    ///     2016-4-7 版本：1.0 YangHengLian 创建。
    ///		
    /// <author>
    ///		<name>YangHengLian</name>
    ///		<date>2016-4-7</date>
    /// </author> 
    /// </summary>
    public partial class FrmAddPrintBySelectedRow : BaseForm
    {
        /// <summary>
        /// 打印记录集合，从其他窗体传过来
        /// </summary>
        private readonly List<Model.ZtoPrintBillEntity> _list;

        /// <summary>
        /// 是否添加到打印数据里面
        /// </summary>
        private readonly bool IsAddToPrintData = true;

        /// <summary>
        /// 是否新增成功记录
        /// </summary>
        private bool _isAdd = false;

        public FrmAddPrintBySelectedRow()
        {
            InitializeComponent();
        }

        public FrmAddPrintBySelectedRow(List<Model.ZtoPrintBillEntity> _list)
        {
            InitializeComponent();
            this._list = _list;
        }

        public FrmAddPrintBySelectedRow(List<Model.ZtoPrintBillEntity> _list, bool isAddToPrintData)
        {
            InitializeComponent();
            this._list = _list;
            this.IsAddToPrintData = isAddToPrintData;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmAddPrintBySelectedRow_Load(object sender, EventArgs e)
        {
            if (_list != null)
            {
                foreach (ZtoPrintBillEntity ztoPrintBillEntity in _list)
                {
                    string keyValue = ztoPrintBillEntity.ReceiveMan + "-" + ztoPrintBillEntity.ReceivePhone;
                    listPrintList.Items.Add(keyValue);
                }
            }
        }

        /// <summary>
        /// 保存本地
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (_list.Any())
            {
                var printBillEntities = new List<ZtoPrintBillEntity>();
                foreach (ZtoPrintBillEntity ztoPrintBillEntity in _list)
                {
                    ztoPrintBillEntity.OrderNumber = "";
                    ztoPrintBillEntity.BillCode = "";
                    printBillEntities.Add(ztoPrintBillEntity);
                }
                var printAddEntities = new List<ZtoPrintBillEntity>();

                for (int i = 0; i < int.Parse(cmbPrintNumber.Text); i++)
                {
                    printAddEntities.AddRange(printBillEntities);
                }
                if (printAddEntities.Any())
                {
                    var ztoPrintBillManager = new ZtoPrintBillManager(BillPrintHelper.DbHelper);
                    if (!IsAddToPrintData)
                    {
                        ztoPrintBillManager.DbHelper = BillPrintHelper.BackupDbHelper;
                    }
                    int result = 0;
                    foreach (ZtoPrintBillEntity ztoPrintBillEntity in printAddEntities)
                    {
                        ztoPrintBillEntity.OrderNumber = Guid.NewGuid().ToString("N").ToLower();
                        ztoPrintBillManager.Add(ztoPrintBillEntity, true);
                        ++result;
                    }
                    _isAdd = result > 0;
                    MessageBox.Show(string.Format("新增成功{0}条记录", result), DotNet.Utilities.AppMessage.MSG0000, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void cmbPrintNumber_EditValueChanged(object sender, EventArgs e)
        {
            if (cmbPrintNumber.Text == "0" || string.IsNullOrEmpty(cmbPrintNumber.Text))
            {
                cmbPrintNumber.Text = "1";
            }
        }

        private void FrmAddPrintBySelectedRow_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_isAdd)
            {
                DialogResult = DialogResult.OK;
            }
        }
    }
}
