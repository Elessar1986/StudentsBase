using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseKNEU
{
    [Serializable]
    public class HumanClass : IBaseInterface
    {
        public string Name { set; get; }
        public string Lastname { set; get; }
        public string Middlename { get; set; }
        public int Age { get; set; }

        public HumanClass() {}

        public HumanClass(string name,string middlename, string lastname, int age)
        {
            Name = name;
            Lastname = lastname;
            Middlename = middlename;
            Age = age;
        }

        virtual public void Print()
        {
            Console.Write($"{Name} {Middlename} {Lastname} {Age} лет");
        }
    }
}
