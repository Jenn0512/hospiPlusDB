using Microsoft.Data.SqlClient;
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

namespace hospiPlus
{
    /// <summary>
    /// Lógica de interacción para gestionConsultas.xaml
    /// </summary>
    public partial class gestionConsultas : Window
    {
        public gestionConsultas()
        {
            InitializeComponent();
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e, DatePicker datePicker)

        {
            string connectionString = "Server=LAPTOP-IUMP3CQR\\SQLEXPRESS;Database=HospiPlusDB;Integrated Security=True; TrustServerCertificate=True";

            try
            {
                // Obtener valores del formulario
                string nombre = txtNombre.Text;
                string apellido = txtApellido.Text;
                int edad = int.TryParse(txtEdad.Text, out int parsedEdad) ? parsedEdad : 0;
                string direccion = txtDireccion.Text;
                string genero = cmbGenero.Text;
                string medico = cmbMedico.Text;
                string especialidad = txtEspecialidad.Text;
                string motivoConsulta = txtMotivo.Text;
                string diagnostico = txtDiagnostico.Text;
                decimal peso = decimal.TryParse(txtPeso.Text, out decimal parsedPeso) ? parsedPeso : 0;
                decimal altura = decimal.TryParse(txtAltura.Text, out decimal parsedAltura) ? parsedAltura : 0;
                string presion = txtPresion.Text;
                string hora = cmbHora.Text;
                DateTime? fecha = datePicker.SelectedDate;

                if (string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(apellido) || fecha == null)
                {
                    MessageBox.Show("Por favor, complete los campos obligatorios (Nombre, Apellido, Fecha).", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Conexión a la base de datos e inserción
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string query = @"
                        INSERT INTO ConsultasMedicas 
                        (Nombre, Apellido, Edad, Direccion, Genero, Medico, Especialidad, FechaConsulta, HoraConsulta, 
                         MotivoConsulta, Diagnostico, Peso, Altura, PresionArterial)
                        VALUES 
                        (@Nombre, @Apellido, @Edad, @Direccion, @Genero, @Medico, @Especialidad, @FechaConsulta, @HoraConsulta, 
                         @MotivoConsulta, @Diagnostico, @Peso, @Altura, @PresionArterial)";

                    using (SqlCommand command = new SqlCommand(query, conn))
                    {
                        // Asignar parámetros
                        command.Parameters.AddWithValue("@Nombre", nombre);
                        command.Parameters.AddWithValue("@Apellido", apellido);
                        command.Parameters.AddWithValue("@Edad", edad);
                        command.Parameters.AddWithValue("@Direccion", direccion);
                        command.Parameters.AddWithValue("@Genero", genero);
                        command.Parameters.AddWithValue("@Medico", medico);
                        command.Parameters.AddWithValue("@Especialidad", especialidad);
                        command.Parameters.AddWithValue("@FechaConsulta", fecha.Value);
                        command.Parameters.AddWithValue("@HoraConsulta", hora);
                        command.Parameters.AddWithValue("@MotivoConsulta", motivoConsulta);
                        command.Parameters.AddWithValue("@Diagnostico", diagnostico);
                        command.Parameters.AddWithValue("@Peso", peso);
                        command.Parameters.AddWithValue("@Altura", altura);
                        command.Parameters.AddWithValue("@PresionArterial", presion);

                        // Ejecutar el comando
                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Consulta médica guardada exitosamente.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        else
                        {
                            MessageBox.Show("No se pudo guardar la consulta médica.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error al guardar los datos: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e, DatePicker datePicker)
        {

            // Limpiar campos
            txtNombre.Clear();
            txtApellido.Clear();
            txtEdad.Clear();
            txtDireccion.Clear();
            cmbGenero.SelectedIndex = -1;
            cmbMedico.SelectedIndex = -1;
            txtEspecialidad.Clear();
            txtMotivo.Clear();
            txtDiagnostico.Clear();
            txtPeso.Clear();
            txtAltura.Clear();
            txtPresion.Clear();
            cmbHora.SelectedIndex = -1;
            datePicker.SelectedDate = null;

            MessageBox.Show("Formulario cancelado y datos limpiados.", "Cancelar", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
