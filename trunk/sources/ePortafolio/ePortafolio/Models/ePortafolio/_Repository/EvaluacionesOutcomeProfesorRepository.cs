using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ePortafolio.Models.ePortafolio.Entities; 
using  ePortafolio.Models.ePortafolio;

namespace ePortafolio.Models.ePortafolio.Repository
{
    public partial class EvaluacionesOutcomeProfesorRepository 
    {
        String connectionString = "";
	  ePortafolioDataContext DataContextObjectType;

        private ePortafolioDataContext GetDataContextObject()
        {
            return DataContextFactory.GetWebRequestScopedDataContext<ePortafolioDataContext>(null, connectionString);
        }

        public EvaluacionesOutcomeProfesorRepository(String connectionString)
        {
            this.connectionString = connectionString;
        }

        private IQueryable<EvaluacionesOutcomeProfesorBE> GetQueryable()
        {
            var DataContextObject = GetDataContextObject();
            var EvaluacionesOutcomeProfesor= from x in DataContextObject.EvaluacionesOutcomeProfesor
                             select new EvaluacionesOutcomeProfesorBE
                             {
						AlumnoId = x.AlumnoId,
						EvaluacionId = x.EvaluacionId,
						Nota = x.Nota,
						OutcomeId = x.OutcomeId,
						PeriodoId = x.PeriodoId,
						ProfesorId = x.ProfesorId,
                             };
            return EvaluacionesOutcomeProfesor;
        }

        private EvaluacionesOutcomeProfesorBE GetLinqFK(EvaluacionesOutcomeProfesor DataContextObject)
        {
		if(DataContextObject==null)
			return null;
            return new EvaluacionesOutcomeProfesorBE()
                {
						AlumnoId = DataContextObject.AlumnoId,
						EvaluacionId = DataContextObject.EvaluacionId,
						Nota = DataContextObject.Nota,
						OutcomeId = DataContextObject.OutcomeId,
						PeriodoId = DataContextObject.PeriodoId,
						ProfesorId = DataContextObject.ProfesorId,
                };
        }

        public EvaluacionesOutcomeProfesorBE GetLinq(EvaluacionesOutcomeProfesor DataContextObject)
        {
		if(DataContextObject==null)
			return null;
            return new EvaluacionesOutcomeProfesorBE()
                {
				AlumnoId = DataContextObject.AlumnoId,
				EvaluacionId = DataContextObject.EvaluacionId,
				Nota = DataContextObject.Nota,
				OutcomeId = DataContextObject.OutcomeId,
				PeriodoId = DataContextObject.PeriodoId,
				ProfesorId = DataContextObject.ProfesorId,
                };
        }

        public List<EvaluacionesOutcomeProfesorBE> GetAll()
        {
            return GetQueryable().ToList();
        }

        public List<EvaluacionesOutcomeProfesorBE> GetWhere(System.Linq.Expressions.Expression<Func<EvaluacionesOutcomeProfesorBE,bool>> Where)
	        {
            return GetQueryable().Where(Where).ToList();
        }

        public List<EvaluacionesOutcomeProfesorBE> GetWhere<T>(System.Linq.Expressions.Expression<Func<EvaluacionesOutcomeProfesorBE,bool>> Where,System.Linq.Expressions.Expression<Func<EvaluacionesOutcomeProfesorBE,T>> OrderBy)
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

        public EvaluacionesOutcomeProfesorBE GetOne(String AlumnoId, Int32 OutcomeId, String PeriodoId, String ProfesorId)
        {
            	return GetQueryable().SingleOrDefault(x =>  x.AlumnoId == AlumnoId  && x.OutcomeId == OutcomeId  && x.PeriodoId == PeriodoId  && x.ProfesorId == ProfesorId);
        }

        public Int32 GetLastId()
        {
            		return 0;
        }

