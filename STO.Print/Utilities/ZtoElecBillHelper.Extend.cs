using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using STO.Print.Manager;
using STO.Print.Model;

namespace STO.Print.Utilities
{
    public partial class ZtoElecBillHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ztoPrintBillEntities"></param>
        /// <param name="ztoElecUserInfoEntity"></param>
        /// <param name="frm"></param>
        /// <param name="progressBar"></param>
        /// <param name="method"></param>
        public static void GetZtoBillAndBigPenByMultiThread(List<ZtoPrintBillEntity> ztoPrintBillEntities, ZtoElecUserInfoEntity ztoElecUserInfoEntity, Form frm, ProgressBar progressBar, Action method)
        {
            if (!CheckPrintData(ztoPrintBillEntities))
            {
                return;
            }
            // 第一步需要先检查
            // 然后开始分配线程
            // 然后获取，更新数据库
            // 执行绑定方法
            var taskList = new List<Task<Task<List<ZtoPrintBillEntity>>>>();
            var submitCount = Math.Ceiling(ztoPrintBillEntities.Count / 10.0);
            progressBar.Value = 0;
            progressBar.Maximum = ztoPrintBillEntities.Count;
            if (submitCount > 1)
            {
                for (int i = 0; i < submitCount; i++)
                {
                    List<ZtoPrintBillEntity> t = ztoPrintBillEntities.Skip(10 * i).Take(10).ToList();
                    var task = Task.Factory.StartNew(() =>
                                                         {
                                                             var result = new TaskCompletionSource<List<ZtoPrintBillEntity>>();
                                                             result.SetResult(BindElecBillByCustomerId(t, ztoElecUserInfoEntity));
                                                             return result.Task;
                                                         });
                    if (task.Result.Result != null)
                    {
                        progressBar.BeginInvoke(new Action(() =>
                        {
                            if (progressBar.Value < progressBar.Maximum)
                            {
                                progressBar.Value += task.Result.Result.Count;
                            }
                        }));
                        taskList.Add(task);
                    }
                }
            }
            else
            {
                var task = Task.Factory.StartNew(() =>
                {
                    var result = new TaskCompletionSource<List<ZtoPrintBillEntity>>();
                    result.SetResult(BindElecBillByCustomerId(ztoPrintBillEntities, ztoElecUserInfoEntity));
                    return result.Task;
                });
                if (task.Result.Result != null)
                {
                    progressBar.BeginInvoke(new Action(() =>
                    {
                        if (progressBar.Value < progressBar.Maximum)
                        {
                            progressBar.Value += task.Result.Result.Count;
                        }
                    }));
                    taskList.Add(task);
                }
            }
            var successCount = taskList.Sum(item => UpdateBillCodeAndBigPen(item.Result.Result));
            if (successCount > 0)
            {
                MessageUtil.ShowTips(@"获取成功" + successCount + "条电子面单。");
                method();
            }
            else
            {
                MessageUtil.ShowError("获取失败,建议重新获取，一次性不要太多，建议不超过100条");
            }
            //List<Func<List<ZtoPrintBillEntity>>> funcs = new List<Func<List<ZtoPrintBillEntity>>>();
            //funcs.Add(() => BindElecBillByCustomerId(ztoPrintBillEntities, ztoElecUserInfoEntity));
            //funcs.Add(() => BindElecBillByCustomerId(ztoPrintBillEntities, ztoElecUserInfoEntity));
            //funcs.Add(() => BindElecBillByCustomerId(ztoPrintBillEntities, ztoElecUserInfoEntity));
            //funcs.Add(() => BindElecBillByCustomerId(ztoPrintBillEntities, ztoElecUserInfoEntity));
            //foreach (var func in funcs)
            //{
            //    // ThreadPool.QueueUserWorkItem(state => func());
            //    var result = func();
            //    MessageUtil.ShowTips(result.Count.ToString());
            //}
            // http://www.cnblogs.com/yetiea/p/3361925.html
            // C#线程池ThreadPool.QueueUserWorkItem接收线程执行的方法返回值
            //List<ThreadReturnData> testList = new List<ThreadReturnData>();
            //IList<ManualResetEvent> arrManual = new List<ManualResetEvent>();
            //for (int i = 0; i < 1; i++)
            //{
            //    ThreadReturnData temp = new ThreadReturnData();
            //    temp.manual = new ManualResetEvent(false);
            //    arrManual.Add(temp.manual);
            //    ThreadPool.QueueUserWorkItem(temp.ReturnThreadData, ztoPrintBillEntities);
            //    testList.Add(temp);
            //}
            //if (arrManual.Count > 0)
            //{
            //    //等待所有线程执行完
            //    foreach (var manualResetEvent in arrManual)
            //    {
            //        manualResetEvent.WaitOne();
            //    }
            //}
            //foreach (ThreadReturnData d in testList)
            //{
            //    MessageUtil.ShowTips(d.res.Count.ToString());
            //}

            // 声明一个Action委托的List，添加一些委托测试用
            //List<Action> actions = new List<Action>
            //{
            //    ()=>{MessageUtil.ShowTips("A-1");},
            //    ()=>{MessageUtil.ShowTips("A-2");},
            //    ()=>{MessageUtil.ShowTips("A-3");},
            //    ()=>{MessageUtil.ShowTips("A-4");}
            //};
            ////遍历输出结果
            //foreach (var action in actions)
            //{
            //    ThreadPool.QueueUserWorkItem(state => action(), null);
            //}
        }

