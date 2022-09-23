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

namespace Control_de_Tareas
{
    /// <summary>
    /// Interaction logic for Dashboard.xaml
    /// </summary>
    /// 

    public partial class Dashboard : Window
    {
        string color_menu1_idle = "#FFCFCFCF";
        string color_menu1_pressed = "#EDEDED";
        string color_menu2_idle = "#EDEDED";
        string color_menu2_pressed = "#FFFFFF";

        public Dashboard()
        {
            InitializeComponent();
            WindowState = WindowState.Maximized;
        }

        //Botones Main Menú
        private void mainMenuNegocios_Click(object sender, RoutedEventArgs e)
        {
            ApagarBotonesMainMenu();
            if (MenuNegocio.Visibility.Equals(Visibility.Hidden))
            {
                OcultarOtrosMenus(MenuNegocio);
                MenuNegocio.Visibility = Visibility.Visible;
                CambiarColorBoton(mainMenuNegocios, color_menu1_pressed);
            }
            else
            {
                OcultarOtrosMenus(MenuNegocio);
            }
        }

        private void mainMenuGruposTrabajo_Click(object sender, RoutedEventArgs e)
        {
            ApagarBotonesMainMenu();
            if (MenuGrupoTrabajo.Visibility.Equals(Visibility.Hidden))
            {
                OcultarOtrosMenus(MenuGrupoTrabajo);
                MenuGrupoTrabajo.Visibility = Visibility.Visible;
                CambiarColorBoton(mainMenuGruposTrabajo, color_menu2_pressed);
            }
            else
            {
                OcultarOtrosMenus(MenuGrupoTrabajo);
            }
        }


        private void mainMenuFlujosTarea_Click(object sender, RoutedEventArgs e)
        {
            ApagarBotonesMainMenu();
            if (MenuFlujos.Visibility.Equals(Visibility.Hidden))
            {
                OcultarOtrosMenus(MenuFlujos);
                MenuFlujos.Visibility = Visibility.Visible;
                CambiarColorBoton(mainMenuFlujosTarea, color_menu2_pressed);
            }
            else
            {
                OcultarOtrosMenus(MenuFlujos);
            }
        }

        private void mainMenuUsuarios_Click(object sender, RoutedEventArgs e)
        {
            ApagarBotonesMainMenu();
            if (MenuUsuarios.Visibility.Equals(Visibility.Hidden))
            {
                OcultarOtrosMenus(MenuUsuarios);
                MenuUsuarios.Visibility = Visibility.Visible;
                CambiarColorBoton(mainMenuUsuarios, color_menu2_pressed);
            }
            else
            {
                OcultarOtrosMenus(MenuUsuarios);
            }

        }




        //Botones Menu Negocio
        private void btn_negocio_crear_Click(object sender, RoutedEventArgs e)
        {
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

        //Botones Crear Negocio
        private void btn_crearNegocio_limpiar_Click(object sender, RoutedEventArgs e)
        {
            txtbox_nombre.Text = "";
            txtbox_rut_negocio.Text = "";
            txtbox_direccion.Text = "";
            txtbox_nombre_jefe.Text = "";
            txtbox_mail_jefe.Text = "";
            txtbox_num_contacto.Text = "";
            txtbox_web_rrss.Text = "";
            txtbox_girocomercial.Text = "";
            date_pick.SelectedDate = DateTime.Now;
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
    }
}
