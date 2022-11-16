using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
namespace ais_client
{
    public class Portfolio_user
    {
        public int id { get; set; }
        public int Student_ID { get; set; }
        public int Stocks_Id { get; set; }
        //[JsonProperty("Student", IsReference = true)]
        public virtual student Student { get; set; }
    }
}
