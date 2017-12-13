using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseKNEU
{
    [Serializable]
    public class FacultyClass
    {
        public string FacultyName { get; set; }

        public List<CathedraClass> Cathedras { get; set; }
        public List<KursClass> Kurs { get; set; }

        public FacultyClass() { }

        public FacultyClass(string facultyName)
        {
            FacultyName = facultyName;
            Cathedras = new List<CathedraClass>();
            Kurs = new List<KursClass>() { new KursClass(1),
                                            new KursClass(2),
                                            new KursClass(3),
                                            new KursClass(4),
                                            new KursClass(5),
                                            new KursClass(6)};   
        }
    }
}
