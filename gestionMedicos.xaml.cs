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
    /// Lógica de interacción para gestionMedicos.xaml
    /// </summary>
    public partial class gestionMedicos : Window
    {
        public gestionMedicos()
        {
            InitializeComponent();
        }

        private void btnVolverInicio_Click(object sender, RoutedEventArgs e)
        {
            menuPrincipal menuPrincipal = new menuPrincipal();
            menuPrincipal.Show();
            this.Close();
        }



        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {

            string connectionString = "Server=LAPTOP-IUMP3CQR\\SQLEXPRESS;Database=HospiPlusDB;Integrated Security=True; TrustServerCertificate=True";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "INSERT INTO Medicos (Nombre, Apellido, Especialidad, Telefono, Email) " +
                                   "VALUES (@Nombre, @Apellido, @Especialidad, @Telefono, @Email)";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Nombre", txtNombre.Text);
                        cmd.Parameters.AddWithValue("@Apellido", txtApellido.Text);
                        cmd.Parameters.AddWithValue("@Especialidad", cmbEspecialidad.Text);
                        cmd.Parameters.AddWithValue("@Telefono", txtTelefono.Text);
                        cmd.Parameters.AddWithValue("@Email", txtEmail.Text);

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Médico guardado exitosamente.", "Guardado", MessageBoxButton.OK, MessageBoxImage.Information);
                            LimpiarCampos();
                        }
                        else
                        {
                            MessageBox.Show("No se pudo guardar el médico.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al guardar médico: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void LimpiarCampos()
        {
            txtMedicoID.Clear();
            txtNombre.Clear();
            txtApellido.Clear();
            cmbEspecialidad.SelectedIndex = -1;
            txtTelefono.Clear();
            txtEmail.Clear();
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {

            string connectionString = "Server=LAPTOP-IUMP3CQR\\SQLEXPRESS;Database=HospiPlusDB;Integrated Security=True; TrustServerCertificate=True";

            if (!int.TryParse(txtMedicoID.Text, out int medicoID))
            {
                MessageBox.Show("Ingrese un ID válido para eliminar.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "DELETE FROM Medicos WHERE MedicoID = @MedicoID";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MedicoID", medicoID);

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Médico eliminado exitosamente.", "Eliminado", MessageBoxButton.OK, MessageBoxImage.Information);
                            LimpiarCampos();
                        }
                        else
                        {
                            MessageBox.Show("No se pudo eliminar el médico.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al eliminar médico: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void btnEditar_Click(object sender, RoutedEventArgs e)
        {
            string connectionString = "Server=LAPTOP-IUMP3CQR\\SQLEXPRESS;Database=HospiPlusDB;Integrated Security=True; TrustServerCertificate=True";

            if (!int.TryParse(txtMedicoID.Text, out int medicoID))
            {
                MessageBox.Show("Ingrese un ID válido para editar.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "UPDATE Medicos " +
                                   "SET Nombre = @Nombre, Apellido = @Apellido, Especialidad = @Especialidad, Telefono = @Telefono, Email = @Email " +
                                   "WHERE MedicoID = @MedicoID";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MedicoID", medicoID);
                        cmd.Parameters.AddWithValue("@Nombre", txtNombre.Text);
                        cmd.Parameters.AddWithValue("@Apellido", txtApellido.Text);
                        cmd.Parameters.AddWithValue("@Especialidad", cmbEspecialidad.Text);
                        cmd.Parameters.AddWithValue("@Telefono", txtTelefono.Text);
                        cmd.Parameters.AddWithValue("@Email", txtEmail.Text);

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Médico actualizado exitosamente.", "Actualizado", MessageBoxButton.OK, MessageBoxImage.Information);
                            LimpiarCampos();
                        }
                        else
                        {
                            MessageBox.Show("No se pudo actualizar el médico.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al actualizar médico: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void btnBuscarMedicoID_Click(object sender, RoutedEventArgs e)
        {
            string connectionString = "Server=LAPTOP-IUMP3CQR\\SQLEXPRESS;Database=HospiPlusDB;Integrated Security=True; TrustServerCertificate=True";

            if (!int.TryParse(txtBuscarMedico.Text, out int medicoID))
            {
                MessageBox.Show("Ingrese un ID válido para buscar.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT * FROM Medicos WHERE MedicoID = @MedicoID";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@MedicoID", medicoID);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Llenar los campos con los datos obtenidos
                                txtMedicoID.Text = reader["MedicoID"].ToString();
                                txtNombre.Text = reader["Nombre"].ToString();
                                txtApellido.Text = reader["Apellido"].ToString();
                                cmbEspecialidad.Text = reader["Especialidad"].ToString();
                                txtTelefono.Text = reader["Telefono"].ToString();
                                txtEmail.Text = reader["Email"].ToString();
                            }
                            else
                            {
                                MessageBox.Show("Médico no encontrado.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al buscar médico: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
