using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ePortafolio.Models.ePortafolio.Entities; 
using  ePortafolio.Models.ePortafolio;

namespace ePortafolio.Models.ePortafolio.Repository
{
    public partial class TrabajosRepository 
    {
        String connectionString = "";
	  ePortafolioDataContext DataContextObjectType;

        private ePortafolioDataContext GetDataContextObject()
        {
            return DataContextFactory.GetWebRequestScopedDataContext<ePortafolioDataContext>(null, connectionString);
        }

        public TrabajosRepository(String connectionString)
        {
            this.connectionString = connectionString;
        }

        private IQueryable<TrabajosBE> GetQueryable()
        {
            var DataContextObject = GetDataContextObject();
            var Trabajos= from x in DataContextObject.Trabajos
                             select new TrabajosBE
                             {
						Codigo = x.Codigo,
						CursoId = x.CursoId,
						EsGrupal = x.EsGrupal,
						FechaFin = x.FechaFin,
						FechaInicio = x.FechaInicio,
						Iniciativa = x.Iniciativa,
						Instrucciones = x.Instrucciones,
						Nombre = x.Nombre,
						PeriodoId = x.PeriodoId,
						TrabajoId = x.TrabajoId,
                             };
            return Trabajos;
        }

        private TrabajosBE GetLinqFK(Trabajos DataContextObject)
        {
		if(DataContextObject==null)
			return null;
            return new TrabajosBE()
                {
						Codigo = DataContextObject.Codigo,
						CursoId = DataContextObject.CursoId,
						EsGrupal = DataContextObject.EsGrupal,
						FechaFin = DataContextObject.FechaFin,
						FechaInicio = DataContextObject.FechaInicio,
						Iniciativa = DataContextObject.Iniciativa,
						Instrucciones = DataContextObject.Instrucciones,
						Nombre = DataContextObject.Nombre,
						PeriodoId = DataContextObject.PeriodoId,
						TrabajoId = DataContextObject.TrabajoId,
                };
        }

        public TrabajosBE GetLinq(Trabajos DataContextObject)
        {
		if(DataContextObject==null)
			return null;
            return new TrabajosBE()
                {
				Codigo = DataContextObject.Codigo,
				CursoId = DataContextObject.CursoId,
				EsGrupal = DataContextObject.EsGrupal,
				FechaFin = DataContextObject.FechaFin,
				FechaInicio = DataContextObject.FechaInicio,
				Iniciativa = DataContextObject.Iniciativa,
				Instrucciones = DataContextObject.Instrucciones,
				Nombre = DataContextObject.Nombre,
				PeriodoId = DataContextObject.PeriodoId,
				TrabajoId = DataContextObject.TrabajoId,
                };
        }

        public List<TrabajosBE> GetAll()
        {
            return GetQueryable().ToList();
        }

        public List<TrabajosBE> GetWhere(System.Linq.Expressions.Expression<Func<TrabajosBE,bool>> Where)
	        {
            return GetQueryable().Where(Where).ToList();
        }

        public List<TrabajosBE> GetWhere<T>(System.Linq.Expressions.Expression<Func<TrabajosBE,bool>> Where,System.Linq.Expressions.Expression<Func<TrabajosBE,T>> OrderBy)
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

        public TrabajosBE GetOne(Int32 TrabajoId)
        {
            	return GetQueryable().SingleOrDefault(x =>  x.TrabajoId == TrabajoId);
        }

        public Int32 GetLastId()
        {
            		return GetQueryable().Max(x => x.TrabajoId);
        }

