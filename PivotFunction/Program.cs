using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PivotFunction
{
    class Program
    {
        static void Main(string[] args)
        {
            pivotTable pt = new pivotTable();
            string str=  pt.CreatePivotTable();
            if (str != "")
            {
                Console.WriteLine("Table has been created!!! \n");
                Console.WriteLine(str);
                Console.ReadLine(); 
            }
        }
    }
}
