﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.4200
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ePortafolioMVC.Models
{
	using System.Data.Linq;
	using System.Data.Linq.Mapping;
	using System.Data;
	using System.Collections.Generic;
	using System.Reflection;
	using System.Linq;
	using System.Linq.Expressions;
	using System.ComponentModel;
	using System;
	
	
	[System.Data.Linq.Mapping.DatabaseAttribute(Name="SSIA_O")]
	public partial class SSIA_ODBDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    #endregion
		
		public SSIA_ODBDataContext() : 
				base(global::System.Configuration.ConfigurationManager.ConnectionStrings["SSIA_OConnectionString"].ConnectionString, mappingSource)
		{
			OnCreated();
		}
		
		public SSIA_ODBDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public SSIA_ODBDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public SSIA_ODBDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public SSIA_ODBDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		[Function(Name="dbo.LogrosCurso")]
		public ISingleResult<LogrosCursoResult> LogrosCurso([Parameter(Name="CursoId", DbType="Int")] System.Nullable<int> cursoId, [Parameter(Name="PeriodoId", DbType="VarChar(10)")] string periodoId)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), cursoId, periodoId);
			return ((ISingleResult<LogrosCursoResult>)(result.ReturnValue));
		}
	}
	
	public partial class LogrosCursoResult
	{
		
		private int _LogroId;
		
		private string _Codigo;
		
		private string _Nombre;
		
		public LogrosCursoResult()
		{
		}
		
		[Column(Storage="_LogroId", DbType="Int NOT NULL")]
		public int LogroId
		{
			get
			{
				return this._LogroId;
			}
			set
			{
				if ((this._LogroId != value))
				{
					this._LogroId = value;
				}
			}
		}
		
		[Column(Storage="_Codigo", DbType="Char(10)")]
		public string Codigo
		{
			get
			{
				return this._Codigo;
			}
			set
			{
				if ((this._Codigo != value))
				{
					this._Codigo = value;
				}
			}
		}
		
		[Column(Storage="_Nombre", DbType="VarChar(500)")]
		public string Nombre
		{
			get
			{
				return this._Nombre;
			}
			set
			{
				if ((this._Nombre != value))
				{
					this._Nombre = value;
				}
			}
		}
	}
}
#pragma warning restore 1591
