﻿using System;
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
using System.Text.RegularExpressions;

namespace Control_de_Tareas
{
    public partial class Dashboard : Window
    {
        public int idUsuarioLogeado;

        string color_menu1_idle = "#FFCFCFCF";
        string color_menu1_pressed = "#EDEDED";
        string color_menu2_idle = "#EDEDED";
        string color_menu2_pressed = "#FFFFFF";

        private string usuarioEditTarget;
        private string negocioEditTarget;

        public bool seleccionandoNegocio = false;

        public string _negocioSelected;
        public string idNegocioSeleccionado;

        public string[] idListaUsuariosCrearGP;

        //Variables subflujos
        string[] subflujotxt = new string[15];
        public string IDNegocioSeleccionado
        {
            get
            {
                return idNegocioSeleccionado;
            }
            set
            {
                idNegocioSeleccionado = value;
                ActualizarNegocioSelected();
                //falta por agregar Pantalla visible preseleccionada 
            }
        }

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
                Pantalla_Administrar_Roles.Visibility = Visibility.Visible;
                CambiarColorBoton(btn_admin_rol, color_menu2_pressed);
                CConexion cConexion = new CConexion();
                cConexion.LlamarTabla("rol", datagrid_Rol);
            }
            else
            {
                Pantalla_Administrar_Roles.Visibility = Visibility.Hidden;
                CambiarColorBoton(btn_admin_rol, color_menu2_idle);
            }
        }

        private void btn_gptrabajo_listar_Click(object sender, RoutedEventArgs e)
        {
            ApagarBotonesMenu2();
            OcultarOtrasPantallas(Pantalla_ListarGP);
            if (Pantalla_ListarGP.Visibility.Equals(Visibility.Hidden))
            {
                Pantalla_ListarGP.Visibility = Visibility.Visible;
                CambiarColorBoton(btn_gptrabajo_listar, color_menu2_pressed);
                CargarListaGP();
            }
            else
            {
                Pantalla_ListarGP.Visibility = Visibility.Hidden;
                CambiarColorBoton(btn_gptrabajo_listar, color_menu2_idle);
            }
        }

        private void btn_gptrabajo_crear_Click(object sender, RoutedEventArgs e)
        {
            ApagarBotonesMenu2();
            OcultarOtrasPantallas(Pantalla_SinNegocio);
            OcultarOtrasPantallas(Pantalla_Agregar_GP);
            if (idNegocioSeleccionado == null || idNegocioSeleccionado == "1")
            {
                if (Pantalla_SinNegocio.Visibility.Equals(Visibility.Hidden))
                {
                    Pantalla_SinNegocio.Visibility = Visibility.Visible;
                    CambiarColorBoton(btn_gptrabajo_crear, color_menu2_pressed);
                }
                else
                {
                    Pantalla_SinNegocio.Visibility = Visibility.Hidden;
                    CambiarColorBoton(btn_gptrabajo_crear, color_menu2_idle);
                }
            }
            else
            {
                if (Pantalla_Administrar_Roles.Visibility.Equals(Visibility.Hidden))
                {
                    Pantalla_Agregar_GP.Visibility = Visibility.Visible;
                    CambiarColorBoton(btn_gptrabajo_crear, color_menu2_pressed);
                }
                else
                {
                    Pantalla_Agregar_GP.Visibility = Visibility.Hidden;
                    CambiarColorBoton(btn_gptrabajo_crear, color_menu2_idle);
                }
                CargarUsuariosCrearGP();
            }
        }

        //Botones Flujos de Tarea
        private void btn_flujos_crear_Click(object sender, RoutedEventArgs e)
        {
            ApagarBotonesMenu2();
            OcultarOtrasPantallas(Pantalla_SinNegocio);
            OcultarOtrasPantallas(Pantalla_Crear_FlujoTarea);
            if (idNegocioSeleccionado == null || idNegocioSeleccionado == "1")
            {
                if (Pantalla_SinNegocio.Visibility.Equals(Visibility.Hidden))
                {
                    Pantalla_SinNegocio.Visibility = Visibility.Visible;
                    CambiarColorBoton(btn_flujos_crear, color_menu2_pressed);
                }
                else
                {
                    Pantalla_SinNegocio.Visibility = Visibility.Hidden;
                    CambiarColorBoton(btn_flujos_crear, color_menu2_idle);
                }
            }
            else
            {
                if (Pantalla_Crear_FlujoTarea.Visibility.Equals(Visibility.Hidden))
                {
                    Pantalla_Crear_FlujoTarea.Visibility = Visibility.Visible;
                    CargarCboxFlujo();
                    CambiarColorBoton(btn_flujos_crear, color_menu2_pressed);

                }
                else
                {
                    Pantalla_Crear_FlujoTarea.Visibility = Visibility.Hidden;
                    CambiarColorBoton(btn_flujos_crear, color_menu2_idle);
                }
                CargarUsuariosCrearGP();

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
        //Boton Limpiar campos de negocio
        private void btn_crearNegocio_limpiar_Click(object sender, RoutedEventArgs e)
        {
            LimpiarCamposNegocio();
        }
        //Boton Agregar Negocio
        private void btn_agregarNegocio_Click(object sender, RoutedEventArgs e)
        {
            if (txtbox_negocio_nombre.Text == "" || txtbox_negocio_encargado.Text == "" || txtbox_negocio_correo_encargado.Text == "" || txtbox_negocio_rut.Text == "")
            {
                MessageBox.Show("Debes ingresar todos los campos.");
            }
            else
            {
                string myDate2 = date_pick.SelectedDate.Value.ToString("yyyy-MM-dd");
                try
                {
                    CConexion cConexion = new CConexion();
                    cConexion.EstablecerConn();

                    string[] datosNegocio = new string[8];

                    datosNegocio[0] = "0"; // ID
                    datosNegocio[1] = txtbox_negocio_nombre.Text;
                    datosNegocio[2] = txtbox_negocio_encargado.Text;
                    datosNegocio[3] = txtbox_negocio_correo_encargado.Text;
                    datosNegocio[4] = myDate2; //ERROR NO LEE FORMATO FECHA
                    datosNegocio[5] = txtbox_negocio_rut.Text;
                    datosNegocio[6] = txtbox_negocio_direccion.Text;
                    datosNegocio[7] = "0"; //Deleted

                    cConexion.InsertNegocio(datosNegocio);
                    MessageBox.Show("Negocio Agregado Exitosamente");
                } catch (Exception ex)
                {
                    MessageBox.Show("No se pudo agregar negocio. Error: " + ex.Message);
                }
            }
        }

        //Botones Pantalla Listar Negocios
        private void btn_listarNegocios_Click(object sender, RoutedEventArgs e)
        {
            CConexion cConexion = new CConexion();
            cConexion.LlamarTabla("negocio", tablaNegocios);
            //tablaNegocios.Columns[0].Visibility = Visibility.Collapsed;
            //tablaNegocios.Columns[6].Visibility = Visibility.Collapsed;
        }
        //Eliminar Negocio
        private void btn_listarNegocios_Eliminar_Click(object sender, RoutedEventArgs e)
        {
            if (tablaNegocios.SelectedValue == null)
            {
                MessageBox.Show("No se ha seleccionado ningún Usuario");
            }
            else
            {
                if (MessageBox.Show("¿Desea Eliminar el Negocio Seleccionado?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
                {
                    //do no stuff
                }
                else
                {
                    CConexion cConexion = new CConexion();
                    cConexion.EstablecerConn();
                    DataRowView row = (DataRowView)tablaNegocios.SelectedItems[0];
                    string idSelected = row["id"].ToString();
                    cConexion.DeleteRow(idSelected, "negocio");
                    MessageBox.Show("Negocio Eliminado Exitosamente");
                    cConexion.CerrarConn();
                    CConexion ccConexion = new CConexion();
                    ccConexion.LlamarTabla("negocio", tablaNegocios);
                    ccConexion.CerrarConn();
                }
            }
        }
        //Pantalla Editar Negocio
        private void btn_listarNegocios_Editar_Click(object sender, RoutedEventArgs e)
        {
            Pantalla_Editar_Negocio.Visibility = Visibility.Visible;

            if (tablaNegocios.SelectedValue == null)
            {
                MessageBox.Show("No se ha seleccionado ningún Usuario");
            }
            else
            {
                LimpiarCbox();
                //IList rows = tablaUsuarios.SelectedItems;
                DataRowView row = (DataRowView)tablaNegocios.SelectedItems[0];
                string idSelected = row["id"].ToString();
                string[] datosNegocio = new string[6];

                negocioEditTarget = idSelected;
                datosNegocio[0] = row["nombre"].ToString();
                datosNegocio[1] = row["encargado"].ToString();
                datosNegocio[2] = row["fecha_ingreso"].ToString();
                datosNegocio[3] = row["correo_encargado"].ToString();
                datosNegocio[4] = row["rut"].ToString();
                datosNegocio[5] = row["direccion"].ToString();


                Pantalla_Editar_Negocio.Visibility = Visibility.Visible;
                LlenarCamposEditarNegocio(datosNegocio);
            }
        }

        private void btn_editar_negocio_Click(object sender, RoutedEventArgs e)
        {
            if (edit_txtbox_negocio_nombre.Text == "" || edit_txtbox_negocio_encargado.Text == "" || edit_txtbox_negocio_correo_encargado.Text == "" || edit_txtbox_negocio_rut.Text == "")
            {
                MessageBox.Show("Debe Ingresar todos los datos requeridos");
            }
            else
            {
                string myDate = edit_date_pick.SelectedDate.Value.ToShortDateString();
                string myDate2 = edit_date_pick.SelectedDate.Value.ToString("yyyy-MM-dd");

                CConexion cConexion = new CConexion();
                cConexion.EstablecerConn();

                string[] datosNegocio = new string[7];
                datosNegocio[0] = edit_txtbox_negocio_nombre.Text; // Nombre Negocio
                datosNegocio[1] = edit_txtbox_negocio_encargado.Text; //nombre encargado
                datosNegocio[2] = myDate2; //fecha
                datosNegocio[3] = edit_txtbox_negocio_correo_encargado.Text; //correo encargado
                datosNegocio[4] = edit_txtbox_negocio_rut.Text; //rut
                datosNegocio[5] = edit_txtbox_negocio_direccion.Text; //direccion
                datosNegocio[6] = negocioEditTarget; //ID

                cConexion.UpdateNegocio(datosNegocio);
                MessageBox.Show("Negocio Modificado Exitosamente");
                Pantalla_Editar_Negocio.Visibility = Visibility.Hidden;
            }
        }
        //Botones Roles
        private void btn_AgregarRol_Click(object sender, RoutedEventArgs e)
        {
            if (txtbox_rolNombre.Text == "")
            {
                MessageBox.Show("Debes ingresar todos los campos.");
            }
            else
            {
                try
                {
                    CConexion cConexion = new CConexion();
                    cConexion.EstablecerConn();

                    string nombreRol = txtbox_rolNombre.Text;
                    cConexion.InsertRol(nombreRol);
                    MessageBox.Show("Rol Agregado Exitosamente");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("No se pudo agregar rol. Error: " + ex.Message);
                }
            }
        }

        //Botones Grupos de Trabajo
        //Boton Crear Grupo de Trabajo
        private void btn_agregarGP_Click(object sender, RoutedEventArgs e)
        {
            CConexion cConexion = new CConexion();
            cConexion.EstablecerConn();
            List<string> listaIDUsuarios = new List<string>();
            foreach (CheckBox cb in ListBoxUsuariosGP.Items)
            {
                if (cb.IsChecked == true)
                {
                    listaIDUsuarios.Add(cb.Tag.ToString());
                }
            }
            cConexion.AgregarGrupoNegocio(txtBox_CrearGP.Text, idNegocioSeleccionado, listaIDUsuarios);

        }
        //Botones pantalla Listar GP
        //Boton Eliminar GP
        private void btn_eliminar_GP_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                CConexion cConexion = new CConexion();
                cConexion.EstablecerConn();
                DataRowView row = (DataRowView)tabla_listaGP.SelectedItems[0];
                string idSelected = row["id"].ToString();
                string nombreGP = row["nombre"].ToString();
                int cantUsuarios = tabla_usuariosGPSelected.Items.Count;
                string mensaje;
                cConexion.DeleteRow(idSelected, "grupotrabajo");
                string gt_ninguno = cConexion.GetIDByName("grupotrabajo", "Ninguno");
                cConexion.ResetGPUsuarios(idSelected, gt_ninguno);
                cConexion.CerrarConn();
                if (tabla_usuariosGPSelected.Items.Count == 1)
                {
                    mensaje = cantUsuarios + " Usuario del Grupo de Trabajo: " + nombreGP + " se actualizó correctamente a Ninguno";
                }
                else
                {
                    mensaje = cantUsuarios + " Usuarios del Grupo de Trabajo: " + nombreGP + " se actualizaron correctamente a Ninguno";
                }
                MessageBox.Show("Grupo de Trabajo Eliminado Exitosamente.\n " + mensaje);
            } catch (Exception ex)
            {
                MessageBox.Show("Error, no se pudo eliminar el Grupo de Trabajo: " + ex.Message);
            }

            //tabla_usuariosGPSelected.Items.Clear();
            CargarListaGP();

        }

        //Botones Flujo de Tareas
        //Crear Flujo de Tarea
        private void btn_agregarFlujo_Click(object sender, RoutedEventArgs e)
        {

        }

        //Botonos Pantalla Crear Usuario
        //Boton Crear Usuario
        private void btn_agregar_usuario_Click(object sender, RoutedEventArgs e)
        {
            if (txtbox_user_correo.Text == "" || txtbox_user_password.Text.ToString() == "" || txtbox_user_rut.Text == "" || txtbox_user_nombre.Text == "" || txtbox_user_apellidop.Text == "" || txtbox_user_apellidom.Text == "" || txtbox_user_celular.Text == "" || cbox_user_rol.SelectedIndex == -1 || cbox_user_negocio.SelectedIndex == -1 || cbox_user_gtrabajo.SelectedIndex == -1)
            {
                MessageBox.Show("Debes ingresar todos los campos");
            }
            else
            {
                try
                {
                    CConexion cConexion = new CConexion();
                    cConexion.EstablecerConn();

                    string[] datosUsuario = new string[12];
                    datosUsuario[0] = "0";
                    datosUsuario[1] = txtbox_user_correo.Text;
                    datosUsuario[2] = txtbox_user_password.Text.ToString();
                    datosUsuario[3] = txtbox_user_rut.Text;
                    datosUsuario[4] = txtbox_user_nombre.Text;
                    datosUsuario[5] = txtbox_user_apellidop.Text;
                    datosUsuario[6] = txtbox_user_apellidom.Text;
                    datosUsuario[7] = txtbox_user_celular.Text;
                    datosUsuario[8] = "0";
                    datosUsuario[9] = cConexion.GetIDByName("rol", cbox_user_rol.SelectedItem.ToString());
                    datosUsuario[10] = cConexion.GetIDByName("negocio", cbox_user_negocio.SelectedItem.ToString());
                    datosUsuario[11] = cConexion.GetIDByName("grupotrabajo", cbox_user_gtrabajo.SelectedItem.ToString());

                    cConexion.InsertUsuario(datosUsuario);

                    LimpiarCbox();
                    LimpiarCampos();
                    CargarComboboxCrear();
                    MessageBox.Show("Usuario Agregado Exitosamente");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("No se pudo Agregar al usuario. Error: " + ex.Message);
                }
            }
        }
        //Boton Limpiar Campos de Crear Usuario
        private void btn_agregarUser_limpiar_Click(object sender, RoutedEventArgs e)
        {
            LimpiarCampos();
        }
        //Botones Pantalla Listar Usuarios


        private void btn_listarUsuarios_Click(object sender, RoutedEventArgs e)
        {
            if (idNegocioSeleccionado == null || idNegocioSeleccionado == "1")
            {
                CConexion cConexion = new CConexion();
                cConexion.LlamarTabla("usuario", tablaUsuarios);
            }
            else
            {
                CConexion cConexion = new CConexion();
                cConexion.LlamarTablaNegocioSelected("usuario", tablaUsuarios, idNegocioSeleccionado);
            }
            tablaUsuarios.Columns[0].Visibility = Visibility.Collapsed;
            tablaUsuarios.Columns[8].Visibility = Visibility.Collapsed;

        }

        private void btn_editarUsuario_Click(object sender, RoutedEventArgs e)
        {
            if (tablaUsuarios.SelectedValue == null)
            {
                MessageBox.Show("No se ha seleccionado ningún Usuario");
            }
            else
            {
                LimpiarCbox();
                //IList rows = tablaUsuarios.SelectedItems;
                DataRowView row = (DataRowView)tablaUsuarios.SelectedItems[0];
                string idSelected = row["id"].ToString();
                string[] datosUsuario = new string[11];
                usuarioEditTarget = idSelected;
                datosUsuario[0] = idSelected;
                datosUsuario[1] = row["correo"].ToString();
                datosUsuario[2] = row["password"].ToString();
                datosUsuario[3] = row["rut"].ToString();
                datosUsuario[4] = row["nombre"].ToString();
                datosUsuario[5] = row["apellidop"].ToString();
                datosUsuario[6] = row["apellidom"].ToString();
                datosUsuario[7] = row["celular"].ToString();
                datosUsuario[8] = row["rol_id"].ToString();
                datosUsuario[9] = row["negocio_id"].ToString();
                datosUsuario[10] = row["grupotrabajo_id"].ToString();

                Pantalla_Editar_Usuario.Visibility = Visibility.Visible;
                LlenarCamposEditarUser(datosUsuario);
                CargarComboboxEditar();
            }
        }
        private void btn_eliminar_usuario_Click(object sender, RoutedEventArgs e)
        {
            if (tablaUsuarios.SelectedValue == null)
            {
                MessageBox.Show("No se ha seleccionado ningún Usuario");
            }
            else
            {
                if (MessageBox.Show("¿Desea Eliminar el Usuario Seleccionado?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
                {
                    //do no stuff
                }
                else
                {
                    CConexion cConexion = new CConexion();
                    cConexion.EstablecerConn();
                    DataRowView row = (DataRowView)tablaUsuarios.SelectedItems[0];
                    string idSelected = row["id"].ToString();
                    cConexion.DeleteRow(idSelected, "usuario");
                    MessageBox.Show("Usuario Eliminado Exitosamente");
                    //Falta Actualizar Tabla
                    cConexion.CerrarConn();
                    CConexion ccConexion = new CConexion();
                    ccConexion.LlamarTabla("usuario", tablaUsuarios);
                    ccConexion.CerrarConn();
                }
            }
        }

        private void edit_btn_editar_usuario_Click(object sender, RoutedEventArgs e)
        {
            if (edit_cbox_user_negocio.SelectedItem == null || edit_cbox_user_rol.SelectedItem == null || edit_cbox_user_gtrabajo.SelectedItem == null)
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

                string[] datosUsuario = new string[12];
                datosUsuario[0] = usuarioEditTarget;
                datosUsuario[1] = edit_txtbox_user_correo.Text; //correo
                datosUsuario[2] = edit_txtbox_user_password.Text; //password
                datosUsuario[3] = edit_txtbox_user_rut.Text; //rut
                datosUsuario[4] = edit_txtbox_user_nombre.Text; //nombre
                datosUsuario[5] = edit_txtbox_user_apellidop.Text; //apellidop
                datosUsuario[6] = edit_txtbox_user_apellidom.Text; //apellidom
                datosUsuario[7] = edit_txtbox_user_celular.Text; //celular
                datosUsuario[8] = "0"; //deleted
                datosUsuario[9] = editRol; //rol_id
                datosUsuario[10] = editNegocio;//negocio_id
                datosUsuario[11] = editGrupoTrabajo;//grupotrabajo_id

                cConexion.UpdateUsuario(datosUsuario);
                MessageBox.Show("Usuario Modificado Exitosamente");
            }


        }

        //Metodos Extras

        private void LlenarCamposEditarUser(string[] datosUsuario)
        {
            edit_txtbox_user_correo.Text = datosUsuario[1];
            edit_txtbox_user_password.Text = datosUsuario[2];
            edit_txtbox_user_rut.Text = datosUsuario[3];
            edit_txtbox_user_nombre.Text = datosUsuario[4];
            edit_txtbox_user_apellidop.Text = datosUsuario[5];
            edit_txtbox_user_apellidom.Text = datosUsuario[6];
            edit_txtbox_user_celular.Text = datosUsuario[7];
            edit_cbox_user_rol.Text = datosUsuario[8];
            edit_cbox_user_negocio.Text = datosUsuario[9];
            edit_cbox_user_gtrabajo.Text = datosUsuario[10];
        }

        private void LlenarCamposEditarNegocio(string[] datosNegocio)
        {
            edit_txtbox_negocio_nombre.Text = datosNegocio[0];
            edit_txtbox_negocio_encargado.Text = datosNegocio[1];
            edit_date_pick.SelectedDate = DateTime.Parse(datosNegocio[2]);
            edit_txtbox_negocio_correo_encargado.Text = datosNegocio[3];
            edit_txtbox_negocio_rut.Text = datosNegocio[4];
            edit_txtbox_negocio_direccion.Text = datosNegocio[5];
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
                Pantalla_Listar_Usuarios,
                Pantalla_Editar_Usuario,
                Pantalla_Administrar_Roles,
                Pantalla_Agregar_GP,
                Pantalla_ListarGP,
                Pantalla_Crear_FlujoTarea
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

        //Carga datos para creacion de Grupo de Trabajo
        private void CargarUsuariosCrearGP()
        {
            CConexion cConexion = new CConexion();
            cConexion.EstablecerConn();
            ListBoxUsuariosGP.Items.Clear();
            string gt_ninguno = cConexion.GetIDByName("grupotrabajo", "Ninguno");
            string[] listaUsuarios = cConexion.GetUsuariosFromNegocio(idNegocioSeleccionado, gt_ninguno);
            string[] listaNombreRol = cConexion.GetRolFromUsuarios(idNegocioSeleccionado);
            idListaUsuariosCrearGP = cConexion.GetUserIDFromNegocio(idNegocioSeleccionado, gt_ninguno);
            CheckBox box;
            for (int i = 0; i < listaUsuarios.Length; i++)
            {
                box = new CheckBox();
                box.Tag = idListaUsuariosCrearGP[i];
                box.Content = listaUsuarios[i] + " - " + listaNombreRol[i];
                if (box.Content.ToString().Contains("Admin"))
                {
                    box.FontWeight = FontWeights.Bold;
                }
                box.Name = "checkboxUser" + i;
                box.FontFamily = new FontFamily("Inter");
                box.FontSize = 18;

                ListBoxUsuariosGP.Items.Add(box);
            }
        }

        //Carga datos para listar los grupos de trabajo
        private void CargarListaGP()
        {
            CConexion cConexion = new CConexion();
            if (idNegocioSeleccionado == null || idNegocioSeleccionado == "1")
            {
                cConexion.LlamarTabla("grupotrabajo", tabla_listaGP);
            }
            else
            {
                cConexion.LlamarTablaNegocioSelected("grupotrabajo", tabla_listaGP, idNegocioSeleccionado);
            }
            tabla_listaGP.Columns[0].Header = "ID de Grupo de Trabajo";
            tabla_listaGP.Columns[1].Header = "Nombre de Grupo de Trabajo";
            tabla_listaGP.Columns[3].Visibility = Visibility.Collapsed;
            tabla_listaGP.Columns[2].Visibility = Visibility.Collapsed;
        }

        // Llenar lista de integrantes
        private void tabla_listaGP_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {

            if (tabla_listaGP.SelectedItems.Count != 0) // Evita bug extraño
            {
                CConexion cConexion = new CConexion();
                DataRowView row = (DataRowView)tabla_listaGP.SelectedItems[0];
                string idSelected = row["id"].ToString();
                cConexion.LlamarTablaUsuariosGP("usuario", tabla_usuariosGPSelected, idSelected);
                tabla_usuariosGPSelected.Columns[0].Visibility = Visibility.Collapsed;
                tabla_usuariosGPSelected.Columns[2].Visibility = Visibility.Collapsed;
                tabla_usuariosGPSelected.Columns[8].Visibility = Visibility.Collapsed;
                tabla_usuariosGPSelected.Columns[9].Visibility = Visibility.Collapsed;
                tabla_usuariosGPSelected.Columns[10].Visibility = Visibility.Collapsed;
                tabla_usuariosGPSelected.Columns[11].Visibility = Visibility.Collapsed;
                tabla_usuariosGPSelected.Columns[1].Header = "Correo Electrónico";
                tabla_usuariosGPSelected.Columns[3].Header = "RUT";
                tabla_usuariosGPSelected.Columns[4].Header = "Nombre";
                tabla_usuariosGPSelected.Columns[5].Header = "Apellido Paterno";
                tabla_usuariosGPSelected.Columns[6].Header = "Apellido Materno";
                tabla_usuariosGPSelected.Columns[7].Header = "Celular";
            }


        }

        //Carga los Combobox de la pantalla Crear Usuario
        private void CargarComboboxCrear()
        {
            //Llenar Combobox de Add User

            CConexion cConexion = new CConexion();
            cConexion.EstablecerConn();

            if (cbox_user_negocio.Items.Count == 0)
            {
                //Agregar Ninguno cuando no existe Gtrabajo
                if (cbox_user_gtrabajo.Items.Count == 0)
                {
                    cbox_user_gtrabajo.Items.Add("Ninguno");
                }
                string[] roles = cConexion.CargarCombobox("rol");
                foreach (string role in roles)
                {
                    cbox_user_rol.Items.Add(role);
                }

                string[] negocios = cConexion.CargarCombobox("negocio");
                foreach (string negocio in negocios)
                {
                    cbox_user_negocio.Items.Add(negocio);
                }

                string[] grupotrabajo = cConexion.CargarCombobox("grupotrabajo");
                foreach (string grupostrabajo in grupotrabajo)
                {
                    cbox_user_gtrabajo.Items.Add(grupostrabajo);
                }
                if (cbox_user_gtrabajo.Items.Count <= 0) cbox_user_gtrabajo.Items.Add("Ninguno");
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
            if (edit_cbox_user_gtrabajo.Items.Count <= 0) edit_cbox_user_gtrabajo.Items.Add("Ninguno");
        }

        //Actualiza los combobox dependiendo del negocio seleccionado
        private void cbox_user_negocio_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbox_user_negocio.Items.Count > 0)
            {
                cbox_user_gtrabajo.Items.Clear();
                cbox_user_gtrabajo.Items.Add("Ninguno");
                CConexion cconexion = new CConexion();
                cconexion.EstablecerConn();
                string negocio = cbox_user_negocio.SelectedValue.ToString(); //Da error cuando entro a crear seleciono negocio, salgo y vuelvo de pantalla de Crear Usuario

                string[] grupotrabajo = cconexion.CargarComboboxNegocio(negocio);
                foreach (string grupostrabajo in grupotrabajo)
                {
                    cbox_user_gtrabajo.Items.Add(grupostrabajo);
                }
            }
            //Agregar Ninguno cuando no existe Gtrabajo
            if (cbox_user_gtrabajo.Items.Count == 0)
            {
                cbox_user_gtrabajo.Items.Add("Ninguno");
            }
        }
        private void edit_cbox_user_negocio_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (edit_cbox_user_negocio.Items.Count > 0)
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
            //Agregar Ninguno cuando no existe Gtrabajo
            if (cbox_user_gtrabajo.Items.Count == 0)
            {
                edit_cbox_user_gtrabajo.Items.Add("Ninguno");
            }
        }
        //antes de actualizar los combobox es necesario limpiarlos para evitar duplicados
        private void LimpiarCbox()
        {
            cbox_user_gtrabajo.Items.Clear();
            cbox_user_negocio.Items.Clear();
            cbox_user_rol.Items.Clear();

            edit_cbox_user_negocio.Items.Clear();
            edit_cbox_user_gtrabajo.Items.Clear();
            edit_cbox_user_rol.Items.Clear();
        }

        private void LimpiarCampos()
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

        private void LimpiarCamposNegocio()
        {
            txtbox_negocio_nombre.Text = "";
            txtbox_negocio_encargado.Text = "";
            txtbox_negocio_correo_encargado.Text = "";
            txtbox_negocio_rut.Text = "";
            date_pick.SelectedDate = DateTime.Now.Date;
        }

        public void ActualizarNegocioSelected()
        {

            btn_SeleccionarNegocio.Content = _negocioSelected;
            /*
            if(Pantalla_SinNegocio.Visibility == Visibility.Visible)
            {
                Pantalla_SinNegocio.Visibility = Visibility.Hidden;
                Pantalla_Agregar_GP.Visibility = Visibility.Visible;
                CargarUsuariosCrearGP();
            }
            if (Pantalla_Agregar_GP.Visibility == Visibility.Visible)
            {
                CargarUsuariosCrearGP();
            }
            if(Pantalla_ListarGP.Visibility == Visibility.Visible)
            {
                //CargarListaGP();
            }
            */
        }

        private void btn_SeleccionarNegocio_Click(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show(_negocioSelected, idNegocioSeleccionado);

            if (!seleccionandoNegocio)
            {
                seleccionandoNegocio = true;
                NegocioSeleccionado negocioSeleccionado = new NegocioSeleccionado();
                negocioSeleccionado.Show();

                negocioSeleccionado.NegocioSeleccionadoOK += value => seleccionandoNegocio = value;

                negocioSeleccionado.NegocioSeleccionadoString += value => _negocioSelected = value;
                negocioSeleccionado.NegocioSeleccionadoInt += value => IDNegocioSeleccionado = value;
            }

        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        //Cambiar cantidad de objetos en Crear Flujo de Tarea
        private void flujoTareaSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            /*
            if(Pantalla_Crear_FlujoTarea.Visibility == Visibility.Visible)
            {
                int j = 0;
                foreach(TextBox textBox in subflujoTxtBox.Children)
                {
                    subflujotxt[j] = textBox.Text;
                    j++;
                }
                foreach (TextBox textBox in subflujoTxtBox2.Children)
                {
                    subflujotxt[j] = textBox.Text;
                    j++;
                }
                subflujoLabel.Children.Clear();
                subflujoTxtBox.Children.Clear();
                subflujoLabel2.Children.Clear();
                subflujoTxtBox2.Children.Clear();
                int totalSubFlujo = Int32.Parse(SliderSubFlujo.Value.ToString());

                for (int i = 0; i < totalSubFlujo; i++)
                {
                    //Crear Labels
                    Label label = new Label();
                    label.Name = "Paso" + i + 1;
                    label.Content = "Paso " + (i + 1);
                    label.Width = 150;
                    //label.FontFamily = "Inter";
                    label.FontSize = 15;
                    label.Margin = new Thickness(5);
                    label.HorizontalContentAlignment = HorizontalAlignment.Center;

                    if (i <= 5) subflujoLabel.Children.Add(label);
                    if (i > 5) subflujoLabel2.Children.Add(label);

                    //Crear txtbox
                    TextBox textBox = new TextBox();
                    textBox.FontSize = 15;
                    textBox.Name = "subflujo" + (i + 1);
                    textBox.Width = 150;
                    textBox.Margin = new Thickness(5);
                    textBox.Background = new SolidColorBrush(Colors.LightGray);
                    textBox.Foreground = new SolidColorBrush(Colors.White);
                    textBox.Text = subflujotxt[i];

                    if (i <= 5) subflujoTxtBox.Children.Add(textBox);
                    if (i > 5) subflujoTxtBox2.Children.Add(textBox);

                }
            }
            */
        }

        private void CargarCboxFlujo()
        {
            /*
            string negocio = btn_SeleccionarNegocio.Content.ToString();
            CConexion cConexion = new CConexion();
            cConexion.EstablecerConn();
            string[] grupotrabajo = cConexion.CargarComboboxNegocio(negocio);
            foreach (string grupostrabajo in grupotrabajo)
            {
                cboxGrupoTrabajoCrearFlujo.Items.Add(grupostrabajo);
            }
            cConexion.CerrarConn();
            */
        }

        private void flujo_btnConfirmar_Click(object sender, RoutedEventArgs e) //Crea el flujo con sus tareas correspondientes
        {



        }

        private void btn_flujotarea_addTarea_Click(object sender, RoutedEventArgs e)
        {
            //Border border = new Border();
            //border.Margin = new Thickness(10, 0, 10, 0);
            int altura = 31;
            int fontsize = 15;
            Thickness thick = new Thickness(0, 1, 0, 0);
            Thickness thick2 = new Thickness(0, 0, 0, 5);
            Thickness thick3 = new Thickness(0, 0, 0, 1);

            int cantTareasFlujo = StackFlujoLabelTarea.Children.Count;
            //combobox predecedora
            ComboBox comboBox = new ComboBox();
            comboBox.Items.Add("Ninguno");
            foreach (Label labelCbox in StackFlujoLabelTarea.Children)
            {
                comboBox.Items.Add(labelCbox.Content);
            }
            comboBox.FontSize = fontsize;
            comboBox.FontFamily = new FontFamily("Inter");
            comboBox.Width = 150;
            comboBox.Margin = thick2;
            comboBox.SelectedIndex = cantTareasFlujo;
            StackFlujoPredecedora.Children.Add(comboBox);
            //Nombre Tarea: 
            Label label = new Label();
            label.Height = altura;
            label.Content = "Tarea " + (cantTareasFlujo + 1) + ": ";
            label.FontSize = fontsize;
            label.Margin = thick;
            label.FontFamily = new FontFamily("Inter");
            StackFlujoLabelTarea.Children.Add(label);
            // txtbox
            TextBox textBox = new TextBox();
            textBox.Height = altura;
            textBox.FontSize = fontsize;
            textBox.FontFamily = new FontFamily("Inter");
            textBox.Width = 200;
            StackFlujotxtBox.Children.Add(textBox);
            // cantidad de dias
            textBox = new TextBox();
            textBox.Height = altura;
            textBox.FontSize = fontsize;
            textBox.FontFamily = new FontFamily("Inter");
            textBox.Text = "1";
            textBox.Width = 50;
            StackFlujoDias.Children.Add(textBox);
            //Dia(s)
            label = new Label();
            label.Height = altura;
            label.Content = "Dia(s)";
            label.FontSize = fontsize;
            label.Margin = thick;
            label.FontFamily = new FontFamily("Inter");
            StackFlujoDiasLabel.Children.Add(label);
            //Subtearea nueva
            CheckBox checkBox = new CheckBox();
            checkBox.Height = altura;
            checkBox.Content = "Subtarea    ";
            checkBox.Margin = thick3;
            checkBox.FontSize = fontsize;
            checkBox.FontFamily = new FontFamily("Inter");
            checkBox.Checked += new RoutedEventHandler(CheckboxUpdate);
            checkBox.Unchecked += new RoutedEventHandler(CheckboxUpdate);
            checkBox.IsEnabled = false; // La ultima tarea no puede ser subtarea, pq requiere de subtareas que la llenen
            StackFlujoNewSubtarea.Children.Add(checkBox);
            //Subtearea de Anterior
            checkBox = new CheckBox();
            checkBox.Height = altura;
            checkBox.Content = "Microtarea";
            checkBox.Margin = thick3;
            checkBox.FontSize = fontsize;
            checkBox.IsEnabled= false;
            checkBox.FontFamily = new FontFamily("Inter");
            checkBox.Checked += new RoutedEventHandler(CheckboxUpdate);
            checkBox.Unchecked += new RoutedEventHandler(CheckboxUpdate);
            StackFlujoSubtarea.Children.Add(checkBox);
            //Label Predecedora
            label = new Label();
            label.Height = altura;
            label.Content = "Predecesor: ";
            label.FontSize = fontsize;
            label.Margin = thick;
            label.FontFamily = new FontFamily("Inter");
            label.Visibility = Visibility.Visible;
            StackFlujoPredecedorLabel.Children.Add(label);
            //delete task
            /*
            Button button = new Button();
            button.Height = altura;
            button.Content = "X";
            button.FontSize = fontsize;            
            //button.Margin = new Thickness(10, 0, 10, 0);
            button.FontFamily = new FontFamily("Inter");
            button.Foreground = new SolidColorBrush(Colors.White);
            button.Background = new SolidColorBrush(Colors.Red);
            button.BorderBrush = new SolidColorBrush(Colors.Transparent);
            stackFlujoDeleteTask.Children.Add(button);
            */
            int j = 0;
            foreach(CheckBox box in StackFlujoNewSubtarea.Children)
            {
                j++;
                if(j == cantTareasFlujo)
                {
                    box.IsEnabled = true;
                }
            }
        }

        private void btn_flujotarea_deleteTarea_Click(object sender, RoutedEventArgs e)
        {
            int totalTareas = StackFlujoLabelTarea.Children.Count;
            totalTareas -= 1;
            if (totalTareas > 0)
            {
                //StackFlujoLabelTarea.Children.RemoveAt(totalTareas);
                StackFlujoLabelTarea.Children.RemoveAt(totalTareas);
                StackFlujotxtBox.Children.RemoveAt(totalTareas);
                StackFlujoDiasLabel.Children.RemoveAt(totalTareas);
                StackFlujoDias.Children.RemoveAt(totalTareas);
                StackFlujoPredecedora.Children.RemoveAt(totalTareas-1);
                StackFlujoPredecedorLabel.Children.RemoveAt(totalTareas-1);
                StackFlujoSubtarea.Children.RemoveAt(totalTareas - 1);
                StackFlujoNewSubtarea.Children.RemoveAt(totalTareas);

            }
        }


        private void CheckboxUpdate(object sender, RoutedEventArgs e)
        {
            int totalChild = StackFlujoLabelTarea.Children.Count;
            bool[] esNewSubTarea = new bool[totalChild];
            bool[] esSubTarea = new bool[totalChild];
            int i = -1;
            bool isPrevSubTarea = false;
            foreach (CheckBox checkBox in StackFlujoNewSubtarea.Children) //Revisa cuales son New Sub Tarea
            {
                i++;
                if (isPrevSubTarea)
                {
                    checkBox.IsChecked = false;
                    checkBox.IsEnabled = false;
                }
                if (!isPrevSubTarea && i != (totalChild -1))
                {
                    checkBox.IsEnabled = true;
                }

                if (checkBox.IsChecked == true)
                {
                    esNewSubTarea[i] = true;                    
                }
                else
                {
                    esNewSubTarea[i] = false;
                }
                if (checkBox.IsChecked == true)
                {
                    isPrevSubTarea = true;
                }
                else
                {
                    isPrevSubTarea = false;
                }
            }
            i = -1;
            foreach(CheckBox checkBox in StackFlujoSubtarea.Children) //Revisa cuales son subtarea
            {
                i++;
                if(checkBox.IsChecked == true)
                {
                    esSubTarea[i] = true;
                }
                else
                {
                    esSubTarea[i] = false;
                }
            }
            i = -1;
            foreach(TextBox tbox in StackFlujoDias.Children) //Cambia a 0 las new subtareas
            {
                i++;
                if (esNewSubTarea[i])
                {
                    tbox.Text = "0";
                    tbox.IsReadOnly = true;
                    tbox.Foreground = new SolidColorBrush(Colors.Gray);
                }
                else
                {                    
                    tbox.IsReadOnly = false;
                    tbox.Foreground = new SolidColorBrush(Colors.Black);
                }
            }
            i = 0;
            foreach(CheckBox checkBox1 in StackFlujoSubtarea.Children)
            {
                if (esNewSubTarea[i])
                {
                    checkBox1.IsChecked = true;
                    esSubTarea[i] = true;
                }
                if(i > 0)
                {
                    if (esSubTarea[i - 1])
                    {
                        //checkBox1.IsChecked = true;
                        checkBox1.IsEnabled = true;
                    }

                }
                i++;
            }
        }

        private void LimpiarCamposCrearFlujo()
        {
            int totalTareas = StackFlujoLabelTarea.Children.Count;
            for(int i = 0; i <= totalTareas+1; i++)
            {
                totalTareas = StackFlujoLabelTarea.Children.Count;
                totalTareas -= 1;
                if (totalTareas > 0)
                {
                    //StackFlujoLabelTarea.Children.RemoveAt(totalTareas);
                    StackFlujoLabelTarea.Children.RemoveAt(totalTareas);
                    StackFlujotxtBox.Children.RemoveAt(totalTareas);
                    StackFlujoDiasLabel.Children.RemoveAt(totalTareas);
                    StackFlujoDias.Children.RemoveAt(totalTareas);
                    StackFlujoPredecedora.Children.RemoveAt(totalTareas - 1);
                    StackFlujoPredecedorLabel.Children.RemoveAt(totalTareas - 1);
                    StackFlujoSubtarea.Children.RemoveAt(totalTareas - 1);
                    StackFlujoNewSubtarea.Children.RemoveAt(totalTareas);
                }
            }

            txtBox_NombreFlujo.Text = "";
            txtBox_DescFlujo.Document.Blocks.Clear();
            flujo_tarea1_name.Text = "";
        }

        private bool CheckCrearFlujoCamposCheck()
        {
            bool camposOK = true;
            if (txtBox_NombreFlujo.Text == "") camposOK = false;
            if(new TextRange(txtBox_DescFlujo.Document.ContentStart, txtBox_DescFlujo.Document.ContentEnd).Text == "") camposOK=false;
            foreach (TextBox textBox in StackFlujotxtBox.Children)
            {
                if(textBox.Text == "") camposOK=false;
            }
            foreach(TextBox textBox1 in StackFlujoDias.Children)
            {
                if(textBox1.Text == "") camposOK=false;
            }

            return camposOK;
        }

        private void btn_flujotarea_finalizar_Click(object sender, RoutedEventArgs e)
        {
            if (CheckCrearFlujoCamposCheck())
            {
                CConexion cConexion = new CConexion();
                cConexion.EstablecerConn();


                string[] datos_FlujoPL = { "", "", "" };
                datos_FlujoPL[0] = txtBox_NombreFlujo.Text;
                datos_FlujoPL[1] = new TextRange(txtBox_DescFlujo.Document.ContentStart, txtBox_DescFlujo.Document.ContentEnd).Text;
                datos_FlujoPL[2] = idNegocioSeleccionado;

                //cConexion.InsertFlujo_PL(datos_FlujoPL); // Query para insertar flujo pl

                string ID_flujoPL = cConexion.GetIDByName("flujo_pl", datos_FlujoPL[0]);

                //Crear Tareas en BD
                int cantTareas = StackFlujoLabelTarea.Children.Count;

                string[] nombreTarea = new string[cantTareas];
                string[] cantDias = new string[cantTareas];
                string[] newSubtarea = new string[cantTareas];
                string[] subTarea = new string[cantTareas];
                string[] predecedor = new string[cantTareas];

                string[] datosTarea = new string[8];

                int i = 0;
                foreach (TextBox textBox in StackFlujotxtBox.Children) // Nombre tarea
                {
                    nombreTarea[i] = textBox.Text;
                    i++;
                }
                i = 0;
                foreach(TextBox textBox in StackFlujoDias.Children) // cantidad dias
                {
                    cantDias[i] = textBox.Text;
                    i++;
                }
                i = 0;
                foreach (CheckBox checkBox in StackFlujoNewSubtarea.Children) // subtarea nueva
                {
                    string check;
                    if (checkBox.IsChecked == true)
                    {
                        check = "1";
                    }
                    else
                    {
                        check = "0";
                    }
                    newSubtarea[i] = check;
                    i++;
                }
                i = 0;
                subTarea[i] = "0"; //Se agrega antes porque no existe la opcion de "de subtarea" para la primera
                i++;
                foreach (CheckBox checkBox in StackFlujoSubtarea.Children) // subtarea nueva    error
                {
                    string check;
                    if (checkBox.IsChecked == true)
                    {
                        check = "1";
                    }
                    else
                    {
                        check = "0";
                    }
                    subTarea[i] = check;
                    i++;
                }

                i = 0;
                predecedor[i] = "0"; //Se agrega antes porque no existe la opcion de "de subtarea" para la primera
                i++;
                foreach (ComboBox cbox in StackFlujoPredecedora.Children) // predecedor *********SACAR BORDER
                {
                    predecedor[i] = cbox.SelectedIndex.ToString();
                    if(predecedor[i] == null) predecedor[i] = "0";
                    i++;
                }

                for(i = 0; i < cantTareas; i++) // realiza las querys correspondientes
                {
                    datosTarea[0] = i.ToString();
                    datosTarea[1] = nombreTarea[i];
                    datosTarea[2] = "No Hay";
                    datosTarea[3] = cantDias[i];
                    datosTarea[4] = predecedor[i];
                    datosTarea[5] = ID_flujoPL;
                    datosTarea[6] = newSubtarea[i];
                    datosTarea[7] = subTarea[i];

                    //cConexion.InsertTarea_PL(datosTarea);
                }
                MessageBox.Show("Flujo Creado Exitosamente");

                LimpiarCamposCrearFlujo();

            }
            else
            {
                MessageBox.Show("Debe llenar todos los campos");
            }

        }



        //Codigos Antiguos
        /*
         * 
         * 
         *                 i++;
                
                if (esNewSubTarea[i-1])
                {
                    checkBox1.IsChecked = true;
                    esSubTarea[i] = true;
                }
                if (esNewSubTarea[i])
                {
                    checkBox1.IsChecked = false;
                    esSubTarea[i] = false;
                }
                else
                {
                    checkBox1.IsEnabled = true;
                }                
                
                if (esSubTarea[i-1])
                {
                    checkBox1.IsEnabled = true;
                }
                else
                {
                    //checkBox1.IsEnabled= false;
                }


         * 
        private void AgregarFlujoKanban()
        {
            bool camposLlenos = true;

            foreach (TextBox textbox in subflujoTxtBox.Children)
            {
                if (textbox.Text == "") camposLlenos = false;
            }
            if (subflujoTxtBox2.Children.Count > 0)
            {
                foreach (TextBox textBox in subflujoTxtBox2.Children)
                {
                    if (textBox.Text == "") camposLlenos = false;
                }
            }
            if (txtBoxNombreFlujo.Text == "") camposLlenos = false;
            if (!camposLlenos)
            {
                MessageBox.Show("Debes ingresar todos los campos");

            }
            else //Ejecutar Query
            {
                CConexion cConexion = new CConexion();
                cConexion.EstablecerConn();
                int plantilla = 0;
                if ((bool)CheckBoxFlujoTarea.IsChecked == true) plantilla = 1;

                int orden = cConexion.GetFlujoTareaCount(idNegocioSeleccionado) + 1; //obtiene cantidad de flujos en negocio para deifinir ultimo en la lista en el orden
                cConexion.InsertFlujoTarea(txtBoxNombreFlujo.Text, orden, plantilla); //inserta flujo de negocio

                //obtiene ID de tarea recien creada
                //Obtener cantidad de subflujos
                int totalSubflujos = subflujoTxtBox.Children.Count + subflujoTxtBox2.Children.Count;
                int i = 0;
                foreach (TextBox txtBox in subflujoTxtBox.Children)
                {
                    cConexion.InsertSubFlujo(txtBox.Text, i);
                }
                if (totalSubflujos > 5)
                {
                    //foreach
                }

            }
        }
        */
    }
}
