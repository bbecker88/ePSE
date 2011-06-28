using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ePortafolio.Models.ePortafolio.Entities; 
using  ePortafolio.Models.ePortafolio;

namespace ePortafolio.Models.ePortafolio.Repository
{
    public partial class ArchivosRepository 
    {
        String connectionString = "";
	  ePortafolioDataContext DataContextObjectType;

        private ePortafolioDataContext GetDataContextObject()
        {
            return DataContextFactory.GetWebRequestScopedDataContext<ePortafolioDataContext>(null, connectionString);
        }

        public ArchivosRepository(String connectionString)
        {
            this.connectionString = connectionString;
        }

        private IQueryable<ArchivosBE> GetQueryable()
        {
            var DataContextObject = GetDataContextObject();
            var Archivos= from x in DataContextObject.Archivos
                             select new ArchivosBE
                             {
						AlumnoId = x.AlumnoId,
						ArchivoId = x.ArchivoId,
						FechaSubida = x.FechaSubida,
						Nombre = x.Nombre,
						Ruta = x.Ruta,
                             };
            return Archivos;
        }

        private ArchivosBE GetLinqFK(Archivos DataContextObject)
        {
		if(DataContextObject==null)
			return null;
            return new ArchivosBE()
                {
						AlumnoId = DataContextObject.AlumnoId,
						ArchivoId = DataContextObject.ArchivoId,
						FechaSubida = DataContextObject.FechaSubida,
						Nombre = DataContextObject.Nombre,
						Ruta = DataContextObject.Ruta,
                };
        }

        public ArchivosBE GetLinq(Archivos DataContextObject)
        {
		if(DataContextObject==null)
			return null;
            return new ArchivosBE()
                {
				AlumnoId = DataContextObject.AlumnoId,
				ArchivoId = DataContextObject.ArchivoId,
				FechaSubida = DataContextObject.FechaSubida,
				Nombre = DataContextObject.Nombre,
				Ruta = DataContextObject.Ruta,
                };
        }

        public List<ArchivosBE> GetAll()
        {
            return GetQueryable().ToList();
        }

        public List<ArchivosBE> GetWhere(System.Linq.Expressions.Expression<Func<ArchivosBE,bool>> Where)
	        {
            return GetQueryable().Where(Where).ToList();
        }

        public List<ArchivosBE> GetWhere<T>(System.Linq.Expressions.Expression<Func<ArchivosBE,bool>> Where,System.Linq.Expressions.Expression<Func<ArchivosBE,T>> OrderBy)
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

        public ArchivosBE GetOne(Int32 ArchivoId)
        {
            	return GetQueryable().SingleOrDefault(x =>  x.ArchivoId == ArchivoId);
        }

        public Int32 GetLastId()
        {
            		return GetQueryable().Max(x => x.ArchivoId);
        }

        public bool InsertIdentity(ArchivosBE objInsert, bool ThrowException)
        {
		var DataContextObject = GetDataContextObject();
		Archivos objInsertLinq = new Archivos();
			objInsertLinq.AlumnoId = objInsert.AlumnoId;
			objInsertLinq.ArchivoId = objInsert.ArchivoId;
			objInsertLinq.FechaSubida = objInsert.FechaSubida;
			objInsertLinq.Nombre = objInsert.Nombre;
			objInsertLinq.Ruta = objInsert.Ruta;
		DataContextObject.Archivos.InsertOnSubmit(objInsertLinq);
		try
            	{	
		  DataContextObject.SubmitChanges();
		  objInsert.ArchivoId = objInsertLinq.ArchivoId;
                return true;
            	}
            	catch (Exception Ex)
            	{
                if (ThrowException)
                    throw Ex;
                return false;
            	}
        }

        public void Insert(ArchivosBE objInsert)
        {
		var DataContextObject = GetDataContextObject();
		Archivos objInsertLinq = new Archivos();
			objInsertLinq.AlumnoId = objInsert.AlumnoId;
			objInsertLinq.ArchivoId = objInsert.ArchivoId;
			objInsertLinq.FechaSubida = objInsert.FechaSubida;
			objInsertLinq.Nombre = objInsert.Nombre;
			objInsertLinq.Ruta = objInsert.Ruta;
		DataContextObject.Archivos.InsertOnSubmit(objInsertLinq);
        }

        public void Insert(List<ArchivosBE> listObjInsert)
        {
		var DataContextObject = GetDataContextObject();
		foreach(var objInsert in listObjInsert)
		{
		Archivos objInsertLinq = new Archivos();
			objInsertLinq.AlumnoId = objInsert.AlumnoId;
			objInsertLinq.ArchivoId = objInsert.ArchivoId;
			objInsertLinq.FechaSubida = objInsert.FechaSubida;
			objInsertLinq.Nombre = objInsert.Nombre;
			objInsertLinq.Ruta = objInsert.Ruta;
			DataContextObject.Archivos.InsertOnSubmit(objInsertLinq);
		}		
        }

