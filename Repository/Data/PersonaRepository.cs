using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace Repository.Data
{
    internal class PersonaRepository : IPersona
    {
        private IDbConnection conexionDB;
        public PersonaRepository(string _connectionString)
        {
            conexionDB = new IDbConection(_connectionString).dbConnection();
        }
        public bool add(PersonaModel persona)
        {
            try
            {
                if (conexionDB.Execute("insert into Persona(nombre, apellido, cedula) values(@nombre, @apellido, @cedula)", persona) > 0)
                return true;
                else
                    return false;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public  PersonaModel get(int id)
        {
            try
            {
                return conexionDB.QueryFirst<PersonaModel>($"SELECT * FROM Persona WHERE id = {id}");

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<PersonaModel> list()
        {
            try
            {
                return conexionDB.Query<PersonaModel>($"SELECT * FROM Persona order by id asc ");
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public string remove(int id)
        {
            try
            {
                conexionDB.Execute($"DELETE FROM Persona WHERE id = {id} ");
                return " Los datos fueron eliminados exitosamente...";
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public string update(PersonaModel persona, int id)
        {
            try
            {
                conexionDB.Execute($"UPDATE Persona SET " +
                    "nombre = @nombre, " +
                    "apellido = @apellido, " +
                    "cedula = @cedula, " +
                    $"WHERE id = {id}", persona);
                return " Los datos fueron modificados exitosamente...";

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
    }
}