        public bool InsertIdentity(TrabajosBE objInsert, bool ThrowException)
        {
		var DataContextObject = GetDataContextObject();
		Trabajos objInsertLinq = new Trabajos();
			objInsertLinq.Codigo = objInsert.Codigo;
			objInsertLinq.CursoId = objInsert.CursoId;
			objInsertLinq.EsGrupal = objInsert.EsGrupal;
			objInsertLinq.FechaFin = objInsert.FechaFin;
			objInsertLinq.FechaInicio = objInsert.FechaInicio;
			objInsertLinq.Iniciativa = objInsert.Iniciativa;
			objInsertLinq.Instrucciones = objInsert.Instrucciones;
			objInsertLinq.Nombre = objInsert.Nombre;
			objInsertLinq.PeriodoId = objInsert.PeriodoId;
			objInsertLinq.TrabajoId = objInsert.TrabajoId;
		DataContextObject.Trabajos.InsertOnSubmit(objInsertLinq);
		try
            	{	
		  DataContextObject.SubmitChanges();
		  objInsert.TrabajoId = objInsertLinq.TrabajoId;
                return true;
            	}
            	catch (Exception Ex)
            	{
                if (ThrowException)
                    throw Ex;
                return false;
            	}
        }

        public void Insert(TrabajosBE objInsert)
        {
		var DataContextObject = GetDataContextObject();
		Trabajos objInsertLinq = new Trabajos();
			objInsertLinq.Codigo = objInsert.Codigo;
			objInsertLinq.CursoId = objInsert.CursoId;
			objInsertLinq.EsGrupal = objInsert.EsGrupal;
			objInsertLinq.FechaFin = objInsert.FechaFin;
			objInsertLinq.FechaInicio = objInsert.FechaInicio;
			objInsertLinq.Iniciativa = objInsert.Iniciativa;
			objInsertLinq.Instrucciones = objInsert.Instrucciones;
			objInsertLinq.Nombre = objInsert.Nombre;
			objInsertLinq.PeriodoId = objInsert.PeriodoId;
			objInsertLinq.TrabajoId = objInsert.TrabajoId;
		DataContextObject.Trabajos.InsertOnSubmit(objInsertLinq);
        }

        public void Insert(List<TrabajosBE> listObjInsert)
        {
		var DataContextObject = GetDataContextObject();
		foreach(var objInsert in listObjInsert)
		{
		Trabajos objInsertLinq = new Trabajos();
			objInsertLinq.Codigo = objInsert.Codigo;
			objInsertLinq.CursoId = objInsert.CursoId;
			objInsertLinq.EsGrupal = objInsert.EsGrupal;
			objInsertLinq.FechaFin = objInsert.FechaFin;
			objInsertLinq.FechaInicio = objInsert.FechaInicio;
			objInsertLinq.Iniciativa = objInsert.Iniciativa;
			objInsertLinq.Instrucciones = objInsert.Instrucciones;
			objInsertLinq.Nombre = objInsert.Nombre;
			objInsertLinq.PeriodoId = objInsert.PeriodoId;
			objInsertLinq.TrabajoId = objInsert.TrabajoId;
			DataContextObject.Trabajos.InsertOnSubmit(objInsertLinq);
		}		
        }

        public void InsertOrUpdate(TrabajosBE objInsertOrUpdate)
        {
			var DataContextObject = GetDataContextObject();
			var existentObj = DataContextObject.Trabajos.SingleOrDefault(x =>  x.TrabajoId == objInsertOrUpdate.TrabajoId);
            	if (existentObj == null)
              	Insert(objInsertOrUpdate);
            	else
                	Update(objInsertOrUpdate);	
        } 

        public void InsertOrUpdate(List<TrabajosBE> listObjInsertOrUpdate)
        {
			foreach(var objInsertOrUpdate in listObjInsertOrUpdate)
			{
				InsertOrUpdate(objInsertOrUpdate);
			}	
        } 

        public void DeleteWhere(System.Linq.Expressions.Expression<Func<TrabajosBE,bool>> Filtro)
        {
            var DataContextObject = GetDataContextObject();
		Delete(GetWhere(Filtro));
        }

        public void Delete(TrabajosBE objDelete)
        {
		var DataContextObject = GetDataContextObject();
            var objDeleteLinq = DataContextObject.Trabajos.Single(x =>  x.TrabajoId == objDelete.TrabajoId);
		DataContextObject.Trabajos.DeleteOnSubmit(objDeleteLinq);
        }

