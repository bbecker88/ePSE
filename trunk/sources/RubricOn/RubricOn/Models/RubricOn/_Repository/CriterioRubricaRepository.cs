using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RubricOn.Models.RubricOn.Entities; 
using  RubricOn.Models.RubricOn;

namespace RubricOn.Models.RubricOn.Repository
{
    public partial class CriterioRubricaRepository 
    {
        String connectionString = "";
	  RubricOnDataContext DataContextObjectType;

        private RubricOnDataContext GetDataContextObject()
        {
            return DataContextFactory.GetWebRequestScopedDataContext<RubricOnDataContext>(null, connectionString);
        }

        public CriterioRubricaRepository(String connectionString)
        {
            this.connectionString = connectionString;
        }

        private IQueryable<CriterioRubricaBE> GetQueryable()
        {
            var DataContextObject = GetDataContextObject();
            var CriterioRubrica= from x in DataContextObject.CriterioRubrica
                             select new CriterioRubricaBE
                             {
						AspectoRubricaId = x.AspectoRubricaId,
						CriterioRubricaId = x.CriterioRubricaId,
						Nombre = x.Nombre,
						Orden = x.Orden,
						Valor = x.Valor,
						ExtraAspectoRubrica = RubricOnRepositoryFactory.GetAspectosRubricaRepository().GetLinq(x.AspectosRubrica),
                             };
            return CriterioRubrica;
        }

        private CriterioRubricaBE GetLinqFK(CriterioRubrica DataContextObject)
        {
		if(DataContextObject==null)
			return null;
            return new CriterioRubricaBE()
                {
						AspectoRubricaId = DataContextObject.AspectoRubricaId,
						CriterioRubricaId = DataContextObject.CriterioRubricaId,
						Nombre = DataContextObject.Nombre,
						Orden = DataContextObject.Orden,
						Valor = DataContextObject.Valor,
						ExtraAspectoRubrica = RubricOnRepositoryFactory.GetAspectosRubricaRepository().GetLinq(DataContextObject.AspectosRubrica),
                };
        }

        public CriterioRubricaBE GetLinq(CriterioRubrica DataContextObject)
        {
		if(DataContextObject==null)
			return null;
            return new CriterioRubricaBE()
                {
				AspectoRubricaId = DataContextObject.AspectoRubricaId,
				CriterioRubricaId = DataContextObject.CriterioRubricaId,
				Nombre = DataContextObject.Nombre,
				Orden = DataContextObject.Orden,
				Valor = DataContextObject.Valor,
				ExtraAspectoRubrica = new AspectosRubricaBE() { AspectoRubricaId= DataContextObject.AspectoRubricaId},
                };
        }

        public List<CriterioRubricaBE> GetAll()
        {
            return GetQueryable().ToList();
        }

        public List<CriterioRubricaBE> GetWhere(System.Linq.Expressions.Expression<Func<CriterioRubricaBE,bool>> Where)
	        {
            return GetQueryable().Where(Where).ToList();
        }

        public List<CriterioRubricaBE> GetWhere<T>(System.Linq.Expressions.Expression<Func<CriterioRubricaBE,bool>> Where,System.Linq.Expressions.Expression<Func<CriterioRubricaBE,T>> OrderBy)
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

        public CriterioRubricaBE GetOne(Int32 CriterioRubricaId)
        {
            	return GetQueryable().SingleOrDefault(x =>  x.CriterioRubricaId == CriterioRubricaId);
        }

        public Int32 GetLastId()
        {
            		return GetQueryable().Max(x => x.CriterioRubricaId);
        }

        public bool InsertIdentity(CriterioRubricaBE objInsert, bool ThrowException)
        {
		var DataContextObject = GetDataContextObject();
		CriterioRubrica objInsertLinq = new CriterioRubrica();
			objInsertLinq.AspectoRubricaId = objInsert.AspectoRubricaId;
			objInsertLinq.CriterioRubricaId = objInsert.CriterioRubricaId;
			objInsertLinq.Nombre = objInsert.Nombre;
			objInsertLinq.Orden = objInsert.Orden;
			objInsertLinq.Valor = objInsert.Valor;
		DataContextObject.CriterioRubrica.InsertOnSubmit(objInsertLinq);
		try
            	{	
		  DataContextObject.SubmitChanges();
		  objInsert.CriterioRubricaId = objInsertLinq.CriterioRubricaId;
                return true;
            	}
            	catch (Exception Ex)
            	{
                if (ThrowException)
                    throw Ex;
                return false;
            	}
        }

        public void Insert(CriterioRubricaBE objInsert)
        {
		var DataContextObject = GetDataContextObject();
		CriterioRubrica objInsertLinq = new CriterioRubrica();
			objInsertLinq.AspectoRubricaId = objInsert.AspectoRubricaId;
			objInsertLinq.CriterioRubricaId = objInsert.CriterioRubricaId;
			objInsertLinq.Nombre = objInsert.Nombre;
			objInsertLinq.Orden = objInsert.Orden;
			objInsertLinq.Valor = objInsert.Valor;
		DataContextObject.CriterioRubrica.InsertOnSubmit(objInsertLinq);
        }

