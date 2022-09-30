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
        public int idUsuarioLogeado;

        private string negocioSelected;

        string color_menu1_idle = "#FFCFCFCF";
        string color_menu1_pressed = "#EDEDED";
        string color_menu2_idle = "#EDEDED";
        string color_menu2_pressed = "#FFFFFF";

        string queryResult;

        private string usuarioEditTarget;


        public Dashboard()
        {
            InitializeComponent();
            WindowState = WindowState.Maximized;            
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


        //Botones Menu 2 Negocio
        private void btn_negocio_crear_Click(object sender, RoutedEventArgs e)
        {
            ApagarBotonesMenu2();
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
            ApagarBotonesMenu2();
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

        //Botones Menu 2 Grupos de Trabajo
        private void btn_admin_rol_Click(object sender, RoutedEventArgs e)
        {
            ApagarBotonesMenu2();
            OcultarOtrasPantallas(Pantalla_Administrar_Roles);
            if (Pantalla_Administrar_Roles.Visibility.Equals(Visibility.Hidden))
            {
                date_pick.SelectedDate = DateTime.Now;
                Pantalla_Administrar_Roles.Visibility = Visibility.Visible;
                CambiarColorBoton(btn_admin_rol, color_menu2_pressed);
            }
            else
            {
                Pantalla_Administrar_Roles.Visibility = Visibility.Hidden;
                CambiarColorBoton(btn_admin_rol, color_menu2_idle);
            }
        }

        //Botones Menu 2 Usuarios
        private void btn_usuarios_crear_Click(object sender, RoutedEventArgs e)
        {
            LimpiarCbox();
            ApagarBotonesMenu2();
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
            CargarComboboxCrear();

        }

        private void btn_usuarios_listar_Click(object sender, RoutedEventArgs e)
        {
            ApagarBotonesMenu2();
            OcultarOtrasPantallas(Pantalla_Listar_Usuarios);
            if (Pantalla_Listar_Usuarios.Visibility.Equals(Visibility.Hidden))
            {
                Pantalla_Listar_Usuarios.Visibility = Visibility.Visible;
                CambiarColorBoton(btn_usuarios_listar, color_menu2_pressed);
            }
            else
            {
                Pantalla_Listar_Usuarios.Visibility = Visibility.Hidden;
                CambiarColorBoton(btn_usuarios_listar, color_menu2_idle);
            }
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
            CConexion cConexion = new CConexion();
            cConexion.LlamarTabla("negocio", tablaNegocios);
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

            totalID = cmd.ExecuteScalar().ToString();
            //Crear variables para query
            string u_correo = txtbox_user_correo.Text;
            string u_password = txtbox_user_password.Text;
            string u_rut = txtbox_user_rut.Text;
            string u_nombre = txtbox_user_nombre.Text;
            string u_apellidop = txtbox_user_apellidop.Text;
            string u_apellidom = txtbox_user_apellidom.Text;
            string u_celular = txtbox_user_celular.Text;
            string u_negocio = cbox_user_negocio.SelectedItem as string;
            string u_rol = cbox_user_rol.SelectedItem as string;
            string u_grupotrabajo = cbox_user_gtrabajo.SelectedItem as string;

            // Obtener ID de: Rol, Negocio, Grupo de Trabajo
            //Obtener Negocio
            
            query = "select ID FROM negocio where nombre = '" + u_negocio + "';";
            cmd = new MySqlCommand(query, conex);
            MySqlDataReader mydr;

            try
            {
                //conex.Open();
                mydr = cmd.ExecuteReader();
                while (mydr.Read())
                {
                    string subj = mydr.GetString("id");
                    u_negocio = subj;
                }
                mydr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
           
            
            //Obtener Rol
            query = "select ID FROM rol where nombre = '"+ u_rol+"';";
            try
            {
                mydr = cmd.ExecuteReader();
                while (mydr.Read())
                {
                    string subj = mydr.GetString("id");
                    u_rol = subj;
                }
                mydr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
            
            //Obtener GrupoTrabajo
            query = "select ID FROM grupotrabajo where nombre = '" + u_grupotrabajo + "';";
            try
            {
                //conex.Open();
                mydr = cmd.ExecuteReader();
                while (mydr.Read())
                {
                    string subj = mydr.GetString("id");
                    u_grupotrabajo = subj;
                }
                mydr.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            //Agregar columnas y campos al query
            query = "INSERT INTO usuario VALUES("+totalID+", '"+u_correo+"', '"+u_password+"', '"+u_rut+"', '"+u_nombre+"', '"+u_apellidop+"', '"+u_apellidom+"', '"+u_celular+"', 0, "+u_rol+", "+u_negocio+", "+u_grupotrabajo+", NULL);";
            cmd = new MySql.Data.MySqlClient.MySqlCommand(query, conex);
            cmd.ExecuteNonQuery();

            conex.Close();
        }
            //Boton Limpiar Campos de Crear Usuario
        private void btn_agregarUser_limpiar_Click(object sender, RoutedEventArgs e)
        {
            txtbox_user_password.Text = "";
            txtbox_user_nombre.Text = "";
            txtbox_user_rut.Text = "";
            txtbox_user_apellidop.Text = "";
            txtbox_user_apellidom.Text = "";
            txtbox_user_correo.Text = "";
            txtbox_user_celular.Text = "";
            cbox_user_negocio.SelectedItem = null;
            cbox_user_rol.SelectedItem = null;
            cbox_user_gtrabajo.SelectedItem = null;
        }
        //Botones Pantalla Listar Usuarios

        private void btn_listarUsuarios_Click(object sender, RoutedEventArgs e)
        {
            CConexion cConexion = new CConexion();
            cConexion.LlamarTabla("usuario", tablaUsuarios);
        }        

        private void btn_editarUsuario_Click(object sender, RoutedEventArgs e)
        {
            if(tablaUsuarios.SelectedValue == null)
            {
                MessageBox.Show("No se ha seleccionado ningún Usuario");
            }
            else
            {
                //IList rows = tablaUsuarios.SelectedItems;
                DataRowView row = (DataRowView)tablaUsuarios.SelectedItems[0];
                string idSelected = row["id"].ToString();
                string[] datosUsuario = new string[11];

                usuarioEditTarget = idSelected;
                datosUsuario[1] = row["negocio_id"].ToString();
                datosUsuario[2] = row["rut"].ToString();
                datosUsuario[3] = row["correo"].ToString();
                datosUsuario[4] = row["password"].ToString();
                datosUsuario[5] = row["rol_id"].ToString(); // transformar a ID
                datosUsuario[6] = row["nombre"].ToString();
                datosUsuario[7] = row["apellidop"].ToString();
                datosUsuario[8] = row["apellidom"].ToString();
                datosUsuario[9] = row["celular"].ToString();
                datosUsuario[10] = row["grupotrabajo_id"].ToString();

                Pantalla_Editar_Usuario.Visibility = Visibility.Visible;
                LlenarCamposEditarUser(datosUsuario);
                CargarComboboxEditar();


            }
        }

        private void edit_btn_editar_usuario_Click(object sender, RoutedEventArgs e)
        {
            if(edit_cbox_user_negocio.SelectedItem == null || edit_cbox_user_rol.SelectedItem == null || edit_cbox_user_gtrabajo.SelectedItem == null)
            {
                MessageBox.Show("Debe Ingresar todos los datos requeridos");
            }
            else
            {
                CConexion cConexion = new CConexion();
                cConexion.EstablecerConn();

                string editRol = cConexion.GetIDByName("rol", edit_cbox_user_rol.SelectedItem.ToString());
                string editNegocio = cConexion.GetIDByName("negocio", edit_cbox_user_negocio.SelectedItem.ToString());
                string editGrupoTrabajo = cConexion.GetIDByName("grupotrabajo", edit_cbox_user_gtrabajo.SelectedItem.ToString());
                Console.WriteLine(editRol);

                string[] datosUsuario = new string[13];
                datosUsuario[0]  = usuarioEditTarget; // NADA pq es ID
                datosUsuario[1]  = edit_txtbox_user_correo.Text; //correo
                datosUsuario[2]  = edit_txtbox_user_password.Text; //password
                datosUsuario[3]  = edit_txtbox_user_rut.Text; //rut
                datosUsuario[4]  = edit_txtbox_user_nombre.Text; //nombre
                datosUsuario[5]  = edit_txtbox_user_apellidop.Text; //apellidop
                datosUsuario[6]  = edit_txtbox_user_apellidom.Text; //apellidom
                datosUsuario[7]  = edit_txtbox_user_celular.Text; //celular
                datosUsuario[8]  = ""; //deleted
                datosUsuario[9]  = editRol; //rol_id
                datosUsuario[10] = editNegocio;//negocio_id
                datosUsuario[11] = editGrupoTrabajo;//grupotrabajo_id

                cConexion.UpdateUsuario(datosUsuario);
                MessageBox.Show("Usuario Modificado Exitosamente");
            }


        }

        //Metodos Extras

        private void LlenarCamposEditarUser(string[] datosUsuario)
        {
            edit_cbox_user_negocio.Text = datosUsuario[1];
            edit_txtbox_user_rut.Text = datosUsuario[2];
            edit_txtbox_user_correo.Text = datosUsuario[3];
            edit_txtbox_user_password.Text = datosUsuario[4];
            edit_cbox_user_rol.Text = datosUsuario[5];
            edit_txtbox_user_nombre.Text = datosUsuario[6];
            edit_txtbox_user_apellidop.Text = datosUsuario[7];
            edit_txtbox_user_apellidom.Text = datosUsuario[8];
            edit_txtbox_user_celular.Text = datosUsuario[9];
            edit_cbox_user_gtrabajo.Text = datosUsuario[10];
        }

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

        private void ApagarBotonesMenu2()
        {
            //Negocio
            CambiarColorBoton(btn_negocio_crear, color_menu2_idle);
            CambiarColorBoton(btn_negocio_listar, color_menu2_idle);
            //Grupo de Trabajo
            CambiarColorBoton(btn_admin_rol, color_menu2_idle);
            CambiarColorBoton(btn_gptrabajo_crear, color_menu2_idle);
            CambiarColorBoton(btn_gptrabajo_listar, color_menu2_idle);
            //Usuarios
            CambiarColorBoton(btn_usuarios_crear, color_menu2_idle);
            CambiarColorBoton(btn_usuarios_listar, color_menu2_idle);

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

        //Oculta todas las pantallas menos la recien seleccionada
        private void OcultarOtrasPantallas(Grid selectedGrid)
        {
            List<Grid> lista_pantalla;
            lista_pantalla = new List<Grid>
            {
                Pantalla_Agregar_Negocio,
                Pantalla_Listar_Negocio,
                Pantalla_Administrar_Roles,
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

        //Carga los Combobox de la pantalla Crear Usuario
        private void CargarComboboxCrear()
        {
            //Llenar Combobox de Add User

            CConexion cConexion = new CConexion();
            cConexion.EstablecerConn();

            string[] roles = cConexion.CargarCombobox("rol");
            foreach (string role in roles)
            {
                cbox_user_rol.Items.Add(role);
            }

            string[] negocios = cConexion.CargarCombobox("negocio");
            foreach(string negocio in negocios)
            {
                cbox_user_negocio.Items.Add(negocio);
            }

            string[] grupotrabajo = cConexion.CargarCombobox("grupotrabajo");
            foreach(string grupostrabajo in grupotrabajo)
            {
                cbox_user_gtrabajo.Items.Add(grupostrabajo);
            }

        }

        private void CargarComboboxEditar()
        {
            //Llenar Combobox de Add User

            CConexion cConexion = new CConexion();
            cConexion.EstablecerConn();

            string[] roles = cConexion.CargarCombobox("rol");
            foreach (string role in roles)
            {
                edit_cbox_user_rol.Items.Add(role);
            }

            string[] negocios = cConexion.CargarCombobox("negocio");
            foreach (string negocio in negocios)
            {
                edit_cbox_user_negocio.Items.Add(negocio);
            }

            string[] grupotrabajo = cConexion.CargarCombobox("grupotrabajo");
            foreach (string grupostrabajo in grupotrabajo)
            {
                edit_cbox_user_gtrabajo.Items.Add(grupostrabajo);
            }

        }

        //Actualiza los combobox dependiendo del negocio seleccionado
        private void cbox_user_negocio_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cbox_user_gtrabajo.Items.Clear();
            CConexion cconexion = new CConexion();
            cconexion.EstablecerConn();
            string negocio = cbox_user_negocio.SelectedValue.ToString(); //Da error cuando entro a crear seleciono negocio, salgo y vuelvo de pantalla de Crear Usuario

            string[] grupotrabajo = cconexion.CargarComboboxNegocio(negocio);
            foreach(string grupostrabajo in grupotrabajo)
            {
                cbox_user_gtrabajo.Items.Add(grupostrabajo);
            }
        }
        private void edit_cbox_user_negocio_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            edit_cbox_user_gtrabajo.Items.Clear();
            CConexion cconexion = new CConexion();
            cconexion.EstablecerConn();
            string negocio = edit_cbox_user_negocio.SelectedValue.ToString(); //Da error cuando entro a crear seleciono negocio, salgo y vuelvo de pantalla de Crear Usuario

            string[] grupotrabajo = cconexion.CargarComboboxNegocio(negocio);
            foreach (string grupostrabajo in grupotrabajo)
            {
                edit_cbox_user_gtrabajo.Items.Add(grupostrabajo);
            }
        }
        //antes de actualizar los combobox es necesario limpiarlos para evitar duplicados
        private void LimpiarCbox()
        {
            cbox_user_gtrabajo.Items.Clear ();
            cbox_user_negocio.Items.Clear ();
            cbox_user_rol.Items.Clear ();
        }

    }
}
