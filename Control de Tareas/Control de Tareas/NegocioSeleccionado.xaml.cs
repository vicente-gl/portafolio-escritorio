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
    /// Interaction logic for NegocioSeleccionado.xaml
    /// </summary>
    public partial class NegocioSeleccionado : Window
    {
        public event Action<bool> NegocioSeleccionadoOK;
        public NegocioSeleccionado()
        {
            InitializeComponent();
            LlenarTabla();
        }

        private void Btn_SeleccionarNegocio_Volver_Click(object sender, RoutedEventArgs e)
        {
            if (NegocioSeleccionadoOK != null)
                NegocioSeleccionadoOK(false);
            this.Close();
            
        }

        private void LlenarTabla()
        {
            CConexion cConexion = new CConexion();
            cConexion.LlamarTablaSeleccionarNegocio("negocio", DataGrid_SeleccionarNegocio);
            //DataGrid_SeleccionarNegocio.Columns[0].Visibility = Visibility.Collapsed;
        }


        private void Btn_SeleccionarNegocio_Seleccionar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DataGrid_SeleccionarNegocio_PreviewMouseWheel(object sender, MouseWheelEventArgs e) //Envia info de mouse wheel a scrollviewer
        {
            //ScrollViewer.ScrollToVerticalOffset(ScrollViewer.VerticalOffset - e.Delta / 3);
        }
    }
}
