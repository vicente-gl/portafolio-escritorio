using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MySql.Data.MySqlClient;
using System.Data;

namespace Control_de_Tareas
{
    public partial class Dashboard : Window
    {
        public int negocioSeleccionado = 0;
        public int idUsuarioLogeado;

        private string negocioSelected;


        string color_menu1_idle = "#FFCFCFCF";
        string color_menu1_pressed = "#EDEDED";
        string color_menu2_idle = "#EDEDED";
        string color_menu2_pressed = "#FFFFFF";

        string queryResult;

        public Dashboard()
        {

            InitializeComponent();
            WindowState = WindowState.Maximized;            
        }

        public void LogearUsuario()
        {
            //Poner nombre de usuario logeado
            //por ahora se seleccionara el usuario con id 0

            MySqlConnection conex = new MySqlConnection();
            CConexion cConexion = new CConexion();

            conex.ConnectionString = cConexion.cadenaConexion;
            conex.Open();
            Console.WriteLine("logeado desde dashboard: " + idUsuarioLogeado);
            string query = "SELECT CONCAT(nombre, ' ', apellidop, ' ', apellidom) AS nombrecompleto FROM usuario WHERE id = " + idUsuarioLogeado + ";";
            var cmd = new MySql.Data.MySqlClient.MySqlCommand(query, conex);
            var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                queryResult = reader.GetString("nombrecompleto");
            }
            //System.Windows.MessageBox.Show(mainWindow.logedUser.ToString());
            label_logedUser.Content = queryResult;
            conex.Close();
        }

        //Botones Main Menú
        private void mainMenuNegocios_Click(object sender, RoutedEventArgs e)
        {
            ApagarBotonesMainMenu();
            OcultarOtrosMenus(MenuNegocio);
            if (MenuNegocio.Visibility.Equals(Visibility.Hidden))
            {
                MenuNegocio.Visibility = Visibility.Visible;
                CambiarColorBoton(mainMenuNegocios, color_menu1_pressed);
            }

        }

        private void mainMenuGruposTrabajo_Click(object sender, RoutedEventArgs e)
        {
            ApagarBotonesMainMenu();
            OcultarOtrosMenus(MenuGrupoTrabajo);
            if (MenuGrupoTrabajo.Visibility.Equals(Visibility.Hidden))
            {
                MenuGrupoTrabajo.Visibility = Visibility.Visible;
                CambiarColorBoton(mainMenuGruposTrabajo, color_menu2_pressed);
            }
        }


        private void mainMenuFlujosTarea_Click(object sender, RoutedEventArgs e)
        {
            ApagarBotonesMainMenu();
            OcultarOtrosMenus(MenuFlujos);
            if (MenuFlujos.Visibility.Equals(Visibility.Hidden))
            {
                MenuFlujos.Visibility = Visibility.Visible;
                CambiarColorBoton(mainMenuFlujosTarea, color_menu2_pressed);
            }
        }

        private void mainMenuUsuarios_Click(object sender, RoutedEventArgs e)
        {
            ApagarBotonesMainMenu();
            OcultarOtrosMenus(MenuUsuarios);
            if (MenuUsuarios.Visibility.Equals(Visibility.Hidden))
            {
                MenuUsuarios.Visibility = Visibility.Visible;
                CambiarColorBoton(mainMenuUsuarios, color_menu2_pressed);
            }

        }




        //Botones Menu Negocio
        private void btn_negocio_crear_Click(object sender, RoutedEventArgs e)
        {
            OcultarOtrasPantallas(Pantalla_Agregar_Negocio);
            if (Pantalla_Agregar_Negocio.Visibility.Equals(Visibility.Hidden))
            {
                date_pick.SelectedDate = DateTime.Now;
                Pantalla_Agregar_Negocio.Visibility = Visibility.Visible;
                CambiarColorBoton(btn_negocio_crear, color_menu2_pressed);
            }
            else
            {
                Pantalla_Agregar_Negocio.Visibility = Visibility.Hidden;
                CambiarColorBoton(btn_negocio_crear, color_menu2_idle);
            }
        }

        private void btn_negocio_listar_Click(object sender, RoutedEventArgs e)
        {
            OcultarOtrasPantallas(Pantalla_Listar_Negocio);
            if (Pantalla_Listar_Negocio.Visibility.Equals(Visibility.Hidden))
            {
                date_pick.SelectedDate = DateTime.Now;
                Pantalla_Listar_Negocio.Visibility = Visibility.Visible;
                CambiarColorBoton(btn_negocio_listar, color_menu2_pressed);
            }
            else
            {
                Pantalla_Listar_Negocio.Visibility = Visibility.Hidden;
                CambiarColorBoton(btn_negocio_listar, color_menu2_idle);
            }
        }

