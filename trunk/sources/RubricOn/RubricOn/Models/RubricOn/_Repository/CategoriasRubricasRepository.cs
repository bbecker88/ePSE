using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RubricOn.Models.RubricOn.Entities; 
using  RubricOn.Models.RubricOn;

namespace RubricOn.Models.RubricOn.Repository
{
    public partial class CategoriasRubricasRepository 
    {
        String connectionString = "";
	  RubricOnDataContext DataContextObjectType;

        private RubricOnDataContext GetDataContextObject()
        {
            return DataContextFactory.GetWebRequestScopedDataContext<RubricOnDataContext>(null, connectionString);
        }

        public CategoriasRubricasRepository(String connectionString)
        {
            this.connectionString = connectionString;
        }

        private IQueryable<CategoriasRubricasBE> GetQueryable()
        {
            var DataContextObject = GetDataContextObject();
            var CategoriasRubricas= from x in DataContextObject.CategoriasRubricas
                             select new CategoriasRubricasBE
                             {
						CategoriaRubricaId = x.CategoriaRubricaId,
						Nombre = x.Nombre,
						Orden = x.Orden,
						OutcomeId = x.OutcomeId,
						RubricaId = x.RubricaId,
						TipoArtefacto = x.TipoArtefacto,
						Version = x.Version,
                             };
            return CategoriasRubricas;
        }

        private CategoriasRubricasBE GetLinqFK(CategoriasRubricas DataContextObject)
        {
		if(DataContextObject==null)
			return null;
            return new CategoriasRubricasBE()
                {
						CategoriaRubricaId = DataContextObject.CategoriaRubricaId,
						Nombre = DataContextObject.Nombre,
						Orden = DataContextObject.Orden,
						OutcomeId = DataContextObject.OutcomeId,
						RubricaId = DataContextObject.RubricaId,
						TipoArtefacto = DataContextObject.TipoArtefacto,
						Version = DataContextObject.Version,
                };
        }

        public CategoriasRubricasBE GetLinq(CategoriasRubricas DataContextObject)
        {
		if(DataContextObject==null)
			return null;
            return new CategoriasRubricasBE()
                {
				CategoriaRubricaId = DataContextObject.CategoriaRubricaId,
				Nombre = DataContextObject.Nombre,
				Orden = DataContextObject.Orden,
				OutcomeId = DataContextObject.OutcomeId,
				RubricaId = DataContextObject.RubricaId,
				TipoArtefacto = DataContextObject.TipoArtefacto,
				Version = DataContextObject.Version,
                };
        }

        public List<CategoriasRubricasBE> GetAll()
        {
            return GetQueryable().ToList();
        }

        public List<CategoriasRubricasBE> GetWhere(System.Linq.Expressions.Expression<Func<CategoriasRubricasBE,bool>> Where)
	        {
            return GetQueryable().Where(Where).ToList();
        }

        public List<CategoriasRubricasBE> GetWhere<T>(System.Linq.Expressions.Expression<Func<CategoriasRubricasBE,bool>> Where,System.Linq.Expressions.Expression<Func<CategoriasRubricasBE,T>> OrderBy)
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

        public CategoriasRubricasBE GetOne(Int32 CategoriaRubricaId)
        {
            	return GetQueryable().SingleOrDefault(x =>  x.CategoriaRubricaId == CategoriaRubricaId);
        }

        public Int32 GetLastId()
        {
            		return GetQueryable().Max(x => x.CategoriaRubricaId);
        }

        public bool InsertIdentity(CategoriasRubricasBE objInsert, bool ThrowException)
        {
		var DataContextObject = GetDataContextObject();
		CategoriasRubricas objInsertLinq = new CategoriasRubricas();
			objInsertLinq.CategoriaRubricaId = objInsert.CategoriaRubricaId;
			objInsertLinq.Nombre = objInsert.Nombre;
			objInsertLinq.Orden = objInsert.Orden;
			objInsertLinq.OutcomeId = objInsert.OutcomeId;
			objInsertLinq.RubricaId = objInsert.RubricaId;
			objInsertLinq.TipoArtefacto = objInsert.TipoArtefacto;
			objInsertLinq.Version = objInsert.Version;
		DataContextObject.CategoriasRubricas.InsertOnSubmit(objInsertLinq);
		try
            	{	
		  DataContextObject.SubmitChanges();
		  objInsert.CategoriaRubricaId = objInsertLinq.CategoriaRubricaId;
                return true;
            	}
            	catch (Exception Ex)
            	{
                if (ThrowException)
                    throw Ex;
                return false;
            	}
        }

        public void Insert(CategoriasRubricasBE objInsert)
        {
		var DataContextObject = GetDataContextObject();
		CategoriasRubricas objInsertLinq = new CategoriasRubricas();
			objInsertLinq.CategoriaRubricaId = objInsert.CategoriaRubricaId;
			objInsertLinq.Nombre = objInsert.Nombre;
			objInsertLinq.Orden = objInsert.Orden;
			objInsertLinq.OutcomeId = objInsert.OutcomeId;
			objInsertLinq.RubricaId = objInsert.RubricaId;
			objInsertLinq.TipoArtefacto = objInsert.TipoArtefacto;
			objInsertLinq.Version = objInsert.Version;
		DataContextObject.CategoriasRubricas.InsertOnSubmit(objInsertLinq);
        }

