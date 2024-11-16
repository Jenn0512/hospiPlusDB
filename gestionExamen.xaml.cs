using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
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
    /// Lógica de interacción para gestionExamen.xaml
    /// </summary>
    public partial class gestionExamen : Window
    {
        public gestionExamen()
        {
            InitializeComponent();
        }

        private void InitializeComboBoxes()
        {
            // Opciones iniciales para los ComboBoxes
            cmbGenero.Items.Add("Masculino");
            cmbGenero.Items.Add("Femenino");
            cmbGenero.Items.Add("Otro");

            cmbTipoExamen.Items.Add("Análisis de Sangre");
            cmbTipoExamen.Items.Add("Rayos X");
            cmbTipoExamen.Items.Add("Resonancia Magnética");

            cmbMedico.Items.Add("Dr. Juan Pérez");
            cmbMedico.Items.Add("Dra. María López");
        }



        private void btnGuardar_Click(object sender, RoutedEventArgs e, DateTime? fechaExamen)
        {
            // Captura de datos desde la interfaz
            string nombre = txtNombre.Text;
            string apellido = txtApellido.Text;
            string direccion = txtDireccion.Text;
            string genero = cmbGenero.SelectedItem?.ToString();
            string tipoExamen = cmbTipoExamen.SelectedItem?.ToString();
            string descripcion = txtDescripcion.Text;
            string medico = cmbMedico.SelectedItem?.ToString();
            if (string.IsNullOrWhiteSpace(nombre) || fechaExamen == null)
            {
                MessageBox.Show("Por favor complete los campos obligatorios.", "Error");
                return;
            }

            // Guardar en la base de datos
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"
                        INSERT INTO Examenes (PacienteID, ConsultaID, TipoExamen, FechaExamen, Resultado)
                        VALUES (@PacienteID, NULL, @TipoExamen, @FechaExamen, @Resultado)";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        // Puedes reemplazar @PacienteID con un valor dinámico
                        cmd.Parameters.AddWithValue("@PacienteID", 1);
                        cmd.Parameters.AddWithValue("@TipoExamen", tipoExamen ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@FechaExamen", fechaExamen);
                        cmd.Parameters.AddWithValue("@Resultado", descripcion ?? (object)DBNull.Value);

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Examen guardado exitosamente.", "Éxito");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar el examen: {ex.Message}", "Error");
            }
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e, DatePicker datePicker)
        {
            // Restablecer todos los campos
            txtNombre.Clear();
            txtApellido.Clear();
            txtDireccion.Clear();
            txtDescripcion.Clear();
            txtBuscar.Clear();
            cmbGenero.SelectedIndex = -1;
            cmbTipoExamen.SelectedIndex = -1;
            cmbMedico.SelectedIndex = -1;
            datePicker.SelectedDate = null;
            MessageBox.Show("Formulario reiniciado.", "Cancelar");
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            // Lógica para eliminar un registro
            MessageBox.Show("Funcionalidad de eliminar pendiente de implementar.", "Eliminar");
        }
        

        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            // Buscar en la base de datos
            string criterioBusqueda = txtBuscar.Text;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"
                        SELECT * FROM Examenes
                        WHERE TipoExamen LIKE @Criterio OR Resultado LIKE @Criterio";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Criterio", $"%{criterioBusqueda}%");
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                string resultados = "Resultados encontrados:\n\n";
                                while (reader.Read())
                                {
                                    resultados += $"ExamenID: {reader["ExamenID"]}, Tipo: {reader["TipoExamen"]}, Resultado: {reader["Resultado"]}\n";
                                }
                                MessageBox.Show(resultados, "Búsqueda");
                            }
                            else
                            {
                                MessageBox.Show("No se encontraron resultados.", "Búsqueda");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al buscar: {ex.Message}", "Error");
            }
        }
    
    }
}
