using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ePortafolio.Models.SSIA.Entities; 
using  ePortafolio.Models.SSIA;

namespace ePortafolio.Models.SSIA.Repository
{
    public partial class OutcomesAlumnoRepository 
    {
        String connectionString = "";
	  SSIADataContext DataContextObjectType;

        private SSIADataContext GetDataContextObject()
        {
            return DataContextFactory.GetWebRequestScopedDataContext<SSIADataContext>(null, connectionString);
        }

        public OutcomesAlumnoRepository(String connectionString)
        {
            this.connectionString = connectionString;
        }

        private IQueryable<OutcomesAlumnoBE> GetQueryable()
        {
            var DataContextObject = GetDataContextObject();
            var OutcomesAlumno= from x in DataContextObject.OutcomesAlumno
                             select new OutcomesAlumnoBE
                             {
						AlumnoId = x.AlumnoId,
						CorreoElectronico = x.CorreoElectronico,
						DescripcionOutcome = x.DescripcionOutcome,
						Nombre = x.Nombre,
						NombreOutcome = x.NombreOutcome,
						OutcomeId = x.OutcomeId,
                             };
            return OutcomesAlumno;
        }

        private OutcomesAlumnoBE GetLinqFK(OutcomesAlumno DataContextObject)
        {
		if(DataContextObject==null)
			return null;
            return new OutcomesAlumnoBE()
                {
						AlumnoId = DataContextObject.AlumnoId,
						CorreoElectronico = DataContextObject.CorreoElectronico,
						DescripcionOutcome = DataContextObject.DescripcionOutcome,
						Nombre = DataContextObject.Nombre,
						NombreOutcome = DataContextObject.NombreOutcome,
						OutcomeId = DataContextObject.OutcomeId,
                };
        }

        public OutcomesAlumnoBE GetLinq(OutcomesAlumno DataContextObject)
        {
		if(DataContextObject==null)
			return null;
            return new OutcomesAlumnoBE()
                {
				AlumnoId = DataContextObject.AlumnoId,
				CorreoElectronico = DataContextObject.CorreoElectronico,
				DescripcionOutcome = DataContextObject.DescripcionOutcome,
				Nombre = DataContextObject.Nombre,
				NombreOutcome = DataContextObject.NombreOutcome,
				OutcomeId = DataContextObject.OutcomeId,
                };
        }

        public List<OutcomesAlumnoBE> GetAll()
        {
            return GetQueryable().ToList();
        }

        public List<OutcomesAlumnoBE> GetWhere(System.Linq.Expressions.Expression<Func<OutcomesAlumnoBE,bool>> Where)
	        {
            return GetQueryable().Where(Where).ToList();
        }

        public List<OutcomesAlumnoBE> GetWhere<T>(System.Linq.Expressions.Expression<Func<OutcomesAlumnoBE,bool>> Where,System.Linq.Expressions.Expression<Func<OutcomesAlumnoBE,T>> OrderBy)
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

        public OutcomesAlumnoBE GetOne(int OutcomeId, String AlumnoId)
        {
            return GetQueryable().SingleOrDefault(x => x.OutcomeId == OutcomeId && x.AlumnoId == AlumnoId);
        }

        public Int32 GetLastId()
        {
			return 0;
        }

        public bool InsertIdentity(OutcomesAlumnoBE objInsert, bool ThrowException)
        {
		var DataContextObject = GetDataContextObject();
		OutcomesAlumno objInsertLinq = new OutcomesAlumno();
			objInsertLinq.AlumnoId = objInsert.AlumnoId;
			objInsertLinq.CorreoElectronico = objInsert.CorreoElectronico;
			objInsertLinq.DescripcionOutcome = objInsert.DescripcionOutcome;
			objInsertLinq.Nombre = objInsert.Nombre;
			objInsertLinq.NombreOutcome = objInsert.NombreOutcome;
			objInsertLinq.OutcomeId = objInsert.OutcomeId;
		DataContextObject.OutcomesAlumno.InsertOnSubmit(objInsertLinq);
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

        public void Insert(OutcomesAlumnoBE objInsert)
        {
		var DataContextObject = GetDataContextObject();
		OutcomesAlumno objInsertLinq = new OutcomesAlumno();
			objInsertLinq.AlumnoId = objInsert.AlumnoId;
			objInsertLinq.CorreoElectronico = objInsert.CorreoElectronico;
			objInsertLinq.DescripcionOutcome = objInsert.DescripcionOutcome;
			objInsertLinq.Nombre = objInsert.Nombre;
			objInsertLinq.NombreOutcome = objInsert.NombreOutcome;
			objInsertLinq.OutcomeId = objInsert.OutcomeId;
		DataContextObject.OutcomesAlumno.InsertOnSubmit(objInsertLinq);
        }

        public void Insert(List<OutcomesAlumnoBE> listObjInsert)
        {
		var DataContextObject = GetDataContextObject();
		foreach(var objInsert in listObjInsert)
		{
		OutcomesAlumno objInsertLinq = new OutcomesAlumno();
			objInsertLinq.AlumnoId = objInsert.AlumnoId;
			objInsertLinq.CorreoElectronico = objInsert.CorreoElectronico;
			objInsertLinq.DescripcionOutcome = objInsert.DescripcionOutcome;
			objInsertLinq.Nombre = objInsert.Nombre;
			objInsertLinq.NombreOutcome = objInsert.NombreOutcome;
			objInsertLinq.OutcomeId = objInsert.OutcomeId;
			DataContextObject.OutcomesAlumno.InsertOnSubmit(objInsertLinq);
		}		
        }

        public void InsertOrUpdate(OutcomesAlumnoBE objInsertOrUpdate)
        {
			return;
        } 

        public void InsertOrUpdate(List<OutcomesAlumnoBE> listObjInsertOrUpdate)
        {
			return;
        } 

        public void DeleteWhere(System.Linq.Expressions.Expression<Func<OutcomesAlumnoBE,bool>> Filtro)
        {
            var DataContextObject = GetDataContextObject();
		Delete(GetWhere(Filtro));
        }

        public void Delete(OutcomesAlumnoBE objDelete)
        {
			return;
        }

        public void Delete(List<OutcomesAlumnoBE> listObjDelete)
        {
			return;
        }

        public void TryDeleteWhere(System.Linq.Expressions.Expression<Func<OutcomesAlumnoBE,bool>> Filtro)
        {
            var DataContextObject = GetDataContextObject();
		TryDelete(GetWhere(Filtro));
        }

        public void TryDelete(OutcomesAlumnoBE objDelete)
        {
			return;
        }

        public void TryDelete(List<OutcomesAlumnoBE> listObjDelete)
        {
			return;
        }

        public bool Exists(OutcomesAlumnoBE objExists)
        {
			return false;
        }

        public void Update(OutcomesAlumnoBE objUpdate)
        {
			return;
        }

        public void Update(List<OutcomesAlumnoBE> listObjUpdate)
        {
			return;
        }
    }
}
