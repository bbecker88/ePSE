using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ePortafolio.Models.SSIA.Entities;
using ePortafolio.Models.ePortafolio.Entities;
using ePortafolio.Models.ePortafolio;
using ePortafolio.Models.SSIA;

namespace ePortafolio.ViewModel
{
    public class MostrarDetalleArchivosViewModel
    {
        public Boolean Habilitado { get; set; }

        public AlumnosBE Alumno { get; set; }
        public GruposBE Grupo { get; set; }

        public List<ArchivosBE> Archivos  { get; set; }

        public MostrarDetalleArchivosViewModel(int TrabajoId, String AlumnoId)
        {
            Grupo = ePortafolioRepositoryFactory.GetGruposRepository().GetGrupoTrabajo(TrabajoId, AlumnoId);
            Alumno = SSIARepositoryFactory.GetAlumnosRepository().GetOne(AlumnoId);
            Archivos = ePortafolioRepositoryFactory.GetArchivosRepository().GetArchivosGrupo(Grupo.GrupoId);

            Habilitado = Grupo.EvaluacionId == null;
        }
    }
}
