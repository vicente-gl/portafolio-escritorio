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

namespace Control_de_Tareas
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            TryLogin();
        }



        private void TryLogin()
        {

            MainWindow mainWindow = new MainWindow();
            mainWindow.Hide();

            string name = txtBoxUser.Text;
            string pass = txtBoxPassword.Password.ToString();
            Console.WriteLine(name);
            Console.WriteLine(pass);
            Console.WriteLine("Login Exitoso");

            Dashboard dashboard = new Dashboard();
            dashboard.Show();
            mainWindow.Hide();
        }
    }
}
