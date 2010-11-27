﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ePortafolioMVC.Models.Entities;

namespace ePortafolioMVC.Models.Repository
{
    public class ResultadoProgramaRepository : ePortafolioMVC.Models.Repository.IResultadoProgramaRepository
    {
        public BEResultadoPrograma GetResultadoPrograma(int CursoId, String PeriodoId)
        {
            SSIADBDataContext SSIADBDataContext = new SSIADBDataContext();

            var ResultadoPrograma = SSIADBDataContext.LogrosCurso(CursoId, PeriodoId).ToList();

            if (ResultadoPrograma != null && ResultadoPrograma.Count > 0)
            {
                return new BEResultadoPrograma
                        {
                            Codigo = ResultadoPrograma[0].Codigo,
                            Descripcion = ResultadoPrograma[0].Nombre,
                            ResultadoProgramaId = ResultadoPrograma[0].LogroId
                        };

            }
            return null;
        }

        public BEResultadoPrograma GetResultadoProgramaNoFK(int CursoId, String PeriodoId)
        {
            SSIADBDataContext SSIADBDataContext = new SSIADBDataContext();

            var ResultadoPrograma = SSIADBDataContext.LogrosCurso(CursoId, PeriodoId).ToList();

            if (ResultadoPrograma != null && ResultadoPrograma.Count > 0)
            {
                return new BEResultadoPrograma
                {
                    Codigo = ResultadoPrograma[0].Codigo,
                    Descripcion = ResultadoPrograma[0].Nombre,
                    ResultadoProgramaId = ResultadoPrograma[0].LogroId
                };

            }
            return null;
        }
    }
}