        //Botones Menu Usuarios
        private void btn_usuarios_crear_Click(object sender, RoutedEventArgs e)
        {
            OcultarOtrasPantallas(Pantalla_Agregar_Usuario);
            if (Pantalla_Agregar_Usuario.Visibility.Equals(Visibility.Hidden))
            {
                Pantalla_Agregar_Usuario.Visibility = Visibility.Visible;
                CambiarColorBoton(btn_usuarios_crear, color_menu2_pressed);
            }
            else
            {
                Pantalla_Agregar_Usuario.Visibility = Visibility.Hidden;
                CambiarColorBoton(btn_usuarios_crear, color_menu2_idle);
            }
            CargarCombobox();

        }


        private void btn_usuarios_listar_Click(object sender, RoutedEventArgs e)
        {
            OcultarOtrasPantallas(Pantalla_Listar_Usuarios);
            if (Pantalla_Listar_Usuarios.Visibility.Equals(Visibility.Hidden))
            {
                Pantalla_Listar_Usuarios.Visibility = Visibility.Visible;
                CambiarColorBoton(btn_listarUsuarios, color_menu2_pressed);
            }
            else
            {
                Pantalla_Listar_Usuarios.Visibility = Visibility.Hidden;
                CambiarColorBoton(btn_listarUsuarios, color_menu2_idle);
            }
        }
        //Botonos Pantalla Crear Usuario
            //Boton Crear Usuario
        private void btn_agregar_usuario_Click(object sender, RoutedEventArgs e)
        {
            MySqlConnection conex = new MySqlConnection();
            CConexion cConexion = new CConexion();
            MainWindow mainWindow = new MainWindow();

            conex.ConnectionString = cConexion.cadenaConexion;
            conex.Open();
            //Crear contador de rows para ID
            string totalID;
            string query = "select COUNT(id) FROM usuario;";
            var cmd = new MySql.Data.MySqlClient.MySqlCommand(query, conex);
            //var reader = cmd.ExecuteReader();
            totalID = cmd.ExecuteScalar().ToString();

            Console.WriteLine(totalID);
            /*while (reader.Read())
            {
                //totalID = reader.GetString(query);
                queryResult = cmd.ExecuteScalar().ToString();
            }*/
            totalID = queryResult;
            //Crear variables para query
            string u_rut = txtbox_user_rut.Text;
            string u_rol = cbox_user_rol.SelectedItem as string;
            string u_nombre = txtbox_user_nombre.Text;
            string u_apellidop = txtbox_user_apellidop.Text;
            string u_apellidom = txtbox_user_apellidom.Text;
            string u_correo = txtbox_user_correo.Text;
            string u_celular = txtbox_user_celular.Text;
            string u_jefe = cbox_user_jefe.SelectedItem as string;
            string u_negocio = cbox_user_negocio.SelectedItem as string;
            string u_grupotrabajo = cbox_user_gtrabajo.SelectedItem as string;


            //Agregar columnas y campos al query
            
            //query = "INSERT INTO usuario VALUES ("+totalID+", "+u_rut+", "+u_rol+", '"+u_nombre+"', '"+u_apellidop+"', '"+u_apellidom+"', '"+u_correo+"', "+u_celular+", 0, 0, 0, 0, 0, 0, NULL, NULL, null);";
            query = "INSERT INTO usuario VALUES (3, 18999333-2, 0, '" + u_nombre + "', '" + u_apellidop + "', '" + u_apellidom + "', '" + u_correo + "', " + u_celular + ", 0, 0, 0, 0, 0, 0, NULL, NULL, null);";
            cmd = new MySql.Data.MySqlClient.MySqlCommand(query, conex);
            cmd.ExecuteReader();

            conex.Close();
            //reader = cmd.ExecuteReader();
        }
            //Boton Limpiar Campos de Crear Usuario
        private void btn_agregarUser_limpiar_Click(object sender, RoutedEventArgs e)
        {
            txtbox_user_username.Text = "";
            txtbox_user_password.Text = "";
            txtbox_user_nombre.Text = "";
            txtbox_user_rut.Text = "";
            txtbox_user_apellidop.Text = "";
            txtbox_user_apellidom.Text = "";
            txtbox_user_correo.Text = "";
            txtbox_user_celular.Text = "";
            cbox_user_jefe.SelectedItem = null;
            cbox_user_negocio.SelectedItem = null;
            cbox_user_rol.SelectedItem = null;
            cbox_user_gtrabajo.SelectedItem = null;
        }
        //Botones Pantalla Listar Usuarios

        private void btn_listarUsuarios_Click(object sender, RoutedEventArgs e)
        {
            //tablaUsuarios.DataContext = GetUserList();
            LlamarTabla("usuario");

        }


        private void LlamarTabla(string tabla)
        {

        }

        
        //Botones Pantalla Crear Negocio
        private void btn_crearNegocio_limpiar_Click(object sender, RoutedEventArgs e)
        {
            txtbox_negocio_nombre.Text = "";
            txtbox_negocio_rut_negocio.Text = "";
            txtbox_negocio_direccion.Text = "";
            txtbox_negocio_nombre_jefe.Text = "";
            txtbox_negocio_mail_jefe.Text = "";
            txtbox_negocio_num_contacto.Text = "";
            txtbox_negocio_web_rrss.Text = "";
            txtbox_negocio_girocomercial.Text = "";
            date_pick.SelectedDate = DateTime.Now;
        }

