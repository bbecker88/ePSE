using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RubricOn.Models.RubricOn.Entities; 
using  RubricOn.Models.RubricOn;

namespace RubricOn.Models.RubricOn.Repository
{
    public partial class ResultadosRubricasRepository 
    {
        String connectionString = "";
	  RubricOnDataContext DataContextObjectType;

        private RubricOnDataContext GetDataContextObject()
        {
            return DataContextFactory.GetWebRequestScopedDataContext<RubricOnDataContext>(null, connectionString);
        }

        public ResultadosRubricasRepository(String connectionString)
        {
            this.connectionString = connectionString;
        }

        private IQueryable<ResultadosRubricasBE> GetQueryable()
        {
            var DataContextObject = GetDataContextObject();
            var ResultadosRubricas= from x in DataContextObject.ResultadosRubricas
                             select new ResultadosRubricasBE
                             {
						EvaluacionId = x.EvaluacionId,
						Resultado = x.Resultado,
						ExtraEvaluacion = RubricOnRepositoryFactory.GetEvaluacionesRepository().GetLinq(x.Evaluaciones),
                             };
            return ResultadosRubricas;
        }

        private ResultadosRubricasBE GetLinqFK(ResultadosRubricas DataContextObject)
        {
		if(DataContextObject==null)
			return null;
            return new ResultadosRubricasBE()
                {
						EvaluacionId = DataContextObject.EvaluacionId,
						Resultado = DataContextObject.Resultado,
						ExtraEvaluacion = RubricOnRepositoryFactory.GetEvaluacionesRepository().GetLinq(DataContextObject.Evaluaciones),
                };
        }

        public ResultadosRubricasBE GetLinq(ResultadosRubricas DataContextObject)
        {
		if(DataContextObject==null)
			return null;
            return new ResultadosRubricasBE()
                {
				EvaluacionId = DataContextObject.EvaluacionId,
				Resultado = DataContextObject.Resultado,
				ExtraEvaluacion = new EvaluacionesBE() { EvaluacionId= DataContextObject.EvaluacionId},
                };
        }

        public List<ResultadosRubricasBE> GetAll()
        {
            return GetQueryable().ToList();
        }

        public List<ResultadosRubricasBE> GetWhere(System.Linq.Expressions.Expression<Func<ResultadosRubricasBE,bool>> Where)
	        {
            return GetQueryable().Where(Where).ToList();
        }

        public List<ResultadosRubricasBE> GetWhere<T>(System.Linq.Expressions.Expression<Func<ResultadosRubricasBE,bool>> Where,System.Linq.Expressions.Expression<Func<ResultadosRubricasBE,T>> OrderBy)
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

        public ResultadosRubricasBE GetOne(Int32 EvaluacionId)
        {
            	return GetQueryable().SingleOrDefault(x =>  x.EvaluacionId == EvaluacionId);
        }

        public Int32 GetLastId()
        {
            		return GetQueryable().Max(x => x.EvaluacionId);
        }

