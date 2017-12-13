using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseKNEU
{
    [Serializable]
    public class GroupClass 
    {
        public string GroupName { get; set; }
        public string Faculty { get; set; }
        public int Kurs;
        public List<StudentClass> Students { get; set; }

        public GroupClass() { }

        public GroupClass(string groupName,string faculty,int kurs)
        {
            GroupName = groupName;
            Faculty = faculty;
            Kurs = kurs;
            Students = new List<StudentClass>();
        }

    }
}
