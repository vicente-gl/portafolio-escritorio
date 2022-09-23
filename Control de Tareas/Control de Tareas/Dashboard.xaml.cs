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
        int negocioSeleccionado = 0;

        string color_menu1_idle = "#FFCFCFCF";
        string color_menu1_pressed = "#EDEDED";
        string color_menu2_idle = "#EDEDED";
        string color_menu2_pressed = "#FFFFFF";

        string queryResult;

        public Dashboard()
        {

            InitializeComponent();
            WindowState = WindowState.Maximized;

            //Poner nombre de usuario logeado
            //por ahora se seleccionara el usuario con id 0

            MySqlConnection conex = new MySqlConnection();
            CConexion cConexion = new CConexion();
            MainWindow mainWindow = new MainWindow();

            conex.ConnectionString = cConexion.cadenaConexion;
            conex.Open();
            string query = "SELECT CONCAT(nombre, ' ', apellidop, ' ', apellidom) AS nombrecompleto FROM usuario WHERE id = " + mainWindow.logedUser + ";";
            //string query = "SELECT CONCAT(nombre, ' ', apellidop, ' ', apellidom) AS nombrecompleto FROM usuario WHERE id = 1;";
            var cmd = new MySql.Data.MySqlClient.MySqlCommand(query, conex);
            var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                queryResult = reader.GetString("nombrecompleto");
            }

            label_logedUser.Content = queryResult;



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

        //Botones Pantalla Listar Usuarios

        private void btn_listarUsuarios_Click(object sender, RoutedEventArgs e)
        {
            //tablaUsuarios.DataContext = GetUserList();
            LlamarTabla("usuario");

        }


        private void LlamarTabla(string tabla)
        {

        }


        //Botones Crear Negocio
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

        //Botones Listar Negocios

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
            lista_menu2 = new List<Grid>();

            lista_menu2.Add(MenuNegocio);
            lista_menu2.Add(MenuGrupoTrabajo);
            lista_menu2.Add(MenuFlujos);
            lista_menu2.Add(MenuUsuarios);
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
            lista_pantalla = new List<Grid>();

            lista_pantalla.Add(Pantalla_Agregar_Negocio);
            lista_pantalla.Add(Pantalla_Listar_Negocio);
            lista_pantalla.Add(Pantalla_Agregar_Usuario);
            lista_pantalla.Add(Pantalla_Listar_Usuarios);
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
    }
}
