using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PopulateDB
{
    class Program
    {
        static void Main(string[] args)
        {
            Populate.PopulateDB();
            Console.WriteLine("Press any key.");
            Console.ReadLine();
        }
    }
}