        public void Delete(List<TrabajosBE> listObjDelete)
        {
		var DataContextObject = GetDataContextObject();
		foreach(var objDelete in listObjDelete)
		{
            	var objDeleteLinq = DataContextObject.Trabajos.Single(x =>  x.TrabajoId == objDelete.TrabajoId);
			DataContextObject.Trabajos.DeleteOnSubmit(objDeleteLinq);
		}
        }

        public void TryDeleteWhere(System.Linq.Expressions.Expression<Func<TrabajosBE,bool>> Filtro)
        {
            var DataContextObject = GetDataContextObject();
		TryDelete(GetWhere(Filtro));
        }

        public void TryDelete(TrabajosBE objDelete)
        {
		var DataContextObject = GetDataContextObject();
            var objDeleteLinq = DataContextObject.Trabajos.SingleOrDefault(x =>  x.TrabajoId == objDelete.TrabajoId);
		if(objDeleteLinq !=null)
			DataContextObject.Trabajos.DeleteOnSubmit(objDeleteLinq);
        }

        public void TryDelete(List<TrabajosBE> listObjDelete)
        {
		var DataContextObject = GetDataContextObject();
		foreach(var objDelete in listObjDelete)
		{
            	var objDeleteLinq = DataContextObject.Trabajos.SingleOrDefault(x =>  x.TrabajoId == objDelete.TrabajoId);
			if(objDeleteLinq !=null)			
				DataContextObject.Trabajos.DeleteOnSubmit(objDeleteLinq);
		}
        }

        public bool Exists(TrabajosBE objExists)
        {
		var DataContextObject = GetDataContextObject();
            return DataContextObject.Trabajos.Any(x =>  x.TrabajoId == objExists.TrabajoId);
        }

        public void Update(TrabajosBE objUpdate)
        {
		var DataContextObject = GetDataContextObject();
            var objUpdateLinq = DataContextObject.Trabajos.Single(x =>  x.TrabajoId == objUpdate.TrabajoId);
			objUpdateLinq.Codigo = objUpdate.Codigo;
			objUpdateLinq.CursoId = objUpdate.CursoId;
			objUpdateLinq.EsGrupal = objUpdate.EsGrupal;
			objUpdateLinq.FechaFin = objUpdate.FechaFin;
			objUpdateLinq.FechaInicio = objUpdate.FechaInicio;
			objUpdateLinq.Iniciativa = objUpdate.Iniciativa;
			objUpdateLinq.Instrucciones = objUpdate.Instrucciones;
			objUpdateLinq.Nombre = objUpdate.Nombre;
			objUpdateLinq.PeriodoId = objUpdate.PeriodoId;
			objUpdateLinq.TrabajoId = objUpdate.TrabajoId;
        }

        public void Update(List<TrabajosBE> listObjUpdate)
        {
		var DataContextObject = GetDataContextObject();
		foreach(var objUpdate in listObjUpdate)
		{
            	var objUpdateLinq = DataContextObject.Trabajos.Single(x =>  x.TrabajoId == objUpdate.TrabajoId);
			objUpdateLinq.Codigo = objUpdate.Codigo;
			objUpdateLinq.CursoId = objUpdate.CursoId;
			objUpdateLinq.EsGrupal = objUpdate.EsGrupal;
			objUpdateLinq.FechaFin = objUpdate.FechaFin;
			objUpdateLinq.FechaInicio = objUpdate.FechaInicio;
			objUpdateLinq.Iniciativa = objUpdate.Iniciativa;
			objUpdateLinq.Instrucciones = objUpdate.Instrucciones;
			objUpdateLinq.Nombre = objUpdate.Nombre;
			objUpdateLinq.PeriodoId = objUpdate.PeriodoId;
			objUpdateLinq.TrabajoId = objUpdate.TrabajoId;
		}
        }
    }
}
