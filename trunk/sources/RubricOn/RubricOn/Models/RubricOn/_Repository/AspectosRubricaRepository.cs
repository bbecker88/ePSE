using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RubricOn.Models.RubricOn.Entities; 
using  RubricOn.Models.RubricOn;

namespace RubricOn.Models.RubricOn.Repository
{
    public partial class AspectosRubricaRepository 
    {
        String connectionString = "";
	  RubricOnDataContext DataContextObjectType;

        private RubricOnDataContext GetDataContextObject()
        {
            return DataContextFactory.GetWebRequestScopedDataContext<RubricOnDataContext>(null, connectionString);
        }

        public AspectosRubricaRepository(String connectionString)
        {
            this.connectionString = connectionString;
        }

        private IQueryable<AspectosRubricaBE> GetQueryable()
        {
            var DataContextObject = GetDataContextObject();
            var AspectosRubrica= from x in DataContextObject.AspectosRubrica
                             select new AspectosRubricaBE
                             {
						AspectoRubricaId = x.AspectoRubricaId,
						CategoriaRubricaId = x.CategoriaRubricaId,
						Nombre = x.Nombre,
						Orden = x.Orden,
						ExtraCategoriaRubrica = RubricOnRepositoryFactory.GetCategoriasRubricasRepository().GetLinq(x.CategoriasRubricas),
                             };
            return AspectosRubrica;
        }

        private AspectosRubricaBE GetLinqFK(AspectosRubrica DataContextObject)
        {
		if(DataContextObject==null)
			return null;
            return new AspectosRubricaBE()
                {
						AspectoRubricaId = DataContextObject.AspectoRubricaId,
						CategoriaRubricaId = DataContextObject.CategoriaRubricaId,
						Nombre = DataContextObject.Nombre,
						Orden = DataContextObject.Orden,
						ExtraCategoriaRubrica = RubricOnRepositoryFactory.GetCategoriasRubricasRepository().GetLinq(DataContextObject.CategoriasRubricas),
                };
        }

        public AspectosRubricaBE GetLinq(AspectosRubrica DataContextObject)
        {
		if(DataContextObject==null)
			return null;
            return new AspectosRubricaBE()
                {
				AspectoRubricaId = DataContextObject.AspectoRubricaId,
				CategoriaRubricaId = DataContextObject.CategoriaRubricaId,
				Nombre = DataContextObject.Nombre,
				Orden = DataContextObject.Orden,
				ExtraCategoriaRubrica = new CategoriasRubricasBE() { CategoriaRubricaId= DataContextObject.CategoriaRubricaId},
                };
        }

        public List<AspectosRubricaBE> GetAll()
        {
            return GetQueryable().ToList();
        }

        public List<AspectosRubricaBE> GetWhere(System.Linq.Expressions.Expression<Func<AspectosRubricaBE,bool>> Where)
	        {
            return GetQueryable().Where(Where).ToList();
        }

        public List<AspectosRubricaBE> GetWhere<T>(System.Linq.Expressions.Expression<Func<AspectosRubricaBE,bool>> Where,System.Linq.Expressions.Expression<Func<AspectosRubricaBE,T>> OrderBy)
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

        public AspectosRubricaBE GetOne(Int32 AspectoRubricaId)
        {
            	return GetQueryable().SingleOrDefault(x =>  x.AspectoRubricaId == AspectoRubricaId);
        }

        public Int32 GetLastId()
        {
            		return GetQueryable().Max(x => x.AspectoRubricaId);
        }

        public bool InsertIdentity(AspectosRubricaBE objInsert, bool ThrowException)
        {
		var DataContextObject = GetDataContextObject();
		AspectosRubrica objInsertLinq = new AspectosRubrica();
			objInsertLinq.AspectoRubricaId = objInsert.AspectoRubricaId;
			objInsertLinq.CategoriaRubricaId = objInsert.CategoriaRubricaId;
			objInsertLinq.Nombre = objInsert.Nombre;
			objInsertLinq.Orden = objInsert.Orden;
		DataContextObject.AspectosRubrica.InsertOnSubmit(objInsertLinq);
		try
            	{	
		  DataContextObject.SubmitChanges();
		  objInsert.AspectoRubricaId = objInsertLinq.AspectoRubricaId;
                return true;
            	}
            	catch (Exception Ex)
            	{
                if (ThrowException)
                    throw Ex;
                return false;
            	}
        }

        public void Insert(AspectosRubricaBE objInsert)
        {
		var DataContextObject = GetDataContextObject();
		AspectosRubrica objInsertLinq = new AspectosRubrica();
			objInsertLinq.AspectoRubricaId = objInsert.AspectoRubricaId;
			objInsertLinq.CategoriaRubricaId = objInsert.CategoriaRubricaId;
			objInsertLinq.Nombre = objInsert.Nombre;
			objInsertLinq.Orden = objInsert.Orden;
		DataContextObject.AspectosRubrica.InsertOnSubmit(objInsertLinq);
        }