        public void Insert(List<CategoriasRubricasBE> listObjInsert)
        {
		var DataContextObject = GetDataContextObject();
		foreach(var objInsert in listObjInsert)
		{
		CategoriasRubricas objInsertLinq = new CategoriasRubricas();
			objInsertLinq.CategoriaRubricaId = objInsert.CategoriaRubricaId;
			objInsertLinq.Nombre = objInsert.Nombre;
			objInsertLinq.Orden = objInsert.Orden;
			objInsertLinq.OutcomeId = objInsert.OutcomeId;
			objInsertLinq.RubricaId = objInsert.RubricaId;
			objInsertLinq.TipoArtefacto = objInsert.TipoArtefacto;
			objInsertLinq.Version = objInsert.Version;
			DataContextObject.CategoriasRubricas.InsertOnSubmit(objInsertLinq);
		}		
        }

        public void InsertOrUpdate(CategoriasRubricasBE objInsertOrUpdate)
        {
			var DataContextObject = GetDataContextObject();
			var existentObj = DataContextObject.CategoriasRubricas.SingleOrDefault(x =>  x.CategoriaRubricaId == objInsertOrUpdate.CategoriaRubricaId);
            	if (existentObj == null)
              	Insert(objInsertOrUpdate);
            	else
                	Update(objInsertOrUpdate);	
        } 

        public void InsertOrUpdate(List<CategoriasRubricasBE> listObjInsertOrUpdate)
        {
			foreach(var objInsertOrUpdate in listObjInsertOrUpdate)
			{
				InsertOrUpdate(objInsertOrUpdate);
			}	
        } 

        public void DeleteWhere(System.Linq.Expressions.Expression<Func<CategoriasRubricasBE,bool>> Filtro)
        {
            var DataContextObject = GetDataContextObject();
		Delete(GetWhere(Filtro));
        }

        public void Delete(CategoriasRubricasBE objDelete)
        {
		var DataContextObject = GetDataContextObject();
            var objDeleteLinq = DataContextObject.CategoriasRubricas.Single(x =>  x.CategoriaRubricaId == objDelete.CategoriaRubricaId);
		DataContextObject.CategoriasRubricas.DeleteOnSubmit(objDeleteLinq);
        }

        public void Delete(List<CategoriasRubricasBE> listObjDelete)
        {
		var DataContextObject = GetDataContextObject();
		foreach(var objDelete in listObjDelete)
		{
            	var objDeleteLinq = DataContextObject.CategoriasRubricas.Single(x =>  x.CategoriaRubricaId == objDelete.CategoriaRubricaId);
			DataContextObject.CategoriasRubricas.DeleteOnSubmit(objDeleteLinq);
		}
        }

        public void TryDeleteWhere(System.Linq.Expressions.Expression<Func<CategoriasRubricasBE,bool>> Filtro)
        {
            var DataContextObject = GetDataContextObject();
		TryDelete(GetWhere(Filtro));
        }

        public void TryDelete(CategoriasRubricasBE objDelete)
        {
		var DataContextObject = GetDataContextObject();
            var objDeleteLinq = DataContextObject.CategoriasRubricas.SingleOrDefault(x =>  x.CategoriaRubricaId == objDelete.CategoriaRubricaId);
		if(objDeleteLinq !=null)
			DataContextObject.CategoriasRubricas.DeleteOnSubmit(objDeleteLinq);
        }

        public void TryDelete(List<CategoriasRubricasBE> listObjDelete)
        {
		var DataContextObject = GetDataContextObject();
		foreach(var objDelete in listObjDelete)
		{
            	var objDeleteLinq = DataContextObject.CategoriasRubricas.SingleOrDefault(x =>  x.CategoriaRubricaId == objDelete.CategoriaRubricaId);
			if(objDeleteLinq !=null)			
				DataContextObject.CategoriasRubricas.DeleteOnSubmit(objDeleteLinq);
		}
        }

        public bool Exists(CategoriasRubricasBE objExists)
        {
		var DataContextObject = GetDataContextObject();
            return DataContextObject.CategoriasRubricas.Any(x =>  x.CategoriaRubricaId == objExists.CategoriaRubricaId);
        }

        public void Update(CategoriasRubricasBE objUpdate)
        {
		var DataContextObject = GetDataContextObject();
            var objUpdateLinq = DataContextObject.CategoriasRubricas.Single(x =>  x.CategoriaRubricaId == objUpdate.CategoriaRubricaId);
			objUpdateLinq.CategoriaRubricaId = objUpdate.CategoriaRubricaId;
			objUpdateLinq.Nombre = objUpdate.Nombre;
			objUpdateLinq.Orden = objUpdate.Orden;
			objUpdateLinq.OutcomeId = objUpdate.OutcomeId;
			objUpdateLinq.RubricaId = objUpdate.RubricaId;
			objUpdateLinq.TipoArtefacto = objUpdate.TipoArtefacto;
			objUpdateLinq.Version = objUpdate.Version;
        }

        public void Update(List<CategoriasRubricasBE> listObjUpdate)
        {
		var DataContextObject = GetDataContextObject();
		foreach(var objUpdate in listObjUpdate)
		{
            	var objUpdateLinq = DataContextObject.CategoriasRubricas.Single(x =>  x.CategoriaRubricaId == objUpdate.CategoriaRubricaId);
			objUpdateLinq.CategoriaRubricaId = objUpdate.CategoriaRubricaId;
			objUpdateLinq.Nombre = objUpdate.Nombre;
			objUpdateLinq.Orden = objUpdate.Orden;
			objUpdateLinq.OutcomeId = objUpdate.OutcomeId;
			objUpdateLinq.RubricaId = objUpdate.RubricaId;
			objUpdateLinq.TipoArtefacto = objUpdate.TipoArtefacto;
			objUpdateLinq.Version = objUpdate.Version;
		}
        }
    }
}
