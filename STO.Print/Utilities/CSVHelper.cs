//-------------------------------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2015 , STO , Ltd .
//-------------------------------------------------------------------------------------

using System;
using System.Data;
using System.IO;
using System.Text;

namespace STO.Print.Utilities
{
    using DotNet.Utilities;

    /// <summary>
    /// CSV文件读取帮助类
    ///
    /// 修改纪录
    ///
    ///	   2015-10-30  版本：1.0 YangHengLian 创建主键，注意命名空间的排序。
    /// 
    /// 版本：1.0
    ///
    /// <author>
    ///		<name>YangHengLian</name>
    ///		<date>2014-08-30</date>
    /// </author>
    /// </summary>
    public class CsvHelper
    {
        private readonly DataTable _resoultTable = new DataTable("resoultTable");
        private string _filepath;
        private const int Headindex = 1;
        private const int Dataindex = 2;
        private int _headIndex = Headindex;
        private int _dataIndex = Dataindex;

        /// <summary>
        /// 设定文件路径，默认第一行为表投行，第二行为数据起始行。
        /// </summary>
        /// <param name="filepath">文件路径</param>
        public CsvHelper(string filepath)
        {
            SetFilepath(filepath);
            SetHeadindex(Headindex);
            SetDataindex(Dataindex);
        }

        /// <summary>
        /// 设定文件路径，表头行，默认表头行的下一行为数据起始行。
        /// </summary>
        /// <param name="filepath">文件路径</param>
        /// <param name="headIndex">表头行</param>
        public CsvHelper(string filepath, int headIndex)
        {
            SetFilepath(filepath);
            SetHeadindex(headIndex);
            SetDataindex(headIndex + 1);
        }

        /// <summary>
        /// 设定文件路径，表头行，数据起始行。
        /// </summary>
        /// <param name="filepath">文件路径</param>
        /// <param name="headIndex">表头行</param>
        /// <param name="dataIndex">数据起始行</param>
        public CsvHelper(string filepath, int headIndex, int dataIndex)
        {
            SetFilepath(filepath);
            SetHeadindex(headIndex);
            SetDataindex(dataIndex);
        }

        private void SetFilepath(string filepath)
        {
            _filepath = filepath;
        }

        private void SetHeadindex(int headIndex)
        {
            _headIndex = headIndex;
        }

        private void SetDataindex(int dataIndex)
        {
            _dataIndex = dataIndex;
        }

        private void ReadCsv2(string filepath, int headIndex, int dataIndex)
        {
            try
            {
                _resoultTable.Clear();
                var fs = new FileStream(filepath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                var sr = new StreamReader(fs, Encoding.GetEncoding("gb2312"));
                string line;
                int ckey = 0;
                while ((line = sr.ReadLine()) != null)
                {
                    if (++ckey >= headIndex)
                    {
                        if (ckey == headIndex)
                            CreateHeads(line);
                        else if (ckey >= dataIndex)
                            FillData(line);
                    }
                }
                sr.Close();
                fs.Close();
            }
            catch (Exception err)
            {
                System.Windows.Forms.MessageBox.Show(string.Format("数据读取失败!失败原因：{0}", err.Message),
                    AppMessage.MSG0000, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
            }
        }

        private void CreateHeads(string head)
        {
            DataColumn dcol;
            int lastdouhao = 0;
            int countyinhao = 0;
            for (int index = 0; index < head.Length; index++)
            {
                if (head[index] == '"')
                {
                    countyinhao++;
                    if (index == head.Length - 1 && countyinhao % 2 == 0)
                    {
                        dcol = new DataColumn(head.Substring(lastdouhao).Trim('"').Replace("\"\"", "\""));
                        _resoultTable.Columns.Add(dcol);
                    }
                }
                if (head[index] == ',' && countyinhao % 2 == 0)
                {
                    dcol = new DataColumn(head.Substring(lastdouhao, index - lastdouhao).Trim('"').Replace("\"\"", "\""));
                    _resoultTable.Columns.Add(dcol);
                    countyinhao = 0;
                    lastdouhao = index + 1;
                    if (index == head.Length - 1)
                    {
                        dcol = new DataColumn(head.Substring(index + 1).Trim('"').Replace("\"\"", "\""));
                        _resoultTable.Columns.Add(dcol);
                    }
                }
                if (index == head.Length - 1 && head[index] != '"' && head[index] != ',')
                {
                    dcol = new DataColumn(head.Substring(lastdouhao).Trim('"').Replace("\"\"", "\""));
                    _resoultTable.Columns.Add(dcol);
                }
            }
        }

        private void FillData(string dataline)
        {
            DataRow drow = _resoultTable.NewRow();
            int columindex = 0;
            int lastdouhao = 0;
            int countyinhao = 0;
            for (int index = 0; index < dataline.Length; index++)
            {
                if (dataline[index] == '"')
                {
                    countyinhao++;
                    if (index == dataline.Length - 1 && countyinhao % 2 == 0)
                    {
                        drow[_resoultTable.Columns[columindex]] = dataline.Substring(lastdouhao).Trim('"').Replace("\"\"", "\"");
                        columindex++;
                    }
                }
                if (dataline[index] == ',' && countyinhao % 2 == 0)
                {
                    drow[_resoultTable.Columns[columindex]] = dataline.Substring(lastdouhao, index - lastdouhao).Trim('"').Replace("\"\"", "\"");
                    columindex++;
                    countyinhao = 0;
                    lastdouhao = index + 1;
                    if (index == dataline.Length - 1)
                    {
                        drow[_resoultTable.Columns[columindex]] = dataline.Substring(index + 1).Trim('"').Replace("\"\"", "\"");
                        columindex++;
                    }
                }
                if (index == dataline.Length - 1 && dataline[index] != '"' && dataline[index] != ',')
                {
                    drow[_resoultTable.Columns[columindex]] = dataline.Substring(lastdouhao).Trim('"').Replace("\"\"", "\"");
                    columindex++;
                }
            }
            _resoultTable.Rows.Add(drow);
        }

        /// <summary>
        /// 根据csv文件构建datatable。
        /// </summary>
        public void CreateTable()
        {
            ReadCsv2(_filepath, _headIndex, _dataIndex);
        }

        /// <summary>
        /// 返回结果，一个数据表。
        /// </summary>
        /// <returns>返回datatable</returns>
        public DataTable GetResoultTable()
        {
            return _resoultTable;
        }

    }
}
