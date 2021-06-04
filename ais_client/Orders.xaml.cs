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

namespace ais_client
{
    /// <summary>
    /// Логика взаимодействия для Orders.xaml
    /// </summary>
    public partial class Orders : Page
    {
        private ObservableCollection<MyOrder> myorders;
        private ObservableCollection<MyOrder> MyHistory;
        private System.Windows.Threading.DispatcherTimer timer;
        string studentID;
        RestClient client;
        string stocksrequest = "/Students/details/";
        public Orders(string studentID, string Uri)
        {
            client = new RestClient(new Uri(Uri));
            this.studentID = studentID;
            stocksrequest += studentID;
            InitializeComponent();
            startTime();
        }
        private void startTime()
        {
            timer = new System.Windows.Threading.DispatcherTimer();
            timer.Tick += new EventHandler(loadAsync);
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Start();
        }
        private async void loadAsync(object sender, EventArgs e)
        {
            await Task.Run(() => load());
        }

        private void load()
        {
            
            RestRequest restrequest = new RestRequest(stocksrequest, Method.GET);
            var restresponse = client.Execute(restrequest, Method.GET);
            student Student_Current = JsonConvert.DeserializeObject<student>(restresponse.Content);
            Unzip_student(Student_Current);
        }

        private void Unzip_student(student student)
        {
            myorders = new ObservableCollection<MyOrder>();
            MyHistory = new ObservableCollection<MyOrder>();
            foreach (MyOrder order in student.MyOrder)
            {
                if (order.Function == "buy" || order.Function == "sell")
                    myorders.Add(order);
                else MyHistory.Add(order);
            }
            System.Windows.Application.Current.Dispatcher.Invoke((ThreadStart)delegate {
                ListView_Order.ItemsSource = myorders;
                ListView_History.ItemsSource = MyHistory;
            });
        }

        private void toggle_list_Unchecked(object sender, RoutedEventArgs e)
        {
            ListView_Order.Visibility = Visibility.Visible;
            ListView_History.Visibility = Visibility.Collapsed;
        }

        private void toggle_list_Checked(object sender, RoutedEventArgs e)
        {
            ListView_Order.Visibility = Visibility.Collapsed;
            ListView_History.Visibility = Visibility.Visible;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            RestRequest restrequest = new RestRequest("MyOrder/Delete/" + ((MyOrder)((Button)sender).DataContext).Order_ID.ToString(), Method.GET);
            var restresponse = client.Execute(restrequest, Method.POST);
        }
    }
}
