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
using System.Collections.ObjectModel;
using RestSharp;
using Newtonsoft.Json;
using System.Threading;

namespace ais_client
{
    /// <summary>
    /// Логика взаимодействия для Portfolio.xaml
    /// </summary>
    /// 
    
   
    public partial class Portfolio : Page
    {
        static public RestClient client = new RestClient(new Uri("https://ais-rest.conveyor.cloud"));
        static RestRequest restrequest;
        static string stocksrequest = "/Students/GetStocks/23";
        private ObservableCollection<User_stock> stocks_list;   // Определить класс 

        public Portfolio()
        {
            InitializeComponent();
            //  this.Loaded += PortfolioLoaded;
            load();
            StocksList.ItemsSource = stocks_list;
        }

        //private async void PortfolioLoaded(object sender, RoutedEventArgs e)
        //{
        //    await load();
        //}
        //private async Task load()
        //{
        //    var cancellationTokenSource = new CancellationTokenSource();
        //    restrequest = new RestRequest(stocksrequest, Method.GET);
        //    var restresponse = await client.ExecuteTaskAsync(restrequest, cancellationTokenSource.Token);
        //    stocks_list = JsonConvert.DeserializeObject<ObservableCollection<User_stock>>(restresponse.Content);
        //    client.Execute(restrequest, Method.GET);
        //}
        private void load()
        {
            restrequest = new RestRequest(stocksrequest, Method.GET);
            var restresponse = client.Execute(restrequest, Method.GET);
            stocks_list = JsonConvert.DeserializeObject<ObservableCollection<User_stock>>(restresponse.Content);
        }
        //private static async void load()
        //{
        //    var cancellationTokenSource = new CancellationTokenSource();
        //    var restrequest = new RestRequest(stocksrequest, Method.GET);
        //    var restresponse = await client.ExecuteTaskAsync(restrequest,cancellationTokenSource.Token);

        //    Student_Current = JsonConvert.DeserializeObject<student>(restresponse.Content);
        //   // Student_Current = JsonConvert.DeserializeObject<student>(restresponse.Content);
        //}
    }
}
