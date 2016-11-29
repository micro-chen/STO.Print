using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DotNet.Utilities;
using STO.Print.AddBillForm;
using STO.Print.Manager;
using STO.Print.Model;
using STO.Print.Utilities;

namespace STO.Print
{
    public partial class FrmChangeSendMan : BaseForm
    {
        /// <summary>
        /// 是否更新成功
        /// </summary>
        private bool _isUpdate;

        /// <summary>
        /// 打印记录传递过来
        /// </summary>
        public List<ZtoPrintBillEntity> ZtoPrintBillEntities { get; set; }

        public FrmChangeSendMan()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 选择发件人
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ckChooseSendMan_CheckedChanged(object sender, System.EventArgs e)
        {
            if (ckChooseSendMan.Checked)
            {
                var chooseReceiveMan = new FrmChooseSendMan();
                if (chooseReceiveMan.ShowDialog() == DialogResult.OK)
                {
                    if (!string.IsNullOrEmpty(chooseReceiveMan.sendManControl1.ChooseId))
                    {
                        ZtoUserManager userManager = new ZtoUserManager(BillPrintHelper.DbHelper);
                        ZtoUserEntity userEntity = userManager.GetObject(chooseReceiveMan.sendManControl1.ChooseId);
                        if (userEntity != null)
                        {
                            this.Tag = userEntity;
                            txtSendMan.Text = "发件人姓名为：" + userEntity.Realname + Environment.NewLine + "发件人电话为：" + userEntity.Mobile + userEntity.TelePhone + Environment.NewLine + "发件人地址为：" + userEntity.Address;
                        }
                    }
                }
                chooseReceiveMan.Dispose();
            }
        }

        /// <summary>
        /// 窗体加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmChangeSendMan_Load(object sender, EventArgs e)
        {
            if (ZtoPrintBillEntities != null && ZtoPrintBillEntities.Any())
            {
                lblTotalNumber.Text += ZtoPrintBillEntities.Count + "个收件人记录要改变发件人";
                foreach (var ztoPrintBillEntity in ZtoPrintBillEntities)
                {
                    txtReceiveManList.Text += (ztoPrintBillEntity.ReceiveMan + "-" + ztoPrintBillEntity.ReceivePhone) + Environment.NewLine;
                }
            }
        }

        /// <summary>
        /// 确定保存新的发件人的
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOk_Click(object sender, EventArgs e)
        {
            if (this.Tag != null)
            {
                var sendManEntity = this.Tag as ZtoUserEntity;
                if (sendManEntity != null)
                {
                    var printBillManager = new ZtoPrintBillManager(BillPrintHelper.DbHelper);
                    var count = 0;
                    foreach (ZtoPrintBillEntity ztoPrintBillEntity in ZtoPrintBillEntities)
                    {
                        var updateParameters = new List<KeyValuePair<string, object>>
                                           {
                                               new KeyValuePair<string, object>(ZtoPrintBillEntity.FieldSendMan, sendManEntity.Realname),
                                               new KeyValuePair<string, object>(ZtoPrintBillEntity.FieldSendPhone, sendManEntity.Mobile+" "+sendManEntity.TelePhone),
                                               new KeyValuePair<string, object>(ZtoPrintBillEntity.FieldSendAddress, sendManEntity.Address),
                                               new KeyValuePair<string, object>(ZtoPrintBillEntity.FieldSendProvince, sendManEntity.Province),
                                               new KeyValuePair<string, object>(ZtoPrintBillEntity.FieldSendCity, sendManEntity.City),
                                               new KeyValuePair<string, object>(ZtoPrintBillEntity.FieldSendCounty, sendManEntity.County) ,
                                               new KeyValuePair<string, object>(ZtoPrintBillEntity.FieldSendCompany, sendManEntity.Company) ,
                                               new KeyValuePair<string, object>(ZtoPrintBillEntity.FieldSendPostcode, sendManEntity.Postcode) 
                                           };
                        printBillManager.SetProperty(new KeyValuePair<string, object>(ZtoPrintBillEntity.FieldId, ztoPrintBillEntity.Id), updateParameters);
                        ++count;
                    }
                    if (count > 0)
                    {
                        _isUpdate = true;
                        string content = string.Format("成功更新条{0}记录，已经改成了新发件人：【{1}，{2}】", ZtoPrintBillEntities.Count, sendManEntity.Realname, sendManEntity.Mobile);
                        MessageUtil.ShowTips(content);
                        Close();
                    }
                }
            }
        }

        private void FrmChangeSendMan_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_isUpdate)
            {
                DialogResult = DialogResult.OK;
            }
        }
    }
}
