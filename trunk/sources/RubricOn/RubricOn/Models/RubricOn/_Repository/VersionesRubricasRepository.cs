using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RubricOn.Models.RubricOn.Entities; 
using  RubricOn.Models.RubricOn;

namespace RubricOn.Models.RubricOn.Repository
{
    public partial class VersionesRubricasRepository 
    {
        String connectionString = "";
	  RubricOnDataContext DataContextObjectType;

        private RubricOnDataContext GetDataContextObject()
        {
            return DataContextFactory.GetWebRequestScopedDataContext<RubricOnDataContext>(null, connectionString);
        }

        public VersionesRubricasRepository(String connectionString)
        {
            this.connectionString = connectionString;
        }

        private IQueryable<VersionesRubricasBE> GetQueryable()
        {
            var DataContextObject = GetDataContextObject();
            var VersionesRubricas= from x in DataContextObject.VersionesRubricas
                             select new VersionesRubricasBE
                             {
						CreadorId = x.CreadorId,
						Descripcion = x.Descripcion,
						EsActual = x.EsActual,
						FechaCreacion = x.FechaCreacion,
						RubricaId = x.RubricaId,
						TipoArtefacto = x.TipoArtefacto,
						TipoRubrica = x.TipoRubrica,
						Version = x.Version,
                             };
            return VersionesRubricas;
        }

        private VersionesRubricasBE GetLinqFK(VersionesRubricas DataContextObject)
        {
		if(DataContextObject==null)
			return null;
            return new VersionesRubricasBE()
                {
						CreadorId = DataContextObject.CreadorId,
						Descripcion = DataContextObject.Descripcion,
						EsActual = DataContextObject.EsActual,
						FechaCreacion = DataContextObject.FechaCreacion,
						RubricaId = DataContextObject.RubricaId,
						TipoArtefacto = DataContextObject.TipoArtefacto,
						TipoRubrica = DataContextObject.TipoRubrica,
						Version = DataContextObject.Version,
                };
        }

        public VersionesRubricasBE GetLinq(VersionesRubricas DataContextObject)
        {
		if(DataContextObject==null)
			return null;
            return new VersionesRubricasBE()
                {
				CreadorId = DataContextObject.CreadorId,
				Descripcion = DataContextObject.Descripcion,
				EsActual = DataContextObject.EsActual,
				FechaCreacion = DataContextObject.FechaCreacion,
				RubricaId = DataContextObject.RubricaId,
				TipoArtefacto = DataContextObject.TipoArtefacto,
				TipoRubrica = DataContextObject.TipoRubrica,
				Version = DataContextObject.Version,
                };
        }

        public List<VersionesRubricasBE> GetAll()
        {
            return GetQueryable().ToList();
        }

        public List<VersionesRubricasBE> GetWhere(System.Linq.Expressions.Expression<Func<VersionesRubricasBE,bool>> Where)
	        {
            return GetQueryable().Where(Where).ToList();
        }

        public List<VersionesRubricasBE> GetWhere<T>(System.Linq.Expressions.Expression<Func<VersionesRubricasBE,bool>> Where,System.Linq.Expressions.Expression<Func<VersionesRubricasBE,T>> OrderBy)
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

        public VersionesRubricasBE GetOne(String RubricaId, String TipoArtefacto, String Version)
        {
            	return GetQueryable().SingleOrDefault(x =>  x.RubricaId == RubricaId  && x.TipoArtefacto == TipoArtefacto  && x.Version == Version);
        }

        public Int32 GetLastId()
        {
            		return 0;
        }

