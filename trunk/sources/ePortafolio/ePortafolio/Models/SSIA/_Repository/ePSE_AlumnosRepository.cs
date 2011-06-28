using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ePortafolio.Models.SSIA.Entities; 
using  ePortafolio.Models.SSIA;

namespace ePortafolio.Models.SSIA.Repository
{
    public partial class AlumnosRepository 
    {
        String connectionString = "";
	  SSIADataContext DataContextObjectType;

        private SSIADataContext GetDataContextObject()
        {
            return DataContextFactory.GetWebRequestScopedDataContext<SSIADataContext>(null, connectionString);
        }

        public AlumnosRepository(String connectionString)
        {
            this.connectionString = connectionString;
        }

        private IQueryable<AlumnosBE> GetQueryable()
        {
            var DataContextObject = GetDataContextObject();
            var Alumnos= from x in DataContextObject.Alumnos
                             select new AlumnosBE
                             {
						AlumnoId = x.AlumnoId,
						CorreoElectronico = x.CorreoElectronico,
						Nombre = x.Nombre,
						NombreCarrera = x.NombreCarrera,
                             };
            return Alumnos;
        }

        private AlumnosBE GetLinqFK(Alumnos DataContextObject)
        {
		if(DataContextObject==null)
			return null;
            return new AlumnosBE()
                {
						AlumnoId = DataContextObject.AlumnoId,
						CorreoElectronico = DataContextObject.CorreoElectronico,
						Nombre = DataContextObject.Nombre,
						NombreCarrera = DataContextObject.NombreCarrera,
                };
        }

        public AlumnosBE GetLinq(Alumnos DataContextObject)
        {
		if(DataContextObject==null)
			return null;
            return new AlumnosBE()
                {
				AlumnoId = DataContextObject.AlumnoId,
				CorreoElectronico = DataContextObject.CorreoElectronico,
				Nombre = DataContextObject.Nombre,
				NombreCarrera = DataContextObject.NombreCarrera,
                };
        }

        public List<AlumnosBE> GetAll()
        {
            return GetQueryable().ToList();
        }

        public List<AlumnosBE> GetWhere(System.Linq.Expressions.Expression<Func<AlumnosBE,bool>> Where)
	        {
            return GetQueryable().Where(Where).ToList();
        }

        public List<AlumnosBE> GetWhere<T>(System.Linq.Expressions.Expression<Func<AlumnosBE,bool>> Where,System.Linq.Expressions.Expression<Func<AlumnosBE,T>> OrderBy)
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

        public AlumnosBE GetOne(String AlumnoId)
        {
            return GetQueryable().SingleOrDefault(x => x.AlumnoId == AlumnoId );
        }
        public Int32 GetLastId()
        {
			return 0;
        }

        public bool InsertIdentity(AlumnosBE objInsert, bool ThrowException)
        {
		var DataContextObject = GetDataContextObject();
		Alumnos objInsertLinq = new Alumnos();
			objInsertLinq.AlumnoId = objInsert.AlumnoId;
			objInsertLinq.CorreoElectronico = objInsert.CorreoElectronico;
			objInsertLinq.Nombre = objInsert.Nombre;
			objInsertLinq.NombreCarrera = objInsert.NombreCarrera;
		DataContextObject.Alumnos.InsertOnSubmit(objInsertLinq);
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

        public void Insert(AlumnosBE objInsert)
        {
		var DataContextObject = GetDataContextObject();
		Alumnos objInsertLinq = new Alumnos();
			objInsertLinq.AlumnoId = objInsert.AlumnoId;
			objInsertLinq.CorreoElectronico = objInsert.CorreoElectronico;
			objInsertLinq.Nombre = objInsert.Nombre;
			objInsertLinq.NombreCarrera = objInsert.NombreCarrera;
		DataContextObject.Alumnos.InsertOnSubmit(objInsertLinq);
        }

        public void Insert(List<AlumnosBE> listObjInsert)
        {
		var DataContextObject = GetDataContextObject();
		foreach(var objInsert in listObjInsert)
		{
		Alumnos objInsertLinq = new Alumnos();
			objInsertLinq.AlumnoId = objInsert.AlumnoId;
			objInsertLinq.CorreoElectronico = objInsert.CorreoElectronico;
			objInsertLinq.Nombre = objInsert.Nombre;
			objInsertLinq.NombreCarrera = objInsert.NombreCarrera;
			DataContextObject.Alumnos.InsertOnSubmit(objInsertLinq);
		}		
        }

        public void InsertOrUpdate(AlumnosBE objInsertOrUpdate)
        {
			return;
        } 

        public void InsertOrUpdate(List<AlumnosBE> listObjInsertOrUpdate)
        {
			return;
        } 

        public void DeleteWhere(System.Linq.Expressions.Expression<Func<AlumnosBE,bool>> Filtro)
        {
            var DataContextObject = GetDataContextObject();
		Delete(GetWhere(Filtro));
        }

        public void Delete(AlumnosBE objDelete)
        {
			return;
        }

        public void Delete(List<AlumnosBE> listObjDelete)
        {
			return;
        }

        public void TryDeleteWhere(System.Linq.Expressions.Expression<Func<AlumnosBE,bool>> Filtro)
        {
            var DataContextObject = GetDataContextObject();
		TryDelete(GetWhere(Filtro));
        }

        public void TryDelete(AlumnosBE objDelete)
        {
			return;
        }

        public void TryDelete(List<AlumnosBE> listObjDelete)
        {
			return;
        }

        public bool Exists(AlumnosBE objExists)
        {
			return false;
        }

        public void Update(AlumnosBE objUpdate)
        {
			return;
        }

        public void Update(List<AlumnosBE> listObjUpdate)
        {
			return;
        }
    }
}
