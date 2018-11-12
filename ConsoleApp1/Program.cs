using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] planets = new string[] { "Mercury", "Venus", "Earth", "Mars", "Jupiter", "Saturn", "Neptune", "Uranus" };
            int m = 6;
            int n;
            Console.Write("Enter a number:");
            string x = Console.ReadLine();

            KeyValuePair<string, int> keyValuePair = new KeyValuePair<string, int>("hello", 23);
            Console.WriteLine(keyValuePair.Value);

         
            if (!string.IsNullOrEmpty(x))
            {
                n = Convert.ToInt32(x);

                for (int i = 0; i < planets.Count(); i++)
                {
                    if (n < planets.Count())
                    {
                        if (n == i + 1)
                            Console.WriteLine("\t" + planets[i] + "<<");
                        else
                            Console.WriteLine("\t" + planets[i]);
                    }

                    // yield break;
                }

                Console.WriteLine("++n:" + ++n);
                Console.WriteLine("n++:" + n++);

                Console.WriteLine("Boolean:" + (n == m));
            }
        }
    }
}
