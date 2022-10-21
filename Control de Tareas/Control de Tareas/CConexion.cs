using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;
using System.Windows.Data;
using System.Windows;
using Oracle;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;

namespace Control_de_Tareas
{
    internal class CConexion
    {
        MySqlConnection conex = new MySqlConnection();

        OracleConnection conn = new OracleConnection();


        static string servidor = "dbcontroltareas.ct2rrxcaxo9w.us-east-1.rds.amazonaws.com";
        static string bd = "ProcessSA2";
        static string usuario = "admin";
        static string password = "duoc1234";
        static string puerto = "3306";

        public string cadenaConexion = "server=" + servidor + ";" + "port=" + puerto + ";" + "uid=" + usuario + ";" + "pwd=" + password + ";" + "database=" + bd + ";";
        //public string cadenaConexion2 = "Data Source = (DESCRIPTION = (ADDRESS_LIST = (ADDRESS = (PROTOCOL = TCP)(HOST = adb.sa-santiago-1.oraclecloud.com)(PORT = 1521)))(CONNECT_DATA = (SERVICE_NAME = g52de4c04e63870_processsa_high.adb.oraclecloud.com))); User ID = ADMIN / Schema; Password= Duoc12345678;";
        public string cadenaConexion2 = "User ID = ADMIN; Password = Duoc12345678; Data Source = processsa_high";


        public bool EstablecerConn()
        {
            try
            {
                /*
                conex.ConnectionString = cadenaConexion;
                conex.Open();
                return true;
                */
                conn.ConnectionString = cadenaConexion2;
                conn.Open();
                return true;

            }
            catch (OracleException e)
            {
                
                System.Windows.MessageBox.Show("No se pudo establecer conexion, Error: " + e);
                return false;
            }
        }

        public void CerrarConn2()
        {
            conn.Close();
        }

