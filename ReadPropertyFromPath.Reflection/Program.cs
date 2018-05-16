using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadPropertyFromPath.Reflection
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
            var pathArr = path.Split('.');
            Console.WriteLine(ReadProperty(cust, pathArr));
            Console.ReadLine();
        }

        private static string ReadProperty<T>(T cust, string[] pathArr)
        {
            if (pathArr.Length == 0)
            {
                return "Error Parsing Path";
            }
            var prop = pathArr[0];
            var properties = cust.GetType().GetProperties();
            var isFinalType = pathArr.Length == 1;

            var property = properties?.Where(p => isFinalType ? p.Name == prop : p.PropertyType.Name == prop).FirstOrDefault();

            if (property == null)
            {
                return $"Error Finding Property {prop}";
            }
            var value = property.GetValue(cust);

          var result= isFinalType?value.ToString(): ReadProperty(value, pathArr.Skip(1).ToArray());
            return result;

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
