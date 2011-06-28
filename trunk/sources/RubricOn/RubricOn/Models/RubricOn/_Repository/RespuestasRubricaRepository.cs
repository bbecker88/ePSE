using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RubricOn.Models.RubricOn.Entities; 
using  RubricOn.Models.RubricOn;

namespace RubricOn.Models.RubricOn.Repository
{
    public partial class RespuestasRubricaRepository 
    {
        String connectionString = "";
	  RubricOnDataContext DataContextObjectType;

        private RubricOnDataContext GetDataContextObject()
        {
            return DataContextFactory.GetWebRequestScopedDataContext<RubricOnDataContext>(null, connectionString);
        }

        public RespuestasRubricaRepository(String connectionString)
        {
            this.connectionString = connectionString;
        }

        private IQueryable<RespuestasRubricaBE> GetQueryable()
        {
            var DataContextObject = GetDataContextObject();
            var RespuestasRubrica= from x in DataContextObject.RespuestasRubrica
                             select new RespuestasRubricaBE
                             {
						CriterioRubricaId = x.CriterioRubricaId,
						EvaluacionId = x.EvaluacionId,
						ExtraCriterioRubrica = RubricOnRepositoryFactory.GetCriterioRubricaRepository().GetLinq(x.CriterioRubrica),
						ExtraEvaluacion = RubricOnRepositoryFactory.GetEvaluacionesRepository().GetLinq(x.Evaluaciones),
                             };
            return RespuestasRubrica;
        }

        private RespuestasRubricaBE GetLinqFK(RespuestasRubrica DataContextObject)
        {
		if(DataContextObject==null)
			return null;
            return new RespuestasRubricaBE()
                {
						CriterioRubricaId = DataContextObject.CriterioRubricaId,
						EvaluacionId = DataContextObject.EvaluacionId,
						ExtraCriterioRubrica = RubricOnRepositoryFactory.GetCriterioRubricaRepository().GetLinq(DataContextObject.CriterioRubrica),
						ExtraEvaluacion = RubricOnRepositoryFactory.GetEvaluacionesRepository().GetLinq(DataContextObject.Evaluaciones),
                };
        }

        public RespuestasRubricaBE GetLinq(RespuestasRubrica DataContextObject)
        {
		if(DataContextObject==null)
			return null;
            return new RespuestasRubricaBE()
                {
				CriterioRubricaId = DataContextObject.CriterioRubricaId,
				EvaluacionId = DataContextObject.EvaluacionId,
				ExtraCriterioRubrica = new CriterioRubricaBE() { CriterioRubricaId= DataContextObject.CriterioRubricaId},
				ExtraEvaluacion = new EvaluacionesBE() { EvaluacionId= DataContextObject.EvaluacionId},
                };
        }

        public List<RespuestasRubricaBE> GetAll()
        {
            return GetQueryable().ToList();
        }

        public List<RespuestasRubricaBE> GetWhere(System.Linq.Expressions.Expression<Func<RespuestasRubricaBE,bool>> Where)
	        {
            return GetQueryable().Where(Where).ToList();
        }

        public List<RespuestasRubricaBE> GetWhere<T>(System.Linq.Expressions.Expression<Func<RespuestasRubricaBE,bool>> Where,System.Linq.Expressions.Expression<Func<RespuestasRubricaBE,T>> OrderBy)
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

        public RespuestasRubricaBE GetOne(Int32 CriterioRubricaId, Int32 EvaluacionId)
        {
            	return GetQueryable().SingleOrDefault(x =>  x.CriterioRubricaId == CriterioRubricaId  && x.EvaluacionId == EvaluacionId);
        }

        public Int32 GetLastId()
        {
            		return GetQueryable().Max(x => x.CriterioRubricaId);
        }

