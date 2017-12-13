using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseKNEU
{
    
    [Serializable]
    public class KursClass 
    {
        public int Kurs { get; set; }
        
        public List<GroupClass> Groups { get; set; }

        public KursClass() { }

        public KursClass(int kurs)
        {
            Kurs = kurs;
            
            Groups = new List<GroupClass>();
        }

       
    }
}
