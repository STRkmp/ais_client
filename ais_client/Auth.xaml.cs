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
using RestSharp;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text.RegularExpressions;
namespace ais_client
{
    /// <summary>
    /// Логика взаимодействия для Auth.xaml
    /// </summary>
    public partial class Auth : Page
    {
        MainWindow window;
        RestClient client;
        public Auth(MainWindow window, string Uri)
        {
            InitializeComponent();
            this.window = window;
            client = new RestClient(new Uri(Uri));
        }
        public void Auth_click(object sender, RoutedEventArgs e)
        {
            if (CheckData(txtfio.Text,txtpassport.Text))
            {
                string[] FIO = (txtfio.Text).Split(' ');
                student stud = new student() { Fam = FIO[0],Im = FIO[1], Otch = FIO[2], Passport = Convert.ToInt32(txtpassport.Text) };
                JObject jo = (JObject)JToken.FromObject(stud);
                var restrequest = new RestRequest("/Students/Auth", Method.POST);
                restrequest.AddParameter("application/json", jo, ParameterType.RequestBody);
                restrequest.RequestFormat = RestSharp.DataFormat.Json;
                var restresponse = client.Execute(restrequest);
                if (!Int32.TryParse(restresponse.Content, out _))
                {
                    MessageBox.Show(restresponse.Content);
                    return;
                }
                window.startTime(restresponse.Content);
            }
        }

        public static bool CheckData(string fio, string passport)
        {
            if (fio == "" || passport == "" || fio.Split(' ').Length != 3) return false;
            if (!Int32.TryParse(passport, out _)) return false;                                     //Повторение CheckDigits

            return true;
        }
        private void CheckDigits(object sender, TextCompositionEventArgs e) => e.Handled = Regex.IsMatch(e.Text, "[^0-9]+");
        private void CheckAlph(object sender, TextCompositionEventArgs e) => e.Handled = Regex.IsMatch(e.Text, "[^a-zA-Z]+");
    }
}
