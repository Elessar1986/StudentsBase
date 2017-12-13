using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseEngineNS;

namespace DatabaseKNEU
{
    class Program
    {
        static void Main(string[] args)
        {
            
            DatabaseEngine prog = new DatabaseEngine();
            prog.RunBase();
            Console.ReadKey();
        }
    }
}
