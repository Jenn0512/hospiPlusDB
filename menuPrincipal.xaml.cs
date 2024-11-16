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
    /// Lógica de interacción para menuPrincipal.xaml
    /// </summary>
    public partial class menuPrincipal : Window
    {
        public menuPrincipal()
        {
            InitializeComponent();
        }

        private void btnPaciente_Click(object sender, RoutedEventArgs e)
        {
            gestionPacientes gestionP = new gestionPacientes();
            gestionP.Show();
            this.Close();
        }

        private void btnMedico_Click(object sender, RoutedEventArgs e)
        {
            gestionMedicos gestionMedicos = new gestionMedicos();
            gestionMedicos.Show();
            this.Close();
           
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            gestiondeCitas gestiondeCitas = new gestiondeCitas();
            gestiondeCitas.Show();
            this.Close();
        }
    }
}
