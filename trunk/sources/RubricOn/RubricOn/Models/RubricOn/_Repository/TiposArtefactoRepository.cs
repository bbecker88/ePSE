using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RubricOn.Models.RubricOn.Entities; 
using  RubricOn.Models.RubricOn;

namespace RubricOn.Models.RubricOn.Repository
{
    public partial class TiposArtefactoRepository 
    {
        String connectionString = "";
	  RubricOnDataContext DataContextObjectType;

        private RubricOnDataContext GetDataContextObject()
        {
            return DataContextFactory.GetWebRequestScopedDataContext<RubricOnDataContext>(null, connectionString);
        }

        public TiposArtefactoRepository(String connectionString)
        {
            this.connectionString = connectionString;
        }

        private IQueryable<TiposArtefactoBE> GetQueryable()
        {
            var DataContextObject = GetDataContextObject();
            var TiposArtefacto= from x in DataContextObject.TiposArtefacto
                             select new TiposArtefactoBE
                             {
						Descripcion = x.Descripcion,
						TipoArtefacto = x.TipoArtefacto,
                             };
            return TiposArtefacto;
        }

        private TiposArtefactoBE GetLinqFK(TiposArtefacto DataContextObject)
        {
		if(DataContextObject==null)
			return null;
            return new TiposArtefactoBE()
                {
						Descripcion = DataContextObject.Descripcion,
						TipoArtefacto = DataContextObject.TipoArtefacto,
                };
        }

        public TiposArtefactoBE GetLinq(TiposArtefacto DataContextObject)
        {
		if(DataContextObject==null)
			return null;
            return new TiposArtefactoBE()
                {
				Descripcion = DataContextObject.Descripcion,
				TipoArtefacto = DataContextObject.TipoArtefacto,
                };
        }

        public List<TiposArtefactoBE> GetAll()
        {
            return GetQueryable().ToList();
        }

        public List<TiposArtefactoBE> GetWhere(System.Linq.Expressions.Expression<Func<TiposArtefactoBE,bool>> Where)
	        {
            return GetQueryable().Where(Where).ToList();
        }

        public List<TiposArtefactoBE> GetWhere<T>(System.Linq.Expressions.Expression<Func<TiposArtefactoBE,bool>> Where,System.Linq.Expressions.Expression<Func<TiposArtefactoBE,T>> OrderBy)
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

        public TiposArtefactoBE GetOne(String TipoArtefacto)
        {
            	return GetQueryable().SingleOrDefault(x =>  x.TipoArtefacto == TipoArtefacto);
        }

        public Int32 GetLastId()
        {
            		return 0;
        }

