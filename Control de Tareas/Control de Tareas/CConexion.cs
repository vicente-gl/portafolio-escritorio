﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;
using System.Windows.Data;
using System.Windows;

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

        public void LlamarTablaFull(string tabla, System.Windows.Controls.DataGrid datagridItem)
        {
            EstablecerConn();
            string query = "SELECT * FROM " + tabla + ";";
            MySqlCommand cmd = new MySqlCommand(query, conex);

            try
            {
                MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds, "LoadDataBinding");
                datagridItem.DataContext = ds;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public void LlamarTabla(string tabla, System.Windows.Controls.DataGrid datagridItem)
        {
            EstablecerConn();
            string query = "SELECT * FROM " + tabla + " where Deleted = 0;";
            MySqlCommand cmd = new MySqlCommand(query, conex);

            try
            {
                MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds, "LoadDataBinding");
                datagridItem.DataContext = ds;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public string GetNameByID(string id, string tabla)
        {
            //EstablecerConn();

            string query = "select nombre FROM " + tabla + " where id = " + id + ";";
            var cmd = new MySql.Data.MySqlClient.MySqlCommand(query, conex);
            var reader = cmd.ExecuteReader();
            string result = "";

            while (reader.Read())
            {
                result = reader.GetString("nombre");
            }

            return result;
        }

        public string GetIDByName(string tabla, string name)
        {
            //EstablecerConn();

            string query = "select id FROM " + tabla + " where nombre = '" + name + "';";
            var cmd = new MySql.Data.MySqlClient.MySqlCommand(query, conex);
            var reader = cmd.ExecuteReader();
            string result = "";

            while (reader.Read())
            {
                result = reader.GetString("id");
            }
            reader.Close();
            return result;
        }

        public string[] CargarCombobox(string tabla)
        {
            string query = "SELECT nombre FROM " + tabla + ";";
            MySqlCommand cmd = new MySqlCommand(query, conex);
            MySqlDataReader mydr;
            List<string> datosCombo = new List<string>();
            try
            {
                mydr = cmd.ExecuteReader();
                while (mydr.Read())
                {
                    string subj = mydr.GetString("nombre");
                    datosCombo.Add(subj);
                }
                mydr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return datosCombo.ToArray();
        }

        public string[] CargarComboboxNegocio(string negocio)
        {

            string query = "SELECT id FROM negocio WHERE nombre = '" + negocio + "';";
            var cmd = new MySql.Data.MySqlClient.MySqlCommand(query, conex);
            var reader = cmd.ExecuteReader();
            string result = "";

            while (reader.Read())
            {
                result = reader.GetString("id");
            }
            reader.Close();

            query = "select nombre FROM grupotrabajo where id_negocio = " + result + ";";
            cmd = new MySqlCommand(query, conex);
            MySqlDataReader mydr;
            List<string> datosCombo = new List<string>();
            try
            {
                mydr = cmd.ExecuteReader();
                while (mydr.Read())
                {
                    string subj = mydr.GetString("nombre");
                    datosCombo.Add(subj);
                }
                mydr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return datosCombo.ToArray();
        }

        public void UpdateUsuario (string[] datosUsuario)
        {
            string query = "UPDATE usuario SET correo = '"+datosUsuario[1]+"', password = '"+datosUsuario[2]+"', rut = '"+datosUsuario[3]+"', nombre = '"+datosUsuario[4]+"', apellidop = '"+datosUsuario[5]+"', apellidom = '"+datosUsuario[6]+"', celular = '"+datosUsuario[7]+"', rol_id = "+datosUsuario[9]+", negocio_id = "+datosUsuario[10]+", grupotrabajo_id = "+datosUsuario[11]+" WHERE id = "+datosUsuario[0]+";";
            var cmd = new MySql.Data.MySqlClient.MySqlCommand(query, conex);
            var reader = cmd.ExecuteNonQuery();
        }

        public void DeleteUsuario(string idUsuario)
        {
            string query = "UPDATE usuario SET deleted = '1' WHERE id = " + idUsuario + ";";
            var cmd = new MySql.Data.MySqlClient.MySqlCommand(query, conex);
            var reader = cmd.ExecuteNonQuery();
        }

        public int CantidadRows()
        {
            string totalID;
            string query = "select COUNT(id) FROM usuario;";
            var cmd = new MySql.Data.MySqlClient.MySqlCommand(query, conex);
            totalID = cmd.ExecuteScalar().ToString();

            return Int32.Parse(totalID);
        }

    }
}
