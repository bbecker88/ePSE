using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ePortafolio.Models.ePortafolio.Entities; 
using  ePortafolio.Models.ePortafolio;

namespace ePortafolio.Models.ePortafolio.Repository
{
    public partial class ArchivosGrupoRepository 
    {
        String connectionString = "";
	  ePortafolioDataContext DataContextObjectType;

        private ePortafolioDataContext GetDataContextObject()
        {
            return DataContextFactory.GetWebRequestScopedDataContext<ePortafolioDataContext>(null, connectionString);
        }

        public ArchivosGrupoRepository(String connectionString)
        {
            this.connectionString = connectionString;
        }

        private IQueryable<ArchivosGrupoBE> GetQueryable()
        {
            var DataContextObject = GetDataContextObject();
            var ArchivosGrupo= from x in DataContextObject.ArchivosGrupo
                             select new ArchivosGrupoBE
                             {
						ArchivoId = x.ArchivoId,
						GrupoId = x.GrupoId,
						ExtraArchivo = ePortafolioRepositoryFactory.GetArchivosRepository().GetLinq(x.Archivos),
						ExtraGrupo = ePortafolioRepositoryFactory.GetGruposRepository().GetLinq(x.Grupos),
                             };
            return ArchivosGrupo;
        }

        private ArchivosGrupoBE GetLinqFK(ArchivosGrupo DataContextObject)
        {
		if(DataContextObject==null)
			return null;
            return new ArchivosGrupoBE()
                {
						ArchivoId = DataContextObject.ArchivoId,
						GrupoId = DataContextObject.GrupoId,
						ExtraArchivo = ePortafolioRepositoryFactory.GetArchivosRepository().GetLinq(DataContextObject.Archivos),
						ExtraGrupo = ePortafolioRepositoryFactory.GetGruposRepository().GetLinq(DataContextObject.Grupos),
                };
        }

        public ArchivosGrupoBE GetLinq(ArchivosGrupo DataContextObject)
        {
		if(DataContextObject==null)
			return null;
            return new ArchivosGrupoBE()
                {
				ArchivoId = DataContextObject.ArchivoId,
				GrupoId = DataContextObject.GrupoId,
				ExtraArchivo = new ArchivosBE() { ArchivoId= DataContextObject.ArchivoId},
				ExtraGrupo = new GruposBE() { GrupoId= DataContextObject.GrupoId},
                };
        }

        public List<ArchivosGrupoBE> GetAll()
        {
            return GetQueryable().ToList();
        }

        public List<ArchivosGrupoBE> GetWhere(System.Linq.Expressions.Expression<Func<ArchivosGrupoBE,bool>> Where)
	        {
            return GetQueryable().Where(Where).ToList();
        }

        public List<ArchivosGrupoBE> GetWhere<T>(System.Linq.Expressions.Expression<Func<ArchivosGrupoBE,bool>> Where,System.Linq.Expressions.Expression<Func<ArchivosGrupoBE,T>> OrderBy)
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

        public ArchivosGrupoBE GetOne(Int32 ArchivoId, Int32 GrupoId)
        {
            	return GetQueryable().SingleOrDefault(x =>  x.ArchivoId == ArchivoId  && x.GrupoId == GrupoId);
        }

        public Int32 GetLastId()
        {
            		return GetQueryable().Max(x => x.ArchivoId);
        }

