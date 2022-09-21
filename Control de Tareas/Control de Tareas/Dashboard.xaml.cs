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
    public partial class Dashboard : Window
    {
        public Dashboard()
        {
            InitializeComponent();
            WindowState = WindowState.Maximized;
        }

        private void mainMenuNegocios_Click(object sender, RoutedEventArgs e)
        {
            if (MenuNegocio.Visibility.Equals(Visibility.Hidden))
            {
                MenuNegocio.Visibility = Visibility.Visible;
            }
            else
            {
                MenuNegocio.Visibility = Visibility.Hidden;

            }
        }

        private void btn_negocio_crear_Click(object sender, RoutedEventArgs e)
        {
            if (Pantalla_Agregar_Negocio.Visibility.Equals(Visibility.Hidden))
            {
                date_pick.SelectedDate = DateTime.Now;
                Pantalla_Agregar_Negocio.Visibility = Visibility.Visible;
            }
            else
            {
                Pantalla_Agregar_Negocio.Visibility = Visibility.Hidden;

            }
        }

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
    }
}
