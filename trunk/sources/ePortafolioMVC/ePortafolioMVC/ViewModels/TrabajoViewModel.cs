using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ePortafolioMVC.Models;
using System.Web.Mvc;
using ePortafolioMVC.ViewModels.Classes;

namespace ePortafolioMVC.ViewModels
{
    public class TrabajoViewModel
    {
        public Trabajo trabajo {get;set;}
        public SelectList EsGrupal { get; set; }

        public TrabajoViewModel(Trabajo trabajo)
        {
            this.trabajo = trabajo;
            var Valores = new List<Valor>();
            Valores.Add(new Valor(true, "imgGrupal.png"));
            Valores.Add(new Valor(false,"imgIndividual.png"));

            EsGrupal = new SelectList(Valores, "ID", "Descripcion", trabajo.EsGrupal);
        }
    }
}
