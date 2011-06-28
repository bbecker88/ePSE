using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RubricOn.Models.RubricOn.Entities; 
using  RubricOn.Models.RubricOn;

namespace RubricOn.Models.RubricOn.Repository
{
    public partial class EvaluacionesRepository 
    {
        String connectionString = "";
	  RubricOnDataContext DataContextObjectType;

        private RubricOnDataContext GetDataContextObject()
        {
            return DataContextFactory.GetWebRequestScopedDataContext<RubricOnDataContext>(null, connectionString);
        }

        public EvaluacionesRepository(String connectionString)
        {
            this.connectionString = connectionString;
        }

        private IQueryable<EvaluacionesBE> GetQueryable()
        {
            var DataContextObject = GetDataContextObject();
            var Evaluaciones= from x in DataContextObject.Evaluaciones
                             select new EvaluacionesBE
                             {
						CodigoEvaluadoId = x.CodigoEvaluadoId,
						CodigoEvaluadorId = x.CodigoEvaluadorId,
						EvaluacionId = x.EvaluacionId,
						FechaEvaluacion = x.FechaEvaluacion,
						RubricaId = x.RubricaId,
						TipoArtefacto = x.TipoArtefacto,
						Version = x.Version,
                             };
            return Evaluaciones;
        }

        private EvaluacionesBE GetLinqFK(Evaluaciones DataContextObject)
        {
		if(DataContextObject==null)
			return null;
            return new EvaluacionesBE()
                {
						CodigoEvaluadoId = DataContextObject.CodigoEvaluadoId,
						CodigoEvaluadorId = DataContextObject.CodigoEvaluadorId,
						EvaluacionId = DataContextObject.EvaluacionId,
						FechaEvaluacion = DataContextObject.FechaEvaluacion,
						RubricaId = DataContextObject.RubricaId,
						TipoArtefacto = DataContextObject.TipoArtefacto,
						Version = DataContextObject.Version,
                };
        }

        public EvaluacionesBE GetLinq(Evaluaciones DataContextObject)
        {
		if(DataContextObject==null)
			return null;
            return new EvaluacionesBE()
                {
				CodigoEvaluadoId = DataContextObject.CodigoEvaluadoId,
				CodigoEvaluadorId = DataContextObject.CodigoEvaluadorId,
				EvaluacionId = DataContextObject.EvaluacionId,
				FechaEvaluacion = DataContextObject.FechaEvaluacion,
				RubricaId = DataContextObject.RubricaId,
				TipoArtefacto = DataContextObject.TipoArtefacto,
				Version = DataContextObject.Version,
                };
        }

        public List<EvaluacionesBE> GetAll()
        {
            return GetQueryable().ToList();
        }

        public List<EvaluacionesBE> GetWhere(System.Linq.Expressions.Expression<Func<EvaluacionesBE,bool>> Where)
	        {
            return GetQueryable().Where(Where).ToList();
        }

        public List<EvaluacionesBE> GetWhere<T>(System.Linq.Expressions.Expression<Func<EvaluacionesBE,bool>> Where,System.Linq.Expressions.Expression<Func<EvaluacionesBE,T>> OrderBy)
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

        public EvaluacionesBE GetOne(Int32 EvaluacionId)
        {
            	return GetQueryable().SingleOrDefault(x =>  x.EvaluacionId == EvaluacionId);
        }

        public Int32 GetLastId()
        {
            		return GetQueryable().Max(x => x.EvaluacionId);
        }

        public bool InsertIdentity(EvaluacionesBE objInsert, bool ThrowException)
        {
		var DataContextObject = GetDataContextObject();
		Evaluaciones objInsertLinq = new Evaluaciones();
			objInsertLinq.CodigoEvaluadoId = objInsert.CodigoEvaluadoId;
			objInsertLinq.CodigoEvaluadorId = objInsert.CodigoEvaluadorId;
			objInsertLinq.EvaluacionId = objInsert.EvaluacionId;
			objInsertLinq.FechaEvaluacion = objInsert.FechaEvaluacion;
			objInsertLinq.RubricaId = objInsert.RubricaId;
			objInsertLinq.TipoArtefacto = objInsert.TipoArtefacto;
			objInsertLinq.Version = objInsert.Version;
		DataContextObject.Evaluaciones.InsertOnSubmit(objInsertLinq);
		try
            	{	
		  DataContextObject.SubmitChanges();
		  objInsert.EvaluacionId = objInsertLinq.EvaluacionId;
                return true;
            	}
            	catch (Exception Ex)
            	{
                if (ThrowException)
                    throw Ex;
                return false;
            	}
        }

        public void Insert(EvaluacionesBE objInsert)
        {
		var DataContextObject = GetDataContextObject();
		Evaluaciones objInsertLinq = new Evaluaciones();
			objInsertLinq.CodigoEvaluadoId = objInsert.CodigoEvaluadoId;
			objInsertLinq.CodigoEvaluadorId = objInsert.CodigoEvaluadorId;
			objInsertLinq.EvaluacionId = objInsert.EvaluacionId;
			objInsertLinq.FechaEvaluacion = objInsert.FechaEvaluacion;
			objInsertLinq.RubricaId = objInsert.RubricaId;
			objInsertLinq.TipoArtefacto = objInsert.TipoArtefacto;
			objInsertLinq.Version = objInsert.Version;
		DataContextObject.Evaluaciones.InsertOnSubmit(objInsertLinq);
        }

