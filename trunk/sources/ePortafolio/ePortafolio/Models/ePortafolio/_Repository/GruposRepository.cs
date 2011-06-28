using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ePortafolio.Models.ePortafolio.Entities; 
using  ePortafolio.Models.ePortafolio;

namespace ePortafolio.Models.ePortafolio.Repository
{
    public partial class GruposRepository 
    {
        String connectionString = "";
	  ePortafolioDataContext DataContextObjectType;

        private ePortafolioDataContext GetDataContextObject()
        {
            return DataContextFactory.GetWebRequestScopedDataContext<ePortafolioDataContext>(null, connectionString);
        }

        public GruposRepository(String connectionString)
        {
            this.connectionString = connectionString;
        }

        private IQueryable<GruposBE> GetQueryable()
        {
            var DataContextObject = GetDataContextObject();
            var Grupos= from x in DataContextObject.Grupos
                             select new GruposBE
                             {
						EvaluacionId = x.EvaluacionId,
						GrupoId = x.GrupoId,
						LiderId = x.LiderId,
						NombreTrabajo = x.NombreTrabajo,
						Nota = x.Nota,
						SeccionId = x.SeccionId,
						TrabajoId = x.TrabajoId,
						ExtraTrabajo = ePortafolioRepositoryFactory.GetTrabajosRepository().GetLinq(x.Trabajos),
                             };
            return Grupos;
        }

        private GruposBE GetLinqFK(Grupos DataContextObject)
        {
		if(DataContextObject==null)
			return null;
            return new GruposBE()
                {
						EvaluacionId = DataContextObject.EvaluacionId,
						GrupoId = DataContextObject.GrupoId,
						LiderId = DataContextObject.LiderId,
						NombreTrabajo = DataContextObject.NombreTrabajo,
						Nota = DataContextObject.Nota,
						SeccionId = DataContextObject.SeccionId,
						TrabajoId = DataContextObject.TrabajoId,
						ExtraTrabajo = ePortafolioRepositoryFactory.GetTrabajosRepository().GetLinq(DataContextObject.Trabajos),
                };
        }

        public GruposBE GetLinq(Grupos DataContextObject)
        {
		if(DataContextObject==null)
			return null;
            return new GruposBE()
                {
				EvaluacionId = DataContextObject.EvaluacionId,
				GrupoId = DataContextObject.GrupoId,
				LiderId = DataContextObject.LiderId,
				NombreTrabajo = DataContextObject.NombreTrabajo,
				Nota = DataContextObject.Nota,
				SeccionId = DataContextObject.SeccionId,
				TrabajoId = DataContextObject.TrabajoId,
				ExtraTrabajo = new TrabajosBE() { TrabajoId= DataContextObject.TrabajoId},
                };
        }

        public List<GruposBE> GetAll()
        {
            return GetQueryable().ToList();
        }

        public List<GruposBE> GetWhere(System.Linq.Expressions.Expression<Func<GruposBE,bool>> Where)
	        {
            return GetQueryable().Where(Where).ToList();
        }

        public List<GruposBE> GetWhere<T>(System.Linq.Expressions.Expression<Func<GruposBE,bool>> Where,System.Linq.Expressions.Expression<Func<GruposBE,T>> OrderBy)
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

        public GruposBE GetOne(Int32 GrupoId)
        {
            	return GetQueryable().SingleOrDefault(x =>  x.GrupoId == GrupoId);
        }

        public Int32 GetLastId()
        {
            		return GetQueryable().Max(x => x.GrupoId);
        }

        public bool InsertIdentity(GruposBE objInsert, bool ThrowException)
        {
		var DataContextObject = GetDataContextObject();
		Grupos objInsertLinq = new Grupos();
			objInsertLinq.EvaluacionId = objInsert.EvaluacionId;
			objInsertLinq.GrupoId = objInsert.GrupoId;
			objInsertLinq.LiderId = objInsert.LiderId;
			objInsertLinq.NombreTrabajo = objInsert.NombreTrabajo;
			objInsertLinq.Nota = objInsert.Nota;
			objInsertLinq.SeccionId = objInsert.SeccionId;
			objInsertLinq.TrabajoId = objInsert.TrabajoId;
		DataContextObject.Grupos.InsertOnSubmit(objInsertLinq);
		try
            	{	
		  DataContextObject.SubmitChanges();
		  objInsert.GrupoId = objInsertLinq.GrupoId;
                return true;
            	}
            	catch (Exception Ex)
            	{
                if (ThrowException)
                    throw Ex;
                return false;
            	}
        }

        public void Insert(GruposBE objInsert)
        {
		var DataContextObject = GetDataContextObject();
		Grupos objInsertLinq = new Grupos();
			objInsertLinq.EvaluacionId = objInsert.EvaluacionId;
			objInsertLinq.GrupoId = objInsert.GrupoId;
			objInsertLinq.LiderId = objInsert.LiderId;
			objInsertLinq.NombreTrabajo = objInsert.NombreTrabajo;
			objInsertLinq.Nota = objInsert.Nota;
			objInsertLinq.SeccionId = objInsert.SeccionId;
			objInsertLinq.TrabajoId = objInsert.TrabajoId;
		DataContextObject.Grupos.InsertOnSubmit(objInsertLinq);
        }

