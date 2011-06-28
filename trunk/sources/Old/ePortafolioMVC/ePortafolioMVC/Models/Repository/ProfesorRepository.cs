using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ePortafolioMVC.Models.Entities;

namespace ePortafolioMVC.Models.Repository
{
    public class ProfesorRepository : ePortafolioMVC.Models.Repository.IProfesorRepository 
    {
        public BEProfesor GetProfesor(String ProfesorId)
        {
            pyrIntegradoDBDataContext pyrIntegradoDBDataContext = new pyrIntegradoDBDataContext();

            var Profesor = pyrIntegradoDBDataContext.ePSE_Profesores.SingleOrDefault(p => p.ProfesorId == ProfesorId);

            if (Profesor != null)
            {
                return new BEProfesor
                {
                    ProfesorId = Profesor.ProfesorId,
                    Nombre = Profesor.Nombre
                };
            }

            return null;
        }
        
        public BEProfesor GetProfesorNoFK(String ProfesorId)
        {
            pyrIntegradoDBDataContext pyrIntegradoDBDataContext = new pyrIntegradoDBDataContext();

            var Profesor = pyrIntegradoDBDataContext.ePSE_Profesores.SingleOrDefault(p => p.ProfesorId == ProfesorId);

            if (Profesor != null)
            {
                return new BEProfesor
                {
                    ProfesorId = Profesor.ProfesorId,
                    Nombre = Profesor.Nombre
                };
            }

            return null;
        }
    }
}
