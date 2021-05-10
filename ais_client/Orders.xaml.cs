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
namespace ais_client
{
    /// <summary>
    /// Логика взаимодействия для Orders.xaml
    /// </summary>
    public partial class Orders : Page
    {
        private ObservableCollection<MyOrder> MyOrders = new ObservableCollection<MyOrder>();
        public Orders()
        {
            InitializeComponent();
            load();
            ListView_Order.ItemsSource = MyOrders;
        }

        public void load()
        {
            RestClient client = new RestClient(new Uri("https://ais-rest.conveyor.cloud"));
            string stocksrequest = "/Students/details/23";
            RestRequest restrequest = new RestRequest(stocksrequest, Method.GET);
            var restresponse = client.Execute(restrequest, Method.GET);
            student Student_Current = JsonConvert.DeserializeObject<student>(restresponse.Content);
            Unzip_student(Student_Current);
        }

        private void Unzip_student(student student)
        {
            foreach (MyOrder order in student.MyOrder)
            {
                MyOrders.Add(order);
            }

        }

    }
}
