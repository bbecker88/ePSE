using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ePortafolio.Models.SSIA.Entities; 
using  ePortafolio.Models.SSIA;

namespace ePortafolio.Models.SSIA.Repository
{
    public partial class SeccionesCursoRepository 
    {
        String connectionString = "";
	  SSIADataContext DataContextObjectType;

        private SSIADataContext GetDataContextObject()
        {
            return DataContextFactory.GetWebRequestScopedDataContext<SSIADataContext>(null, connectionString);
        }

        public SeccionesCursoRepository(String connectionString)
        {
            this.connectionString = connectionString;
        }

        private IQueryable<SeccionesCursoBE> GetQueryable()
        {
            var DataContextObject = GetDataContextObject();
            var SeccionesCurso= from x in DataContextObject.SeccionesCurso
                             select new SeccionesCursoBE
                             {
						CodigoCurso = x.CodigoCurso,
						CursoId = x.CursoId,
						NombreCurso = x.NombreCurso,
						NombreProfesor = x.NombreProfesor,
						PeriodoId = x.PeriodoId,
						ProfesorId = x.ProfesorId,
						SeccionId = x.SeccionId,
                             };
            return SeccionesCurso;
        }

        private SeccionesCursoBE GetLinqFK(SeccionesCurso DataContextObject)
        {
		if(DataContextObject==null)
			return null;
            return new SeccionesCursoBE()
                {
						CodigoCurso = DataContextObject.CodigoCurso,
						CursoId = DataContextObject.CursoId,
						NombreCurso = DataContextObject.NombreCurso,
						NombreProfesor = DataContextObject.NombreProfesor,
						PeriodoId = DataContextObject.PeriodoId,
						ProfesorId = DataContextObject.ProfesorId,
						SeccionId = DataContextObject.SeccionId,
                };
        }

        public SeccionesCursoBE GetLinq(SeccionesCurso DataContextObject)
        {
		if(DataContextObject==null)
			return null;
            return new SeccionesCursoBE()
                {
				CodigoCurso = DataContextObject.CodigoCurso,
				CursoId = DataContextObject.CursoId,
				NombreCurso = DataContextObject.NombreCurso,
				NombreProfesor = DataContextObject.NombreProfesor,
				PeriodoId = DataContextObject.PeriodoId,
				ProfesorId = DataContextObject.ProfesorId,
				SeccionId = DataContextObject.SeccionId,
                };
        }

        public List<SeccionesCursoBE> GetAll()
        {
            return GetQueryable().ToList();
        }

        public List<SeccionesCursoBE> GetWhere(System.Linq.Expressions.Expression<Func<SeccionesCursoBE,bool>> Where)
	        {
            return GetQueryable().Where(Where).ToList();
        }

        public List<SeccionesCursoBE> GetWhere<T>(System.Linq.Expressions.Expression<Func<SeccionesCursoBE,bool>> Where,System.Linq.Expressions.Expression<Func<SeccionesCursoBE,T>> OrderBy)
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

        public SeccionesCursoBE GetOne(String PeriodoId, int CursoId,String SeccionId)
        {
            return GetQueryable().SingleOrDefault(x => x.PeriodoId == PeriodoId && x.CursoId == CursoId && x.SeccionId == SeccionId);
        }

        public Int32 GetLastId()
        {
			return 0;
        }

        public bool InsertIdentity(SeccionesCursoBE objInsert, bool ThrowException)
        {
		var DataContextObject = GetDataContextObject();
		SeccionesCurso objInsertLinq = new SeccionesCurso();
			objInsertLinq.CodigoCurso = objInsert.CodigoCurso;
			objInsertLinq.CursoId = objInsert.CursoId;
			objInsertLinq.NombreCurso = objInsert.NombreCurso;
			objInsertLinq.NombreProfesor = objInsert.NombreProfesor;
			objInsertLinq.PeriodoId = objInsert.PeriodoId;
			objInsertLinq.ProfesorId = objInsert.ProfesorId;
			objInsertLinq.SeccionId = objInsert.SeccionId;
		DataContextObject.SeccionesCurso.InsertOnSubmit(objInsertLinq);
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

        public void Insert(SeccionesCursoBE objInsert)
        {
		var DataContextObject = GetDataContextObject();
		SeccionesCurso objInsertLinq = new SeccionesCurso();
			objInsertLinq.CodigoCurso = objInsert.CodigoCurso;
			objInsertLinq.CursoId = objInsert.CursoId;
			objInsertLinq.NombreCurso = objInsert.NombreCurso;
			objInsertLinq.NombreProfesor = objInsert.NombreProfesor;
			objInsertLinq.PeriodoId = objInsert.PeriodoId;
			objInsertLinq.ProfesorId = objInsert.ProfesorId;
			objInsertLinq.SeccionId = objInsert.SeccionId;
		DataContextObject.SeccionesCurso.InsertOnSubmit(objInsertLinq);
        }

        public void Insert(List<SeccionesCursoBE> listObjInsert)
        {
		var DataContextObject = GetDataContextObject();
		foreach(var objInsert in listObjInsert)
		{
		SeccionesCurso objInsertLinq = new SeccionesCurso();
			objInsertLinq.CodigoCurso = objInsert.CodigoCurso;
			objInsertLinq.CursoId = objInsert.CursoId;
			objInsertLinq.NombreCurso = objInsert.NombreCurso;
			objInsertLinq.NombreProfesor = objInsert.NombreProfesor;
			objInsertLinq.PeriodoId = objInsert.PeriodoId;
			objInsertLinq.ProfesorId = objInsert.ProfesorId;
			objInsertLinq.SeccionId = objInsert.SeccionId;
			DataContextObject.SeccionesCurso.InsertOnSubmit(objInsertLinq);
		}		
        }

        public void InsertOrUpdate(SeccionesCursoBE objInsertOrUpdate)
        {
			return;
        } 

        public void InsertOrUpdate(List<SeccionesCursoBE> listObjInsertOrUpdate)
        {
			return;
        } 

        public void DeleteWhere(System.Linq.Expressions.Expression<Func<SeccionesCursoBE,bool>> Filtro)
        {
            var DataContextObject = GetDataContextObject();
		Delete(GetWhere(Filtro));
        }

        public void Delete(SeccionesCursoBE objDelete)
        {
			return;
        }

        public void Delete(List<SeccionesCursoBE> listObjDelete)
        {
			return;
        }

        public void TryDeleteWhere(System.Linq.Expressions.Expression<Func<SeccionesCursoBE,bool>> Filtro)
        {
            var DataContextObject = GetDataContextObject();
		TryDelete(GetWhere(Filtro));
        }

        public void TryDelete(SeccionesCursoBE objDelete)
        {
			return;
        }

        public void TryDelete(List<SeccionesCursoBE> listObjDelete)
        {
			return;
        }

        public bool Exists(SeccionesCursoBE objExists)
        {
			return false;
        }

        public void Update(SeccionesCursoBE objUpdate)
        {
			return;
        }

        public void Update(List<SeccionesCursoBE> listObjUpdate)
        {
			return;
        }
    }
}