        public void InsertOrUpdate(ArchivosBE objInsertOrUpdate)
        {
			var DataContextObject = GetDataContextObject();
			var existentObj = DataContextObject.Archivos.SingleOrDefault(x =>  x.ArchivoId == objInsertOrUpdate.ArchivoId);
            	if (existentObj == null)
              	Insert(objInsertOrUpdate);
            	else
                	Update(objInsertOrUpdate);	
        } 

        public void InsertOrUpdate(List<ArchivosBE> listObjInsertOrUpdate)
        {
			foreach(var objInsertOrUpdate in listObjInsertOrUpdate)
			{
				InsertOrUpdate(objInsertOrUpdate);
			}	
        } 

        public void DeleteWhere(System.Linq.Expressions.Expression<Func<ArchivosBE,bool>> Filtro)
        {
            var DataContextObject = GetDataContextObject();
		Delete(GetWhere(Filtro));
        }

        public void Delete(ArchivosBE objDelete)
        {
		var DataContextObject = GetDataContextObject();
            var objDeleteLinq = DataContextObject.Archivos.Single(x =>  x.ArchivoId == objDelete.ArchivoId);
		DataContextObject.Archivos.DeleteOnSubmit(objDeleteLinq);
        }

        public void Delete(List<ArchivosBE> listObjDelete)
        {
		var DataContextObject = GetDataContextObject();
		foreach(var objDelete in listObjDelete)
		{
            	var objDeleteLinq = DataContextObject.Archivos.Single(x =>  x.ArchivoId == objDelete.ArchivoId);
			DataContextObject.Archivos.DeleteOnSubmit(objDeleteLinq);
		}
        }

        public void TryDeleteWhere(System.Linq.Expressions.Expression<Func<ArchivosBE,bool>> Filtro)
        {
            var DataContextObject = GetDataContextObject();
		TryDelete(GetWhere(Filtro));
        }

        public void TryDelete(ArchivosBE objDelete)
        {
		var DataContextObject = GetDataContextObject();
            var objDeleteLinq = DataContextObject.Archivos.SingleOrDefault(x =>  x.ArchivoId == objDelete.ArchivoId);
		if(objDeleteLinq !=null)
			DataContextObject.Archivos.DeleteOnSubmit(objDeleteLinq);
        }

        public void TryDelete(List<ArchivosBE> listObjDelete)
        {
		var DataContextObject = GetDataContextObject();
		foreach(var objDelete in listObjDelete)
		{
            	var objDeleteLinq = DataContextObject.Archivos.SingleOrDefault(x =>  x.ArchivoId == objDelete.ArchivoId);
			if(objDeleteLinq !=null)			
				DataContextObject.Archivos.DeleteOnSubmit(objDeleteLinq);
		}
        }

        public bool Exists(ArchivosBE objExists)
        {
		var DataContextObject = GetDataContextObject();
            return DataContextObject.Archivos.Any(x =>  x.ArchivoId == objExists.ArchivoId);
        }

        public void Update(ArchivosBE objUpdate)
        {
		var DataContextObject = GetDataContextObject();
            var objUpdateLinq = DataContextObject.Archivos.Single(x =>  x.ArchivoId == objUpdate.ArchivoId);
			objUpdateLinq.AlumnoId = objUpdate.AlumnoId;
			objUpdateLinq.ArchivoId = objUpdate.ArchivoId;
			objUpdateLinq.FechaSubida = objUpdate.FechaSubida;
			objUpdateLinq.Nombre = objUpdate.Nombre;
			objUpdateLinq.Ruta = objUpdate.Ruta;
        }

        public void Update(List<ArchivosBE> listObjUpdate)
        {
		var DataContextObject = GetDataContextObject();
		foreach(var objUpdate in listObjUpdate)
		{
            	var objUpdateLinq = DataContextObject.Archivos.Single(x =>  x.ArchivoId == objUpdate.ArchivoId);
			objUpdateLinq.AlumnoId = objUpdate.AlumnoId;
			objUpdateLinq.ArchivoId = objUpdate.ArchivoId;
			objUpdateLinq.FechaSubida = objUpdate.FechaSubida;
			objUpdateLinq.Nombre = objUpdate.Nombre;
			objUpdateLinq.Ruta = objUpdate.Ruta;
		}
        }
    }
}
