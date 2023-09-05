using DataAccessLayer.IDALs;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DALs
{
    public class DAL_Personas_ADONET : IDAL_Personas
    {
        private Dictionary<string, Persona> personas = new Dictionary<string, Persona>();

        public void Delete(string documento)
        {
            personas.Remove(documento);
        }

        public List<Persona> Get()
        {
            return personas.Values.ToList();
        }

        public Persona Get(string documento)
        {
            return personas[documento];
        }

        public void Insert(Persona persona)
        {
            personas.Add(persona.Documento, persona);
        }

        public void Update(Persona persona)
        {
            personas[persona.Documento] = persona;
        }
    }
}
