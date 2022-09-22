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

        bool menu1_selected = false;
        bool menu2_selected = false;


        public Dashboard()
        {
            InitializeComponent();
            WindowState = WindowState.Maximized;
        }

        //Botones Main Menú
        private void mainMenuNegocios_Click(object sender, RoutedEventArgs e)
        {
            OcultarMenu2();
            ApagarBotonesMainMenu();
            if (MenuNegocio.Visibility.Equals(Visibility.Hidden) && !menu1_selected)
            {
                MenuNegocio.Visibility = Visibility.Visible;
                CambiarColorBoton(mainMenuNegocios, color_menu1_pressed);
                menu1_selected = true;
            }
            else
            {
                menu1_selected = false;
            }
        }

        private void mainMenuGruposTrabajo_Click(object sender, RoutedEventArgs e)
        {
            OcultarMenu2();
            ApagarBotonesMainMenu();
            if (MenuNegocio.Visibility.Equals(Visibility.Hidden) && !menu1_selected)
            {
                CambiarColorBoton(mainMenuGruposTrabajo, color_menu2_pressed);
                menu2_selected = true;
            }
            else
            {
                menu2_selected = false;
            }
        }




        //Botones Menu Negocio
        private void btn_negocio_crear_Click(object sender, RoutedEventArgs e)
        {
            if (Pantalla_Agregar_Negocio.Visibility.Equals(Visibility.Hidden) && !menu2_selected)
            {
                date_pick.SelectedDate = DateTime.Now;
                Pantalla_Agregar_Negocio.Visibility = Visibility.Visible;

                CambiarColorBoton(btn_negocio_crear, color_menu2_pressed);

                menu2_selected = true;

            }
            else
            {
                Pantalla_Agregar_Negocio.Visibility = Visibility.Hidden;

                var bc = new BrushConverter();
                CambiarColorBoton(btn_negocio_crear, color_menu2_idle);

                menu2_selected = false;

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

        private void OcultarMenu2()
        {
            MenuNegocio.Visibility = Visibility.Hidden;
            MenuGrupoTrabajo.Visibility = Visibility.Hidden;
            
        }
    }
}
