using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseKNEU
{
    [Serializable]
    public class TeacherClass : HumanClass
    {
       
        public string Faculty { get; set; }
        public string Cathedra { get; set; }

        public TeacherClass() : base()
        { }

        public TeacherClass(string name,
                            string middlename,
                            string lastname,
                            int age) : base(name, middlename, lastname, age)
        { }


        public override void Print()
        {
            base.Print();
            Console.WriteLine($"\nФак: {Faculty}\nКаф: {Cathedra}");
            Console.WriteLine("__________________________________");
        }
    }
}