        public bool InsertIdentity(VersionesRubricasBE objInsert, bool ThrowException)
        {
		var DataContextObject = GetDataContextObject();
		VersionesRubricas objInsertLinq = new VersionesRubricas();
			objInsertLinq.CreadorId = objInsert.CreadorId;
			objInsertLinq.Descripcion = objInsert.Descripcion;
			objInsertLinq.EsActual = objInsert.EsActual;
			objInsertLinq.FechaCreacion = objInsert.FechaCreacion;
			objInsertLinq.RubricaId = objInsert.RubricaId;
			objInsertLinq.TipoArtefacto = objInsert.TipoArtefacto;
			objInsertLinq.TipoRubrica = objInsert.TipoRubrica;
			objInsertLinq.Version = objInsert.Version;
		DataContextObject.VersionesRubricas.InsertOnSubmit(objInsertLinq);
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

        public void Insert(VersionesRubricasBE objInsert)
        {
		var DataContextObject = GetDataContextObject();
		VersionesRubricas objInsertLinq = new VersionesRubricas();
			objInsertLinq.CreadorId = objInsert.CreadorId;
			objInsertLinq.Descripcion = objInsert.Descripcion;
			objInsertLinq.EsActual = objInsert.EsActual;
			objInsertLinq.FechaCreacion = objInsert.FechaCreacion;
			objInsertLinq.RubricaId = objInsert.RubricaId;
			objInsertLinq.TipoArtefacto = objInsert.TipoArtefacto;
			objInsertLinq.TipoRubrica = objInsert.TipoRubrica;
			objInsertLinq.Version = objInsert.Version;
		DataContextObject.VersionesRubricas.InsertOnSubmit(objInsertLinq);
        }

        public void Insert(List<VersionesRubricasBE> listObjInsert)
        {
		var DataContextObject = GetDataContextObject();
		foreach(var objInsert in listObjInsert)
		{
		VersionesRubricas objInsertLinq = new VersionesRubricas();
			objInsertLinq.CreadorId = objInsert.CreadorId;
			objInsertLinq.Descripcion = objInsert.Descripcion;
			objInsertLinq.EsActual = objInsert.EsActual;
			objInsertLinq.FechaCreacion = objInsert.FechaCreacion;
			objInsertLinq.RubricaId = objInsert.RubricaId;
			objInsertLinq.TipoArtefacto = objInsert.TipoArtefacto;
			objInsertLinq.TipoRubrica = objInsert.TipoRubrica;
			objInsertLinq.Version = objInsert.Version;
			DataContextObject.VersionesRubricas.InsertOnSubmit(objInsertLinq);
		}		
        }

        public void InsertOrUpdate(VersionesRubricasBE objInsertOrUpdate)
        {
			var DataContextObject = GetDataContextObject();
			var existentObj = DataContextObject.VersionesRubricas.SingleOrDefault(x =>  x.RubricaId == objInsertOrUpdate.RubricaId  && x.TipoArtefacto == objInsertOrUpdate.TipoArtefacto  && x.Version == objInsertOrUpdate.Version);
            	if (existentObj == null)
              	Insert(objInsertOrUpdate);
            	else
                	Update(objInsertOrUpdate);	
        } 

        public void InsertOrUpdate(List<VersionesRubricasBE> listObjInsertOrUpdate)
        {
			foreach(var objInsertOrUpdate in listObjInsertOrUpdate)
			{
				InsertOrUpdate(objInsertOrUpdate);
			}	
        } 

        public void DeleteWhere(System.Linq.Expressions.Expression<Func<VersionesRubricasBE,bool>> Filtro)
        {
            var DataContextObject = GetDataContextObject();
		Delete(GetWhere(Filtro));
        }

        public void Delete(VersionesRubricasBE objDelete)
        {
		var DataContextObject = GetDataContextObject();
            var objDeleteLinq = DataContextObject.VersionesRubricas.Single(x =>  x.RubricaId == objDelete.RubricaId  && x.TipoArtefacto == objDelete.TipoArtefacto  && x.Version == objDelete.Version);
		DataContextObject.VersionesRubricas.DeleteOnSubmit(objDeleteLinq);
        }

        public void Delete(List<VersionesRubricasBE> listObjDelete)
        {
		var DataContextObject = GetDataContextObject();
		foreach(var objDelete in listObjDelete)
		{
            	var objDeleteLinq = DataContextObject.VersionesRubricas.Single(x =>  x.RubricaId == objDelete.RubricaId  && x.TipoArtefacto == objDelete.TipoArtefacto  && x.Version == objDelete.Version);
			DataContextObject.VersionesRubricas.DeleteOnSubmit(objDeleteLinq);
		}
        }

        public void TryDeleteWhere(System.Linq.Expressions.Expression<Func<VersionesRubricasBE,bool>> Filtro)
        {
            var DataContextObject = GetDataContextObject();
		TryDelete(GetWhere(Filtro));
        }

        public void TryDelete(VersionesRubricasBE objDelete)
        {
		var DataContextObject = GetDataContextObject();
            var objDeleteLinq = DataContextObject.VersionesRubricas.SingleOrDefault(x =>  x.RubricaId == objDelete.RubricaId  && x.TipoArtefacto == objDelete.TipoArtefacto  && x.Version == objDelete.Version);
		if(objDeleteLinq !=null)
			DataContextObject.VersionesRubricas.DeleteOnSubmit(objDeleteLinq);
        }

        public void TryDelete(List<VersionesRubricasBE> listObjDelete)
        {
		var DataContextObject = GetDataContextObject();
		foreach(var objDelete in listObjDelete)
		{
            	var objDeleteLinq = DataContextObject.VersionesRubricas.SingleOrDefault(x =>  x.RubricaId == objDelete.RubricaId  && x.TipoArtefacto == objDelete.TipoArtefacto  && x.Version == objDelete.Version);
			if(objDeleteLinq !=null)			
				DataContextObject.VersionesRubricas.DeleteOnSubmit(objDeleteLinq);
		}
        }

        public bool Exists(VersionesRubricasBE objExists)
        {
		var DataContextObject = GetDataContextObject();
            return DataContextObject.VersionesRubricas.Any(x =>  x.RubricaId == objExists.RubricaId  && x.TipoArtefacto == objExists.TipoArtefacto  && x.Version == objExists.Version);
        }

        public void Update(VersionesRubricasBE objUpdate)
        {
		var DataContextObject = GetDataContextObject();
            var objUpdateLinq = DataContextObject.VersionesRubricas.Single(x =>  x.RubricaId == objUpdate.RubricaId  && x.TipoArtefacto == objUpdate.TipoArtefacto  && x.Version == objUpdate.Version);
			objUpdateLinq.CreadorId = objUpdate.CreadorId;
			objUpdateLinq.Descripcion = objUpdate.Descripcion;
			objUpdateLinq.EsActual = objUpdate.EsActual;
			objUpdateLinq.FechaCreacion = objUpdate.FechaCreacion;
			objUpdateLinq.RubricaId = objUpdate.RubricaId;
			objUpdateLinq.TipoArtefacto = objUpdate.TipoArtefacto;
			objUpdateLinq.TipoRubrica = objUpdate.TipoRubrica;
			objUpdateLinq.Version = objUpdate.Version;
        }

        public void Update(List<VersionesRubricasBE> listObjUpdate)
        {
		var DataContextObject = GetDataContextObject();
		foreach(var objUpdate in listObjUpdate)
		{
            	var objUpdateLinq = DataContextObject.VersionesRubricas.Single(x =>  x.RubricaId == objUpdate.RubricaId  && x.TipoArtefacto == objUpdate.TipoArtefacto  && x.Version == objUpdate.Version);
			objUpdateLinq.CreadorId = objUpdate.CreadorId;
			objUpdateLinq.Descripcion = objUpdate.Descripcion;
			objUpdateLinq.EsActual = objUpdate.EsActual;
			objUpdateLinq.FechaCreacion = objUpdate.FechaCreacion;
			objUpdateLinq.RubricaId = objUpdate.RubricaId;
			objUpdateLinq.TipoArtefacto = objUpdate.TipoArtefacto;
			objUpdateLinq.TipoRubrica = objUpdate.TipoRubrica;
			objUpdateLinq.Version = objUpdate.Version;
		}
        }
    }
}
