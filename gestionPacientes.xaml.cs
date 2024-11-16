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
    /// Lógica de interacción para gestionPacientes.xaml
    /// </summary>
    public partial class gestionPacientes : Window
    {
        public gestionPacientes()
        {
            //SELECCION DE GENERO
            InitializeComponent();
            listBoxGenero.Items.Add("Selecionar un Genero");
            listBoxGenero.Items.Add("Maculino");
            listBoxGenero.Items.Add("Femenino");
            listBoxGenero.SelectedIndex = 0;
            //SELECCION DE TIPO DE SANGRE
            listBoxTipoSangre.Items.Add("Selecione el tipo de sangre");
            listBoxTipoSangre.Items.Add("O+RH");
            listBoxTipoSangre.Items.Add("A+RH");
            listBoxTipoSangre.Items.Add("B+RH");
            listBoxTipoSangre.Items.Add("AB+RH");
            listBoxTipoSangre.Items.Add("O-RH");
            listBoxTipoSangre.Items.Add("A-RH");
            listBoxTipoSangre.Items.Add("B-RH");
            listBoxTipoSangre.Items.Add("AB-RH");
            listBoxTipoSangre.SelectedIndex = 0;
            //SELECCION DE ENFERMEDADES
            listBoxEnfermedades.Items.Add("Seleccione una enfermedad");
            listBoxEnfermedades.Items.Add("Hipertensión");
            listBoxEnfermedades.Items.Add("Diabetes");
            listBoxEnfermedades.Items.Add("Enfermedad Renal");
            listBoxEnfermedades.Items.Add("Cancer");
            listBoxEnfermedades.Items.Add("Ninguna");
            listBoxEnfermedades.SelectedIndex = 0;
            //SELECCIONE ALERGIAS
            listBoxAlergias.Items.Add("Selecione una Alergia");
            listBoxAlergias.Items.Add("Alergia a Alimentos");
            listBoxAlergias.Items.Add("Alergia a Animales");
            listBoxAlergias.Items.Add("Alergia a Polen");
            listBoxAlergias.Items.Add("Alergia a Fregancias");
            listBoxAlergias.Items.Add("Alergia a Medicamentos");
            listBoxAlergias.Items.Add("Ninguna");
            listBoxAlergias.SelectedIndex = 0;
            //SELECIONE SI O NO CIRUGIAS
            listBoxCirugiasPasadas.Items.Add("Selecione");
            listBoxCirugiasPasadas.Items.Add("SI");
            listBoxCirugiasPasadas.Items.Add("NO");
            listBoxCirugiasPasadas.SelectedIndex = 0;
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            string connectionString = "Server=LAPTOP-IUMP3CQR\\SQLEXPRESS;Database=HospiPlusDB;Integrated Security=True; TrustServerCertificate=True";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Pacientes (PacienteID, Nombre, Apellido, Edad, FechaNacimiento, Genero, Telefono, Direccion, TipodeSangre, Enfermedades, Alergias, CirugiasPasadas, HistorialMedico, FechaUltimaVisita, MedicoAsignado, MedicamentoActual, Email) " +
                               "VALUES (@PacienteID, @Nombre, @Apellido, @Edad, @FechaNacimiento, @Genero, @Telefono, @Direccion, @TipodeSangre, @Enfermedades, @Alergias, @CirugiasPasadas, @HistorialMedico, @FechaUltimaVisita, @MedicoAsignado, @MedicamentoActual, @Email)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@PacienteID", txtID.Text);
                cmd.Parameters.AddWithValue("@Nombre", txtNombre.Text);
                cmd.Parameters.AddWithValue("@Apellido", txtApellido.Text);
                cmd.Parameters.AddWithValue("@Edad", txtEdad.Text);
                cmd.Parameters.AddWithValue("@FechaNacimiento", dtpFechaNacimiento.SelectedDate ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Genero", listBoxGenero.SelectedItem?.ToString() ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Telefono", txtTelefono.Text);
                cmd.Parameters.AddWithValue("@Direccion", txtDireccion2.Text);
                cmd.Parameters.AddWithValue("@TipodeSangre", listBoxTipoSangre.SelectedItem?.ToString() ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Enfermedades", listBoxEnfermedades.SelectedItem?.ToString() ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Alergias", listBoxAlergias.SelectedItem?.ToString() ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@CirugiasPasadas", listBoxCirugiasPasadas.SelectedItem?.ToString() ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@HistorialMedico", txtHistorialMedico.Text);
                cmd.Parameters.AddWithValue("@FechaUltimaVisita", dtpFechaUltimaVisita.SelectedDate ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@MedicoAsignado", txtMedicoAsignado.Text);
                cmd.Parameters.AddWithValue("@MedicamentoActual", txtMedicamentoActual.Text);
                cmd.Parameters.AddWithValue("@Email", txtEmail.Text);

                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    MessageBox.Show("Formulario guardado con éxito.");
                    LimpiarCampos();
                }
                else
                {
                    MessageBox.Show("No se pudo guardar el formulario. Por favor, revisa los datos.");
                }
            }


        }

        private void LimpiarCampos()
        {
            txtID.Clear();
            txtNombre.Clear();
            txtApellido.Clear();
            txtEdad.Clear();
            txtTelefono.Clear();
            txtDireccion2.Clear();
            txtHistorialMedico.Clear();
            txtMedicoAsignado.Clear();
            txtMedicamentoActual.Clear();
            txtEmail.Clear();
            

            // Para ComboBox y ListBox, debes establecer SelectedIndex en -1 para deseleccionar
            listBoxGenero.SelectedIndex = -1;
            listBoxTipoSangre.SelectedIndex = -1;
            listBoxEnfermedades.SelectedIndex = -1;
            listBoxAlergias.SelectedIndex = -1;
            listBoxCirugiasPasadas.SelectedIndex = -1;

            dtpFechaNacimiento.SelectedDate = null;
            dtpFechaUltimaVisita.SelectedDate = null;
        }

        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            {
                string pacienteID = txtBusquedaID.Text; // Asume que el ID del paciente se ingresa en un TextBox.

                if (string.IsNullOrWhiteSpace(pacienteID))
                {
                    MessageBox.Show("Por favor ingrese un ID de paciente.");
                    return;
                }

                string connectionString = "Server=LAPTOP-IUMP3CQR\\SQLEXPRESS;Database=HospiPlusDB;Integrated Security=True; TrustServerCertificate=True";
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM Pacientes WHERE PacienteID = @PacienteID";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@PacienteID", pacienteID);

                    try
                    {
                        conn.Open();
                        SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            reader.Read(); // Leer la primera fila de datos.

                            // Asignar los datos al formulario.
                            txtNombre.Text = reader["Nombre"].ToString();
                            txtApellido.Text = reader["Apellido"].ToString();
                            txtEdad.Text = reader["Edad"].ToString();
                            dtpFechaNacimiento.SelectedDate = reader["FechaNacimiento"] as DateTime?;
                            listBoxGenero.SelectedItem = reader["Genero"].ToString();
                            txtTelefono.Text = reader["Telefono"].ToString();
                            txtDireccion2.Text = reader["Direccion"].ToString();
                            listBoxTipoSangre.SelectedItem = reader["TipodeSangre"].ToString();
                            listBoxEnfermedades.SelectedItem = reader["Enfermedades"].ToString();
                            listBoxAlergias.SelectedItem = reader["Alergias"].ToString();
                            listBoxCirugiasPasadas.SelectedItem = reader["CirugiasPasadas"].ToString();
                            txtHistorialMedico.Text = reader["HistorialMedico"].ToString();
                            dtpFechaUltimaVisita.SelectedDate = reader["FechaUltimaVisita"] as DateTime?;
                            txtMedicoAsignado.Text = reader["MedicoAsignado"].ToString();
                            txtMedicamentoActual.Text = reader["MedicamentoActual"].ToString();
                            txtEmail.Text = reader["Email"].ToString();

                            MessageBox.Show("Paciente encontrado.");
                        }
                        else
                        {
                            MessageBox.Show("No se encontró un paciente con ese ID.");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al buscar el paciente: " + ex.Message);
                    }
                }
            }

        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            string pacienteID = txtBusquedaID.Text; // Asume que el ID del paciente se ingresa en un TextBox.

            if (string.IsNullOrWhiteSpace(pacienteID))
            {
                MessageBox.Show("Por favor ingrese un ID de paciente válido.");
                return;
            }

            string connectionString = "Server=LAPTOP-IUMP3CQR\\SQLEXPRESS;Database=HospiPlusDB;Integrated Security=True; TrustServerCertificate=True";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM Pacientes WHERE PacienteID = @PacienteID";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@PacienteID", pacienteID);

                try
                {
                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Paciente eliminado con éxito.");
                        LimpiarCampos();
                    }
                    else
                    {
                        MessageBox.Show("No se encontró un paciente con ese ID.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al eliminar el paciente: " + ex.Message);
                }
            }
        }

        private void btnEditar_Click(object sender, RoutedEventArgs e)
        {
            string pacienteID = txtBusquedaID.Text; // Asume que el ID del paciente se ingresa en un TextBox.

            if (string.IsNullOrWhiteSpace(pacienteID))
            {
                MessageBox.Show("Por favor ingrese un ID de paciente válido.");
                return;
            }

            string connectionString = "Server=LAPTOP-IUMP3CQR\\SQLEXPRESS;Database=HospiPlusDB;Integrated Security=True; TrustServerCertificate=True";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "UPDATE Pacientes SET " +
                               "Nombre = @Nombre, Apellido = @Apellido, Edad = @Edad, FechaNacimiento = @FechaNacimiento, " +
                               "Genero = @Genero, Telefono = @Telefono, Direccion = @Direccion, TipodeSangre = @TipodeSangre, " +
                               "Enfermedades = @Enfermedades, Alergias = @Alergias, CirugiasPasadas = @CirugiasPasadas, " +
                               "HistorialMedico = @HistorialMedico, FechaUltimaVisita = @FechaUltimaVisita, " +
                               "MedicoAsignado = @MedicoAsignado, MedicamentoActual = @MedicamentoActual, Email = @Email " +
                               "WHERE PacienteID = @PacienteID";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@PacienteID", pacienteID);
                cmd.Parameters.AddWithValue("@Nombre", txtNombre.Text);
                cmd.Parameters.AddWithValue("@Apellido", txtApellido.Text);
                cmd.Parameters.AddWithValue("@Edad", txtEdad.Text);
                cmd.Parameters.AddWithValue("@FechaNacimiento", dtpFechaNacimiento.SelectedDate ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Genero", listBoxGenero.SelectedItem?.ToString() ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Telefono", txtTelefono.Text);
                cmd.Parameters.AddWithValue("@Direccion", txtDireccion2.Text);
                cmd.Parameters.AddWithValue("@TipodeSangre", listBoxTipoSangre.SelectedItem?.ToString() ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Enfermedades", listBoxEnfermedades.SelectedItem?.ToString() ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Alergias", listBoxAlergias.SelectedItem?.ToString() ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@CirugiasPasadas", listBoxCirugiasPasadas.SelectedItem?.ToString() ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@HistorialMedico", txtHistorialMedico.Text);
                cmd.Parameters.AddWithValue("@FechaUltimaVisita", dtpFechaUltimaVisita.SelectedDate ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@MedicoAsignado", txtMedicoAsignado.Text);
                cmd.Parameters.AddWithValue("@MedicamentoActual", txtMedicamentoActual.Text);
                cmd.Parameters.AddWithValue("@Email", txtEmail.Text);

                try
                {
                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Datos del paciente actualizados con éxito.");
                        LimpiarCampos();
                    }
                    else
                    {
                        MessageBox.Show("No se encontró un paciente con ese ID.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al editar los datos del paciente: " + ex.Message);
                }
            }
        }

        private void btnVolverMenu_Click(object sender, RoutedEventArgs e)
        {
            menuPrincipal menuPrincipal = new menuPrincipal();
            menuPrincipal.Show();
            this.Close();
        }
    }

}
