using ClassProject2.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayWithReflection
{
    class Program
    {
        static void Main(string[] args)
        {
            var type = typeof(Product);
            foreach (var property in type.GetProperties())
            {
                Console.WriteLine($"{property.Name} = {property.PropertyType}");
            };

            //var instance = new Product();
            var instance = Activator.CreateInstance(type);

            //instance.Name = "Product A";
            var propName = type.GetProperty("Name");
            propName.SetValue(instance, "Product A");

            //instance.Name;
            var name = propName.GetValue(instance);
        }
    }
}
