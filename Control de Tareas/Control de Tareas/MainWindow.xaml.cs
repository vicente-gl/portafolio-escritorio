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
        private string username, password, sql;
        private CConexion conn = new CConexion();
        private MySqlCommand command;
        public int logedUser = 0;


        bool btnlogin = true;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            TryLogin();
            username = txtBoxUser.Text;
            password = txtBoxPassword.Password;

        }



        private void TryLogin()
        {
            {
               if (btnlogin)
                {
                string name = txtBoxUser.Text;
                string pass = txtBoxPassword.Password.ToString();
                Console.WriteLine(name);
                Console.WriteLine(pass);
                Console.WriteLine("Login Exitoso");

                Dashboard dashboard = new Dashboard();
                dashboard.Show();

                this.Close();

                }
                else
                {
                    System.Windows.MessageBox.Show("Debe");
                }
            }
        }

        private void btn_connectBD_Click(object sender, RoutedEventArgs e)
        {
            CConexion objConexion = new CConexion();
            objConexion.EstablecerConn();
        }
    }
}
