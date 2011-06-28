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
        public List<TrabajosBE> GetTrabajosIndependientes(String AlumnoId)
        {
            var DataContextObject = GetDataContextObject();
		    var TrabajosIndependientes = from x in DataContextObject.Trabajos
                                         where x.Grupos.Any(g=>g.AlumnosGrupo.Any(ag=>ag.AlumnoId == AlumnoId)) && x.Iniciativa != "UPC"
                                         select GetLinq(x);
            return TrabajosIndependientes.ToList();
        }

        public List<TrabajosBE> GetTrabajosEntregados(String AlumnoId)
        {
            var DataContextObject = GetDataContextObject();
            var TrabajosEntregados = from x in DataContextObject.Trabajos
                                     where x.Grupos.Any(g => g.AlumnosGrupo.Any(ag => ag.AlumnoId == AlumnoId) && g.ArchivosGrupo.Count > 0)
                                         select GetLinq(x);
            return TrabajosEntregados.ToList();
        }
    }
}
