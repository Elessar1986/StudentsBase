using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DatabaseKNEU
{
    public static class SerializeAll
    {
        public static void SaveAs(object obj)
        {
            var xmlSerializer = new XmlSerializer(obj.GetType());
            using (FileStream fs = new FileStream("BASE.xml", FileMode.Create))
            {
                xmlSerializer.Serialize(fs, obj);
            }
                
            
        }

        public static T DeserializeAll<T>()
        {
            var xmlSerializer = new XmlSerializer(typeof(T));
            using (FileStream fs = new FileStream("BASE.xml", FileMode.Open))
                return (T)xmlSerializer.Deserialize(fs);
        }
    }
}
