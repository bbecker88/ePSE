using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ePortafolio.Models.ePortafolio.Entities; 
using  ePortafolio.Models.ePortafolio;

namespace ePortafolio.Models.ePortafolio.Repository
{
    public partial class EvaluacionesGruposProfesorRepository 
    {
        String connectionString = "";
	  ePortafolioDataContext DataContextObjectType;

        private ePortafolioDataContext GetDataContextObject()
        {
            return DataContextFactory.GetWebRequestScopedDataContext<ePortafolioDataContext>(null, connectionString);
        }

        public EvaluacionesGruposProfesorRepository(String connectionString)
        {
            this.connectionString = connectionString;
        }

        private IQueryable<EvaluacionesGruposProfesorBE> GetQueryable()
        {
            var DataContextObject = GetDataContextObject();
            var EvaluacionesGruposProfesor= from x in DataContextObject.EvaluacionesGruposProfesor
                             select new EvaluacionesGruposProfesorBE
                             {
						GrupoId = x.GrupoId,
						ProfesorId = x.ProfesorId,
						ExtraGrupo = ePortafolioRepositoryFactory.GetGruposRepository().GetLinq(x.Grupos),
                             };
            return EvaluacionesGruposProfesor;
        }

        private EvaluacionesGruposProfesorBE GetLinqFK(EvaluacionesGruposProfesor DataContextObject)
        {
		if(DataContextObject==null)
			return null;
            return new EvaluacionesGruposProfesorBE()
                {
						GrupoId = DataContextObject.GrupoId,
						ProfesorId = DataContextObject.ProfesorId,
						ExtraGrupo = ePortafolioRepositoryFactory.GetGruposRepository().GetLinq(DataContextObject.Grupos),
                };
        }

        public EvaluacionesGruposProfesorBE GetLinq(EvaluacionesGruposProfesor DataContextObject)
        {
		if(DataContextObject==null)
			return null;
            return new EvaluacionesGruposProfesorBE()
                {
				GrupoId = DataContextObject.GrupoId,
				ProfesorId = DataContextObject.ProfesorId,
				ExtraGrupo = new GruposBE() { GrupoId= DataContextObject.GrupoId},
                };
        }

        public List<EvaluacionesGruposProfesorBE> GetAll()
        {
            return GetQueryable().ToList();
        }

        public List<EvaluacionesGruposProfesorBE> GetWhere(System.Linq.Expressions.Expression<Func<EvaluacionesGruposProfesorBE,bool>> Where)
	        {
            return GetQueryable().Where(Where).ToList();
        }

        public List<EvaluacionesGruposProfesorBE> GetWhere<T>(System.Linq.Expressions.Expression<Func<EvaluacionesGruposProfesorBE,bool>> Where,System.Linq.Expressions.Expression<Func<EvaluacionesGruposProfesorBE,T>> OrderBy)
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

        public EvaluacionesGruposProfesorBE GetOne(Int32 GrupoId, String ProfesorId)
        {
            	return GetQueryable().SingleOrDefault(x =>  x.GrupoId == GrupoId  && x.ProfesorId == ProfesorId);
        }

        public Int32 GetLastId()
        {
            		return GetQueryable().Max(x => x.GrupoId);
        }