        public void Insert(List<GruposBE> listObjInsert)
        {
		var DataContextObject = GetDataContextObject();
		foreach(var objInsert in listObjInsert)
		{
		Grupos objInsertLinq = new Grupos();
			objInsertLinq.EvaluacionId = objInsert.EvaluacionId;
			objInsertLinq.GrupoId = objInsert.GrupoId;
			objInsertLinq.LiderId = objInsert.LiderId;
			objInsertLinq.NombreTrabajo = objInsert.NombreTrabajo;
			objInsertLinq.Nota = objInsert.Nota;
			objInsertLinq.SeccionId = objInsert.SeccionId;
			objInsertLinq.TrabajoId = objInsert.TrabajoId;
			DataContextObject.Grupos.InsertOnSubmit(objInsertLinq);
		}		
        }

        public void InsertOrUpdate(GruposBE objInsertOrUpdate)
        {
			var DataContextObject = GetDataContextObject();
			var existentObj = DataContextObject.Grupos.SingleOrDefault(x =>  x.GrupoId == objInsertOrUpdate.GrupoId);
            	if (existentObj == null)
              	Insert(objInsertOrUpdate);
            	else
                	Update(objInsertOrUpdate);	
        } 

        public void InsertOrUpdate(List<GruposBE> listObjInsertOrUpdate)
        {
			foreach(var objInsertOrUpdate in listObjInsertOrUpdate)
			{
				InsertOrUpdate(objInsertOrUpdate);
			}	
        } 

        public void DeleteWhere(System.Linq.Expressions.Expression<Func<GruposBE,bool>> Filtro)
        {
            var DataContextObject = GetDataContextObject();
		Delete(GetWhere(Filtro));
        }

        public void Delete(GruposBE objDelete)
        {
		var DataContextObject = GetDataContextObject();
            var objDeleteLinq = DataContextObject.Grupos.Single(x =>  x.GrupoId == objDelete.GrupoId);
		DataContextObject.Grupos.DeleteOnSubmit(objDeleteLinq);
        }

        public void Delete(List<GruposBE> listObjDelete)
        {
		var DataContextObject = GetDataContextObject();
		foreach(var objDelete in listObjDelete)
		{
            	var objDeleteLinq = DataContextObject.Grupos.Single(x =>  x.GrupoId == objDelete.GrupoId);
			DataContextObject.Grupos.DeleteOnSubmit(objDeleteLinq);
		}
        }

        public void TryDeleteWhere(System.Linq.Expressions.Expression<Func<GruposBE,bool>> Filtro)
        {
            var DataContextObject = GetDataContextObject();
		TryDelete(GetWhere(Filtro));
        }

        public void TryDelete(GruposBE objDelete)
        {
		var DataContextObject = GetDataContextObject();
            var objDeleteLinq = DataContextObject.Grupos.SingleOrDefault(x =>  x.GrupoId == objDelete.GrupoId);
		if(objDeleteLinq !=null)
			DataContextObject.Grupos.DeleteOnSubmit(objDeleteLinq);
        }

        public void TryDelete(List<GruposBE> listObjDelete)
        {
		var DataContextObject = GetDataContextObject();
		foreach(var objDelete in listObjDelete)
		{
            	var objDeleteLinq = DataContextObject.Grupos.SingleOrDefault(x =>  x.GrupoId == objDelete.GrupoId);
			if(objDeleteLinq !=null)			
				DataContextObject.Grupos.DeleteOnSubmit(objDeleteLinq);
		}
        }

        public bool Exists(GruposBE objExists)
        {
		var DataContextObject = GetDataContextObject();
            return DataContextObject.Grupos.Any(x =>  x.GrupoId == objExists.GrupoId);
        }

        public void Update(GruposBE objUpdate)
        {
		var DataContextObject = GetDataContextObject();
            var objUpdateLinq = DataContextObject.Grupos.Single(x =>  x.GrupoId == objUpdate.GrupoId);
			objUpdateLinq.EvaluacionId = objUpdate.EvaluacionId;
			objUpdateLinq.GrupoId = objUpdate.GrupoId;
			objUpdateLinq.LiderId = objUpdate.LiderId;
			objUpdateLinq.NombreTrabajo = objUpdate.NombreTrabajo;
			objUpdateLinq.Nota = objUpdate.Nota;
			objUpdateLinq.SeccionId = objUpdate.SeccionId;
			objUpdateLinq.TrabajoId = objUpdate.TrabajoId;
        }

        public void Update(List<GruposBE> listObjUpdate)
        {
		var DataContextObject = GetDataContextObject();
		foreach(var objUpdate in listObjUpdate)
		{
            	var objUpdateLinq = DataContextObject.Grupos.Single(x =>  x.GrupoId == objUpdate.GrupoId);
			objUpdateLinq.EvaluacionId = objUpdate.EvaluacionId;
			objUpdateLinq.GrupoId = objUpdate.GrupoId;
			objUpdateLinq.LiderId = objUpdate.LiderId;
			objUpdateLinq.NombreTrabajo = objUpdate.NombreTrabajo;
			objUpdateLinq.Nota = objUpdate.Nota;
			objUpdateLinq.SeccionId = objUpdate.SeccionId;
			objUpdateLinq.TrabajoId = objUpdate.TrabajoId;
		}
        }
    }
}
