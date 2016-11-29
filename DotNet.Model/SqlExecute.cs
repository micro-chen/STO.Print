//--------------------------------------------------------------------
// All Rights Reserved , Copyright (C) 2015 , Hairihan TECH, Ltd.
//--------------------------------------------------------------------

using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;

namespace DotNet.Model
{
    using DotNet.Utilities;
    
    [DataContract]
	[KnownType(typeof(DbParameter))]
	public class SqlExecute
	{
		public SqlExecute()
			: this("", null, CommandType.Text)
		{
		}

		public SqlExecute(string commandText)
			: this(commandText, null, CommandType.Text)
		{
		}

		public SqlExecute(string commandText, CommandType commandType)
			: this(commandText, null, commandType)
		{
		}

		public SqlExecute(string commandText, object parameters, CommandType commandType)
		{
			CommandText = commandText;
			CommandType = commandType;
			if(parameters != null)
			{
				if(parameters is DbParameter[])
				{
					Parameters = (parameters as DbParameter[]).ToList();
				}
				else
				{
					foreach(var item in parameters.GetType().GetProperties())
					{
						object value = item.GetValue(parameters, null);
						Parameters.Add(new DbParameter(item.Name, value));
					}
				}
			}
		}

		public SqlExecute AddParameter(string name, object value, ParameterDirection parameterDirection)
		{
			Parameters.Add(new DbParameter(name, value, parameterDirection));
			return this;
		}

		public void SetValueAt(int index, object value)
		{
			Parameters[index].Value = value;
		}

		public object GetValueAt(int index)
		{
			return Parameters[index].Value;
		}

		[DataMember]
		private List<DbParameter> Parameters = new List<DbParameter>();

		[DataMember]
		public string CommandText { get; set; }

		[DataMember]
		public CommandType CommandType { get; set; }

		public System.Data.IDbDataParameter[] GetParameters(IDbHelper dbHelper)
		{
			if(Parameters.Count == 0) return null;
			System.Data.IDbDataParameter[] result = new System.Data.IDbDataParameter[Parameters.Count];
			for(int i = 0; i <= Parameters.Count - 1; i++)
			{
				result[i] = Parameters[i].GetIDbDataParameter(dbHelper);
			}
			return result;
		}
	}
}
