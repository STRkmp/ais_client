using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ais_client
{
    public class MyOrder
    {
        public int Order_ID { get; set; }

        public DateTime Date { get; set; }
        public int Student_id { get; set; }
        public int Stock_id { get; set; }
        public double Stock_Price { get; set; }
        public int Quantity { get; set; }
        public string Function { get; set; }
        public string Type { get; set; }

        public virtual User_stock Stock { get; set; }
        public virtual student Student { get; set; }
    }
}
