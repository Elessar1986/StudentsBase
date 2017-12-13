using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseKNEU
{
    [Serializable]
    public class StudentClass : HumanClass
    {
        public string Group { get; set; }
        public string Faculty { get; set; }
        public string Cathedra { get; set; }
        public int Kurs { get; set; }

        public StudentClass() : base()
        { }

        public StudentClass(string name,
                            string middlename,
                            string lastname,
                            int age) : base(name, middlename, lastname, age)
        { }


        public override void Print()
        {
            base.Print();
            Console.WriteLine($" Группа: {Group}");
        }
        public void PrintCath()
        {
            base.Print();
            Console.WriteLine($" { Cathedra}");
        }
        public void PrintAll()
        {
            base.Print();
            Console.WriteLine($"\nФак: {Faculty}\nКаф: {Cathedra} Груп: {Group} Курс: {Kurs + 1}");
            Console.WriteLine("__________________________________");
        }

    }
}
