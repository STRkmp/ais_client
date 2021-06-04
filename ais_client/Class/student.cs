using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
namespace ais_client
{
    
   public class student
    {
        public int Student_ID { get; set; }
        public double Currency { get; set; }
        public int group_id { get; set; }
        public string Fam { get; set; }
        public string Im { get; set; }
        public string Otch { get; set; }
        public int Passport { get; set; }
        public virtual ICollection<MyOrder> MyOrder { get; set; }
        public virtual ICollection<Portfolio_user> Portfolio { get; set; }
    }
}
