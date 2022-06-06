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
using LabProject.Код;
using LabProject.Окна;

namespace LabProject
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new ViewEquis();
        }

        private void Connection_Button(object sender, RoutedEventArgs e)
        {
            Connection v = new Connection(null);
            v.Show();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Auth v = new Auth();
            v.Show();
            this.Close();
        }

        private void OpenUsers(object sender, RoutedEventArgs e)
        {
            Users v = new Users(null);
            v.Show();
        }

        private void OpenRezerv(object sender, RoutedEventArgs e)
        {
            Rezerv v = new Rezerv();
            v.Show();
        }

        private void Close(object sender, MouseButtonEventArgs e)
        {
            Auth auth = new Auth();
            this.Close();
            auth.Show();
        }

        private void Update(object sender, MouseButtonEventArgs e)
        {
            MainWindow newWin = new MainWindow();
            Application.Current.MainWindow = newWin;
            newWin.Show();
            this.Close();
        }
    }
}
