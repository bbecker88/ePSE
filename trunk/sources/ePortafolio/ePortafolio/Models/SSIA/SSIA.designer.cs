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

namespace ePortafolio.Models.SSIA
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
	
	
	[System.Data.Linq.Mapping.DatabaseAttribute(Name="SSIA")]
	public partial class SSIADataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    #endregion
		
		public SSIADataContext() : 
				base(global::System.Configuration.ConfigurationManager.ConnectionStrings["SSIAConnectionString"].ConnectionString, mappingSource)
		{
			OnCreated();
		}
		
		public SSIADataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public SSIADataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public SSIADataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public SSIADataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<Alumnos> Alumnos
		{
			get
			{
				return this.GetTable<Alumnos>();
			}
		}
		
		public System.Data.Linq.Table<SeccionesCurso> SeccionesCurso
		{
			get
			{
				return this.GetTable<SeccionesCurso>();
			}
		}
		
		public System.Data.Linq.Table<AlumnosCurso> AlumnosCurso
		{
			get
			{
				return this.GetTable<AlumnosCurso>();
			}
		}
		
		public System.Data.Linq.Table<Coordinadores> Coordinadores
		{
			get
			{
				return this.GetTable<Coordinadores>();
			}
		}
		
		public System.Data.Linq.Table<Cursos> Cursos
		{
			get
			{
				return this.GetTable<Cursos>();
			}
		}
		
		public System.Data.Linq.Table<CursosPeriodos> CursosPeriodos
		{
			get
			{
				return this.GetTable<CursosPeriodos>();
			}
		}
		
		public System.Data.Linq.Table<Outcomes> Outcomes
		{
			get
			{
				return this.GetTable<Outcomes>();
			}
		}
		
		public System.Data.Linq.Table<OutcomesAlumno> OutcomesAlumno
		{
			get
			{
				return this.GetTable<OutcomesAlumno>();
			}
		}
		
		public System.Data.Linq.Table<Periodos> Periodos
		{
			get
			{
				return this.GetTable<Periodos>();
			}
		}
		
		public System.Data.Linq.Table<Profesores> Profesores
		{
			get
			{
				return this.GetTable<Profesores>();
			}
		}
	}
	
	[Table(Name="EPSE.Alumnos")]
	public partial class Alumnos
	{
		
		private string _AlumnoId;
		
		private string _Nombre;
		
		private string _CorreoElectronico;
		
		private string _NombreCarrera;
		
		public Alumnos()
		{
		}
		
		[Column(Storage="_AlumnoId", DbType="VarChar(12)")]
		public string AlumnoId
		{
			get
			{
				return this._AlumnoId;
			}
			set
			{
				if ((this._AlumnoId != value))
				{
					this._AlumnoId = value;
				}
			}
		}
		
		[Column(Storage="_Nombre", DbType="VarChar(153)")]
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
		
		[Column(Storage="_CorreoElectronico", DbType="VarChar(23)")]
		public string CorreoElectronico
		{
			get
			{
				return this._CorreoElectronico;
			}
			set
			{
				if ((this._CorreoElectronico != value))
				{
					this._CorreoElectronico = value;
				}
			}
		}
		
		[Column(Storage="_NombreCarrera", DbType="VarChar(50) NOT NULL", CanBeNull=false)]
		public string NombreCarrera
		{
			get
			{
				return this._NombreCarrera;
			}
			set
			{
				if ((this._NombreCarrera != value))
				{
					this._NombreCarrera = value;
				}
			}
		}
	}
	
	[Table(Name="EPSE.SeccionesCurso")]
	public partial class SeccionesCurso
	{
		
		private string _PeriodoId;
		
		private int _CursoId;
		
		private string _SeccionId;
		
		private string _ProfesorId;
		
		private string _NombreProfesor;
		
		private string _NombreCurso;
		
		private string _CodigoCurso;
		
		public SeccionesCurso()
		{
		}
		
		[Column(Storage="_PeriodoId", DbType="VarChar(5) NOT NULL", CanBeNull=false)]
		public string PeriodoId
		{
			get
			{
				return this._PeriodoId;
			}
			set
			{
				if ((this._PeriodoId != value))
				{
					this._PeriodoId = value;
				}
			}
		}
		
		[Column(Storage="_CursoId", DbType="Int NOT NULL")]
		public int CursoId
		{
			get
			{
				return this._CursoId;
			}
			set
			{
				if ((this._CursoId != value))
				{
					this._CursoId = value;
				}
			}
		}
		
		[Column(Storage="_SeccionId", DbType="VarChar(4)")]
		public string SeccionId
		{
			get
			{
				return this._SeccionId;
			}
			set
			{
				if ((this._SeccionId != value))
				{
					this._SeccionId = value;
				}
			}
		}
		
		[Column(Storage="_ProfesorId", DbType="VarChar(15)")]
		public string ProfesorId
		{
			get
			{
				return this._ProfesorId;
			}
			set
			{
				if ((this._ProfesorId != value))
				{
					this._ProfesorId = value;
				}
			}
		}
		
		[Column(Storage="_NombreProfesor", DbType="VarChar(153)")]
		public string NombreProfesor
		{
			get
			{
				return this._NombreProfesor;
			}
			set
			{
				if ((this._NombreProfesor != value))
				{
					this._NombreProfesor = value;
				}
			}
		}
		
		[Column(Storage="_NombreCurso", DbType="VarChar(100)")]
		public string NombreCurso
		{
			get
			{
				return this._NombreCurso;
			}
			set
			{
				if ((this._NombreCurso != value))
				{
					this._NombreCurso = value;
				}
			}
		}
		
		[Column(Storage="_CodigoCurso", DbType="VarChar(5) NOT NULL", CanBeNull=false)]
		public string CodigoCurso
		{
			get
			{
				return this._CodigoCurso;
			}
			set
			{
				if ((this._CodigoCurso != value))
				{
					this._CodigoCurso = value;
				}
			}
		}
	}
	
	[Table(Name="EPSE.AlumnosCurso")]
	public partial class AlumnosCurso
	{
		
		private string _PeriodoId;
		
		private int _CursoId;
		
		private string _SeccionId;
		
		private string _AlumnoId;
		
		private string _NombreCurso;
		
		private string _CodigoCurso;
		
		private string _NombreAlumno;
		
		public AlumnosCurso()
		{
		}
		
		[Column(Storage="_PeriodoId", DbType="VarChar(5) NOT NULL", CanBeNull=false)]
		public string PeriodoId
		{
			get
			{
				return this._PeriodoId;
			}
			set
			{
				if ((this._PeriodoId != value))
				{
					this._PeriodoId = value;
				}
			}
		}
		
		[Column(Storage="_CursoId", DbType="Int NOT NULL")]
		public int CursoId
		{
			get
			{
				return this._CursoId;
			}
			set
			{
				if ((this._CursoId != value))
				{
					this._CursoId = value;
				}
			}
		}
		
		[Column(Storage="_SeccionId", DbType="Char(4) NOT NULL", CanBeNull=false)]
		public string SeccionId
		{
			get
			{
				return this._SeccionId;
			}
			set
			{
				if ((this._SeccionId != value))
				{
					this._SeccionId = value;
				}
			}
		}
		
		[Column(Storage="_AlumnoId", DbType="VarChar(12)")]
		public string AlumnoId
		{
			get
			{
				return this._AlumnoId;
			}
			set
			{
				if ((this._AlumnoId != value))
				{
					this._AlumnoId = value;
				}
			}
		}
		
		[Column(Storage="_NombreCurso", DbType="VarChar(100)")]
		public string NombreCurso
		{
			get
			{
				return this._NombreCurso;
			}
			set
			{
				if ((this._NombreCurso != value))
				{
					this._NombreCurso = value;
				}
			}
		}
		
		[Column(Storage="_CodigoCurso", DbType="VarChar(5)")]
		public string CodigoCurso
		{
			get
			{
				return this._CodigoCurso;
			}
			set
			{
				if ((this._CodigoCurso != value))
				{
					this._CodigoCurso = value;
				}
			}
		}
		
		[Column(Storage="_NombreAlumno", DbType="VarChar(153)")]
		public string NombreAlumno
		{
			get
			{
				return this._NombreAlumno;
			}
			set
			{
				if ((this._NombreAlumno != value))
				{
					this._NombreAlumno = value;
				}
			}
		}
	}
	
	[Table(Name="EPSE.Coordinadores")]
	public partial class Coordinadores
	{
		
		private string _CoordinadorId;
		
		private string _Nombre;
		
		public Coordinadores()
		{
		}
		
		[Column(Storage="_CoordinadorId", DbType="VarChar(10) NOT NULL", CanBeNull=false)]
		public string CoordinadorId
		{
			get
			{
				return this._CoordinadorId;
			}
			set
			{
				if ((this._CoordinadorId != value))
				{
					this._CoordinadorId = value;
				}
			}
		}
		
		[Column(Storage="_Nombre", DbType="VarChar(37) NOT NULL", CanBeNull=false)]
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
	
	[Table(Name="EPSE.Cursos")]
	public partial class Cursos
	{
		
		private int _CursoId;
		
		private string _Codigo;
		
		private string _Nombre;
		
		public Cursos()
		{
		}
		
		[Column(Storage="_CursoId", DbType="Int NOT NULL")]
		public int CursoId
		{
			get
			{
				return this._CursoId;
			}
			set
			{
				if ((this._CursoId != value))
				{
					this._CursoId = value;
				}
			}
		}
		
		[Column(Storage="_Codigo", DbType="VarChar(5) NOT NULL", CanBeNull=false)]
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
		
		[Column(Storage="_Nombre", DbType="VarChar(100)")]
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
	
	[Table(Name="EPSE.CursosPeriodos")]
	public partial class CursosPeriodos
	{
		
		private string _PeriodoId;
		
		private int _CursoId;
		
		private string _CoordinadorId;
		
		private string _NombreCoordinador;
		
		private string _NombreCurso;
		
		private string _CodigoCurso;
		
		public CursosPeriodos()
		{
		}
		
		[Column(Storage="_PeriodoId", DbType="VarChar(5) NOT NULL", CanBeNull=false)]
		public string PeriodoId
		{
			get
			{
				return this._PeriodoId;
			}
			set
			{
				if ((this._PeriodoId != value))
				{
					this._PeriodoId = value;
				}
			}
		}
		
		[Column(Storage="_CursoId", DbType="Int NOT NULL")]
		public int CursoId
		{
			get
			{
				return this._CursoId;
			}
			set
			{
				if ((this._CursoId != value))
				{
					this._CursoId = value;
				}
			}
		}
		
		[Column(Storage="_CoordinadorId", DbType="VarChar(15) NOT NULL", CanBeNull=false)]
		public string CoordinadorId
		{
			get
			{
				return this._CoordinadorId;
			}
			set
			{
				if ((this._CoordinadorId != value))
				{
					this._CoordinadorId = value;
				}
			}
		}
		
		[Column(Storage="_NombreCoordinador", DbType="VarChar(153) NOT NULL", CanBeNull=false)]
		public string NombreCoordinador
		{
			get
			{
				return this._NombreCoordinador;
			}
			set
			{
				if ((this._NombreCoordinador != value))
				{
					this._NombreCoordinador = value;
				}
			}
		}
		
		[Column(Storage="_NombreCurso", DbType="VarChar(100) NOT NULL", CanBeNull=false)]
		public string NombreCurso
		{
			get
			{
				return this._NombreCurso;
			}
			set
			{
				if ((this._NombreCurso != value))
				{
					this._NombreCurso = value;
				}
			}
		}
		
		[Column(Storage="_CodigoCurso", DbType="VarChar(5) NOT NULL", CanBeNull=false)]
		public string CodigoCurso
		{
			get
			{
				return this._CodigoCurso;
			}
			set
			{
				if ((this._CodigoCurso != value))
				{
					this._CodigoCurso = value;
				}
			}
		}
	}
	
	[Table(Name="EPSE.Outcomes")]
	public partial class Outcomes
	{
		
		private int _OutcomeId;
		
		private string _Outcome;
		
		private string _Descripcion;
		
		public Outcomes()
		{
		}
		
		[Column(Storage="_OutcomeId", DbType="Int NOT NULL")]
		public int OutcomeId
		{
			get
			{
				return this._OutcomeId;
			}
			set
			{
				if ((this._OutcomeId != value))
				{
					this._OutcomeId = value;
				}
			}
		}
		
		[Column(Storage="_Outcome", DbType="VarChar(2) NOT NULL", CanBeNull=false)]
		public string Outcome
		{
			get
			{
				return this._Outcome;
			}
			set
			{
				if ((this._Outcome != value))
				{
					this._Outcome = value;
				}
			}
		}
		
		[Column(Storage="_Descripcion", DbType="VarChar(130) NOT NULL", CanBeNull=false)]
		public string Descripcion
		{
			get
			{
				return this._Descripcion;
			}
			set
			{
				if ((this._Descripcion != value))
				{
					this._Descripcion = value;
				}
			}
		}
	}
	
	[Table(Name="EPSE.OutcomesAlumno")]
	public partial class OutcomesAlumno
	{
		
		private string _AlumnoId;
		
		private string _Nombre;
		
		private string _CorreoElectronico;
		
		private int _OutcomeId;
		
		private string _NombreOutcome;
		
		private string _DescripcionOutcome;
		
		public OutcomesAlumno()
		{
		}
		
		[Column(Storage="_AlumnoId", DbType="VarChar(12)")]
		public string AlumnoId
		{
			get
			{
				return this._AlumnoId;
			}
			set
			{
				if ((this._AlumnoId != value))
				{
					this._AlumnoId = value;
				}
			}
		}
		
		[Column(Storage="_Nombre", DbType="VarChar(153)")]
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
		
		[Column(Storage="_CorreoElectronico", DbType="VarChar(23)")]
		public string CorreoElectronico
		{
			get
			{
				return this._CorreoElectronico;
			}
			set
			{
				if ((this._CorreoElectronico != value))
				{
					this._CorreoElectronico = value;
				}
			}
		}
		
		[Column(Storage="_OutcomeId", DbType="Int NOT NULL")]
		public int OutcomeId
		{
			get
			{
				return this._OutcomeId;
			}
			set
			{
				if ((this._OutcomeId != value))
				{
					this._OutcomeId = value;
				}
			}
		}
		
		[Column(Storage="_NombreOutcome", DbType="VarChar(2) NOT NULL", CanBeNull=false)]
		public string NombreOutcome
		{
			get
			{
				return this._NombreOutcome;
			}
			set
			{
				if ((this._NombreOutcome != value))
				{
					this._NombreOutcome = value;
				}
			}
		}
		
		[Column(Storage="_DescripcionOutcome", DbType="VarChar(130) NOT NULL", CanBeNull=false)]
		public string DescripcionOutcome
		{
			get
			{
				return this._DescripcionOutcome;
			}
			set
			{
				if ((this._DescripcionOutcome != value))
				{
					this._DescripcionOutcome = value;
				}
			}
		}
	}
	
	[Table(Name="EPSE.Periodos")]
	public partial class Periodos
	{
		
		private string _PeriodoId;
		
		private bool _EsActual;
		
		public Periodos()
		{
		}
		
		[Column(Storage="_PeriodoId", DbType="VarChar(5) NOT NULL", CanBeNull=false)]
		public string PeriodoId
		{
			get
			{
				return this._PeriodoId;
			}
			set
			{
				if ((this._PeriodoId != value))
				{
					this._PeriodoId = value;
				}
			}
		}
		
		[Column(Storage="_EsActual", DbType="Bit")]
		public bool EsActual
		{
			get
			{
				return this._EsActual;
			}
			set
			{
				if ((this._EsActual != value))
				{
					this._EsActual = value;
				}
			}
		}
	}
	
	[Table(Name="EPSE.Profesores")]
	public partial class Profesores
	{
		
		private string _ProfesorId;
		
		private string _Nombre;
		
		public Profesores()
		{
		}
		
		[Column(Storage="_ProfesorId", DbType="VarChar(15)")]
		public string ProfesorId
		{
			get
			{
				return this._ProfesorId;
			}
			set
			{
				if ((this._ProfesorId != value))
				{
					this._ProfesorId = value;
				}
			}
		}
		
		[Column(Storage="_Nombre", DbType="VarChar(153)")]
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