        public bool InsertIdentity(RespuestasRubricaBE objInsert, bool ThrowException)
        {
		var DataContextObject = GetDataContextObject();
		RespuestasRubrica objInsertLinq = new RespuestasRubrica();
			objInsertLinq.CriterioRubricaId = objInsert.CriterioRubricaId;
			objInsertLinq.EvaluacionId = objInsert.EvaluacionId;
		DataContextObject.RespuestasRubrica.InsertOnSubmit(objInsertLinq);
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

        public void Insert(RespuestasRubricaBE objInsert)
        {
		var DataContextObject = GetDataContextObject();
		RespuestasRubrica objInsertLinq = new RespuestasRubrica();
			objInsertLinq.CriterioRubricaId = objInsert.CriterioRubricaId;
			objInsertLinq.EvaluacionId = objInsert.EvaluacionId;
		DataContextObject.RespuestasRubrica.InsertOnSubmit(objInsertLinq);
        }

        public void Insert(List<RespuestasRubricaBE> listObjInsert)
        {
		var DataContextObject = GetDataContextObject();
		foreach(var objInsert in listObjInsert)
		{
		RespuestasRubrica objInsertLinq = new RespuestasRubrica();
			objInsertLinq.CriterioRubricaId = objInsert.CriterioRubricaId;
			objInsertLinq.EvaluacionId = objInsert.EvaluacionId;
			DataContextObject.RespuestasRubrica.InsertOnSubmit(objInsertLinq);
		}		
        }

        public void InsertOrUpdate(RespuestasRubricaBE objInsertOrUpdate)
        {
			var DataContextObject = GetDataContextObject();
			var existentObj = DataContextObject.RespuestasRubrica.SingleOrDefault(x =>  x.CriterioRubricaId == objInsertOrUpdate.CriterioRubricaId  && x.EvaluacionId == objInsertOrUpdate.EvaluacionId);
            	if (existentObj == null)
              	Insert(objInsertOrUpdate);
            	else
                	Update(objInsertOrUpdate);	
        } 

        public void InsertOrUpdate(List<RespuestasRubricaBE> listObjInsertOrUpdate)
        {
			foreach(var objInsertOrUpdate in listObjInsertOrUpdate)
			{
				InsertOrUpdate(objInsertOrUpdate);
			}	
        } 

        public void DeleteWhere(System.Linq.Expressions.Expression<Func<RespuestasRubricaBE,bool>> Filtro)
        {
            var DataContextObject = GetDataContextObject();
		Delete(GetWhere(Filtro));
        }

        public void Delete(RespuestasRubricaBE objDelete)
        {
		var DataContextObject = GetDataContextObject();
            var objDeleteLinq = DataContextObject.RespuestasRubrica.Single(x =>  x.CriterioRubricaId == objDelete.CriterioRubricaId  && x.EvaluacionId == objDelete.EvaluacionId);
		DataContextObject.RespuestasRubrica.DeleteOnSubmit(objDeleteLinq);
        }

        public void Delete(List<RespuestasRubricaBE> listObjDelete)
        {
		var DataContextObject = GetDataContextObject();
		foreach(var objDelete in listObjDelete)
		{
            	var objDeleteLinq = DataContextObject.RespuestasRubrica.Single(x =>  x.CriterioRubricaId == objDelete.CriterioRubricaId  && x.EvaluacionId == objDelete.EvaluacionId);
			DataContextObject.RespuestasRubrica.DeleteOnSubmit(objDeleteLinq);
		}
        }

        public void TryDeleteWhere(System.Linq.Expressions.Expression<Func<RespuestasRubricaBE,bool>> Filtro)
        {
            var DataContextObject = GetDataContextObject();
		TryDelete(GetWhere(Filtro));
        }

        public void TryDelete(RespuestasRubricaBE objDelete)
        {
		var DataContextObject = GetDataContextObject();
            var objDeleteLinq = DataContextObject.RespuestasRubrica.SingleOrDefault(x =>  x.CriterioRubricaId == objDelete.CriterioRubricaId  && x.EvaluacionId == objDelete.EvaluacionId);
		if(objDeleteLinq !=null)
			DataContextObject.RespuestasRubrica.DeleteOnSubmit(objDeleteLinq);
        }

        public void TryDelete(List<RespuestasRubricaBE> listObjDelete)
        {
		var DataContextObject = GetDataContextObject();
		foreach(var objDelete in listObjDelete)
		{
            	var objDeleteLinq = DataContextObject.RespuestasRubrica.SingleOrDefault(x =>  x.CriterioRubricaId == objDelete.CriterioRubricaId  && x.EvaluacionId == objDelete.EvaluacionId);
			if(objDeleteLinq !=null)			
				DataContextObject.RespuestasRubrica.DeleteOnSubmit(objDeleteLinq);
		}
        }

        public bool Exists(RespuestasRubricaBE objExists)
        {
		var DataContextObject = GetDataContextObject();
            return DataContextObject.RespuestasRubrica.Any(x =>  x.CriterioRubricaId == objExists.CriterioRubricaId  && x.EvaluacionId == objExists.EvaluacionId);
        }

        public void Update(RespuestasRubricaBE objUpdate)
        {
		var DataContextObject = GetDataContextObject();
            var objUpdateLinq = DataContextObject.RespuestasRubrica.Single(x =>  x.CriterioRubricaId == objUpdate.CriterioRubricaId  && x.EvaluacionId == objUpdate.EvaluacionId);
			objUpdateLinq.CriterioRubricaId = objUpdate.CriterioRubricaId;
			objUpdateLinq.EvaluacionId = objUpdate.EvaluacionId;
        }

        public void Update(List<RespuestasRubricaBE> listObjUpdate)
        {
		var DataContextObject = GetDataContextObject();
		foreach(var objUpdate in listObjUpdate)
		{
            	var objUpdateLinq = DataContextObject.RespuestasRubrica.Single(x =>  x.CriterioRubricaId == objUpdate.CriterioRubricaId  && x.EvaluacionId == objUpdate.EvaluacionId);
			objUpdateLinq.CriterioRubricaId = objUpdate.CriterioRubricaId;
			objUpdateLinq.EvaluacionId = objUpdate.EvaluacionId;
		}
        }
    }
}
