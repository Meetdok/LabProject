using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
using LabProject.Model;
using LabProject.Код;
using MySql.Data.MySqlClient;

namespace LabProject.Окна
{
    /// <summary>
    /// Логика взаимодействия для Rezerv.xaml
    /// </summary>
    public partial class Rezerv : Window
    {
        public Rezerv()
        {
            InitializeComponent();
            DataContext = new RezervVM();
        }

        private void CloseRezerv(object sender, RoutedEventArgs e)
        {
            Rezerv v = new Rezerv();
            this.Close();
        }
       
        private void Update(object sender, MouseButtonEventArgs e)
        {
            Rezerv newRez = new Rezerv();
            Application.Current.MainWindow = newRez;
            newRez.Show();
            this.Close();
        }

     
        private void DeleteRezerv(object sender, RoutedEventArgs e)
        {
            
        }       
    }
}
