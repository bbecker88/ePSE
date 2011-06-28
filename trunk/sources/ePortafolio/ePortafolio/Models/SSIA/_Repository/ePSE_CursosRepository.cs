using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ePortafolio.Models.SSIA.Entities; 
using  ePortafolio.Models.SSIA;

namespace ePortafolio.Models.SSIA.Repository
{
    public partial class CursosRepository 
    {
        String connectionString = "";
	  SSIADataContext DataContextObjectType;

        private SSIADataContext GetDataContextObject()
        {
            return DataContextFactory.GetWebRequestScopedDataContext<SSIADataContext>(null, connectionString);
        }

        public CursosRepository(String connectionString)
        {
            this.connectionString = connectionString;
        }

        private IQueryable<CursosBE> GetQueryable()
        {
            var DataContextObject = GetDataContextObject();
            var Cursos= from x in DataContextObject.Cursos
                             select new CursosBE
                             {
						Codigo = x.Codigo,
						CursoId = x.CursoId,
						Nombre = x.Nombre,
                             };
            return Cursos;
        }

        private CursosBE GetLinqFK(Cursos DataContextObject)
        {
		if(DataContextObject==null)
			return null;
            return new CursosBE()
                {
						Codigo = DataContextObject.Codigo,
						CursoId = DataContextObject.CursoId,
						Nombre = DataContextObject.Nombre,
                };
        }

        public CursosBE GetLinq(Cursos DataContextObject)
        {
		if(DataContextObject==null)
			return null;
            return new CursosBE()
                {
				Codigo = DataContextObject.Codigo,
				CursoId = DataContextObject.CursoId,
				Nombre = DataContextObject.Nombre,
                };
        }

        public List<CursosBE> GetAll()
        {
            return GetQueryable().ToList();
        }

        public List<CursosBE> GetWhere(System.Linq.Expressions.Expression<Func<CursosBE,bool>> Where)
	        {
            return GetQueryable().Where(Where).ToList();
        }

        public List<CursosBE> GetWhere<T>(System.Linq.Expressions.Expression<Func<CursosBE,bool>> Where,System.Linq.Expressions.Expression<Func<CursosBE,T>> OrderBy)
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

        public CursosBE GetOne(int CursoId)
        {
            return GetQueryable().SingleOrDefault(x => x.CursoId == CursoId);
        }

        public Int32 GetLastId()
        {
			return 0;
        }

        public bool InsertIdentity(CursosBE objInsert, bool ThrowException)
        {
		var DataContextObject = GetDataContextObject();
		Cursos objInsertLinq = new Cursos();
			objInsertLinq.Codigo = objInsert.Codigo;
			objInsertLinq.CursoId = objInsert.CursoId;
			objInsertLinq.Nombre = objInsert.Nombre;
		DataContextObject.Cursos.InsertOnSubmit(objInsertLinq);
		try
            	{	
		  DataContextObject.SubmitChanges();
		  objInsert.CursoId = objInsertLinq.CursoId;
                return true;
            	}
            	catch (Exception Ex)
            	{
                if (ThrowException)
                    throw Ex;
                return false;
            	}
        }

        public void Insert(CursosBE objInsert)
        {
		var DataContextObject = GetDataContextObject();
		Cursos objInsertLinq = new Cursos();
			objInsertLinq.Codigo = objInsert.Codigo;
			objInsertLinq.CursoId = objInsert.CursoId;
			objInsertLinq.Nombre = objInsert.Nombre;
		DataContextObject.Cursos.InsertOnSubmit(objInsertLinq);
        }

        public void Insert(List<CursosBE> listObjInsert)
        {
		var DataContextObject = GetDataContextObject();
		foreach(var objInsert in listObjInsert)
		{
		Cursos objInsertLinq = new Cursos();
			objInsertLinq.Codigo = objInsert.Codigo;
			objInsertLinq.CursoId = objInsert.CursoId;
			objInsertLinq.Nombre = objInsert.Nombre;
			DataContextObject.Cursos.InsertOnSubmit(objInsertLinq);
		}		
        }

        public void InsertOrUpdate(CursosBE objInsertOrUpdate)
        {
			return;
        } 

        public void InsertOrUpdate(List<CursosBE> listObjInsertOrUpdate)
        {
			return;
        } 

        public void DeleteWhere(System.Linq.Expressions.Expression<Func<CursosBE,bool>> Filtro)
        {
            var DataContextObject = GetDataContextObject();
		Delete(GetWhere(Filtro));
        }

        public void Delete(CursosBE objDelete)
        {
			return;
        }

        public void Delete(List<CursosBE> listObjDelete)
        {
			return;
        }

        public void TryDeleteWhere(System.Linq.Expressions.Expression<Func<CursosBE,bool>> Filtro)
        {
            var DataContextObject = GetDataContextObject();
		TryDelete(GetWhere(Filtro));
        }

        public void TryDelete(CursosBE objDelete)
        {
			return;
        }

        public void TryDelete(List<CursosBE> listObjDelete)
        {
			return;
        }

        public bool Exists(CursosBE objExists)
        {
			return false;
        }

        public void Update(CursosBE objUpdate)
        {
			return;
        }

        public void Update(List<CursosBE> listObjUpdate)
        {
			return;
        }
    }
}
