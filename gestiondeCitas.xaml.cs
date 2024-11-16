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
    /// Lógica de interacción para gestiondeCitas.xaml
    /// </summary>
    public partial class gestiondeCitas : Window
    {
        public gestiondeCitas()
        {
            InitializeComponent();
            cmbMedico.Items.Add("Seleccione un Medico");
            cmbMedico.Items.Add("Luis Garcia");
            cmbMedico.Items.Add("Maria Torres");
            cmbMedico.Items.Add("Carlos Hernandez");
            cmbMedico.Items.Add("Lucia Ramirez");
            cmbMedico.Items.Add("Jose Martinez");
            cmbMedico.Items.Add("Elena Lopez");
            cmbMedico.Items.Add("Andres Gonzales");
            cmbMedico.Items.Add("Sofia Castro");
            cmbMedico.Items.Add("Manuel Perez");
            cmbMedico.Items.Add("Adriana Morales");


            cmbHora.Items.Add("Seleccione una Hora");
            cmbHora.Items.Add("08:00 AM");
            cmbHora.Items.Add("08:30 AM");
            cmbHora.Items.Add("09:00 AM");
            cmbHora.Items.Add("09:30 AM");
            cmbHora.Items.Add("10:00 AM");
            cmbHora.Items.Add("10:30 AM");
            cmbHora.Items.Add("11:00 AM");
            cmbHora.Items.Add("11:30 AM");
            cmbHora.Items.Add("12:00 PM");
            cmbHora.Items.Add("12:30 PM");
            cmbHora.Items.Add("01:00 PM");
            cmbHora.Items.Add("01:30 PM");
            cmbHora.Items.Add("02:00 PM");
            cmbHora.Items.Add("02:30 PM");
            cmbHora.Items.Add("03:00 PM");
            cmbHora.Items.Add("03:30 PM");
            cmbHora.Items.Add("04:00 PM");
            cmbHora.Items.Add("04:30 PM");
            cmbHora.Items.Add("05:00 PM");
            cmbHora.Items.Add("05:30 PM");


        }



        

        // Método para limpiar los campos después de programar la cita
        private void LimpiarCamposPaciente()
        {
            txtPacienteID.Clear();
            cmbMedico.SelectedIndex = -1;
            dtpFechaCita.SelectedDate = null;
        }

        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(txtPacienteID1.Text.Trim(), out int PacienteID))
            {
                MessageBox.Show("Ingrese un ID de paciente válido.");
                return;
            }

            string connectionString = "Server=LAPTOP-IUMP3CQR\\SQLEXPRESS;Database=HospiPlusDB;Integrated Security=True; TrustServerCertificate=True";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT Nombre, Apellido, Direccion FROM Pacientes WHERE PacienteID = @PacienteID";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@PacienteID", PacienteID);

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        txtPacienteNombre.Text = reader["Nombre"].ToString();
                        txtPacienteApellido.Text = reader["Apellido"].ToString();
                        txtDireccion.Text = reader["Direccion"].ToString();
                    }
                    else
                    {
                        MessageBox.Show("No se encontró un paciente con este ID.");
                        LimpiarCamposPaciente();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al buscar el paciente: " + ex.Message);
            }
        }

        private void btnProgramarCita_Click(object sender, RoutedEventArgs e)
        {
            int PacienteID, medicoID;
            DateTime fechaCita;
            string estadoCita = "Programada";

            if (!int.TryParse(txtPacienteID.Text, out PacienteID))
            {
                MessageBox.Show("Por favor, ingrese un ID de paciente válido.");
                return;
            }

            if (!int.TryParse(cmbMedico.SelectedValue.ToString(), out medicoID))
            {
                MessageBox.Show("Seleccione un médico válido.");
                return;
            }

            if (!DateTime.TryParse(dtpFechaCita.Text, out fechaCita))
            {
                MessageBox.Show("Ingrese una fecha válida.");
                return;
            }

            string connectionString = "Server=LAPTOP-IUMP3CQR\\SQLEXPRESS;Database=HospiPlusDB;Integrated Security=True; TrustServerCertificate=True";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // Validar límite de citas
                    string consultaLimite = @"SELECT COUNT(*) 
                                      FROM Citas 
                                      WHERE MedicoID = @MedicoID AND CAST(FechaCita AS DATE) = @FechaCita";
                    SqlCommand cmdLimite = new SqlCommand(consultaLimite, conn);
                    cmdLimite.Parameters.AddWithValue("@MedicoID", medicoID);
                    cmdLimite.Parameters.AddWithValue("@FechaCita", fechaCita.Date);

                    int citasDelDia = (int)cmdLimite.ExecuteScalar();
                    const int LIMITE_CITAS = 10;

                    if (citasDelDia >= LIMITE_CITAS)
                    {
                        MessageBox.Show("El médico ha alcanzado el límite de citas para esta fecha.");
                        return;
                    }

                    // Guardar la cita
                    string insertarCita = @"INSERT INTO Citas (PacienteID, MedicoID, FechaCita, Estado) 
                                    VALUES (@PacienteID, @MedicoID, @FechaCita, @Estado)";
                    SqlCommand cmdInsertar = new SqlCommand(insertarCita, conn);
                    cmdInsertar.Parameters.AddWithValue("@PacienteID", PacienteID);
                    cmdInsertar.Parameters.AddWithValue("@MedicoID", medicoID);
                    cmdInsertar.Parameters.AddWithValue("@FechaCita", fechaCita);
                    cmdInsertar.Parameters.AddWithValue("@Estado", estadoCita);

                    int filasAfectadas = cmdInsertar.ExecuteNonQuery();
                    if (filasAfectadas > 0)
                    {
                        MessageBox.Show("Cita programada con éxito.");
                        LimpiarCampos();
                    }
                    else
                    {
                        MessageBox.Show("No se pudo programar la cita.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar la cita: " + ex.Message);
            }

        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {

            LimpiarCampos();
        }

        private void LimpiarCampos()
        {
            txtPacienteID.Clear();
            txtPacienteNombre.Clear();
            txtPacienteApellido.Clear();
            txtDireccion.Clear();
            dtpFechaCita.SelectedDate = null;
            cmbMedico.SelectedIndex = -1;
            txtMotivo.Clear();

        }

        private void btnDisponibilidad_Click(object sender, RoutedEventArgs e)
        {
            int medicoID;
            DateTime fechaCita;

            if (!int.TryParse(cmbMedico.SelectedValue.ToString(), out medicoID))
            {
                MessageBox.Show("Seleccione un médico válido.");
                return;
            }

            if (!DateTime.TryParse(dtpFechaCita.Text, out fechaCita))
            {
                MessageBox.Show("Seleccione una fecha válida.");
                return;
            }

            string connectionString = "Server=LAPTOP-IUMP3CQR\\SQLEXPRESS;Database=HospiPlusDB;Integrated Security=True; TrustServerCertificate=True";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string consultaDisponibilidad = @"SELECT COUNT(*) 
                                              FROM Citas 
                                              WHERE MedicoID = @MedicoID AND CAST(FechaCita AS DATE) = @FechaCita";
                    SqlCommand cmd = new SqlCommand(consultaDisponibilidad, conn);
                    cmd.Parameters.AddWithValue("@MedicoID", medicoID);
                    cmd.Parameters.AddWithValue("@FechaCita", fechaCita.Date);

                    int citasDelDia = (int)cmd.ExecuteScalar();
                    MessageBox.Show($"El médico seleccionado tiene {citasDelDia} citas programadas para esta fecha.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al consultar la disponibilidad: " + ex.Message);
            }

        }

        



        
    }
}