        public void Insert(List<EvaluacionesBE> listObjInsert)
        {
		var DataContextObject = GetDataContextObject();
		foreach(var objInsert in listObjInsert)
		{
		Evaluaciones objInsertLinq = new Evaluaciones();
			objInsertLinq.CodigoEvaluadoId = objInsert.CodigoEvaluadoId;
			objInsertLinq.CodigoEvaluadorId = objInsert.CodigoEvaluadorId;
			objInsertLinq.EvaluacionId = objInsert.EvaluacionId;
			objInsertLinq.FechaEvaluacion = objInsert.FechaEvaluacion;
			objInsertLinq.RubricaId = objInsert.RubricaId;
			objInsertLinq.TipoArtefacto = objInsert.TipoArtefacto;
			objInsertLinq.Version = objInsert.Version;
			DataContextObject.Evaluaciones.InsertOnSubmit(objInsertLinq);
		}		
        }

        public void InsertOrUpdate(EvaluacionesBE objInsertOrUpdate)
        {
			var DataContextObject = GetDataContextObject();
			var existentObj = DataContextObject.Evaluaciones.SingleOrDefault(x =>  x.EvaluacionId == objInsertOrUpdate.EvaluacionId);
            	if (existentObj == null)
              	Insert(objInsertOrUpdate);
            	else
                	Update(objInsertOrUpdate);	
        } 

        public void InsertOrUpdate(List<EvaluacionesBE> listObjInsertOrUpdate)
        {
			foreach(var objInsertOrUpdate in listObjInsertOrUpdate)
			{
				InsertOrUpdate(objInsertOrUpdate);
			}	
        } 

        public void DeleteWhere(System.Linq.Expressions.Expression<Func<EvaluacionesBE,bool>> Filtro)
        {
            var DataContextObject = GetDataContextObject();
		Delete(GetWhere(Filtro));
        }

        public void Delete(EvaluacionesBE objDelete)
        {
		var DataContextObject = GetDataContextObject();
            var objDeleteLinq = DataContextObject.Evaluaciones.Single(x =>  x.EvaluacionId == objDelete.EvaluacionId);
		DataContextObject.Evaluaciones.DeleteOnSubmit(objDeleteLinq);
        }

        public void Delete(List<EvaluacionesBE> listObjDelete)
        {
		var DataContextObject = GetDataContextObject();
		foreach(var objDelete in listObjDelete)
		{
            	var objDeleteLinq = DataContextObject.Evaluaciones.Single(x =>  x.EvaluacionId == objDelete.EvaluacionId);
			DataContextObject.Evaluaciones.DeleteOnSubmit(objDeleteLinq);
		}
        }

        public void TryDeleteWhere(System.Linq.Expressions.Expression<Func<EvaluacionesBE,bool>> Filtro)
        {
            var DataContextObject = GetDataContextObject();
		TryDelete(GetWhere(Filtro));
        }

        public void TryDelete(EvaluacionesBE objDelete)
        {
		var DataContextObject = GetDataContextObject();
            var objDeleteLinq = DataContextObject.Evaluaciones.SingleOrDefault(x =>  x.EvaluacionId == objDelete.EvaluacionId);
		if(objDeleteLinq !=null)
			DataContextObject.Evaluaciones.DeleteOnSubmit(objDeleteLinq);
        }

        public void TryDelete(List<EvaluacionesBE> listObjDelete)
        {
		var DataContextObject = GetDataContextObject();
		foreach(var objDelete in listObjDelete)
		{
            	var objDeleteLinq = DataContextObject.Evaluaciones.SingleOrDefault(x =>  x.EvaluacionId == objDelete.EvaluacionId);
			if(objDeleteLinq !=null)			
				DataContextObject.Evaluaciones.DeleteOnSubmit(objDeleteLinq);
		}
        }

        public bool Exists(EvaluacionesBE objExists)
        {
		var DataContextObject = GetDataContextObject();
            return DataContextObject.Evaluaciones.Any(x =>  x.EvaluacionId == objExists.EvaluacionId);
        }

        public void Update(EvaluacionesBE objUpdate)
        {
		var DataContextObject = GetDataContextObject();
            var objUpdateLinq = DataContextObject.Evaluaciones.Single(x =>  x.EvaluacionId == objUpdate.EvaluacionId);
			objUpdateLinq.CodigoEvaluadoId = objUpdate.CodigoEvaluadoId;
			objUpdateLinq.CodigoEvaluadorId = objUpdate.CodigoEvaluadorId;
			objUpdateLinq.EvaluacionId = objUpdate.EvaluacionId;
			objUpdateLinq.FechaEvaluacion = objUpdate.FechaEvaluacion;
			objUpdateLinq.RubricaId = objUpdate.RubricaId;
			objUpdateLinq.TipoArtefacto = objUpdate.TipoArtefacto;
			objUpdateLinq.Version = objUpdate.Version;
        }

        public void Update(List<EvaluacionesBE> listObjUpdate)
        {
		var DataContextObject = GetDataContextObject();
		foreach(var objUpdate in listObjUpdate)
		{
            	var objUpdateLinq = DataContextObject.Evaluaciones.Single(x =>  x.EvaluacionId == objUpdate.EvaluacionId);
			objUpdateLinq.CodigoEvaluadoId = objUpdate.CodigoEvaluadoId;
			objUpdateLinq.CodigoEvaluadorId = objUpdate.CodigoEvaluadorId;
			objUpdateLinq.EvaluacionId = objUpdate.EvaluacionId;
			objUpdateLinq.FechaEvaluacion = objUpdate.FechaEvaluacion;
			objUpdateLinq.RubricaId = objUpdate.RubricaId;
			objUpdateLinq.TipoArtefacto = objUpdate.TipoArtefacto;
			objUpdateLinq.Version = objUpdate.Version;
		}
        }
    }
}
