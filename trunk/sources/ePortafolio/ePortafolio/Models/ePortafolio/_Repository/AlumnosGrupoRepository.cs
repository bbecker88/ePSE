using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ePortafolio.Models.ePortafolio.Entities; 
using  ePortafolio.Models.ePortafolio;

namespace ePortafolio.Models.ePortafolio.Repository
{
    public partial class AlumnosGrupoRepository 
    {
        String connectionString = "";
	  ePortafolioDataContext DataContextObjectType;

        private ePortafolioDataContext GetDataContextObject()
        {
            return DataContextFactory.GetWebRequestScopedDataContext<ePortafolioDataContext>(null, connectionString);
        }

        public AlumnosGrupoRepository(String connectionString)
        {
            this.connectionString = connectionString;
        }

        private IQueryable<AlumnosGrupoBE> GetQueryable()
        {
            var DataContextObject = GetDataContextObject();
            var AlumnosGrupo= from x in DataContextObject.AlumnosGrupo
                             select new AlumnosGrupoBE
                             {
						AlumnoId = x.AlumnoId,
						EvaluacionId = x.EvaluacionId,
						GrupoId = x.GrupoId,
						Nota = x.Nota,
						ExtraGrupo = ePortafolioRepositoryFactory.GetGruposRepository().GetLinq(x.Grupos),
                             };
            return AlumnosGrupo;
        }

        private AlumnosGrupoBE GetLinqFK(AlumnosGrupo DataContextObject)
        {
		if(DataContextObject==null)
			return null;
            return new AlumnosGrupoBE()
                {
						AlumnoId = DataContextObject.AlumnoId,
						EvaluacionId = DataContextObject.EvaluacionId,
						GrupoId = DataContextObject.GrupoId,
						Nota = DataContextObject.Nota,
						ExtraGrupo = ePortafolioRepositoryFactory.GetGruposRepository().GetLinq(DataContextObject.Grupos),
                };
        }

        public AlumnosGrupoBE GetLinq(AlumnosGrupo DataContextObject)
        {
		if(DataContextObject==null)
			return null;
            return new AlumnosGrupoBE()
                {
				AlumnoId = DataContextObject.AlumnoId,
				EvaluacionId = DataContextObject.EvaluacionId,
				GrupoId = DataContextObject.GrupoId,
				Nota = DataContextObject.Nota,
				ExtraGrupo = new GruposBE() { GrupoId= DataContextObject.GrupoId},
                };
        }

        public List<AlumnosGrupoBE> GetAll()
        {
            return GetQueryable().ToList();
        }

        public List<AlumnosGrupoBE> GetWhere(System.Linq.Expressions.Expression<Func<AlumnosGrupoBE,bool>> Where)
	        {
            return GetQueryable().Where(Where).ToList();
        }

        public List<AlumnosGrupoBE> GetWhere<T>(System.Linq.Expressions.Expression<Func<AlumnosGrupoBE,bool>> Where,System.Linq.Expressions.Expression<Func<AlumnosGrupoBE,T>> OrderBy)
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

        public AlumnosGrupoBE GetOne(String AlumnoId, Int32 GrupoId)
        {
            	return GetQueryable().SingleOrDefault(x =>  x.AlumnoId == AlumnoId  && x.GrupoId == GrupoId);
        }

        public Int32 GetLastId()
        {
            		return 0;
        }