        public bool InsertIdentity(EvaluacionesGruposProfesorBE objInsert, bool ThrowException)
        {
		var DataContextObject = GetDataContextObject();
		EvaluacionesGruposProfesor objInsertLinq = new EvaluacionesGruposProfesor();
			objInsertLinq.GrupoId = objInsert.GrupoId;
			objInsertLinq.ProfesorId = objInsert.ProfesorId;
		DataContextObject.EvaluacionesGruposProfesor.InsertOnSubmit(objInsertLinq);
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

        public void Insert(EvaluacionesGruposProfesorBE objInsert)
        {
		var DataContextObject = GetDataContextObject();
		EvaluacionesGruposProfesor objInsertLinq = new EvaluacionesGruposProfesor();
			objInsertLinq.GrupoId = objInsert.GrupoId;
			objInsertLinq.ProfesorId = objInsert.ProfesorId;
		DataContextObject.EvaluacionesGruposProfesor.InsertOnSubmit(objInsertLinq);
        }

        public void Insert(List<EvaluacionesGruposProfesorBE> listObjInsert)
        {
		var DataContextObject = GetDataContextObject();
		foreach(var objInsert in listObjInsert)
		{
		EvaluacionesGruposProfesor objInsertLinq = new EvaluacionesGruposProfesor();
			objInsertLinq.GrupoId = objInsert.GrupoId;
			objInsertLinq.ProfesorId = objInsert.ProfesorId;
			DataContextObject.EvaluacionesGruposProfesor.InsertOnSubmit(objInsertLinq);
		}		
        }

        public void InsertOrUpdate(EvaluacionesGruposProfesorBE objInsertOrUpdate)
        {
			var DataContextObject = GetDataContextObject();
			var existentObj = DataContextObject.EvaluacionesGruposProfesor.SingleOrDefault(x =>  x.GrupoId == objInsertOrUpdate.GrupoId  && x.ProfesorId == objInsertOrUpdate.ProfesorId);
            	if (existentObj == null)
              	Insert(objInsertOrUpdate);
            	else
                	Update(objInsertOrUpdate);	
        } 

        public void InsertOrUpdate(List<EvaluacionesGruposProfesorBE> listObjInsertOrUpdate)
        {
			foreach(var objInsertOrUpdate in listObjInsertOrUpdate)
			{
				InsertOrUpdate(objInsertOrUpdate);
			}	
        } 

        public void DeleteWhere(System.Linq.Expressions.Expression<Func<EvaluacionesGruposProfesorBE,bool>> Filtro)
        {
            var DataContextObject = GetDataContextObject();
		Delete(GetWhere(Filtro));
        }

        public void Delete(EvaluacionesGruposProfesorBE objDelete)
        {
		var DataContextObject = GetDataContextObject();
            var objDeleteLinq = DataContextObject.EvaluacionesGruposProfesor.Single(x =>  x.GrupoId == objDelete.GrupoId  && x.ProfesorId == objDelete.ProfesorId);
		DataContextObject.EvaluacionesGruposProfesor.DeleteOnSubmit(objDeleteLinq);
        }

        public void Delete(List<EvaluacionesGruposProfesorBE> listObjDelete)
        {
		var DataContextObject = GetDataContextObject();
		foreach(var objDelete in listObjDelete)
		{
            	var objDeleteLinq = DataContextObject.EvaluacionesGruposProfesor.Single(x =>  x.GrupoId == objDelete.GrupoId  && x.ProfesorId == objDelete.ProfesorId);
			DataContextObject.EvaluacionesGruposProfesor.DeleteOnSubmit(objDeleteLinq);
		}
        }

        public void TryDeleteWhere(System.Linq.Expressions.Expression<Func<EvaluacionesGruposProfesorBE,bool>> Filtro)
        {
            var DataContextObject = GetDataContextObject();
		TryDelete(GetWhere(Filtro));
        }

        public void TryDelete(EvaluacionesGruposProfesorBE objDelete)
        {
		var DataContextObject = GetDataContextObject();
            var objDeleteLinq = DataContextObject.EvaluacionesGruposProfesor.SingleOrDefault(x =>  x.GrupoId == objDelete.GrupoId  && x.ProfesorId == objDelete.ProfesorId);
		if(objDeleteLinq !=null)
			DataContextObject.EvaluacionesGruposProfesor.DeleteOnSubmit(objDeleteLinq);
        }

        public void TryDelete(List<EvaluacionesGruposProfesorBE> listObjDelete)
        {
		var DataContextObject = GetDataContextObject();
		foreach(var objDelete in listObjDelete)
		{
            	var objDeleteLinq = DataContextObject.EvaluacionesGruposProfesor.SingleOrDefault(x =>  x.GrupoId == objDelete.GrupoId  && x.ProfesorId == objDelete.ProfesorId);
			if(objDeleteLinq !=null)			
				DataContextObject.EvaluacionesGruposProfesor.DeleteOnSubmit(objDeleteLinq);
		}
        }

        public bool Exists(EvaluacionesGruposProfesorBE objExists)
        {
		var DataContextObject = GetDataContextObject();
            return DataContextObject.EvaluacionesGruposProfesor.Any(x =>  x.GrupoId == objExists.GrupoId  && x.ProfesorId == objExists.ProfesorId);
        }

        public void Update(EvaluacionesGruposProfesorBE objUpdate)
        {
		var DataContextObject = GetDataContextObject();
            var objUpdateLinq = DataContextObject.EvaluacionesGruposProfesor.Single(x =>  x.GrupoId == objUpdate.GrupoId  && x.ProfesorId == objUpdate.ProfesorId);
			objUpdateLinq.GrupoId = objUpdate.GrupoId;
			objUpdateLinq.ProfesorId = objUpdate.ProfesorId;
        }

        public void Update(List<EvaluacionesGruposProfesorBE> listObjUpdate)
        {
		var DataContextObject = GetDataContextObject();
		foreach(var objUpdate in listObjUpdate)
		{
            	var objUpdateLinq = DataContextObject.EvaluacionesGruposProfesor.Single(x =>  x.GrupoId == objUpdate.GrupoId  && x.ProfesorId == objUpdate.ProfesorId);
			objUpdateLinq.GrupoId = objUpdate.GrupoId;
			objUpdateLinq.ProfesorId = objUpdate.ProfesorId;
		}
        }
    }
}
