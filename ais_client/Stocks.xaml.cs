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
using System.Collections.ObjectModel;
using System.Threading;
using Newtonsoft.Json.Linq;
using System.Text.RegularExpressions;

namespace ais_client
{
    /// <summary>
    /// Логика взаимодействия для Stocks.xaml
    /// </summary>
    public partial class Stocks : Page
    {
        RestClient client = new RestClient(new Uri("https://ais-rest.conveyor.cloud"));
        private ObservableCollection<User_stock> ALLStocks_list = new ObservableCollection<User_stock>();
        private ObservableCollection<User_stock> stocks_prices = new ObservableCollection<User_stock>();
        student Student_Current;
        string studentID;                   
        private System.Windows.Threading.DispatcherTimer timer;
        public Stocks(string studentID)
        {
            this.studentID = studentID;
            
            InitializeComponent();
            List_stocks.ItemsSource = ALLStocks_list;
            startTime();
        }
        private void startTime()
        {
            timer = new System.Windows.Threading.DispatcherTimer();
            timer.Tick += new EventHandler(loadAsync);
            timer.Interval = TimeSpan.FromSeconds(2);
            timer.Start();
        }
        private async void loadAsync(object sender, EventArgs e)
        {
            await Task.Run(() => load());
        }
        private void load()
        {
            var restrequest = new RestRequest("/Stocks", Method.GET);
            var restresponse = client.Execute(restrequest);
            stocks_prices = JsonConvert.DeserializeObject<ObservableCollection<User_stock>>(restresponse.Content);
            restrequest = new RestRequest("/Students/details/" + studentID, Method.GET);
            restresponse = client.Execute(restrequest);
            Student_Current = JsonConvert.DeserializeObject<student>(restresponse.Content);
            System.Windows.Application.Current.Dispatcher.Invoke((ThreadStart)delegate {
                if (ALLStocks_list.Count == 0)
                {
                    ALLStocks_list = stocks_prices;
                    List_stocks.ItemsSource = ALLStocks_list;
                }

                if (List_stocks.SelectedItem != null) {
                    txt_price.Text = (stocks_prices.Where(i => i.Stock_ID == ((User_stock)List_stocks.SelectedItem).Stock_ID).FirstOrDefault()).Price.ToString();
                    
                    }
            });
        }


        public static bool CheckText(string type, string function, string user_price, string count)
        {
            if (type == "" || function == "" || count == "" ) return false;
            if (type == "limit" && user_price == null && Convert.ToInt32(user_price) < 1) return false;
            if ( Convert.ToInt32(count) < 1) return false;
            return true;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (CheckText(txt_type.Text,txt_function.Text,txt_user_price.Text,txt_count.Text))
            {
                double price;
                MyOrder order = new MyOrder() { Date = DateTime.Now, Type = txt_type.Text, Function = txt_function.Text, Student_id = Int32.Parse(studentID), Quantity = Int32.Parse(txt_count.Text), Stock_Price = Double.TryParse(txt_user_price.Text, out price) ? price : 0, Stock_id = ((User_stock)List_stocks.SelectedItem).Stock_ID };
                JObject jo = (JObject)JToken.FromObject(order);
                var restrequest = new RestRequest("/MyOrder/Create", Method.POST);
                restrequest.AddParameter("application/json", jo, ParameterType.RequestBody);
                restrequest.RequestFormat = RestSharp.DataFormat.Json;
                var restresponse = client.Execute(restrequest);
            }
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e) => e.Handled = Regex.IsMatch(e.Text, "[^0-9]+");

        private void txt_type_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (txt_type.Text == "limit")
            {
                txt_user_price.Text = "";
                txt_user_price.IsEnabled = false;
                
            }
            else txt_user_price.IsEnabled = true;
        }
    }
}
