using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCompareTest
{
    public class PropertyMapItem
    {
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
    }
}
