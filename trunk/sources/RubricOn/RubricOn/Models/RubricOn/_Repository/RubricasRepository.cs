using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RubricOn.Models.RubricOn.Entities; 
using  RubricOn.Models.RubricOn;

namespace RubricOn.Models.RubricOn.Repository
{
    public partial class RubricasRepository 
    {
        String connectionString = "";
	  RubricOnDataContext DataContextObjectType;

        private RubricOnDataContext GetDataContextObject()
        {
            return DataContextFactory.GetWebRequestScopedDataContext<RubricOnDataContext>(null, connectionString);
        }

        public RubricasRepository(String connectionString)
        {
            this.connectionString = connectionString;
        }

        private IQueryable<RubricasBE> GetQueryable()
        {
            var DataContextObject = GetDataContextObject();
            var Rubricas= from x in DataContextObject.Rubricas
                             select new RubricasBE
                             {
						RubricaId = x.RubricaId,
						TipoArtefacto = x.TipoArtefacto,
						ExtraTipoArtefacto = RubricOnRepositoryFactory.GetTiposArtefactoRepository().GetLinq(x.TiposArtefacto),
                             };
            return Rubricas;
        }

        private RubricasBE GetLinqFK(Rubricas DataContextObject)
        {
		if(DataContextObject==null)
			return null;
            return new RubricasBE()
                {
						RubricaId = DataContextObject.RubricaId,
						TipoArtefacto = DataContextObject.TipoArtefacto,
						ExtraTipoArtefacto = RubricOnRepositoryFactory.GetTiposArtefactoRepository().GetLinq(DataContextObject.TiposArtefacto),
                };
        }

        public RubricasBE GetLinq(Rubricas DataContextObject)
        {
		if(DataContextObject==null)
			return null;
            return new RubricasBE()
                {
				RubricaId = DataContextObject.RubricaId,
				TipoArtefacto = DataContextObject.TipoArtefacto,
				ExtraTipoArtefacto = new TiposArtefactoBE() { TipoArtefacto= DataContextObject.TipoArtefacto},
                };
        }

        public List<RubricasBE> GetAll()
        {
            return GetQueryable().ToList();
        }

        public List<RubricasBE> GetWhere(System.Linq.Expressions.Expression<Func<RubricasBE,bool>> Where)
	        {
            return GetQueryable().Where(Where).ToList();
        }

        public List<RubricasBE> GetWhere<T>(System.Linq.Expressions.Expression<Func<RubricasBE,bool>> Where,System.Linq.Expressions.Expression<Func<RubricasBE,T>> OrderBy)
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

        public RubricasBE GetOne(String RubricaId, String TipoArtefacto)
        {
            	return GetQueryable().SingleOrDefault(x =>  x.RubricaId == RubricaId  && x.TipoArtefacto == TipoArtefacto);
        }

        public Int32 GetLastId()
        {
            		return 0;
        }

