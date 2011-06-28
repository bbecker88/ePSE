using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ePortafolio.Models.SSIA.Entities; 
using  ePortafolio.Models.SSIA;

namespace ePortafolio.Models.SSIA.Repository
{
    public partial class AlumnosCursoRepository 
    {
        String connectionString = "";
	  SSIADataContext DataContextObjectType;

        private SSIADataContext GetDataContextObject()
        {
            return DataContextFactory.GetWebRequestScopedDataContext<SSIADataContext>(null, connectionString);
        }

        public AlumnosCursoRepository(String connectionString)
        {
            this.connectionString = connectionString;
        }

        private IQueryable<AlumnosCursoBE> GetQueryable()
        {
            var DataContextObject = GetDataContextObject();
            var AlumnosCurso= from x in DataContextObject.AlumnosCurso
                             select new AlumnosCursoBE
                             {
						AlumnoId = x.AlumnoId,
						CodigoCurso = x.CodigoCurso,
						CursoId = x.CursoId,
						NombreAlumno = x.NombreAlumno,
						NombreCurso = x.NombreCurso,
						PeriodoId = x.PeriodoId,
						SeccionId = x.SeccionId,
                             };
            return AlumnosCurso;
        }

        private AlumnosCursoBE GetLinqFK(AlumnosCurso DataContextObject)
        {
		if(DataContextObject==null)
			return null;
            return new AlumnosCursoBE()
                {
						AlumnoId = DataContextObject.AlumnoId,
						CodigoCurso = DataContextObject.CodigoCurso,
						CursoId = DataContextObject.CursoId,
						NombreAlumno = DataContextObject.NombreAlumno,
						NombreCurso = DataContextObject.NombreCurso,
						PeriodoId = DataContextObject.PeriodoId,
						SeccionId = DataContextObject.SeccionId,
                };
        }

        public AlumnosCursoBE GetLinq(AlumnosCurso DataContextObject)
        {
		if(DataContextObject==null)
			return null;
            return new AlumnosCursoBE()
                {
				AlumnoId = DataContextObject.AlumnoId,
				CodigoCurso = DataContextObject.CodigoCurso,
				CursoId = DataContextObject.CursoId,
				NombreAlumno = DataContextObject.NombreAlumno,
				NombreCurso = DataContextObject.NombreCurso,
				PeriodoId = DataContextObject.PeriodoId,
				SeccionId = DataContextObject.SeccionId,
                };
        }

        public List<AlumnosCursoBE> GetAll()
        {
            return GetQueryable().ToList();
        }

        public List<AlumnosCursoBE> GetWhere(System.Linq.Expressions.Expression<Func<AlumnosCursoBE,bool>> Where)
	        {
            return GetQueryable().Where(Where).ToList();
        }

        public List<AlumnosCursoBE> GetWhere<T>(System.Linq.Expressions.Expression<Func<AlumnosCursoBE,bool>> Where,System.Linq.Expressions.Expression<Func<AlumnosCursoBE,T>> OrderBy)
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

        public AlumnosCursoBE GetOne(String AlumnoId, int CursoId, String PeriodoId)
        {
            	return GetQueryable().SingleOrDefault(x =>  x.AlumnoId == AlumnoId && x.CursoId == CursoId && x.PeriodoId == PeriodoId);
        }

        public Int32 GetLastId()
        {
			return 0;
        }

        public bool InsertIdentity(AlumnosCursoBE objInsert, bool ThrowException)
        {
		var DataContextObject = GetDataContextObject();
		AlumnosCurso objInsertLinq = new AlumnosCurso();
			objInsertLinq.AlumnoId = objInsert.AlumnoId;
			objInsertLinq.CodigoCurso = objInsert.CodigoCurso;
			objInsertLinq.CursoId = objInsert.CursoId;
			objInsertLinq.NombreAlumno = objInsert.NombreAlumno;
			objInsertLinq.NombreCurso = objInsert.NombreCurso;
			objInsertLinq.PeriodoId = objInsert.PeriodoId;
			objInsertLinq.SeccionId = objInsert.SeccionId;
		DataContextObject.AlumnosCurso.InsertOnSubmit(objInsertLinq);
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

        public void Insert(AlumnosCursoBE objInsert)
        {
		var DataContextObject = GetDataContextObject();
		AlumnosCurso objInsertLinq = new AlumnosCurso();
			objInsertLinq.AlumnoId = objInsert.AlumnoId;
			objInsertLinq.CodigoCurso = objInsert.CodigoCurso;
			objInsertLinq.CursoId = objInsert.CursoId;
			objInsertLinq.NombreAlumno = objInsert.NombreAlumno;
			objInsertLinq.NombreCurso = objInsert.NombreCurso;
			objInsertLinq.PeriodoId = objInsert.PeriodoId;
			objInsertLinq.SeccionId = objInsert.SeccionId;
		DataContextObject.AlumnosCurso.InsertOnSubmit(objInsertLinq);
        }

        public void Insert(List<AlumnosCursoBE> listObjInsert)
        {
		var DataContextObject = GetDataContextObject();
		foreach(var objInsert in listObjInsert)
		{
		AlumnosCurso objInsertLinq = new AlumnosCurso();
			objInsertLinq.AlumnoId = objInsert.AlumnoId;
			objInsertLinq.CodigoCurso = objInsert.CodigoCurso;
			objInsertLinq.CursoId = objInsert.CursoId;
			objInsertLinq.NombreAlumno = objInsert.NombreAlumno;
			objInsertLinq.NombreCurso = objInsert.NombreCurso;
			objInsertLinq.PeriodoId = objInsert.PeriodoId;
			objInsertLinq.SeccionId = objInsert.SeccionId;
			DataContextObject.AlumnosCurso.InsertOnSubmit(objInsertLinq);
		}		
        }

        public void InsertOrUpdate(AlumnosCursoBE objInsertOrUpdate)
        {
			return;
        } 

        public void InsertOrUpdate(List<AlumnosCursoBE> listObjInsertOrUpdate)
        {
			return;
        } 

        public void DeleteWhere(System.Linq.Expressions.Expression<Func<AlumnosCursoBE,bool>> Filtro)
        {
            var DataContextObject = GetDataContextObject();
		Delete(GetWhere(Filtro));
        }

        public void Delete(AlumnosCursoBE objDelete)
        {
			return;
        }

        public void Delete(List<AlumnosCursoBE> listObjDelete)
        {
			return;
        }

        public void TryDeleteWhere(System.Linq.Expressions.Expression<Func<AlumnosCursoBE,bool>> Filtro)
        {
            var DataContextObject = GetDataContextObject();
		TryDelete(GetWhere(Filtro));
        }

        public void TryDelete(AlumnosCursoBE objDelete)
        {
			return;
        }

        public void TryDelete(List<AlumnosCursoBE> listObjDelete)
        {
			return;
        }

        public bool Exists(AlumnosCursoBE objExists)
        {
			return false;
        }

        public void Update(AlumnosCursoBE objUpdate)
        {
			return;
        }

        public void Update(List<AlumnosCursoBE> listObjUpdate)
        {
			return;
        }
    }
}
