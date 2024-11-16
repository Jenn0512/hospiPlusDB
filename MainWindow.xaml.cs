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
using Microsoft.Data.SqlClient;

namespace hospiPlus
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

        private void btnIniciodeSesion_Click(object sender, RoutedEventArgs e)
        {
            string usuario = txtUsuario.Text;
            string contraseña = txtContrasena.Password;

            if (ValidarCredenciales(usuario, contraseña, "SELECT COUNT(1) FROM Usuarios WHERE Username= @usuario AND Username, PasswordHash = @pass"))
            {
                MessageBox.Show("Bienvenido al sistema", "Inicio de sesión",
                    MessageBoxButton.OK, MessageBoxImage.Information);

                // Abrir la ventana de menú principal
                menuPrincipal menuWindow = new menuPrincipal();
                menuWindow.Show();

                // Cerrar la ventana de inicio de sesión actual
                this.Close();
            }
            else
            {
                MessageBox.Show("Credenciales no válidas", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        //KEVIN DANILO AMAYA GUILLEN
        private bool ValidarCredenciales(string usuario, string contrasena, string consulta)
        {
            string conexionString = "Server=JENNIFER-PEREZ\\SQLEXPRESS;Database=HospiPlusDB;integrated security=True;TrustServerCertificate=True";
            bool credencialesValidas = false;

            using (SqlConnection conexion = new SqlConnection(conexionString))
            {
                try
                {
                    conexion.Open();
                    using (SqlCommand comando = new SqlCommand(consulta, conexion))
                    {
                        comando.Parameters.AddWithValue("@usuario", usuario);
                        comando.Parameters.AddWithValue("@pass", contrasena);

                        
                        int count = Convert.ToInt32(comando.ExecuteScalar());
                        credencialesValidas = (count == 1);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error de conexión: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

            return credencialesValidas;
        }
    }
}


