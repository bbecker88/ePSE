using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ePortafolio.Models.ePortafolio.Entities; 
using  ePortafolio.Models.ePortafolio;

namespace ePortafolio.Models.ePortafolio.Repository
{
    public partial class TrabajosOutcomeAlumnoRepository 
    {
        String connectionString = "";
	  ePortafolioDataContext DataContextObjectType;

        private ePortafolioDataContext GetDataContextObject()
        {
            return DataContextFactory.GetWebRequestScopedDataContext<ePortafolioDataContext>(null, connectionString);
        }

        public TrabajosOutcomeAlumnoRepository(String connectionString)
        {
            this.connectionString = connectionString;
        }

        private IQueryable<TrabajosOutcomeAlumnoBE> GetQueryable()
        {
            var DataContextObject = GetDataContextObject();
            var TrabajosOutcomeAlumno= from x in DataContextObject.TrabajosOutcomeAlumno
                             select new TrabajosOutcomeAlumnoBE
                             {
						AlumnoId = x.AlumnoId,
						OutcomeId = x.OutcomeId,
						TrabajoId = x.TrabajoId,
						ExtraTrabajo = ePortafolioRepositoryFactory.GetTrabajosRepository().GetLinq(x.Trabajos),
                             };
            return TrabajosOutcomeAlumno;
        }

        private TrabajosOutcomeAlumnoBE GetLinqFK(TrabajosOutcomeAlumno DataContextObject)
        {
		if(DataContextObject==null)
			return null;
            return new TrabajosOutcomeAlumnoBE()
                {
						AlumnoId = DataContextObject.AlumnoId,
						OutcomeId = DataContextObject.OutcomeId,
						TrabajoId = DataContextObject.TrabajoId,
						ExtraTrabajo = ePortafolioRepositoryFactory.GetTrabajosRepository().GetLinq(DataContextObject.Trabajos),
                };
        }

        public TrabajosOutcomeAlumnoBE GetLinq(TrabajosOutcomeAlumno DataContextObject)
        {
		if(DataContextObject==null)
			return null;
            return new TrabajosOutcomeAlumnoBE()
                {
				AlumnoId = DataContextObject.AlumnoId,
				OutcomeId = DataContextObject.OutcomeId,
				TrabajoId = DataContextObject.TrabajoId,
				ExtraTrabajo = new TrabajosBE() { TrabajoId= DataContextObject.TrabajoId},
                };
        }

        public List<TrabajosOutcomeAlumnoBE> GetAll()
        {
            return GetQueryable().ToList();
        }

        public List<TrabajosOutcomeAlumnoBE> GetWhere(System.Linq.Expressions.Expression<Func<TrabajosOutcomeAlumnoBE,bool>> Where)
	        {
            return GetQueryable().Where(Where).ToList();
        }

        public List<TrabajosOutcomeAlumnoBE> GetWhere<T>(System.Linq.Expressions.Expression<Func<TrabajosOutcomeAlumnoBE,bool>> Where,System.Linq.Expressions.Expression<Func<TrabajosOutcomeAlumnoBE,T>> OrderBy)
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

        public TrabajosOutcomeAlumnoBE GetOne(String AlumnoId, Int32 OutcomeId, Int32 TrabajoId)
        {
            	return GetQueryable().SingleOrDefault(x =>  x.AlumnoId == AlumnoId  && x.OutcomeId == OutcomeId  && x.TrabajoId == TrabajoId);
        }

        public Int32 GetLastId()
        {
            		return 0;
        }

