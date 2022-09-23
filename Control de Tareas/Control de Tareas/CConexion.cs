using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

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

        string cadenaConexion = "server=" + servidor + ";" + "port=" + puerto + ";" + "uid=" + usuario + ";" + "pwd=" + password + ";" + "database=" + bd + ";";

        public MySqlConnection EstablecerConn()
        {
            try
            {
                conex.ConnectionString = cadenaConexion;
                conex.Open();
                System.Windows.MessageBox.Show("Conexión Exitosa");
            }
            catch (MySqlException e)
            {
                System.Windows.MessageBox.Show("No se pudo establecer conexion, Error: " + e);
            }
            return conex;
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
            conex.Close();
        }
    }
}
