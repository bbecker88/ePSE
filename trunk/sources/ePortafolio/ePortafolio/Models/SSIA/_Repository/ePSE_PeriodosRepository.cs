using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ePortafolio.Models.SSIA.Entities; 
using  ePortafolio.Models.SSIA;

namespace ePortafolio.Models.SSIA.Repository
{
    public partial class PeriodosRepository 
    {
        String connectionString = "";
	  SSIADataContext DataContextObjectType;

        private SSIADataContext GetDataContextObject()
        {
            return DataContextFactory.GetWebRequestScopedDataContext<SSIADataContext>(null, connectionString);
        }

        public PeriodosRepository(String connectionString)
        {
            this.connectionString = connectionString;
        }

        private IQueryable<PeriodosBE> GetQueryable()
        {
            var DataContextObject = GetDataContextObject();
            var Periodos= from x in DataContextObject.Periodos
                             select new PeriodosBE
                             {
						EsActual = x.EsActual,
						PeriodoId = x.PeriodoId,
                             };
            return Periodos;
        }

        private PeriodosBE GetLinqFK(Periodos DataContextObject)
        {
		if(DataContextObject==null)
			return null;
            return new PeriodosBE()
                {
						EsActual = DataContextObject.EsActual,
						PeriodoId = DataContextObject.PeriodoId,
                };
        }

        public PeriodosBE GetLinq(Periodos DataContextObject)
        {
		if(DataContextObject==null)
			return null;
            return new PeriodosBE()
                {
				EsActual = DataContextObject.EsActual,
				PeriodoId = DataContextObject.PeriodoId,
                };
        }

        public List<PeriodosBE> GetAll()
        {
            return GetQueryable().ToList();
        }

        public List<PeriodosBE> GetWhere(System.Linq.Expressions.Expression<Func<PeriodosBE,bool>> Where)
	        {
            return GetQueryable().Where(Where).ToList();
        }

        public List<PeriodosBE> GetWhere<T>(System.Linq.Expressions.Expression<Func<PeriodosBE,bool>> Where,System.Linq.Expressions.Expression<Func<PeriodosBE,T>> OrderBy)
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

        public PeriodosBE GetOne(String PeriodoId)
        {
            return GetQueryable().SingleOrDefault(x => x.PeriodoId == PeriodoId);
        }

        public Int32 GetLastId()
        {
			return 0;
        }

        public bool InsertIdentity(PeriodosBE objInsert, bool ThrowException)
        {
		var DataContextObject = GetDataContextObject();
		Periodos objInsertLinq = new Periodos();
			objInsertLinq.EsActual = objInsert.EsActual;
			objInsertLinq.PeriodoId = objInsert.PeriodoId;
		DataContextObject.Periodos.InsertOnSubmit(objInsertLinq);
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

        public void Insert(PeriodosBE objInsert)
        {
		var DataContextObject = GetDataContextObject();
		Periodos objInsertLinq = new Periodos();
			objInsertLinq.EsActual = objInsert.EsActual;
			objInsertLinq.PeriodoId = objInsert.PeriodoId;
		DataContextObject.Periodos.InsertOnSubmit(objInsertLinq);
        }

        public void Insert(List<PeriodosBE> listObjInsert)
        {
		var DataContextObject = GetDataContextObject();
		foreach(var objInsert in listObjInsert)
		{
		Periodos objInsertLinq = new Periodos();
			objInsertLinq.EsActual = objInsert.EsActual;
			objInsertLinq.PeriodoId = objInsert.PeriodoId;
			DataContextObject.Periodos.InsertOnSubmit(objInsertLinq);
		}		
        }

        public void InsertOrUpdate(PeriodosBE objInsertOrUpdate)
        {
			return;
        } 

        public void InsertOrUpdate(List<PeriodosBE> listObjInsertOrUpdate)
        {
			return;
        } 

        public void DeleteWhere(System.Linq.Expressions.Expression<Func<PeriodosBE,bool>> Filtro)
        {
            var DataContextObject = GetDataContextObject();
		Delete(GetWhere(Filtro));
        }

        public void Delete(PeriodosBE objDelete)
        {
			return;
        }

        public void Delete(List<PeriodosBE> listObjDelete)
        {
			return;
        }

        public void TryDeleteWhere(System.Linq.Expressions.Expression<Func<PeriodosBE,bool>> Filtro)
        {
            var DataContextObject = GetDataContextObject();
		TryDelete(GetWhere(Filtro));
        }

        public void TryDelete(PeriodosBE objDelete)
        {
			return;
        }

        public void TryDelete(List<PeriodosBE> listObjDelete)
        {
			return;
        }

        public bool Exists(PeriodosBE objExists)
        {
			return false;
        }

        public void Update(PeriodosBE objUpdate)
        {
			return;
        }

        public void Update(List<PeriodosBE> listObjUpdate)
        {
			return;
        }
    }
}
