using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ePortafolio.Models.SSIA.Entities; 
using  ePortafolio.Models.SSIA;

namespace ePortafolio.Models.SSIA.Repository
{
    public partial class OutcomesRepository 
    {
        String connectionString = "";
	  SSIADataContext DataContextObjectType;

        private SSIADataContext GetDataContextObject()
        {
            return DataContextFactory.GetWebRequestScopedDataContext<SSIADataContext>(null, connectionString);
        }

        public OutcomesRepository(String connectionString)
        {
            this.connectionString = connectionString;
        }

        private IQueryable<OutcomesBE> GetQueryable()
        {
            var DataContextObject = GetDataContextObject();
            var Outcomes= from x in DataContextObject.Outcomes
                             select new OutcomesBE
                             {
						Descripcion = x.Descripcion,
						Outcome = x.Outcome,
						OutcomeId = x.OutcomeId,
                             };
            return Outcomes;
        }

        private OutcomesBE GetLinqFK(Outcomes DataContextObject)
        {
		if(DataContextObject==null)
			return null;
            return new OutcomesBE()
                {
						Descripcion = DataContextObject.Descripcion,
						Outcome = DataContextObject.Outcome,
						OutcomeId = DataContextObject.OutcomeId,
                };
        }

        public OutcomesBE GetLinq(Outcomes DataContextObject)
        {
		if(DataContextObject==null)
			return null;
            return new OutcomesBE()
                {
				Descripcion = DataContextObject.Descripcion,
				Outcome = DataContextObject.Outcome,
				OutcomeId = DataContextObject.OutcomeId,
                };
        }

        public List<OutcomesBE> GetAll()
        {
            return GetQueryable().ToList();
        }

        public List<OutcomesBE> GetWhere(System.Linq.Expressions.Expression<Func<OutcomesBE,bool>> Where)
	        {
            return GetQueryable().Where(Where).ToList();
        }

        public List<OutcomesBE> GetWhere<T>(System.Linq.Expressions.Expression<Func<OutcomesBE,bool>> Where,System.Linq.Expressions.Expression<Func<OutcomesBE,T>> OrderBy)
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

        public OutcomesBE GetOne(int OutcomeId)
        {
            return GetQueryable().SingleOrDefault(x => x.OutcomeId == OutcomeId);
        }

        public Int32 GetLastId()
        {
			return 0;
        }

        public bool InsertIdentity(OutcomesBE objInsert, bool ThrowException)
        {
		var DataContextObject = GetDataContextObject();
		Outcomes objInsertLinq = new Outcomes();
			objInsertLinq.Descripcion = objInsert.Descripcion;
			objInsertLinq.Outcome = objInsert.Outcome;
			objInsertLinq.OutcomeId = objInsert.OutcomeId;
		DataContextObject.Outcomes.InsertOnSubmit(objInsertLinq);
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

        public void Insert(OutcomesBE objInsert)
        {
		var DataContextObject = GetDataContextObject();
		Outcomes objInsertLinq = new Outcomes();
			objInsertLinq.Descripcion = objInsert.Descripcion;
			objInsertLinq.Outcome = objInsert.Outcome;
			objInsertLinq.OutcomeId = objInsert.OutcomeId;
		DataContextObject.Outcomes.InsertOnSubmit(objInsertLinq);
        }

        public void Insert(List<OutcomesBE> listObjInsert)
        {
		var DataContextObject = GetDataContextObject();
		foreach(var objInsert in listObjInsert)
		{
		Outcomes objInsertLinq = new Outcomes();
			objInsertLinq.Descripcion = objInsert.Descripcion;
			objInsertLinq.Outcome = objInsert.Outcome;
			objInsertLinq.OutcomeId = objInsert.OutcomeId;
			DataContextObject.Outcomes.InsertOnSubmit(objInsertLinq);
		}		
        }

        public void InsertOrUpdate(OutcomesBE objInsertOrUpdate)
        {
			return;
        } 

        public void InsertOrUpdate(List<OutcomesBE> listObjInsertOrUpdate)
        {
			return;
        } 

        public void DeleteWhere(System.Linq.Expressions.Expression<Func<OutcomesBE,bool>> Filtro)
        {
            var DataContextObject = GetDataContextObject();
		Delete(GetWhere(Filtro));
        }

        public void Delete(OutcomesBE objDelete)
        {
			return;
        }

        public void Delete(List<OutcomesBE> listObjDelete)
        {
			return;
        }

        public void TryDeleteWhere(System.Linq.Expressions.Expression<Func<OutcomesBE,bool>> Filtro)
        {
            var DataContextObject = GetDataContextObject();
		TryDelete(GetWhere(Filtro));
        }

        public void TryDelete(OutcomesBE objDelete)
        {
			return;
        }

        public void TryDelete(List<OutcomesBE> listObjDelete)
        {
			return;
        }

        public bool Exists(OutcomesBE objExists)
        {
			return false;
        }

        public void Update(OutcomesBE objUpdate)
        {
			return;
        }

        public void Update(List<OutcomesBE> listObjUpdate)
        {
			return;
        }
    }
}
