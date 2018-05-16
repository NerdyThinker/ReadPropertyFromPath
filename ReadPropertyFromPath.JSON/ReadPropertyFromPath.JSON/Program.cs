using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadPropertyFromPath.JSON
{
    class Program
    {
        static void Main(string[] args)
        {
            var cust = new Customer() { Name = "c1", Address = new Address() { City = "Atlanta", State = "GA", Street = "123 XYz Rd", Zip = "32165" } };
            ReadProperty(cust, "Address.City");
        }

        private static void ReadProperty<T>(T cust, string path)
        {
            JObject obj = JObject.FromObject(cust);
            var pathArr = path.Split('.');
            var result= ReadProperty(obj, pathArr);
            Console.WriteLine(result);
            Console.ReadLine();

        }

        private static string ReadProperty(JObject obj, string[] pathArr)
        {
            if(pathArr.Length == 0)
            {
                return "Error Finding the Property";
            }
            if (pathArr.Length==1)
            {
                return obj.GetValue(pathArr[0]).ToString();
            }
            else
            {
                var token = obj.GetValue(pathArr[0]);
                
                return ReadProperty(token.ToObject<JObject>(), pathArr.Skip(1).ToArray());
            }
        }
    }

    public class Customer
    {
        public string Name { get; set; }
        public Address Address { get; set; }
    }

    public class Address
    {
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }

    }
}
