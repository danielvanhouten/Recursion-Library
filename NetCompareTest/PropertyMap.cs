using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCompareTest
{
    public class PropertyMap : IEnumerable<PropertyMapItem>
    {
        private readonly List<PropertyMapItem> _innerList;

        public PropertyMap()
        {
            _innerList = new List<PropertyMapItem>();
        }
        public void Add(PropertyMapItem item)
        {
            _innerList.Add(item);
        }

        public IEnumerator<PropertyMapItem> GetEnumerator()
        {
            return _innerList.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _innerList.GetEnumerator();
        }
    }
   
}