        public bool InsertIdentity(RubricasBE objInsert, bool ThrowException)
        {
		var DataContextObject = GetDataContextObject();
		Rubricas objInsertLinq = new Rubricas();
			objInsertLinq.RubricaId = objInsert.RubricaId;
			objInsertLinq.TipoArtefacto = objInsert.TipoArtefacto;
		DataContextObject.Rubricas.InsertOnSubmit(objInsertLinq);
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

        public void Insert(RubricasBE objInsert)
        {
		var DataContextObject = GetDataContextObject();
		Rubricas objInsertLinq = new Rubricas();
			objInsertLinq.RubricaId = objInsert.RubricaId;
			objInsertLinq.TipoArtefacto = objInsert.TipoArtefacto;
		DataContextObject.Rubricas.InsertOnSubmit(objInsertLinq);
        }

        public void Insert(List<RubricasBE> listObjInsert)
        {
		var DataContextObject = GetDataContextObject();
		foreach(var objInsert in listObjInsert)
		{
		Rubricas objInsertLinq = new Rubricas();
			objInsertLinq.RubricaId = objInsert.RubricaId;
			objInsertLinq.TipoArtefacto = objInsert.TipoArtefacto;
			DataContextObject.Rubricas.InsertOnSubmit(objInsertLinq);
		}		
        }

        public void InsertOrUpdate(RubricasBE objInsertOrUpdate)
        {
			var DataContextObject = GetDataContextObject();
			var existentObj = DataContextObject.Rubricas.SingleOrDefault(x =>  x.RubricaId == objInsertOrUpdate.RubricaId  && x.TipoArtefacto == objInsertOrUpdate.TipoArtefacto);
            	if (existentObj == null)
              	Insert(objInsertOrUpdate);
            	else
                	Update(objInsertOrUpdate);	
        } 

        public void InsertOrUpdate(List<RubricasBE> listObjInsertOrUpdate)
        {
			foreach(var objInsertOrUpdate in listObjInsertOrUpdate)
			{
				InsertOrUpdate(objInsertOrUpdate);
			}	
        } 

        public void DeleteWhere(System.Linq.Expressions.Expression<Func<RubricasBE,bool>> Filtro)
        {
            var DataContextObject = GetDataContextObject();
		Delete(GetWhere(Filtro));
        }

        public void Delete(RubricasBE objDelete)
        {
		var DataContextObject = GetDataContextObject();
            var objDeleteLinq = DataContextObject.Rubricas.Single(x =>  x.RubricaId == objDelete.RubricaId  && x.TipoArtefacto == objDelete.TipoArtefacto);
		DataContextObject.Rubricas.DeleteOnSubmit(objDeleteLinq);
        }

        public void Delete(List<RubricasBE> listObjDelete)
        {
		var DataContextObject = GetDataContextObject();
		foreach(var objDelete in listObjDelete)
		{
            	var objDeleteLinq = DataContextObject.Rubricas.Single(x =>  x.RubricaId == objDelete.RubricaId  && x.TipoArtefacto == objDelete.TipoArtefacto);
			DataContextObject.Rubricas.DeleteOnSubmit(objDeleteLinq);
		}
        }

        public void TryDeleteWhere(System.Linq.Expressions.Expression<Func<RubricasBE,bool>> Filtro)
        {
            var DataContextObject = GetDataContextObject();
		TryDelete(GetWhere(Filtro));
        }

        public void TryDelete(RubricasBE objDelete)
        {
		var DataContextObject = GetDataContextObject();
            var objDeleteLinq = DataContextObject.Rubricas.SingleOrDefault(x =>  x.RubricaId == objDelete.RubricaId  && x.TipoArtefacto == objDelete.TipoArtefacto);
		if(objDeleteLinq !=null)
			DataContextObject.Rubricas.DeleteOnSubmit(objDeleteLinq);
        }

        public void TryDelete(List<RubricasBE> listObjDelete)
        {
		var DataContextObject = GetDataContextObject();
		foreach(var objDelete in listObjDelete)
		{
            	var objDeleteLinq = DataContextObject.Rubricas.SingleOrDefault(x =>  x.RubricaId == objDelete.RubricaId  && x.TipoArtefacto == objDelete.TipoArtefacto);
			if(objDeleteLinq !=null)			
				DataContextObject.Rubricas.DeleteOnSubmit(objDeleteLinq);
		}
        }

        public bool Exists(RubricasBE objExists)
        {
		var DataContextObject = GetDataContextObject();
            return DataContextObject.Rubricas.Any(x =>  x.RubricaId == objExists.RubricaId  && x.TipoArtefacto == objExists.TipoArtefacto);
        }

        public void Update(RubricasBE objUpdate)
        {
		var DataContextObject = GetDataContextObject();
            var objUpdateLinq = DataContextObject.Rubricas.Single(x =>  x.RubricaId == objUpdate.RubricaId  && x.TipoArtefacto == objUpdate.TipoArtefacto);
			objUpdateLinq.RubricaId = objUpdate.RubricaId;
			objUpdateLinq.TipoArtefacto = objUpdate.TipoArtefacto;
        }

        public void Update(List<RubricasBE> listObjUpdate)
        {
		var DataContextObject = GetDataContextObject();
		foreach(var objUpdate in listObjUpdate)
		{
            	var objUpdateLinq = DataContextObject.Rubricas.Single(x =>  x.RubricaId == objUpdate.RubricaId  && x.TipoArtefacto == objUpdate.TipoArtefacto);
			objUpdateLinq.RubricaId = objUpdate.RubricaId;
			objUpdateLinq.TipoArtefacto = objUpdate.TipoArtefacto;
		}
        }
    }
}