        public void Insert(List<CriterioRubricaBE> listObjInsert)
        {
		var DataContextObject = GetDataContextObject();
		foreach(var objInsert in listObjInsert)
		{
		CriterioRubrica objInsertLinq = new CriterioRubrica();
			objInsertLinq.AspectoRubricaId = objInsert.AspectoRubricaId;
			objInsertLinq.CriterioRubricaId = objInsert.CriterioRubricaId;
			objInsertLinq.Nombre = objInsert.Nombre;
			objInsertLinq.Orden = objInsert.Orden;
			objInsertLinq.Valor = objInsert.Valor;
			DataContextObject.CriterioRubrica.InsertOnSubmit(objInsertLinq);
		}		
        }

        public void InsertOrUpdate(CriterioRubricaBE objInsertOrUpdate)
        {
			var DataContextObject = GetDataContextObject();
			var existentObj = DataContextObject.CriterioRubrica.SingleOrDefault(x =>  x.CriterioRubricaId == objInsertOrUpdate.CriterioRubricaId);
            	if (existentObj == null)
              	Insert(objInsertOrUpdate);
            	else
                	Update(objInsertOrUpdate);	
        } 

        public void InsertOrUpdate(List<CriterioRubricaBE> listObjInsertOrUpdate)
        {
			foreach(var objInsertOrUpdate in listObjInsertOrUpdate)
			{
				InsertOrUpdate(objInsertOrUpdate);
			}	
        } 

        public void DeleteWhere(System.Linq.Expressions.Expression<Func<CriterioRubricaBE,bool>> Filtro)
        {
            var DataContextObject = GetDataContextObject();
		Delete(GetWhere(Filtro));
        }

        public void Delete(CriterioRubricaBE objDelete)
        {
		var DataContextObject = GetDataContextObject();
            var objDeleteLinq = DataContextObject.CriterioRubrica.Single(x =>  x.CriterioRubricaId == objDelete.CriterioRubricaId);
		DataContextObject.CriterioRubrica.DeleteOnSubmit(objDeleteLinq);
        }

        public void Delete(List<CriterioRubricaBE> listObjDelete)
        {
		var DataContextObject = GetDataContextObject();
		foreach(var objDelete in listObjDelete)
		{
            	var objDeleteLinq = DataContextObject.CriterioRubrica.Single(x =>  x.CriterioRubricaId == objDelete.CriterioRubricaId);
			DataContextObject.CriterioRubrica.DeleteOnSubmit(objDeleteLinq);
		}
        }

        public void TryDeleteWhere(System.Linq.Expressions.Expression<Func<CriterioRubricaBE,bool>> Filtro)
        {
            var DataContextObject = GetDataContextObject();
		TryDelete(GetWhere(Filtro));
        }

        public void TryDelete(CriterioRubricaBE objDelete)
        {
		var DataContextObject = GetDataContextObject();
            var objDeleteLinq = DataContextObject.CriterioRubrica.SingleOrDefault(x =>  x.CriterioRubricaId == objDelete.CriterioRubricaId);
		if(objDeleteLinq !=null)
			DataContextObject.CriterioRubrica.DeleteOnSubmit(objDeleteLinq);
        }

        public void TryDelete(List<CriterioRubricaBE> listObjDelete)
        {
		var DataContextObject = GetDataContextObject();
		foreach(var objDelete in listObjDelete)
		{
            	var objDeleteLinq = DataContextObject.CriterioRubrica.SingleOrDefault(x =>  x.CriterioRubricaId == objDelete.CriterioRubricaId);
			if(objDeleteLinq !=null)			
				DataContextObject.CriterioRubrica.DeleteOnSubmit(objDeleteLinq);
		}
        }

        public bool Exists(CriterioRubricaBE objExists)
        {
		var DataContextObject = GetDataContextObject();
            return DataContextObject.CriterioRubrica.Any(x =>  x.CriterioRubricaId == objExists.CriterioRubricaId);
        }

        public void Update(CriterioRubricaBE objUpdate)
        {
		var DataContextObject = GetDataContextObject();
            var objUpdateLinq = DataContextObject.CriterioRubrica.Single(x =>  x.CriterioRubricaId == objUpdate.CriterioRubricaId);
			objUpdateLinq.AspectoRubricaId = objUpdate.AspectoRubricaId;
			objUpdateLinq.CriterioRubricaId = objUpdate.CriterioRubricaId;
			objUpdateLinq.Nombre = objUpdate.Nombre;
			objUpdateLinq.Orden = objUpdate.Orden;
			objUpdateLinq.Valor = objUpdate.Valor;
        }

        public void Update(List<CriterioRubricaBE> listObjUpdate)
        {
		var DataContextObject = GetDataContextObject();
		foreach(var objUpdate in listObjUpdate)
		{
            	var objUpdateLinq = DataContextObject.CriterioRubrica.Single(x =>  x.CriterioRubricaId == objUpdate.CriterioRubricaId);
			objUpdateLinq.AspectoRubricaId = objUpdate.AspectoRubricaId;
			objUpdateLinq.CriterioRubricaId = objUpdate.CriterioRubricaId;
			objUpdateLinq.Nombre = objUpdate.Nombre;
			objUpdateLinq.Orden = objUpdate.Orden;
			objUpdateLinq.Valor = objUpdate.Valor;
		}
        }
    }
}
