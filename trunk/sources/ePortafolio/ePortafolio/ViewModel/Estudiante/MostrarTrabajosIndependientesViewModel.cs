using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ePortafolio.Models.ePortafolio;
using ePortafolio.Models.ePortafolio.Entities;
using ePortafolio.Models.SSIA.Entities;
using ePortafolio.Models.SSIA;

namespace ePortafolio.ViewModel
{
    public class MostrarTrabajosIndependientesViewModel
    {

        public AlumnosBE Alumno { get; set; }

        public List<TrabajosBE> TrabajosIndependientes { get; set; }
        public List<GruposBE> Grupos { get; set; }

        public MostrarTrabajosIndependientesViewModel(String AlumnoId)
        {
            Alumno = SSIARepositoryFactory.GetAlumnosRepository().GetOne(AlumnoId);
            TrabajosIndependientes = ePortafolioRepositoryFactory.GetTrabajosRepository().GetTrabajosIndependientes(AlumnoId);
            Grupos = ePortafolioRepositoryFactory.GetGruposRepository().GetGruposIndependientes(AlumnoId);

            TrabajosIndependientes = TrabajosIndependientes.OrderBy(x => x.Nombre).ToList();
        }

                            

    }
}
