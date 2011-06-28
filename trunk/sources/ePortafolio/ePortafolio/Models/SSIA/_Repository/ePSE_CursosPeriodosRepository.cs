using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ePortafolio.Models.SSIA.Entities; 
using  ePortafolio.Models.SSIA;

namespace ePortafolio.Models.SSIA.Repository
{
    public partial class CursosPeriodosRepository 
    {
        String connectionString = "";
	  SSIADataContext DataContextObjectType;

        private SSIADataContext GetDataContextObject()
        {
            return DataContextFactory.GetWebRequestScopedDataContext<SSIADataContext>(null, connectionString);
        }

        public CursosPeriodosRepository(String connectionString)
        {
            this.connectionString = connectionString;
        }

        private IQueryable<CursosPeriodosBE> GetQueryable()
        {
            var DataContextObject = GetDataContextObject();
            var CursosPeriodos= from x in DataContextObject.CursosPeriodos
                             select new CursosPeriodosBE
                             {
						CodigoCurso = x.CodigoCurso,
						CoordinadorId = x.CoordinadorId,
						CursoId = x.CursoId,
						NombreCoordinador = x.NombreCoordinador,
						NombreCurso = x.NombreCurso,
						PeriodoId = x.PeriodoId,
                             };
            return CursosPeriodos;
        }

        private CursosPeriodosBE GetLinqFK(CursosPeriodos DataContextObject)
        {
		if(DataContextObject==null)
			return null;
            return new CursosPeriodosBE()
                {
						CodigoCurso = DataContextObject.CodigoCurso,
						CoordinadorId = DataContextObject.CoordinadorId,
						CursoId = DataContextObject.CursoId,
						NombreCoordinador = DataContextObject.NombreCoordinador,
						NombreCurso = DataContextObject.NombreCurso,
						PeriodoId = DataContextObject.PeriodoId,
                };
        }

        public CursosPeriodosBE GetLinq(CursosPeriodos DataContextObject)
        {
		if(DataContextObject==null)
			return null;
            return new CursosPeriodosBE()
                {
				CodigoCurso = DataContextObject.CodigoCurso,
				CoordinadorId = DataContextObject.CoordinadorId,
				CursoId = DataContextObject.CursoId,
				NombreCoordinador = DataContextObject.NombreCoordinador,
				NombreCurso = DataContextObject.NombreCurso,
				PeriodoId = DataContextObject.PeriodoId,
                };
        }

        public List<CursosPeriodosBE> GetAll()
        {
            return GetQueryable().ToList();
        }

        public List<CursosPeriodosBE> GetWhere(System.Linq.Expressions.Expression<Func<CursosPeriodosBE,bool>> Where)
	        {
            return GetQueryable().Where(Where).ToList();
        }

        public List<CursosPeriodosBE> GetWhere<T>(System.Linq.Expressions.Expression<Func<CursosPeriodosBE,bool>> Where,System.Linq.Expressions.Expression<Func<CursosPeriodosBE,T>> OrderBy)
        {
		if(OrderBy==null)
		{
			return GetQueryable().Where(Where).ToList();
		}
		else
		{
			return GetQueryable().Where(Where).OrderBy(OrderBy).ToList();
		}            
        }

        public CursosPeriodosBE GetOne(int CursoId, String PeriodoId)
        {
            return GetQueryable().SingleOrDefault(x =>  x.CursoId == CursoId && x.PeriodoId == PeriodoId);
        }

        public Int32 GetLastId()
        {
			return 0;
        }

        public bool InsertIdentity(CursosPeriodosBE objInsert, bool ThrowException)
        {
		var DataContextObject = GetDataContextObject();
		CursosPeriodos objInsertLinq = new CursosPeriodos();
			objInsertLinq.CodigoCurso = objInsert.CodigoCurso;
			objInsertLinq.CoordinadorId = objInsert.CoordinadorId;
			objInsertLinq.CursoId = objInsert.CursoId;
			objInsertLinq.NombreCoordinador = objInsert.NombreCoordinador;
			objInsertLinq.NombreCurso = objInsert.NombreCurso;
			objInsertLinq.PeriodoId = objInsert.PeriodoId;
		DataContextObject.CursosPeriodos.InsertOnSubmit(objInsertLinq);
		try
            	{	
		  DataContextObject.SubmitChanges();
                return true;
            	}
            	catch (Exception Ex)
            	{
                if (ThrowException)
                    throw Ex;
                return false;
            	}
        }

        public void Insert(CursosPeriodosBE objInsert)
        {
		var DataContextObject = GetDataContextObject();
		CursosPeriodos objInsertLinq = new CursosPeriodos();
			objInsertLinq.CodigoCurso = objInsert.CodigoCurso;
			objInsertLinq.CoordinadorId = objInsert.CoordinadorId;
			objInsertLinq.CursoId = objInsert.CursoId;
			objInsertLinq.NombreCoordinador = objInsert.NombreCoordinador;
			objInsertLinq.NombreCurso = objInsert.NombreCurso;
			objInsertLinq.PeriodoId = objInsert.PeriodoId;
		DataContextObject.CursosPeriodos.InsertOnSubmit(objInsertLinq);
        }

        public void Insert(List<CursosPeriodosBE> listObjInsert)
        {
		var DataContextObject = GetDataContextObject();
		foreach(var objInsert in listObjInsert)
		{
		CursosPeriodos objInsertLinq = new CursosPeriodos();
			objInsertLinq.CodigoCurso = objInsert.CodigoCurso;
			objInsertLinq.CoordinadorId = objInsert.CoordinadorId;
			objInsertLinq.CursoId = objInsert.CursoId;
			objInsertLinq.NombreCoordinador = objInsert.NombreCoordinador;
			objInsertLinq.NombreCurso = objInsert.NombreCurso;
			objInsertLinq.PeriodoId = objInsert.PeriodoId;
			DataContextObject.CursosPeriodos.InsertOnSubmit(objInsertLinq);
		}		
        }

        public void InsertOrUpdate(CursosPeriodosBE objInsertOrUpdate)
        {
			return;
        } 

        public void InsertOrUpdate(List<CursosPeriodosBE> listObjInsertOrUpdate)
        {
			return;
        } 

        public void DeleteWhere(System.Linq.Expressions.Expression<Func<CursosPeriodosBE,bool>> Filtro)
        {
            var DataContextObject = GetDataContextObject();
		Delete(GetWhere(Filtro));
        }

        public void Delete(CursosPeriodosBE objDelete)
        {
			return;
        }

        public void Delete(List<CursosPeriodosBE> listObjDelete)
        {
			return;
        }

        public void TryDeleteWhere(System.Linq.Expressions.Expression<Func<CursosPeriodosBE,bool>> Filtro)
        {
            var DataContextObject = GetDataContextObject();
		TryDelete(GetWhere(Filtro));
        }

        public void TryDelete(CursosPeriodosBE objDelete)
        {
			return;
        }

        public void TryDelete(List<CursosPeriodosBE> listObjDelete)
        {
			return;
        }

        public bool Exists(CursosPeriodosBE objExists)
        {
			return false;
        }

        public void Update(CursosPeriodosBE objUpdate)
        {
			return;
        }

        public void Update(List<CursosPeriodosBE> listObjUpdate)
        {
			return;
        }
    }
}