        public bool InsertIdentity(ResultadosRubricasBE objInsert, bool ThrowException)
        {
		var DataContextObject = GetDataContextObject();
		ResultadosRubricas objInsertLinq = new ResultadosRubricas();
			objInsertLinq.EvaluacionId = objInsert.EvaluacionId;
			objInsertLinq.Resultado = objInsert.Resultado;
		DataContextObject.ResultadosRubricas.InsertOnSubmit(objInsertLinq);
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

        public void Insert(ResultadosRubricasBE objInsert)
        {
		var DataContextObject = GetDataContextObject();
		ResultadosRubricas objInsertLinq = new ResultadosRubricas();
			objInsertLinq.EvaluacionId = objInsert.EvaluacionId;
			objInsertLinq.Resultado = objInsert.Resultado;
		DataContextObject.ResultadosRubricas.InsertOnSubmit(objInsertLinq);
        }

        public void Insert(List<ResultadosRubricasBE> listObjInsert)
        {
		var DataContextObject = GetDataContextObject();
		foreach(var objInsert in listObjInsert)
		{
		ResultadosRubricas objInsertLinq = new ResultadosRubricas();
			objInsertLinq.EvaluacionId = objInsert.EvaluacionId;
			objInsertLinq.Resultado = objInsert.Resultado;
			DataContextObject.ResultadosRubricas.InsertOnSubmit(objInsertLinq);
		}		
        }

        public void InsertOrUpdate(ResultadosRubricasBE objInsertOrUpdate)
        {
			var DataContextObject = GetDataContextObject();
			var existentObj = DataContextObject.ResultadosRubricas.SingleOrDefault(x =>  x.EvaluacionId == objInsertOrUpdate.EvaluacionId);
            	if (existentObj == null)
              	Insert(objInsertOrUpdate);
            	else
                	Update(objInsertOrUpdate);	
        } 

        public void InsertOrUpdate(List<ResultadosRubricasBE> listObjInsertOrUpdate)
        {
			foreach(var objInsertOrUpdate in listObjInsertOrUpdate)
			{
				InsertOrUpdate(objInsertOrUpdate);
			}	
        } 

        public void DeleteWhere(System.Linq.Expressions.Expression<Func<ResultadosRubricasBE,bool>> Filtro)
        {
            var DataContextObject = GetDataContextObject();
		Delete(GetWhere(Filtro));
        }

        public void Delete(ResultadosRubricasBE objDelete)
        {
		var DataContextObject = GetDataContextObject();
            var objDeleteLinq = DataContextObject.ResultadosRubricas.Single(x =>  x.EvaluacionId == objDelete.EvaluacionId);
		DataContextObject.ResultadosRubricas.DeleteOnSubmit(objDeleteLinq);
        }

        public void Delete(List<ResultadosRubricasBE> listObjDelete)
        {
		var DataContextObject = GetDataContextObject();
		foreach(var objDelete in listObjDelete)
		{
            	var objDeleteLinq = DataContextObject.ResultadosRubricas.Single(x =>  x.EvaluacionId == objDelete.EvaluacionId);
			DataContextObject.ResultadosRubricas.DeleteOnSubmit(objDeleteLinq);
		}
        }

        public void TryDeleteWhere(System.Linq.Expressions.Expression<Func<ResultadosRubricasBE,bool>> Filtro)
        {
            var DataContextObject = GetDataContextObject();
		TryDelete(GetWhere(Filtro));
        }

        public void TryDelete(ResultadosRubricasBE objDelete)
        {
		var DataContextObject = GetDataContextObject();
            var objDeleteLinq = DataContextObject.ResultadosRubricas.SingleOrDefault(x =>  x.EvaluacionId == objDelete.EvaluacionId);
		if(objDeleteLinq !=null)
			DataContextObject.ResultadosRubricas.DeleteOnSubmit(objDeleteLinq);
        }

        public void TryDelete(List<ResultadosRubricasBE> listObjDelete)
        {
		var DataContextObject = GetDataContextObject();
		foreach(var objDelete in listObjDelete)
		{
            	var objDeleteLinq = DataContextObject.ResultadosRubricas.SingleOrDefault(x =>  x.EvaluacionId == objDelete.EvaluacionId);
			if(objDeleteLinq !=null)			
				DataContextObject.ResultadosRubricas.DeleteOnSubmit(objDeleteLinq);
		}
        }

        public bool Exists(ResultadosRubricasBE objExists)
        {
		var DataContextObject = GetDataContextObject();
            return DataContextObject.ResultadosRubricas.Any(x =>  x.EvaluacionId == objExists.EvaluacionId);
        }

        public void Update(ResultadosRubricasBE objUpdate)
        {
		var DataContextObject = GetDataContextObject();
            var objUpdateLinq = DataContextObject.ResultadosRubricas.Single(x =>  x.EvaluacionId == objUpdate.EvaluacionId);
			objUpdateLinq.EvaluacionId = objUpdate.EvaluacionId;
			objUpdateLinq.Resultado = objUpdate.Resultado;
        }

        public void Update(List<ResultadosRubricasBE> listObjUpdate)
        {
		var DataContextObject = GetDataContextObject();
		foreach(var objUpdate in listObjUpdate)
		{
            	var objUpdateLinq = DataContextObject.ResultadosRubricas.Single(x =>  x.EvaluacionId == objUpdate.EvaluacionId);
			objUpdateLinq.EvaluacionId = objUpdate.EvaluacionId;
			objUpdateLinq.Resultado = objUpdate.Resultado;
		}
        }
    }
}