        public void TestStringQuery()
        {

            string query = "select nombre FROM negocio where id = 21";
            OracleCommand cmd = new OracleCommand(query, conn);
            OracleDataReader oraReader;
            oraReader = cmd.ExecuteReader();

            while (oraReader.Read())
            {
                MessageBox.Show(oraReader.GetString(0));
            }
            oraReader.Close();
            conn.Close();


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

        //Oracle OK
        //Llama todos los datos, incluyendo los datos que tengan "deleted" como True
        public void LlamarTablaFull(string tabla, System.Windows.Controls.DataGrid datagridItem)
        {
            EstablecerConn();
            string query = "SELECT * FROM " + tabla + ";";
            OracleCommand cmd = new OracleCommand(query, conn);

            try
            {
                OracleDataAdapter adp = new OracleDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds, "LoadDataBinding");
                datagridItem.DataContext = ds;
            }
            catch (OracleException ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        //Oracle OK
        public void LlamarTabla(string tabla, System.Windows.Controls.DataGrid datagridItem)
        {
            EstablecerConn();
            string query = "SELECT * FROM " + tabla + " where Deleted = 0 ORDER BY ID";
            OracleCommand cmd = new OracleCommand(query, conn);

            try
            {
                OracleDataAdapter adp = new OracleDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds, "LoadDataBinding");
                datagridItem.DataContext = ds;
            }
            catch (OracleException ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public void LlamarTablaUsuariosGP(string tabla, System.Windows.Controls.DataGrid datagridItem, string idGP)
        {
            EstablecerConn();
            string query = "SELECT * FROM " + tabla + " where grupotrabajo_id = "+idGP+"";
            OracleCommand cmd = new OracleCommand(query, conn);

            try
            {
                OracleDataAdapter adp = new OracleDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds, "LoadDataBinding");
                datagridItem.DataContext = ds;
            }
            catch (OracleException ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        //Oracle OK

        public void LlamarTablaNegocioSelected(string tabla, System.Windows.Controls.DataGrid datagridItem, string negocioID)
        {
            EstablecerConn();
            string query = "SELECT * FROM " + tabla + " where Deleted = 0 and negocio_id = "+negocioID+"";
            OracleCommand cmd = new OracleCommand(query, conn);

            try
            {
                OracleDataAdapter adp = new OracleDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds, "LoadDataBinding");
                datagridItem.DataContext = ds;
            }
            catch (OracleException ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }


        //Oracle OK
        public void LlamarTablaSeleccionarNegocio(string tabla, System.Windows.Controls.DataGrid datagridItem)
        {
            EstablecerConn();
            string query = "SELECT nombre, encargado, correo_encargado, rut FROM " + tabla + " where Deleted = 0";
            OracleCommand cmd = new OracleCommand(query, conn);

            try
            {
                OracleDataAdapter adp = new OracleDataAdapter(cmd);
                DataSet ds = new DataSet();
                adp.Fill(ds, "LoadDataBinding");
                datagridItem.DataContext = ds;
            }
            catch (OracleException ex)
            {
                MessageBox.Show(ex.ToString());
            }
            CerrarConn2();
        }

        //Oracle OK
        public string GetNameByID(string id, string tabla)
        {
            string query = "select nombre FROM " + tabla + " where id = " + id + ";";
            OracleCommand cmd = new OracleCommand(query, conn);
            var reader = cmd.ExecuteReader();
            string result = "";

            while (reader.Read())
            {
                result = reader.GetString(1);
            }

            return result;
        }

        //Oracle OK
        public string GetIDByName(string tabla, string name)
        {
            //EstablecerConn(); // para evitar error al seleccionar negocio
            string query = "select id FROM " + tabla + " where nombre = '" + name + "'";
            OracleCommand cmd = new OracleCommand(query, conn);
            var reader = cmd.ExecuteReader();
            string result = "";

            while (reader.Read())
            {
                result = reader.GetString(0);
            }
            reader.Close();
            return result;
        }


        //ORACLE OK????
        public string[] GetUsuariosFromNegocio(string id_negocio )
        {
            string query = "SELECT nombre || ' ' || apellidop || ' ' || apellidom AS nombrecompleto FROM usuario WHERE negocio_id = " + id_negocio + " AND grupotrabajo_id = 1";
            OracleCommand cmd = new OracleCommand(query, conn);
            OracleDataReader mydr;
            List<string> usuariosNegocio = new List<string>();
            try
            {
                mydr = cmd.ExecuteReader();
                while (mydr.Read())
                {
                    string subj = mydr.GetString(0);
                    usuariosNegocio.Add(subj);
                }
                mydr.Close();
            }
            catch (OracleException ex)
            {
                MessageBox.Show(ex.Message);
            }
            return usuariosNegocio.ToArray();
        }
        public string[] GetUserIDFromNegocio(string id_negocio)
        {
            string query = "SELECT id FROM usuario WHERE negocio_id = " + id_negocio + " AND grupotrabajo_id = 1";
            OracleCommand cmd = new OracleCommand(query, conn);
            OracleDataReader mydr;
            List<string> usuariosNegocio = new List<string>();
            try
            {
                mydr = cmd.ExecuteReader();
                while (mydr.Read())
                {
                    string subj = mydr.GetString(0);
                    usuariosNegocio.Add(subj);
                }
                mydr.Close();
            }
            catch (OracleException ex)
            {
                MessageBox.Show(ex.Message);
            }
            return usuariosNegocio.ToArray();
            }

        public string[] GetRolFromUsuarios(string id_negocio)
        {
            string query = "SELECT * FROM USUARIO INNER JOIN ROL ON USUARIO.ROL_ID = ROL.ID WHERE NEGOCIO_ID = " + id_negocio + "";
            OracleCommand cmd = new OracleCommand(query, conn);
            OracleDataReader mydr;
            List<string> usuariosNegocio = new List<string>();
            try
            {
                mydr = cmd.ExecuteReader();
                while (mydr.Read())
                {
                    string subj = mydr.GetString(13);
                    usuariosNegocio.Add(subj);
                }
                mydr.Close();
            }
            catch (OracleException ex)
            {
                MessageBox.Show(ex.Message);
            }
            return usuariosNegocio.ToArray();
        }

        //ORACLE
        public string[] CargarCombobox(string tabla)
        {
            string query = "SELECT nombre FROM " + tabla + " where deleted = 0";
            OracleCommand cmd = new OracleCommand(query, conn);
            OracleDataReader mydr;
            List<string> datosCombo = new List<string>();
            try
            {
                mydr = cmd.ExecuteReader();
                while (mydr.Read())
                {
                    string subj = mydr.GetString(0);
                    datosCombo.Add(subj);
                }
                mydr.Close();
            }
            catch (OracleException ex)
            {
                MessageBox.Show(ex.Message);
            }
            return datosCombo.ToArray();
        }

        //ORACLE OK
        public string[] CargarComboboxNegocio(string negocio)
        {

            string query = "SELECT id FROM negocio WHERE nombre = '" + negocio + "' and deleted = 0";
            OracleCommand cmd = new OracleCommand(query, conn);
            OracleDataReader mydr;
            mydr = cmd.ExecuteReader();
            string result = "";

            while (mydr.Read())
            {
                result = mydr.GetString(0);
            }
            mydr.Close();

            query = "select nombre FROM grupotrabajo where negocio_id = " + result + "";
            cmd = new OracleCommand(query, conn);
            List<string> datosCombo = new List<string>();
            try
            {
                mydr = cmd.ExecuteReader();
                while (mydr.Read())
                {
                    string subj = mydr.GetString(0);
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
            string query = "UPDATE usuario SET correo = '"+datosUsuario[1]+"', password = '"+datosUsuario[2]+"', rut = '"+datosUsuario[3]+"', nombre = '"+datosUsuario[4]+"', apellidop = '"+datosUsuario[5]+"', apellidom = '"+datosUsuario[6]+"', celular = "+datosUsuario[7]+", rol_id = "+datosUsuario[9]+", negocio_id = "+datosUsuario[10]+", grupotrabajo_id = "+datosUsuario[11]+" WHERE id = "+datosUsuario[0]+"";
            OracleCommand cmd = new OracleCommand(query, conn);
            var reader = cmd.ExecuteNonQuery();
        }
        //Oracle OK
        public void UpdateNegocio(string[] datosNegocio)
        {
            string query = "UPDATE negocio SET nombre = '" + datosNegocio[0] + "', encargado = '" + datosNegocio[1] + "', correo_encargado = '" + datosNegocio[3] + "', fecha_ingreso = TO_DATE('"+ datosNegocio[2] + "', 'yyyy-MM-dd'), rut = '" + datosNegocio[4] + "', direccion = '"+datosNegocio[5]+"' WHERE id = " + datosNegocio[6] + "";
            OracleCommand cmd = new OracleCommand(query, conn);
            var reader = cmd.ExecuteNonQuery();
        }
        //Oracle OK
        public void DeleteRow(string id, string tabla)
        {
            string query = "UPDATE "+tabla+" SET deleted = '1' WHERE id = " + id + "";
            OracleCommand cmd = new OracleCommand(query, conn);
            var reader = cmd.ExecuteNonQuery();
        }

        public void ResetGPUsuarios(string idgp_eliminado)
        {
            string query = "UPDATE usuario SET grupotrabajo_id = 1 WHERE grupotrabajo_id = "+idgp_eliminado+"";
            OracleCommand cmd = new OracleCommand(query, conn);
            var reader = cmd.ExecuteNonQuery();
        }

        public int CantidadRows(string tabla)
        {
            string totalID;
            string query = "select COUNT(id) FROM "+tabla+";";
            OracleCommand cmd = new OracleCommand(query, conn);
            totalID = cmd.ExecuteScalar().ToString();

            return Int32.Parse(totalID);
        }

        //ORACLE OK
        public void InsertNegocio(string[] datosNegocio)
        {
            string query = "INSERT INTO NEGOCIO (ID, NOMBRE, ENCARGADO, CORREO_ENCARGADO, FECHA_INGRESO, RUT, DIRECCION, DELETED) VALUES( NEGOCIO_ID_SEQ.NEXTVAL, '"+datosNegocio[1]+ "', '" + datosNegocio[2] + "', '" + datosNegocio[3] + "', TO_DATE('"+ datosNegocio[4] + "', 'yyyy-MM-dd'), '"+datosNegocio[5]+"', '"+datosNegocio[6]+"',0 )";
            OracleCommand cmd = new OracleCommand(query, conn);
            var reader = cmd.ExecuteNonQuery();
        }

        public void InsertRol(string nombreRol)
        {
            string query = "INSERT INTO ROL (ID, NOMBRE, DELETED) VALUES( ROL_ID_SEQ.NEXTVAL, '" + nombreRol + "',0 )";
            OracleCommand cmd = new OracleCommand(query, conn);
            var reader = cmd.ExecuteNonQuery();
        }

        public void InsertUsuario(string[] datosUsuario)
        {

            string query = "INSERT INTO USUARIO (ID, CORREO, PASSWORD, RUT, NOMBRE, APELLIDOP, APELLIDOM, CELULAR, DELETED, ROL_ID, NEGOCIO_ID, GRUPOTRABAJO_ID) VALUES(USUARIO_ID_SEQ.NEXTVAL, '" + datosUsuario[1] + "', '" + datosUsuario[2] + "', '" + datosUsuario[3] + "', '" + datosUsuario[4] + "', '" + datosUsuario[5] + "', '" + datosUsuario[6] + "', " + datosUsuario[7] + ", " + datosUsuario[8] + ", " + datosUsuario[9] + ", " + datosUsuario[10] + ", " + datosUsuario[11] + ")";
            OracleCommand cmd = new OracleCommand(query, conn);
            var reader = cmd.ExecuteNonQuery();
        }

        public void AgregarGrupoNegocio(string nombreGP, string idNegocio, List<string> listaUsuarios)
        {
            string idGP = "";
            //Ingresar nuevo Grupo de Trabajo
            string query = "INSERT INTO grupotrabajo VALUES (null, '" + nombreGP + "', 0, "+ idNegocio + "); \n";
            var cmd = new MySql.Data.MySqlClient.MySqlCommand(query, conex);
            var reader = cmd.ExecuteReader();
            reader.Close();
            //Obtener ID de grupo de trabajo creado            
            query = "SELECT id FROM grupotrabajo where nombre = '" + nombreGP + "';";
            cmd = new MySql.Data.MySqlClient.MySqlCommand(query, conex);
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                idGP = reader.GetString("id");
            }
            reader.Close();
            query = "";
            for(int i = 0; i < listaUsuarios.Count; i++)
            {
                query += "UPDATE usuario SET grupotrabajo_id = " + idGP + " WHERE id = "+ listaUsuarios[i] + "; \n" ;
            }
            cmd = new MySql.Data.MySqlClient.MySqlCommand(query, conex);
            var reader2 = cmd.ExecuteNonQuery();
            
        }

    }
}
