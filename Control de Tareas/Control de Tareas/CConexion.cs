using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;

namespace Control_de_Tareas
{
    internal class CConexion
    {
        MySqlConnection conex = new MySqlConnection();

        static string servidor = "dbcontroltareas.ct2rrxcaxo9w.us-east-1.rds.amazonaws.com";
        static string bd = "ProcessSa";
        static string usuario = "admin";
        static string password = "duoc1234";
        static string puerto = "3306";

        string query = "SELECT * FROM usuario where id = 1;";

        public string cadenaConexion = "server=" + servidor + ";" + "port=" + puerto + ";" + "uid=" + usuario + ";" + "pwd=" + password + ";" + "database=" + bd + ";";

        string testString;
        public bool EstablecerConn()
        {
            try
            {
                conex.ConnectionString = cadenaConexion;
                conex.Open();
                return true;

            }
            catch (MySqlException e)
            {
                System.Windows.MessageBox.Show("No se pudo establecer conexion, Error: " + e);
                return false;
            }
        }


        public string NombreUsuarioLogeado(int idUsuario)
        {
            return null;
        }

        public MySqlCommand ReadSingleDB(int id, string tabla)
        {
            try
            {
                conex.ConnectionString = cadenaConexion;
                conex.Open();

                MySqlCommand dataREAD = new MySqlCommand("SELECT * FROM " + tabla + " WHERE ID= " + id + ";", conex);
                return dataREAD;
            }
            catch (MySqlException e)
            {
                System.Windows.MessageBox.Show("test:" + e);
                return null;
            }
        }

        public void CerrarConn()
        {
            this.conex.Close();
        }

        public MySqlConnection Get_connection()
        {
            return this.conex;
        }

        public bool EjecutarMetodoBaseDatos()
        {
            try
            {
                conex.ConnectionString = cadenaConexion;
                conex.Open();
                return true;
            }
            catch (MySqlException e)
            {
                System.Windows.MessageBox.Show("No se pudo establecer conexion, Error: " + e);
                return false;
            }
        }

        public string getConnString()
        {            
            return cadenaConexion.ToString();
        }
        
        
        public bool CheckCredentials(string email, string pass)
        {

            string query = "SELECT * FROM usuario WHERE correo = '" + email + "' AND password = '"+pass+"';";
            var cmd = new MySql.Data.MySqlClient.MySqlCommand(query, conex);
            var reader = cmd.ExecuteReader();
            string result = "";

            while (reader.Read())
            {
                result = reader.GetString("id");
            }


            if (result == "")
            {
                //Pass o mail incorrecto
                Console.WriteLine("user not found");
                return false;   
            }
            else
            {
                //User found
                reader.Close();
                Console.WriteLine(result);
                query = "SELECT CONCAT(nombre, ' ', apellidop, ' ', apellidom) AS nombrecompleto FROM usuario WHERE id = " + result + ";";
                cmd = new MySql.Data.MySqlClient.MySqlCommand(query, conex);
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    result = reader.GetString("nombrecompleto");
                }

                Dashboard dashboard = new Dashboard();
                dashboard.label_logedUser.Content = result;
                Console.WriteLine(result);
                dashboard.Show();

                return true;
            }
        }
        
    }
}
