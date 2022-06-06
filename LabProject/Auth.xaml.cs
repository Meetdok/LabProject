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
using LabProject.Model;
using LabProject.Tools;
using MySql.Data.MySqlClient;

namespace LabProject
{
    /// <summary>
    /// Логика взаимодействия для Auth.xaml
    /// </summary>
    public partial class Auth : Window
    {
        
        public Auth()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var loginUser = TextBox_login.Text;
            var passUser = TextBox_password.Text;
            var db = MySqlDB.GetDB();
            bool enter1 = false;
            bool enter2 = false;
            bool enter3 = false;
            if (db.OpenConnection())
            {
                string querystring = $"select id, position_id, login, pass from users where login ='{loginUser}' and pass = '{passUser}'";
                using (MySqlCommand command = new MySqlCommand(querystring, MySqlDB.GetDB().GetConnection()))
                {
                    using (var dr = command.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            enter1 = dr.GetInt32("position_id") == 1;
                            enter2 = dr.GetInt32("position_id") == 2;
                            enter3 = dr.GetInt32("position_id") == 3;
                        }
                       
                    }                   
                }
                db.CloseConnection();
            }
            if (enter1)
            {
                
                MainWindow main = new MainWindow();
                this.Close();
                main.Show();
                return;


            }
            if (enter2)
            {

                MainWindow2 main2 = new MainWindow2();
                this.Close();
                main2.Show();
                return;
            }
            if (enter3)
            {

                MainWindow3 main3 = new MainWindow3();
                this.Close();
                main3.Show();
                return;
            }
            
            else
                MessageBox.Show("Неверно!", "Аккаунта не существет!!", MessageBoxButton.OK, MessageBoxImage.Warning);            
        }
        

        private void Phonenum(object sender, MouseButtonEventArgs e)
        {
            Auth2 v = new Auth2();
            v.Show();
            this.Close();
        }

        private void Close(object sender, MouseButtonEventArgs e)
        {
            Auth a = new Auth();
            this.Close();
            Environment.Exit(0);
        }
    }
}