        public void Insert(List<AspectosRubricaBE> listObjInsert)
        {
		var DataContextObject = GetDataContextObject();
		foreach(var objInsert in listObjInsert)
		{
		AspectosRubrica objInsertLinq = new AspectosRubrica();
			objInsertLinq.AspectoRubricaId = objInsert.AspectoRubricaId;
			objInsertLinq.CategoriaRubricaId = objInsert.CategoriaRubricaId;
			objInsertLinq.Nombre = objInsert.Nombre;
			objInsertLinq.Orden = objInsert.Orden;
			DataContextObject.AspectosRubrica.InsertOnSubmit(objInsertLinq);
		}		
        }

        public void InsertOrUpdate(AspectosRubricaBE objInsertOrUpdate)
        {
			var DataContextObject = GetDataContextObject();
			var existentObj = DataContextObject.AspectosRubrica.SingleOrDefault(x =>  x.AspectoRubricaId == objInsertOrUpdate.AspectoRubricaId);
            	if (existentObj == null)
              	Insert(objInsertOrUpdate);
            	else
                	Update(objInsertOrUpdate);	
        } 

        public void InsertOrUpdate(List<AspectosRubricaBE> listObjInsertOrUpdate)
        {
			foreach(var objInsertOrUpdate in listObjInsertOrUpdate)
			{
				InsertOrUpdate(objInsertOrUpdate);
			}	
        } 

        public void DeleteWhere(System.Linq.Expressions.Expression<Func<AspectosRubricaBE,bool>> Filtro)
        {
            var DataContextObject = GetDataContextObject();
		Delete(GetWhere(Filtro));
        }

        public void Delete(AspectosRubricaBE objDelete)
        {
		var DataContextObject = GetDataContextObject();
            var objDeleteLinq = DataContextObject.AspectosRubrica.Single(x =>  x.AspectoRubricaId == objDelete.AspectoRubricaId);
		DataContextObject.AspectosRubrica.DeleteOnSubmit(objDeleteLinq);
        }

        public void Delete(List<AspectosRubricaBE> listObjDelete)
        {
		var DataContextObject = GetDataContextObject();
		foreach(var objDelete in listObjDelete)
		{
            	var objDeleteLinq = DataContextObject.AspectosRubrica.Single(x =>  x.AspectoRubricaId == objDelete.AspectoRubricaId);
			DataContextObject.AspectosRubrica.DeleteOnSubmit(objDeleteLinq);
		}
        }

        public void TryDeleteWhere(System.Linq.Expressions.Expression<Func<AspectosRubricaBE,bool>> Filtro)
        {
            var DataContextObject = GetDataContextObject();
		TryDelete(GetWhere(Filtro));
        }

        public void TryDelete(AspectosRubricaBE objDelete)
        {
		var DataContextObject = GetDataContextObject();
            var objDeleteLinq = DataContextObject.AspectosRubrica.SingleOrDefault(x =>  x.AspectoRubricaId == objDelete.AspectoRubricaId);
		if(objDeleteLinq !=null)
			DataContextObject.AspectosRubrica.DeleteOnSubmit(objDeleteLinq);
        }

        public void TryDelete(List<AspectosRubricaBE> listObjDelete)
        {
		var DataContextObject = GetDataContextObject();
		foreach(var objDelete in listObjDelete)
		{
            	var objDeleteLinq = DataContextObject.AspectosRubrica.SingleOrDefault(x =>  x.AspectoRubricaId == objDelete.AspectoRubricaId);
			if(objDeleteLinq !=null)			
				DataContextObject.AspectosRubrica.DeleteOnSubmit(objDeleteLinq);
		}
        }

        public bool Exists(AspectosRubricaBE objExists)
        {
		var DataContextObject = GetDataContextObject();
            return DataContextObject.AspectosRubrica.Any(x =>  x.AspectoRubricaId == objExists.AspectoRubricaId);
        }

        public void Update(AspectosRubricaBE objUpdate)
        {
		var DataContextObject = GetDataContextObject();
            var objUpdateLinq = DataContextObject.AspectosRubrica.Single(x =>  x.AspectoRubricaId == objUpdate.AspectoRubricaId);
			objUpdateLinq.AspectoRubricaId = objUpdate.AspectoRubricaId;
			objUpdateLinq.CategoriaRubricaId = objUpdate.CategoriaRubricaId;
			objUpdateLinq.Nombre = objUpdate.Nombre;
			objUpdateLinq.Orden = objUpdate.Orden;
        }

        public void Update(List<AspectosRubricaBE> listObjUpdate)
        {
		var DataContextObject = GetDataContextObject();
		foreach(var objUpdate in listObjUpdate)
		{
            	var objUpdateLinq = DataContextObject.AspectosRubrica.Single(x =>  x.AspectoRubricaId == objUpdate.AspectoRubricaId);
			objUpdateLinq.AspectoRubricaId = objUpdate.AspectoRubricaId;
			objUpdateLinq.CategoriaRubricaId = objUpdate.CategoriaRubricaId;
			objUpdateLinq.Nombre = objUpdate.Nombre;
			objUpdateLinq.Orden = objUpdate.Orden;
		}
        }
    }
}
