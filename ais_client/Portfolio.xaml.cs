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
        private ObservableCollection<User_stock> stocks_list;
        private System.Windows.Threading.DispatcherTimer timer;
        RestClient client = new RestClient(new Uri("https://ais-rest.conveyor.cloud"));
        string stocksrequest = "/Students/GetStocks/";
        string studentID;
        public Portfolio(string studentID)
        {
            this.studentID = studentID;
            InitializeComponent();
            startTime();
        }
        private void startTime()
        {
            timer = new System.Windows.Threading.DispatcherTimer();
            timer.Tick += new EventHandler(loadAsync);
            timer.Interval = TimeSpan.FromSeconds(3);
            timer.Start();
        }
        private async void loadAsync(object sender, EventArgs e)
        {
            await Task.Run(() => load());
        }
        private void load()
        {
            var restrequest = new RestRequest("/Students/GetStocks/"+studentID, Method.GET);
            var restresponse = client.Execute(restrequest);
            stocks_list = JsonConvert.DeserializeObject<ObservableCollection<User_stock>>(restresponse.Content);
            System.Windows.Application.Current.Dispatcher.Invoke((ThreadStart)delegate { StocksList.ItemsSource = stocks_list; });
        }
    }
}
