using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RubricOn.Models.RubricOn.Entities;

namespace RubricOn.Models.RubricOn.Repository
{
    public class OutcomesRepository
    {
        public OutcomesRepository(String cs)
        { 
        
        }

        public List<OutcomesBE> GetAll()
        {
            var Lista = new List<OutcomesBE>();
            Lista.Add(new OutcomesBE() { OutcomeId = "A" });
            Lista.Add(new OutcomesBE() { OutcomeId = "B" });
            Lista.Add(new OutcomesBE() { OutcomeId = "C" });
            Lista.Add(new OutcomesBE() { OutcomeId = "D" });
            Lista.Add(new OutcomesBE() { OutcomeId = "E" });
            Lista.Add(new OutcomesBE() { OutcomeId = "F" });
            Lista.Add(new OutcomesBE() { OutcomeId = "G" });
            Lista.Add(new OutcomesBE() { OutcomeId = "H" });
            Lista.Add(new OutcomesBE() { OutcomeId = "I" });
            Lista.Add(new OutcomesBE() { OutcomeId = "J" });
            Lista.Add(new OutcomesBE() { OutcomeId = "K" });
            return Lista;
        }
    }
}
