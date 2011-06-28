using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ePortafolio.Models.SSIA.Entities; 
using  ePortafolio.Models.SSIA;

namespace ePortafolio.Models.SSIA.Repository
{
    public partial class CoordinadoresRepository 
    {
        String connectionString = "";
	  SSIADataContext DataContextObjectType;

        private SSIADataContext GetDataContextObject()
        {
            return DataContextFactory.GetWebRequestScopedDataContext<SSIADataContext>(null, connectionString);
        }

        public CoordinadoresRepository(String connectionString)
        {
            this.connectionString = connectionString;
        }

        private IQueryable<CoordinadoresBE> GetQueryable()
        {
            var DataContextObject = GetDataContextObject();
            var Coordinadores= from x in DataContextObject.Coordinadores
                             select new CoordinadoresBE
                             {
						CoordinadorId = x.CoordinadorId,
						Nombre = x.Nombre,
                             };
            return Coordinadores;
        }

        private CoordinadoresBE GetLinqFK(Coordinadores DataContextObject)
        {
		if(DataContextObject==null)
			return null;
            return new CoordinadoresBE()
                {
						CoordinadorId = DataContextObject.CoordinadorId,
						Nombre = DataContextObject.Nombre,
                };
        }

        public CoordinadoresBE GetLinq(Coordinadores DataContextObject)
        {
		if(DataContextObject==null)
			return null;
            return new CoordinadoresBE()
                {
				CoordinadorId = DataContextObject.CoordinadorId,
				Nombre = DataContextObject.Nombre,
                };
        }

        public List<CoordinadoresBE> GetAll()
        {
            return GetQueryable().ToList();
        }

        public List<CoordinadoresBE> GetWhere(System.Linq.Expressions.Expression<Func<CoordinadoresBE,bool>> Where)
	        {
            return GetQueryable().Where(Where).ToList();
        }

        public List<CoordinadoresBE> GetWhere<T>(System.Linq.Expressions.Expression<Func<CoordinadoresBE,bool>> Where,System.Linq.Expressions.Expression<Func<CoordinadoresBE,T>> OrderBy)
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

        public CoordinadoresBE GetOne(String CoordinadorId)
        {
            return GetQueryable().SingleOrDefault(x => x.CoordinadorId == CoordinadorId);
        }

        public Int32 GetLastId()
        {
			return 0;
        }

        public bool InsertIdentity(CoordinadoresBE objInsert, bool ThrowException)
        {
		var DataContextObject = GetDataContextObject();
		Coordinadores objInsertLinq = new Coordinadores();
			objInsertLinq.CoordinadorId = objInsert.CoordinadorId;
			objInsertLinq.Nombre = objInsert.Nombre;
		DataContextObject.Coordinadores.InsertOnSubmit(objInsertLinq);
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

        public void Insert(CoordinadoresBE objInsert)
        {
		var DataContextObject = GetDataContextObject();
		Coordinadores objInsertLinq = new Coordinadores();
			objInsertLinq.CoordinadorId = objInsert.CoordinadorId;
			objInsertLinq.Nombre = objInsert.Nombre;
		DataContextObject.Coordinadores.InsertOnSubmit(objInsertLinq);
        }

        public void Insert(List<CoordinadoresBE> listObjInsert)
        {
		var DataContextObject = GetDataContextObject();
		foreach(var objInsert in listObjInsert)
		{
		Coordinadores objInsertLinq = new Coordinadores();
			objInsertLinq.CoordinadorId = objInsert.CoordinadorId;
			objInsertLinq.Nombre = objInsert.Nombre;
			DataContextObject.Coordinadores.InsertOnSubmit(objInsertLinq);
		}		
        }

        public void InsertOrUpdate(CoordinadoresBE objInsertOrUpdate)
        {
			return;
        } 

        public void InsertOrUpdate(List<CoordinadoresBE> listObjInsertOrUpdate)
        {
			return;
        } 

        public void DeleteWhere(System.Linq.Expressions.Expression<Func<CoordinadoresBE,bool>> Filtro)
        {
            var DataContextObject = GetDataContextObject();
		Delete(GetWhere(Filtro));
        }

        public void Delete(CoordinadoresBE objDelete)
        {
			return;
        }

        public void Delete(List<CoordinadoresBE> listObjDelete)
        {
			return;
        }

        public void TryDeleteWhere(System.Linq.Expressions.Expression<Func<CoordinadoresBE,bool>> Filtro)
        {
            var DataContextObject = GetDataContextObject();
		TryDelete(GetWhere(Filtro));
        }

        public void TryDelete(CoordinadoresBE objDelete)
        {
			return;
        }

        public void TryDelete(List<CoordinadoresBE> listObjDelete)
        {
			return;
        }

        public bool Exists(CoordinadoresBE objExists)
        {
			return false;
        }

        public void Update(CoordinadoresBE objUpdate)
        {
			return;
        }

        public void Update(List<CoordinadoresBE> listObjUpdate)
        {
			return;
        }
    }
}