        //Botones Pantalla Listar Negocios

        private void btn_listarNegocios_Click(object sender, RoutedEventArgs e)
        {

        }

        //Metodos Extras
        private void CambiarColorBoton(Button botonObjetivo, string nuevo_color)
        {
            var bc = new BrushConverter();
            botonObjetivo.Background = (Brush)bc.ConvertFrom(nuevo_color);
        }

        private void ApagarBotonesMainMenu()
        {
            CambiarColorBoton(mainMenuNegocios, color_menu1_idle);
            CambiarColorBoton(mainMenuGruposTrabajo, color_menu1_idle);
            CambiarColorBoton(mainMenuFlujosTarea, color_menu1_idle);
            CambiarColorBoton(mainMenuUsuarios, color_menu1_idle);
            CambiarColorBoton(mainMenuPerfilesNegocio, color_menu1_idle);
        }
        private void OcultarOtrosMenus(Grid selectedGrid)
        {
            List<Grid> lista_menu2;
            lista_menu2 = new List<Grid>
            {
                MenuNegocio,
                MenuGrupoTrabajo,
                MenuFlujos,
                MenuUsuarios
            };
            //opcion5

            foreach (Grid item in lista_menu2)
            {
                if(selectedGrid != item)
                {
                    item.Visibility = Visibility.Hidden;
                }
                else
                {
                    selectedGrid.Visibility = Visibility.Hidden;
                }
            }
        }
        private void OcultarOtrasPantallas(Grid selectedGrid)
        {
            List<Grid> lista_pantalla;
            lista_pantalla = new List<Grid>
            {
                Pantalla_Agregar_Negocio,
                Pantalla_Listar_Negocio,
                Pantalla_Agregar_Usuario,
                Pantalla_Listar_Usuarios
            };
            //opcion5

            foreach (Grid item in lista_pantalla)
            {
                if (selectedGrid != item)
                {
                    item.Visibility = Visibility.Hidden;
                }
                else
                {
                    selectedGrid.Visibility = Visibility.Hidden;
                }
            }
        }

        private void CargarCombobox()
        {
            //Llenar Combobox de Add User

            MySqlConnection conex = new MySqlConnection();
            CConexion cConexion = new CConexion();
            conex.ConnectionString = cConexion.cadenaConexion;

            string query = "SELECT nombrerol FROM rol;";
            MySqlCommand cmd = new MySqlCommand(query, conex);
            MySqlDataReader mydr;
            try
            {
                conex.Open();
                mydr = cmd.ExecuteReader();
                while (mydr.Read())
                {
                    string subj = mydr.GetString("nombrerol");
                    cbox_user_rol.Items.Add(subj);
                }
                conex.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            //Llenar Jefe
            /*
            query = "SELECT CONCAT(nombre, ' ', apellidop, ' ', apellidom) AS nombrecompleto FROM usuario WHERE rol_id = 3;";
            try
            {
                conex.Open();
                mydr = cmd.ExecuteReader();
                while (mydr.Read())
                {
                    string subj = mydr.GetString("nombrecompleto");
                    cbox_user_rol.Items.Add(subj);
                }
                conex.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            **/

            //Llenar Negocio
            query = "select nombre FROM negocio;";
            cmd = new MySqlCommand(query, conex);
            MySqlDataReader mydr2;
            try
            {
                conex.Open();
                mydr2 = cmd.ExecuteReader();
                while (mydr2.Read())
                {
                    string subj = mydr2.GetString("nombre");
                    cbox_user_negocio.Items.Add(subj);
                }
                conex.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            //Llenar Grupo Trabajo

            query = "select nombre FROM grupotrabajo;";
            cmd = new MySqlCommand(query, conex);
            MySqlDataReader mydr5;
            try
            {
                conex.Open();
                mydr5 = cmd.ExecuteReader();
                while (mydr5.Read())
                {
                    string subj = mydr5.GetString("nombre");
                    cbox_user_gtrabajo.Items.Add(subj);
                }
                conex.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void cbox_user_negocio_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MySqlConnection conex = new MySqlConnection();
            CConexion cConexion = new CConexion();
            
            conex.ConnectionString = cConexion.cadenaConexion;


            //guardar negocio seleccionado para mostrar los grupos de tarea que corresponden a ese negocio
            string query = "SELECT id FROM negocio WHERE nombre = '" + cbox_user_negocio.SelectedValue + "';";

            conex.Open();
            MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(query, conex);
            var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                queryResult = reader.GetString("id");
            }
            negocioSelected = queryResult;
            conex.Close();

            //cambiar combobox de grupos de trabajo

            query = "select nombre FROM grupotrabajo where id_negocio = " + negocioSelected + ";";
            cmd = new MySqlCommand(query, conex);
            MySqlDataReader mydr;

            cbox_user_gtrabajo.Items.Clear();

            try
            {
                conex.Open();
                mydr = cmd.ExecuteReader();
                while (mydr.Read())
                {
                    string subj = mydr.GetString("nombre");
                    cbox_user_gtrabajo.Items.Add(subj);
                }
                conex.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
