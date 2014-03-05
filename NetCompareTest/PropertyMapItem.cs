using System;
<<<<<<< HEAD
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
=======
using System.Collections;
>>>>>>> working version

namespace NetCompareTest
{
    public class PropertyMapItem
    {
<<<<<<< HEAD
        public string Path { get; set; }
        public string PropertyName { get; set; }
        public Type PropertyType { get; set; }
        public object PropertyValue { get; set; }

        public PropertyMapItem(string propertyName, Type propertyType, object value, string path)
        {
            PropertyName = propertyName;
            PropertyType = propertyType;
            PropertyValue = value;
            Path = path;
        }
=======
        public string PropertyName { get; set; }
        public Type PropertyType { get; set; }
        public object PropertyValue { get; set; }
        public bool IsCollection {
            get { return typeof (IEnumerable).IsAssignableFrom(PropertyType); }
        }
        public bool IsCollectionItem { get; set; }
        public int CollectionIndex { get; set; }
>>>>>>> working version
    }
}