        public bool InsertIdentity(EvaluacionesOutcomeProfesorBE objInsert, bool ThrowException)
        {
		var DataContextObject = GetDataContextObject();
		EvaluacionesOutcomeProfesor objInsertLinq = new EvaluacionesOutcomeProfesor();
			objInsertLinq.AlumnoId = objInsert.AlumnoId;
			objInsertLinq.EvaluacionId = objInsert.EvaluacionId;
			objInsertLinq.Nota = objInsert.Nota;
			objInsertLinq.OutcomeId = objInsert.OutcomeId;
			objInsertLinq.PeriodoId = objInsert.PeriodoId;
			objInsertLinq.ProfesorId = objInsert.ProfesorId;
		DataContextObject.EvaluacionesOutcomeProfesor.InsertOnSubmit(objInsertLinq);
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

        public void Insert(EvaluacionesOutcomeProfesorBE objInsert)
        {
		var DataContextObject = GetDataContextObject();
		EvaluacionesOutcomeProfesor objInsertLinq = new EvaluacionesOutcomeProfesor();
			objInsertLinq.AlumnoId = objInsert.AlumnoId;
			objInsertLinq.EvaluacionId = objInsert.EvaluacionId;
			objInsertLinq.Nota = objInsert.Nota;
			objInsertLinq.OutcomeId = objInsert.OutcomeId;
			objInsertLinq.PeriodoId = objInsert.PeriodoId;
			objInsertLinq.ProfesorId = objInsert.ProfesorId;
		DataContextObject.EvaluacionesOutcomeProfesor.InsertOnSubmit(objInsertLinq);
        }

        public void Insert(List<EvaluacionesOutcomeProfesorBE> listObjInsert)
        {
		var DataContextObject = GetDataContextObject();
		foreach(var objInsert in listObjInsert)
		{
		EvaluacionesOutcomeProfesor objInsertLinq = new EvaluacionesOutcomeProfesor();
			objInsertLinq.AlumnoId = objInsert.AlumnoId;
			objInsertLinq.EvaluacionId = objInsert.EvaluacionId;
			objInsertLinq.Nota = objInsert.Nota;
			objInsertLinq.OutcomeId = objInsert.OutcomeId;
			objInsertLinq.PeriodoId = objInsert.PeriodoId;
			objInsertLinq.ProfesorId = objInsert.ProfesorId;
			DataContextObject.EvaluacionesOutcomeProfesor.InsertOnSubmit(objInsertLinq);
		}		
        }

        public void InsertOrUpdate(EvaluacionesOutcomeProfesorBE objInsertOrUpdate)
        {
			var DataContextObject = GetDataContextObject();
			var existentObj = DataContextObject.EvaluacionesOutcomeProfesor.SingleOrDefault(x =>  x.AlumnoId == objInsertOrUpdate.AlumnoId  && x.OutcomeId == objInsertOrUpdate.OutcomeId  && x.PeriodoId == objInsertOrUpdate.PeriodoId  && x.ProfesorId == objInsertOrUpdate.ProfesorId);
            	if (existentObj == null)
              	Insert(objInsertOrUpdate);
            	else
                	Update(objInsertOrUpdate);	
        } 

        public void InsertOrUpdate(List<EvaluacionesOutcomeProfesorBE> listObjInsertOrUpdate)
        {
			foreach(var objInsertOrUpdate in listObjInsertOrUpdate)
			{
				InsertOrUpdate(objInsertOrUpdate);
			}	
        } 

        public void DeleteWhere(System.Linq.Expressions.Expression<Func<EvaluacionesOutcomeProfesorBE,bool>> Filtro)
        {
            var DataContextObject = GetDataContextObject();
		Delete(GetWhere(Filtro));
        }

        public void Delete(EvaluacionesOutcomeProfesorBE objDelete)
        {
		var DataContextObject = GetDataContextObject();
            var objDeleteLinq = DataContextObject.EvaluacionesOutcomeProfesor.Single(x =>  x.AlumnoId == objDelete.AlumnoId  && x.OutcomeId == objDelete.OutcomeId  && x.PeriodoId == objDelete.PeriodoId  && x.ProfesorId == objDelete.ProfesorId);
		DataContextObject.EvaluacionesOutcomeProfesor.DeleteOnSubmit(objDeleteLinq);
        }

        public void Delete(List<EvaluacionesOutcomeProfesorBE> listObjDelete)
        {
		var DataContextObject = GetDataContextObject();
		foreach(var objDelete in listObjDelete)
		{
            	var objDeleteLinq = DataContextObject.EvaluacionesOutcomeProfesor.Single(x =>  x.AlumnoId == objDelete.AlumnoId  && x.OutcomeId == objDelete.OutcomeId  && x.PeriodoId == objDelete.PeriodoId  && x.ProfesorId == objDelete.ProfesorId);
			DataContextObject.EvaluacionesOutcomeProfesor.DeleteOnSubmit(objDeleteLinq);
		}
        }

        public void TryDeleteWhere(System.Linq.Expressions.Expression<Func<EvaluacionesOutcomeProfesorBE,bool>> Filtro)
        {
            var DataContextObject = GetDataContextObject();
		TryDelete(GetWhere(Filtro));
        }

        public void TryDelete(EvaluacionesOutcomeProfesorBE objDelete)
        {
		var DataContextObject = GetDataContextObject();
            var objDeleteLinq = DataContextObject.EvaluacionesOutcomeProfesor.SingleOrDefault(x =>  x.AlumnoId == objDelete.AlumnoId  && x.OutcomeId == objDelete.OutcomeId  && x.PeriodoId == objDelete.PeriodoId  && x.ProfesorId == objDelete.ProfesorId);
		if(objDeleteLinq !=null)
			DataContextObject.EvaluacionesOutcomeProfesor.DeleteOnSubmit(objDeleteLinq);
        }

        public void TryDelete(List<EvaluacionesOutcomeProfesorBE> listObjDelete)
        {
		var DataContextObject = GetDataContextObject();
		foreach(var objDelete in listObjDelete)
		{
            	var objDeleteLinq = DataContextObject.EvaluacionesOutcomeProfesor.SingleOrDefault(x =>  x.AlumnoId == objDelete.AlumnoId  && x.OutcomeId == objDelete.OutcomeId  && x.PeriodoId == objDelete.PeriodoId  && x.ProfesorId == objDelete.ProfesorId);
			if(objDeleteLinq !=null)			
				DataContextObject.EvaluacionesOutcomeProfesor.DeleteOnSubmit(objDeleteLinq);
		}
        }

        public bool Exists(EvaluacionesOutcomeProfesorBE objExists)
        {
		var DataContextObject = GetDataContextObject();
            return DataContextObject.EvaluacionesOutcomeProfesor.Any(x =>  x.AlumnoId == objExists.AlumnoId  && x.OutcomeId == objExists.OutcomeId  && x.PeriodoId == objExists.PeriodoId  && x.ProfesorId == objExists.ProfesorId);
        }

        public void Update(EvaluacionesOutcomeProfesorBE objUpdate)
        {
		var DataContextObject = GetDataContextObject();
            var objUpdateLinq = DataContextObject.EvaluacionesOutcomeProfesor.Single(x =>  x.AlumnoId == objUpdate.AlumnoId  && x.OutcomeId == objUpdate.OutcomeId  && x.PeriodoId == objUpdate.PeriodoId  && x.ProfesorId == objUpdate.ProfesorId);
			objUpdateLinq.AlumnoId = objUpdate.AlumnoId;
			objUpdateLinq.EvaluacionId = objUpdate.EvaluacionId;
			objUpdateLinq.Nota = objUpdate.Nota;
			objUpdateLinq.OutcomeId = objUpdate.OutcomeId;
			objUpdateLinq.PeriodoId = objUpdate.PeriodoId;
			objUpdateLinq.ProfesorId = objUpdate.ProfesorId;
        }

        public void Update(List<EvaluacionesOutcomeProfesorBE> listObjUpdate)
        {
		var DataContextObject = GetDataContextObject();
		foreach(var objUpdate in listObjUpdate)
		{
            	var objUpdateLinq = DataContextObject.EvaluacionesOutcomeProfesor.Single(x =>  x.AlumnoId == objUpdate.AlumnoId  && x.OutcomeId == objUpdate.OutcomeId  && x.PeriodoId == objUpdate.PeriodoId  && x.ProfesorId == objUpdate.ProfesorId);
			objUpdateLinq.AlumnoId = objUpdate.AlumnoId;
			objUpdateLinq.EvaluacionId = objUpdate.EvaluacionId;
			objUpdateLinq.Nota = objUpdate.Nota;
			objUpdateLinq.OutcomeId = objUpdate.OutcomeId;
			objUpdateLinq.PeriodoId = objUpdate.PeriodoId;
			objUpdateLinq.ProfesorId = objUpdate.ProfesorId;
		}
        }
    }
}
