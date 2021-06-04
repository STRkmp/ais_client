using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
namespace ais_client
{


    class SinglPage
    {
        private static List<(string,Page)> instance = new List<(string,Page)>();
        public static Page getInstance(string name, string id,string Uri)
        {
            (string, Page) Page = instance.Find(x => x.Item1.Equals(name));
            if (Page.Item2 == null)
            {
                Type current = Type.GetType("ais_client."+ name);
                if ( current != null)
                {
                    Page = (name, (Page)Activator.CreateInstance(current,id, Uri));
                }
                
                instance.Add(Page);
                  
            }

            return Page.Item2;
        }
    }
}
