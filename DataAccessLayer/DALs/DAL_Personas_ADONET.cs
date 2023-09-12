using DataAccessLayer.IDALs;
using Microsoft.Data.SqlClient;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DALs
{
    public class DAL_Personas_ADONET : IDAL_Personas
    {
        private string connectionString = "Server=localhost,1433;Database=practico;User Id=sa;Password=Abc*123!;Encrypt=False;";
        private Dictionary<string, Persona> personas = new Dictionary<string, Persona>();

        public void Delete(string documento)
        {
            string query = "DELETE FROM personas WHERE documento = @Value";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    using(SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@Value", documento);
                        cmd.ExecuteNonQuery();
                    }

                    connection.Close();
                }
                catch (Exception e)
                {

                }
            }
            
        }

        public List<Persona> Get()
        {
            List<Persona> personas = new List<Persona>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT nombre, documento FROM Personas";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // Leer los datos de la fila y crear objetos Persona
                            string nombre = reader["nombre"].ToString();
                            string documento = reader["documento"].ToString();

                            Persona persona = new Persona();
                            persona.Nombre = nombre;
                            persona.Documento = documento;

                            personas.Add(persona);
                        }
                    }
                }
            }

            return personas;
        }

        public Persona Get(string documento)
        {
            Persona p = new Persona();
            string query = "SELECT * FROM persona WHERE documento = @Value";
            using(SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using(SqlCommand cmd  = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@Value", documento);
                    using(SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while(reader.Read())
                        {
                            string nombre = reader["nombre"].ToString();
                            p.Nombre = nombre;
                            p.Documento = documento;
                        }
                    }
                }
                connection.Close();
            }
            return p;
        }

        public void Insert(Persona persona)
        {
            string query = "INSERT INTO personas(nombre, documento) VALUES(@Nombre, @Documento)";
            using(SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using(SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@Nombre", persona.Nombre);
                    cmd.Parameters.AddWithValue("@Documento", persona.Documento);
                    cmd.ExecuteNonQuery();
                }
                connection.Close();
            }
        }

        public void Update(Persona persona)
        {
            string query = "UPDATE personas SET nombre = @Nombre WHERE documento = @Documento";
            using(SqlConnection connection = new SqlConnection(connectionString)) 
            {
                connection.Open();
                using(SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Nombre", persona.Nombre);
                    command.Parameters.AddWithValue("@Documento", persona.Documento);
                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
        }
    }
}