        /// <summary>
        /// 更新大头笔和单号
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static int UpdateBillCodeAndBigPen(List<ZtoPrintBillEntity> list)
        {
            int result = 0;
            var printBillManager = new ZtoPrintBillManager(BillPrintHelper.DbHelper);
            foreach (var billEntity in list)
            {
                // 进行更新选中记录的（收件）省市区（包括省市区的ID）
                var updateParameters = new List<KeyValuePair<string, object>>
                    {
                        new KeyValuePair<string, object>(ZtoPrintBillEntity.FieldBigPen,billEntity.BigPen),
                        new KeyValuePair<string, object>(ZtoPrintBillEntity.FieldOrderNumber,billEntity.OrderNumber),
                        new KeyValuePair<string, object>(ZtoPrintBillEntity.FieldBillCode,billEntity.BillCode),
                        new KeyValuePair<string, object>(ZtoPrintBillEntity.FieldModifiedOn, DateTime.Now)
                    };
                var whereParameters = new List<KeyValuePair<string, object>>
                    {
                        new KeyValuePair<string, object>(ZtoPrintBillEntity.FieldId, billEntity.Id)
                    };
                var tempResult = printBillManager.SetProperty(whereParameters, updateParameters);
                result += tempResult;
            }
            return result;
        }

        public static bool CheckPrintData(List<ZtoPrintBillEntity> ztoPrintBillEntities)
        {
            return true;
            var errorList = new List<object>();
            // 调用接口都没有通过，那么一般就是商家ID和密码不正确导致
            foreach (ZtoPrintBillEntity ztoPrintBillEntity in ztoPrintBillEntities)
            {
                if (string.IsNullOrEmpty(ztoPrintBillEntity.SendMan))
                {
                    errorList.Add(new { 订单号 = ztoPrintBillEntity.OrderNumber, 错误信息 = "发件人姓名必填" });
                }
                else if (string.IsNullOrEmpty(ztoPrintBillEntity.SendPhone))
                {
                    errorList.Add(new { 订单号 = ztoPrintBillEntity.OrderNumber, 错误信息 = "发件人电话必填" });
                }
                else if (!string.IsNullOrEmpty(ztoPrintBillEntity.SendPhone) && ztoPrintBillEntity.SendPhone.Length > 30)
                {
                    errorList.Add(new { 订单号 = ztoPrintBillEntity.OrderNumber, 错误信息 = "发件人电话太长了" });
                }
                else if (string.IsNullOrEmpty(ztoPrintBillEntity.SendProvince))
                {
                    errorList.Add(new { 订单号 = ztoPrintBillEntity.OrderNumber, 错误信息 = "发件人省份必填" });
                }
                else if (string.IsNullOrEmpty(ztoPrintBillEntity.SendCity))
                {
                    errorList.Add(new { 订单号 = ztoPrintBillEntity.OrderNumber, 错误信息 = "发件人城市必填" });
                }
                else if (string.IsNullOrEmpty(ztoPrintBillEntity.SendCounty))
                {
                    errorList.Add(new { 订单号 = ztoPrintBillEntity.OrderNumber, 错误信息 = "发件人区县必填" });
                }
                else if (string.IsNullOrEmpty(ztoPrintBillEntity.SendAddress))
                {
                    errorList.Add(new { 订单号 = ztoPrintBillEntity.OrderNumber, 错误信息 = "发件人详细地址必填" });
                }
                else if (string.IsNullOrEmpty(ztoPrintBillEntity.ReceiveMan))
                {
                    errorList.Add(new { 订单号 = ztoPrintBillEntity.OrderNumber, 错误信息 = "收件人姓名必填" });
                }
                else if (string.IsNullOrEmpty(ztoPrintBillEntity.ReceivePhone))
                {
                    errorList.Add(new { 订单号 = ztoPrintBillEntity.OrderNumber, 错误信息 = "收件人电话必填" });
                }
                else if (!string.IsNullOrEmpty(ztoPrintBillEntity.ReceivePhone) && ztoPrintBillEntity.ReceivePhone.Length > 30)
                {
                    errorList.Add(new { 订单号 = ztoPrintBillEntity.OrderNumber, 错误信息 = "收件人电话太长了" });
                }
                else if (string.IsNullOrEmpty(ztoPrintBillEntity.ReceiveProvince))
                {
                    errorList.Add(new { 订单号 = ztoPrintBillEntity.OrderNumber, 错误信息 = "收件人省份必填" });
                }
                else if (string.IsNullOrEmpty(ztoPrintBillEntity.ReceiveCity))
                {
                    errorList.Add(new { 订单号 = ztoPrintBillEntity.OrderNumber, 错误信息 = "收件人城市必填" });
                }
                else if (string.IsNullOrEmpty(ztoPrintBillEntity.ReceiveCounty))
                {
                    errorList.Add(new { 订单号 = ztoPrintBillEntity.OrderNumber, 错误信息 = "收件人区县必填" });
                }
                else if (string.IsNullOrEmpty(ztoPrintBillEntity.ReceiveAddress))
                {
                    errorList.Add(new { 订单号 = ztoPrintBillEntity.OrderNumber, 错误信息 = "收件人详细地址必填" });
                }
            }
            if (errorList.Any())
            {
                var frmImportError = new FrmImportError() { errorList = errorList };
                frmImportError.ShowDialog();
                return false;
            }
            return true;
        }
    }
}
