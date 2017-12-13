using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseKNEU
{
    [Serializable]
    public class CathedraClass
    {
        public string CathedraName { get; set; }
        public string Faculty { get; set; }
        public List<TeacherClass> Teachers { get; set; }

        public CathedraClass() { }

        public CathedraClass(string cathedraName, string faculty)
        {
            CathedraName = cathedraName;
            Faculty = faculty;
            Teachers = new List<TeacherClass>();
        }

    }
}
