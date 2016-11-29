//-----------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2014 , Hairihan TECH, Ltd. 
//-----------------------------------------------------------------

using System.Data;

namespace DotNet.Business
{
	public class DRDataRow : IDataRow
	{
		private DataRow dr;

		public DRDataRow(DataRow dr)
		{
			this.dr = dr;
		}

		#region IDataRow 成员
		public object this[string name]
		{
			get
			{
				return dr[name];
			}
		}

		public object this[int i]
		{
			get
			{
				return dr[i];
			}
		}

		public bool ContainsColumn(string columnName)
		{
			return dr.Table.Columns.Contains(columnName);
		}
		#endregion
	}
}
