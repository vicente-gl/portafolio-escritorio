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
using System.Windows.Navigation;
using System.Windows.Shapes;
using MySql.Data.MySqlClient;

namespace Control_de_Tareas
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 

    public partial class MainWindow : Window
    {
        private string queryResult;

        private string username;
        private string password;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            CConexion cConexion = new CConexion();
            username = txtBoxUser.Text;
            password = txtBoxPassword.Password;
            //LoginSinCredencial();

            cConexion.EstablecerConn();
            if(cConexion.CheckCredentials(username, password)){
                this.Close();
            }

        }        

        private void LoginSinCredencial()
        {
            Dashboard dashboard = new Dashboard();
            dashboard.Show();
            this.Close();
        }

        private void btn_connectBD_Click(object sender, RoutedEventArgs e)
        {
            CConexion objConexion = new CConexion();
            objConexion.EstablecerConn();
        }
    }
}