        public bool InsertIdentity(TiposArtefactoBE objInsert, bool ThrowException)
        {
		var DataContextObject = GetDataContextObject();
		TiposArtefacto objInsertLinq = new TiposArtefacto();
			objInsertLinq.Descripcion = objInsert.Descripcion;
			objInsertLinq.TipoArtefacto = objInsert.TipoArtefacto;
		DataContextObject.TiposArtefacto.InsertOnSubmit(objInsertLinq);
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

        public void Insert(TiposArtefactoBE objInsert)
        {
		var DataContextObject = GetDataContextObject();
		TiposArtefacto objInsertLinq = new TiposArtefacto();
			objInsertLinq.Descripcion = objInsert.Descripcion;
			objInsertLinq.TipoArtefacto = objInsert.TipoArtefacto;
		DataContextObject.TiposArtefacto.InsertOnSubmit(objInsertLinq);
        }

        public void Insert(List<TiposArtefactoBE> listObjInsert)
        {
		var DataContextObject = GetDataContextObject();
		foreach(var objInsert in listObjInsert)
		{
		TiposArtefacto objInsertLinq = new TiposArtefacto();
			objInsertLinq.Descripcion = objInsert.Descripcion;
			objInsertLinq.TipoArtefacto = objInsert.TipoArtefacto;
			DataContextObject.TiposArtefacto.InsertOnSubmit(objInsertLinq);
		}		
        }

        public void InsertOrUpdate(TiposArtefactoBE objInsertOrUpdate)
        {
			var DataContextObject = GetDataContextObject();
			var existentObj = DataContextObject.TiposArtefacto.SingleOrDefault(x =>  x.TipoArtefacto == objInsertOrUpdate.TipoArtefacto);
            	if (existentObj == null)
              	Insert(objInsertOrUpdate);
            	else
                	Update(objInsertOrUpdate);	
        } 

        public void InsertOrUpdate(List<TiposArtefactoBE> listObjInsertOrUpdate)
        {
			foreach(var objInsertOrUpdate in listObjInsertOrUpdate)
			{
				InsertOrUpdate(objInsertOrUpdate);
			}	
        } 

        public void DeleteWhere(System.Linq.Expressions.Expression<Func<TiposArtefactoBE,bool>> Filtro)
        {
            var DataContextObject = GetDataContextObject();
		Delete(GetWhere(Filtro));
        }

        public void Delete(TiposArtefactoBE objDelete)
        {
		var DataContextObject = GetDataContextObject();
            var objDeleteLinq = DataContextObject.TiposArtefacto.Single(x =>  x.TipoArtefacto == objDelete.TipoArtefacto);
		DataContextObject.TiposArtefacto.DeleteOnSubmit(objDeleteLinq);
        }

        public void Delete(List<TiposArtefactoBE> listObjDelete)
        {
		var DataContextObject = GetDataContextObject();
		foreach(var objDelete in listObjDelete)
		{
            	var objDeleteLinq = DataContextObject.TiposArtefacto.Single(x =>  x.TipoArtefacto == objDelete.TipoArtefacto);
			DataContextObject.TiposArtefacto.DeleteOnSubmit(objDeleteLinq);
		}
        }

        public void TryDeleteWhere(System.Linq.Expressions.Expression<Func<TiposArtefactoBE,bool>> Filtro)
        {
            var DataContextObject = GetDataContextObject();
		TryDelete(GetWhere(Filtro));
        }

        public void TryDelete(TiposArtefactoBE objDelete)
        {
		var DataContextObject = GetDataContextObject();
            var objDeleteLinq = DataContextObject.TiposArtefacto.SingleOrDefault(x =>  x.TipoArtefacto == objDelete.TipoArtefacto);
		if(objDeleteLinq !=null)
			DataContextObject.TiposArtefacto.DeleteOnSubmit(objDeleteLinq);
        }

        public void TryDelete(List<TiposArtefactoBE> listObjDelete)
        {
		var DataContextObject = GetDataContextObject();
		foreach(var objDelete in listObjDelete)
		{
            	var objDeleteLinq = DataContextObject.TiposArtefacto.SingleOrDefault(x =>  x.TipoArtefacto == objDelete.TipoArtefacto);
			if(objDeleteLinq !=null)			
				DataContextObject.TiposArtefacto.DeleteOnSubmit(objDeleteLinq);
		}
        }

        public bool Exists(TiposArtefactoBE objExists)
        {
		var DataContextObject = GetDataContextObject();
            return DataContextObject.TiposArtefacto.Any(x =>  x.TipoArtefacto == objExists.TipoArtefacto);
        }

        public void Update(TiposArtefactoBE objUpdate)
        {
		var DataContextObject = GetDataContextObject();
            var objUpdateLinq = DataContextObject.TiposArtefacto.Single(x =>  x.TipoArtefacto == objUpdate.TipoArtefacto);
			objUpdateLinq.Descripcion = objUpdate.Descripcion;
			objUpdateLinq.TipoArtefacto = objUpdate.TipoArtefacto;
        }

        public void Update(List<TiposArtefactoBE> listObjUpdate)
        {
		var DataContextObject = GetDataContextObject();
		foreach(var objUpdate in listObjUpdate)
		{
            	var objUpdateLinq = DataContextObject.TiposArtefacto.Single(x =>  x.TipoArtefacto == objUpdate.TipoArtefacto);
			objUpdateLinq.Descripcion = objUpdate.Descripcion;
			objUpdateLinq.TipoArtefacto = objUpdate.TipoArtefacto;
		}
        }
    }
}