        public bool InsertIdentity(ArchivosGrupoBE objInsert, bool ThrowException)
        {
		var DataContextObject = GetDataContextObject();
		ArchivosGrupo objInsertLinq = new ArchivosGrupo();
			objInsertLinq.ArchivoId = objInsert.ArchivoId;
			objInsertLinq.GrupoId = objInsert.GrupoId;
		DataContextObject.ArchivosGrupo.InsertOnSubmit(objInsertLinq);
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

        public void Insert(ArchivosGrupoBE objInsert)
        {
		var DataContextObject = GetDataContextObject();
		ArchivosGrupo objInsertLinq = new ArchivosGrupo();
			objInsertLinq.ArchivoId = objInsert.ArchivoId;
			objInsertLinq.GrupoId = objInsert.GrupoId;
		DataContextObject.ArchivosGrupo.InsertOnSubmit(objInsertLinq);
        }

        public void Insert(List<ArchivosGrupoBE> listObjInsert)
        {
		var DataContextObject = GetDataContextObject();
		foreach(var objInsert in listObjInsert)
		{
		ArchivosGrupo objInsertLinq = new ArchivosGrupo();
			objInsertLinq.ArchivoId = objInsert.ArchivoId;
			objInsertLinq.GrupoId = objInsert.GrupoId;
			DataContextObject.ArchivosGrupo.InsertOnSubmit(objInsertLinq);
		}		
        }

        public void InsertOrUpdate(ArchivosGrupoBE objInsertOrUpdate)
        {
			var DataContextObject = GetDataContextObject();
			var existentObj = DataContextObject.ArchivosGrupo.SingleOrDefault(x =>  x.ArchivoId == objInsertOrUpdate.ArchivoId  && x.GrupoId == objInsertOrUpdate.GrupoId);
            	if (existentObj == null)
              	Insert(objInsertOrUpdate);
            	else
                	Update(objInsertOrUpdate);	
        } 

        public void InsertOrUpdate(List<ArchivosGrupoBE> listObjInsertOrUpdate)
        {
			foreach(var objInsertOrUpdate in listObjInsertOrUpdate)
			{
				InsertOrUpdate(objInsertOrUpdate);
			}	
        } 

        public void DeleteWhere(System.Linq.Expressions.Expression<Func<ArchivosGrupoBE,bool>> Filtro)
        {
            var DataContextObject = GetDataContextObject();
		Delete(GetWhere(Filtro));
        }

        public void Delete(ArchivosGrupoBE objDelete)
        {
		var DataContextObject = GetDataContextObject();
            var objDeleteLinq = DataContextObject.ArchivosGrupo.Single(x =>  x.ArchivoId == objDelete.ArchivoId  && x.GrupoId == objDelete.GrupoId);
		DataContextObject.ArchivosGrupo.DeleteOnSubmit(objDeleteLinq);
        }

        public void Delete(List<ArchivosGrupoBE> listObjDelete)
        {
		var DataContextObject = GetDataContextObject();
		foreach(var objDelete in listObjDelete)
		{
            	var objDeleteLinq = DataContextObject.ArchivosGrupo.Single(x =>  x.ArchivoId == objDelete.ArchivoId  && x.GrupoId == objDelete.GrupoId);
			DataContextObject.ArchivosGrupo.DeleteOnSubmit(objDeleteLinq);
		}
        }

        public void TryDeleteWhere(System.Linq.Expressions.Expression<Func<ArchivosGrupoBE,bool>> Filtro)
        {
            var DataContextObject = GetDataContextObject();
		TryDelete(GetWhere(Filtro));
        }

        public void TryDelete(ArchivosGrupoBE objDelete)
        {
		var DataContextObject = GetDataContextObject();
            var objDeleteLinq = DataContextObject.ArchivosGrupo.SingleOrDefault(x =>  x.ArchivoId == objDelete.ArchivoId  && x.GrupoId == objDelete.GrupoId);
		if(objDeleteLinq !=null)
			DataContextObject.ArchivosGrupo.DeleteOnSubmit(objDeleteLinq);
        }

        public void TryDelete(List<ArchivosGrupoBE> listObjDelete)
        {
		var DataContextObject = GetDataContextObject();
		foreach(var objDelete in listObjDelete)
		{
            	var objDeleteLinq = DataContextObject.ArchivosGrupo.SingleOrDefault(x =>  x.ArchivoId == objDelete.ArchivoId  && x.GrupoId == objDelete.GrupoId);
			if(objDeleteLinq !=null)			
				DataContextObject.ArchivosGrupo.DeleteOnSubmit(objDeleteLinq);
		}
        }

        public bool Exists(ArchivosGrupoBE objExists)
        {
		var DataContextObject = GetDataContextObject();
            return DataContextObject.ArchivosGrupo.Any(x =>  x.ArchivoId == objExists.ArchivoId  && x.GrupoId == objExists.GrupoId);
        }

        public void Update(ArchivosGrupoBE objUpdate)
        {
		var DataContextObject = GetDataContextObject();
            var objUpdateLinq = DataContextObject.ArchivosGrupo.Single(x =>  x.ArchivoId == objUpdate.ArchivoId  && x.GrupoId == objUpdate.GrupoId);
			objUpdateLinq.ArchivoId = objUpdate.ArchivoId;
			objUpdateLinq.GrupoId = objUpdate.GrupoId;
        }

        public void Update(List<ArchivosGrupoBE> listObjUpdate)
        {
		var DataContextObject = GetDataContextObject();
		foreach(var objUpdate in listObjUpdate)
		{
            	var objUpdateLinq = DataContextObject.ArchivosGrupo.Single(x =>  x.ArchivoId == objUpdate.ArchivoId  && x.GrupoId == objUpdate.GrupoId);
			objUpdateLinq.ArchivoId = objUpdate.ArchivoId;
			objUpdateLinq.GrupoId = objUpdate.GrupoId;
		}
        }
    }
}
