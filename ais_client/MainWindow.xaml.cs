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
using System.Collections.ObjectModel;
using System.Threading;
using System.Net.Http;
namespace ais_client
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static student Student_Current;

        public static ObservableCollection<MyOrder> orders_user;
        public MainWindow()
        {
            InitializeComponent();
            load();
            listview_fio_balance.DataContext = Student_Current;
        }
        private void List_navigation_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListViewItem cur = (ListViewItem)List_navigation.SelectedItem;
            MainFrame.Content = SinglPage.getInstance(cur.Name);
        }

        private static async void load()
        {
              RestClient client = new RestClient(new Uri("https://ais-rest.conveyor.cloud"));
            string stocksrequest = "/Students/details/23";
            var cancellationTokenSource = new CancellationTokenSource();
            var restrequest = new RestRequest(stocksrequest, Method.GET);
            var restresponse = await client.ExecuteTaskAsync(restrequest, cancellationTokenSource.Token);

            Student_Current = JsonConvert.DeserializeObject<student>(restresponse.Content);
            
            
         ;
        }
        private void btn_test_Click(object sender, RoutedEventArgs e)
        {
            RestClient client = new RestClient(new Uri("https://ais-rest.conveyor.cloud"));

            User_stock order = new User_stock() { Stock_ID = 0, Name = "PAFMMMM", Price = 4, Count = 463 };
            JObject jo = (JObject)JToken.FromObject(order);
            var restrequest = new RestRequest("/Stocks/Create", Method.POST);
            restrequest.AddParameter("application/json", jo, ParameterType.RequestBody);
            restrequest.RequestFormat = RestSharp.DataFormat.Json;
            var restresponse = client.Execute(restrequest);


            
            // Student_Current = JsonConvert.DeserializeObject<student>(restresponse.Content);
        }
    }
}