        public bool InsertIdentity(AlumnosGrupoBE objInsert, bool ThrowException)
        {
		var DataContextObject = GetDataContextObject();
		AlumnosGrupo objInsertLinq = new AlumnosGrupo();
			objInsertLinq.AlumnoId = objInsert.AlumnoId;
			objInsertLinq.EvaluacionId = objInsert.EvaluacionId;
			objInsertLinq.GrupoId = objInsert.GrupoId;
			objInsertLinq.Nota = objInsert.Nota;
		DataContextObject.AlumnosGrupo.InsertOnSubmit(objInsertLinq);
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

        public void Insert(AlumnosGrupoBE objInsert)
        {
		var DataContextObject = GetDataContextObject();
		AlumnosGrupo objInsertLinq = new AlumnosGrupo();
			objInsertLinq.AlumnoId = objInsert.AlumnoId;
			objInsertLinq.EvaluacionId = objInsert.EvaluacionId;
			objInsertLinq.GrupoId = objInsert.GrupoId;
			objInsertLinq.Nota = objInsert.Nota;
		DataContextObject.AlumnosGrupo.InsertOnSubmit(objInsertLinq);
        }

        public void Insert(List<AlumnosGrupoBE> listObjInsert)
        {
		var DataContextObject = GetDataContextObject();
		foreach(var objInsert in listObjInsert)
		{
		AlumnosGrupo objInsertLinq = new AlumnosGrupo();
			objInsertLinq.AlumnoId = objInsert.AlumnoId;
			objInsertLinq.EvaluacionId = objInsert.EvaluacionId;
			objInsertLinq.GrupoId = objInsert.GrupoId;
			objInsertLinq.Nota = objInsert.Nota;
			DataContextObject.AlumnosGrupo.InsertOnSubmit(objInsertLinq);
		}		
        }

        public void InsertOrUpdate(AlumnosGrupoBE objInsertOrUpdate)
        {
			var DataContextObject = GetDataContextObject();
			var existentObj = DataContextObject.AlumnosGrupo.SingleOrDefault(x =>  x.AlumnoId == objInsertOrUpdate.AlumnoId  && x.GrupoId == objInsertOrUpdate.GrupoId);
            	if (existentObj == null)
              	Insert(objInsertOrUpdate);
            	else
                	Update(objInsertOrUpdate);	
        } 

        public void InsertOrUpdate(List<AlumnosGrupoBE> listObjInsertOrUpdate)
        {
			foreach(var objInsertOrUpdate in listObjInsertOrUpdate)
			{
				InsertOrUpdate(objInsertOrUpdate);
			}	
        } 

        public void DeleteWhere(System.Linq.Expressions.Expression<Func<AlumnosGrupoBE,bool>> Filtro)
        {
            var DataContextObject = GetDataContextObject();
		Delete(GetWhere(Filtro));
        }

        public void Delete(AlumnosGrupoBE objDelete)
        {
		var DataContextObject = GetDataContextObject();
            var objDeleteLinq = DataContextObject.AlumnosGrupo.Single(x =>  x.AlumnoId == objDelete.AlumnoId  && x.GrupoId == objDelete.GrupoId);
		DataContextObject.AlumnosGrupo.DeleteOnSubmit(objDeleteLinq);
        }

        public void Delete(List<AlumnosGrupoBE> listObjDelete)
        {
		var DataContextObject = GetDataContextObject();
		foreach(var objDelete in listObjDelete)
		{
            	var objDeleteLinq = DataContextObject.AlumnosGrupo.Single(x =>  x.AlumnoId == objDelete.AlumnoId  && x.GrupoId == objDelete.GrupoId);
			DataContextObject.AlumnosGrupo.DeleteOnSubmit(objDeleteLinq);
		}
        }

        public void TryDeleteWhere(System.Linq.Expressions.Expression<Func<AlumnosGrupoBE,bool>> Filtro)
        {
            var DataContextObject = GetDataContextObject();
		TryDelete(GetWhere(Filtro));
        }

        public void TryDelete(AlumnosGrupoBE objDelete)
        {
		var DataContextObject = GetDataContextObject();
            var objDeleteLinq = DataContextObject.AlumnosGrupo.SingleOrDefault(x =>  x.AlumnoId == objDelete.AlumnoId  && x.GrupoId == objDelete.GrupoId);
		if(objDeleteLinq !=null)
			DataContextObject.AlumnosGrupo.DeleteOnSubmit(objDeleteLinq);
        }

        public void TryDelete(List<AlumnosGrupoBE> listObjDelete)
        {
		var DataContextObject = GetDataContextObject();
		foreach(var objDelete in listObjDelete)
		{
            	var objDeleteLinq = DataContextObject.AlumnosGrupo.SingleOrDefault(x =>  x.AlumnoId == objDelete.AlumnoId  && x.GrupoId == objDelete.GrupoId);
			if(objDeleteLinq !=null)			
				DataContextObject.AlumnosGrupo.DeleteOnSubmit(objDeleteLinq);
		}
        }

        public bool Exists(AlumnosGrupoBE objExists)
        {
		var DataContextObject = GetDataContextObject();
            return DataContextObject.AlumnosGrupo.Any(x =>  x.AlumnoId == objExists.AlumnoId  && x.GrupoId == objExists.GrupoId);
        }

        public void Update(AlumnosGrupoBE objUpdate)
        {
		var DataContextObject = GetDataContextObject();
            var objUpdateLinq = DataContextObject.AlumnosGrupo.Single(x =>  x.AlumnoId == objUpdate.AlumnoId  && x.GrupoId == objUpdate.GrupoId);
			objUpdateLinq.AlumnoId = objUpdate.AlumnoId;
			objUpdateLinq.EvaluacionId = objUpdate.EvaluacionId;
			objUpdateLinq.GrupoId = objUpdate.GrupoId;
			objUpdateLinq.Nota = objUpdate.Nota;
        }

        public void Update(List<AlumnosGrupoBE> listObjUpdate)
        {
		var DataContextObject = GetDataContextObject();
		foreach(var objUpdate in listObjUpdate)
		{
            	var objUpdateLinq = DataContextObject.AlumnosGrupo.Single(x =>  x.AlumnoId == objUpdate.AlumnoId  && x.GrupoId == objUpdate.GrupoId);
			objUpdateLinq.AlumnoId = objUpdate.AlumnoId;
			objUpdateLinq.EvaluacionId = objUpdate.EvaluacionId;
			objUpdateLinq.GrupoId = objUpdate.GrupoId;
			objUpdateLinq.Nota = objUpdate.Nota;
		}
        }
    }
}
