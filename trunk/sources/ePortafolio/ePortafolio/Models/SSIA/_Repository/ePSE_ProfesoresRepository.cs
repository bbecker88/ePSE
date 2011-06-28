using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ePortafolio.Models.SSIA.Entities; 
using  ePortafolio.Models.SSIA;

namespace ePortafolio.Models.SSIA.Repository
{
    public partial class ProfesoresRepository 
    {
        String connectionString = "";
	  SSIADataContext DataContextObjectType;

        private SSIADataContext GetDataContextObject()
        {
            return DataContextFactory.GetWebRequestScopedDataContext<SSIADataContext>(null, connectionString);
        }

        public ProfesoresRepository(String connectionString)
        {
            this.connectionString = connectionString;
        }

        private IQueryable<ProfesoresBE> GetQueryable()
        {
            var DataContextObject = GetDataContextObject();
            var Profesores= from x in DataContextObject.Profesores
                             select new ProfesoresBE
                             {
						Nombre = x.Nombre,
						ProfesorId = x.ProfesorId,
                             };
            return Profesores;
        }

        private ProfesoresBE GetLinqFK(Profesores DataContextObject)
        {
		if(DataContextObject==null)
			return null;
            return new ProfesoresBE()
                {
						Nombre = DataContextObject.Nombre,
						ProfesorId = DataContextObject.ProfesorId,
                };
        }

        public ProfesoresBE GetLinq(Profesores DataContextObject)
        {
		if(DataContextObject==null)
			return null;
            return new ProfesoresBE()
                {
				Nombre = DataContextObject.Nombre,
				ProfesorId = DataContextObject.ProfesorId,
                };
        }

        public List<ProfesoresBE> GetAll()
        {
            return GetQueryable().ToList();
        }

        public List<ProfesoresBE> GetWhere(System.Linq.Expressions.Expression<Func<ProfesoresBE,bool>> Where)
	        {
            return GetQueryable().Where(Where).ToList();
        }

        public List<ProfesoresBE> GetWhere<T>(System.Linq.Expressions.Expression<Func<ProfesoresBE,bool>> Where,System.Linq.Expressions.Expression<Func<ProfesoresBE,T>> OrderBy)
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

        public ProfesoresBE GetOne(String ProfesorId)
        {
            return GetQueryable().SingleOrDefault(x => x.ProfesorId == ProfesorId);
        }

        public Int32 GetLastId()
        {
			return 0;
        }

        public bool InsertIdentity(ProfesoresBE objInsert, bool ThrowException)
        {
		var DataContextObject = GetDataContextObject();
		Profesores objInsertLinq = new Profesores();
			objInsertLinq.Nombre = objInsert.Nombre;
			objInsertLinq.ProfesorId = objInsert.ProfesorId;
		DataContextObject.Profesores.InsertOnSubmit(objInsertLinq);
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

        public void Insert(ProfesoresBE objInsert)
        {
		var DataContextObject = GetDataContextObject();
		Profesores objInsertLinq = new Profesores();
			objInsertLinq.Nombre = objInsert.Nombre;
			objInsertLinq.ProfesorId = objInsert.ProfesorId;
		DataContextObject.Profesores.InsertOnSubmit(objInsertLinq);
        }

        public void Insert(List<ProfesoresBE> listObjInsert)
        {
		var DataContextObject = GetDataContextObject();
		foreach(var objInsert in listObjInsert)
		{
		Profesores objInsertLinq = new Profesores();
			objInsertLinq.Nombre = objInsert.Nombre;
			objInsertLinq.ProfesorId = objInsert.ProfesorId;
			DataContextObject.Profesores.InsertOnSubmit(objInsertLinq);
		}		
        }

        public void InsertOrUpdate(ProfesoresBE objInsertOrUpdate)
        {
			return;
        } 

        public void InsertOrUpdate(List<ProfesoresBE> listObjInsertOrUpdate)
        {
			return;
        } 

        public void DeleteWhere(System.Linq.Expressions.Expression<Func<ProfesoresBE,bool>> Filtro)
        {
            var DataContextObject = GetDataContextObject();
		Delete(GetWhere(Filtro));
        }

        public void Delete(ProfesoresBE objDelete)
        {
			return;
        }

        public void Delete(List<ProfesoresBE> listObjDelete)
        {
			return;
        }

        public void TryDeleteWhere(System.Linq.Expressions.Expression<Func<ProfesoresBE,bool>> Filtro)
        {
            var DataContextObject = GetDataContextObject();
		TryDelete(GetWhere(Filtro));
        }

        public void TryDelete(ProfesoresBE objDelete)
        {
			return;
        }

        public void TryDelete(List<ProfesoresBE> listObjDelete)
        {
			return;
        }

        public bool Exists(ProfesoresBE objExists)
        {
			return false;
        }

        public void Update(ProfesoresBE objUpdate)
        {
			return;
        }

        public void Update(List<ProfesoresBE> listObjUpdate)
        {
			return;
        }
    }
}
