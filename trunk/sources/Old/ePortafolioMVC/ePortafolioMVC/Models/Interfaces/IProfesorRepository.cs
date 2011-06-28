using System;
namespace ePortafolioMVC.Models.Repository
{
    interface IProfesorRepository
    {
        ePortafolioMVC.Models.Entities.BEProfesor GetProfesor(string ProfesorId);
        ePortafolioMVC.Models.Entities.BEProfesor GetProfesorNoFK(string ProfesorId);
    }
}
