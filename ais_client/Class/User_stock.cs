using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
namespace ais_client
{
    public class User_stock
    {
        public int Stock_ID { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public int Count { get; set; }
        public virtual ICollection<MyOrder> MyOrder { get; set; }
    }
}