        public bool InsertIdentity(TrabajosOutcomeAlumnoBE objInsert, bool ThrowException)
        {
		var DataContextObject = GetDataContextObject();
		TrabajosOutcomeAlumno objInsertLinq = new TrabajosOutcomeAlumno();
			objInsertLinq.AlumnoId = objInsert.AlumnoId;
			objInsertLinq.OutcomeId = objInsert.OutcomeId;
			objInsertLinq.TrabajoId = objInsert.TrabajoId;
		DataContextObject.TrabajosOutcomeAlumno.InsertOnSubmit(objInsertLinq);
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

        public void Insert(TrabajosOutcomeAlumnoBE objInsert)
        {
		var DataContextObject = GetDataContextObject();
		TrabajosOutcomeAlumno objInsertLinq = new TrabajosOutcomeAlumno();
			objInsertLinq.AlumnoId = objInsert.AlumnoId;
			objInsertLinq.OutcomeId = objInsert.OutcomeId;
			objInsertLinq.TrabajoId = objInsert.TrabajoId;
		DataContextObject.TrabajosOutcomeAlumno.InsertOnSubmit(objInsertLinq);
        }

        public void Insert(List<TrabajosOutcomeAlumnoBE> listObjInsert)
        {
		var DataContextObject = GetDataContextObject();
		foreach(var objInsert in listObjInsert)
		{
		TrabajosOutcomeAlumno objInsertLinq = new TrabajosOutcomeAlumno();
			objInsertLinq.AlumnoId = objInsert.AlumnoId;
			objInsertLinq.OutcomeId = objInsert.OutcomeId;
			objInsertLinq.TrabajoId = objInsert.TrabajoId;
			DataContextObject.TrabajosOutcomeAlumno.InsertOnSubmit(objInsertLinq);
		}		
        }

        public void InsertOrUpdate(TrabajosOutcomeAlumnoBE objInsertOrUpdate)
        {
			var DataContextObject = GetDataContextObject();
			var existentObj = DataContextObject.TrabajosOutcomeAlumno.SingleOrDefault(x =>  x.AlumnoId == objInsertOrUpdate.AlumnoId  && x.OutcomeId == objInsertOrUpdate.OutcomeId  && x.TrabajoId == objInsertOrUpdate.TrabajoId);
            	if (existentObj == null)
              	Insert(objInsertOrUpdate);
            	else
                	Update(objInsertOrUpdate);	
        } 

        public void InsertOrUpdate(List<TrabajosOutcomeAlumnoBE> listObjInsertOrUpdate)
        {
			foreach(var objInsertOrUpdate in listObjInsertOrUpdate)
			{
				InsertOrUpdate(objInsertOrUpdate);
			}	
        } 

        public void DeleteWhere(System.Linq.Expressions.Expression<Func<TrabajosOutcomeAlumnoBE,bool>> Filtro)
        {
            var DataContextObject = GetDataContextObject();
		Delete(GetWhere(Filtro));
        }

        public void Delete(TrabajosOutcomeAlumnoBE objDelete)
        {
		var DataContextObject = GetDataContextObject();
            var objDeleteLinq = DataContextObject.TrabajosOutcomeAlumno.Single(x =>  x.AlumnoId == objDelete.AlumnoId  && x.OutcomeId == objDelete.OutcomeId  && x.TrabajoId == objDelete.TrabajoId);
		DataContextObject.TrabajosOutcomeAlumno.DeleteOnSubmit(objDeleteLinq);
        }

        public void Delete(List<TrabajosOutcomeAlumnoBE> listObjDelete)
        {
		var DataContextObject = GetDataContextObject();
		foreach(var objDelete in listObjDelete)
		{
            	var objDeleteLinq = DataContextObject.TrabajosOutcomeAlumno.Single(x =>  x.AlumnoId == objDelete.AlumnoId  && x.OutcomeId == objDelete.OutcomeId  && x.TrabajoId == objDelete.TrabajoId);
			DataContextObject.TrabajosOutcomeAlumno.DeleteOnSubmit(objDeleteLinq);
		}
        }

        public void TryDeleteWhere(System.Linq.Expressions.Expression<Func<TrabajosOutcomeAlumnoBE,bool>> Filtro)
        {
            var DataContextObject = GetDataContextObject();
		TryDelete(GetWhere(Filtro));
        }

        public void TryDelete(TrabajosOutcomeAlumnoBE objDelete)
        {
		var DataContextObject = GetDataContextObject();
            var objDeleteLinq = DataContextObject.TrabajosOutcomeAlumno.SingleOrDefault(x =>  x.AlumnoId == objDelete.AlumnoId  && x.OutcomeId == objDelete.OutcomeId  && x.TrabajoId == objDelete.TrabajoId);
		if(objDeleteLinq !=null)
			DataContextObject.TrabajosOutcomeAlumno.DeleteOnSubmit(objDeleteLinq);
        }

        public void TryDelete(List<TrabajosOutcomeAlumnoBE> listObjDelete)
        {
		var DataContextObject = GetDataContextObject();
		foreach(var objDelete in listObjDelete)
		{
            	var objDeleteLinq = DataContextObject.TrabajosOutcomeAlumno.SingleOrDefault(x =>  x.AlumnoId == objDelete.AlumnoId  && x.OutcomeId == objDelete.OutcomeId  && x.TrabajoId == objDelete.TrabajoId);
			if(objDeleteLinq !=null)			
				DataContextObject.TrabajosOutcomeAlumno.DeleteOnSubmit(objDeleteLinq);
		}
        }

        public bool Exists(TrabajosOutcomeAlumnoBE objExists)
        {
		var DataContextObject = GetDataContextObject();
            return DataContextObject.TrabajosOutcomeAlumno.Any(x =>  x.AlumnoId == objExists.AlumnoId  && x.OutcomeId == objExists.OutcomeId  && x.TrabajoId == objExists.TrabajoId);
        }

        public void Update(TrabajosOutcomeAlumnoBE objUpdate)
        {
		var DataContextObject = GetDataContextObject();
            var objUpdateLinq = DataContextObject.TrabajosOutcomeAlumno.Single(x =>  x.AlumnoId == objUpdate.AlumnoId  && x.OutcomeId == objUpdate.OutcomeId  && x.TrabajoId == objUpdate.TrabajoId);
			objUpdateLinq.AlumnoId = objUpdate.AlumnoId;
			objUpdateLinq.OutcomeId = objUpdate.OutcomeId;
			objUpdateLinq.TrabajoId = objUpdate.TrabajoId;
        }

        public void Update(List<TrabajosOutcomeAlumnoBE> listObjUpdate)
        {
		var DataContextObject = GetDataContextObject();
		foreach(var objUpdate in listObjUpdate)
		{
            	var objUpdateLinq = DataContextObject.TrabajosOutcomeAlumno.Single(x =>  x.AlumnoId == objUpdate.AlumnoId  && x.OutcomeId == objUpdate.OutcomeId  && x.TrabajoId == objUpdate.TrabajoId);
			objUpdateLinq.AlumnoId = objUpdate.AlumnoId;
			objUpdateLinq.OutcomeId = objUpdate.OutcomeId;
			objUpdateLinq.TrabajoId = objUpdate.TrabajoId;
		}
        }
    }
}
