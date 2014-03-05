<<<<<<< HEAD
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCompareTest
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    namespace EPOP.ValidationV2.Mapping
    {
        public class RecursivePropertyMapper
        {
            private readonly Stack<PropertyInfo> _propertyStack;
            private readonly Func<string> _currentPath;
            private readonly PropertyMap _map;

            public RecursivePropertyMapper()
            {
                _propertyStack = new Stack<PropertyInfo>();
                _map = new PropertyMap();

                _currentPath = () =>
                               _propertyStack.Reverse().Select(x => x.Name)
                                             .Aggregate((x, y) => String.Format("{0}.{1}", x, y));
            }

            public PropertyMap Map(object obj)
            {
                RecursiveMap(obj);
                return _map;
            }

            public void RecursiveMap(object obj)
            {
                //if object is list of something
                if (obj as IEnumerable != null)
                {
                    var items = (IEnumerable)obj;
                    foreach (var item in items)
                    {
                        RecursiveMap(item);
                    }
                }
                //handle non-collections
                else
                {
                    foreach (var propertyInfo in obj.GetType().GetProperties().Where(x => x.CanRead))
                    {
                        _propertyStack.Push(propertyInfo);

                        var propVal = propertyInfo.GetValue(obj, null);
                        var propertyMapItem = new PropertyMapItem(propertyInfo.Name, propertyInfo.PropertyType, propVal, _currentPath());
                        _map.Add(propertyMapItem);

                        if (!ShouldIgnore(propertyInfo) && propVal != null)
                            RecursiveMap(propVal);

                        _propertyStack.Pop();
                    }
                }
            }

            private static bool ShouldIgnore(PropertyInfo pInfo)
            {
                return pInfo.PropertyType.IsPrimitive || pInfo.PropertyType.IsValueType || pInfo.PropertyType.Module.ScopeName == "System";
            }
        }
    }
}
=======
﻿//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.Linq;
//using System.Reflection;

//namespace NetCompareTest
//{
//    public class RecursivePropertyMapper
//    {
//        private readonly Func<string> _currentPath;
//        private readonly PropertyMap _map;
//        private readonly Stack<PropertyMapItem> _propertyStack;

//        public RecursivePropertyMapper()
//        {
//            _propertyStack = new Stack<PropertyMapItem>();
//            _map = new PropertyMap();

//            _currentPath = () =>
//            {
//                var current = String.Empty;
//                foreach (var entry in _propertyStack.Reverse())
//                {
//                    current = String.Format("{0}{1}{2}", current + 
//                        ((entry.IsInCollection) ? String.Format("[{0}]", entry.CollectionIndex) : String.Empty),
//                                            current != String.Empty ? "." : String.Empty,
//                                            entry.PropertyName);
//                }
//                return current;
//            };
                          
//        }

//        public PropertyMap Map( object obj )
//        {
//            RecursiveMap( obj );
//            return _map;
//        }

//        public void RecursiveMap( object obj )
//        {
//            //if object is list of something
//            if ( obj as IEnumerable != null )
//            {
//                var items = ( IEnumerable )obj;
//                foreach ( var item in items )
//                {
//                    RecursiveMap( item );
//                }
//            }
//            //handle non-collections
//            else
//            {
//                foreach ( var propertyInfo in obj.GetType().GetProperties().Where( x => x.CanRead ) )
//                {
//                    var pMapItem = new PropertyMapItem
//                    {

//                    };
//                    _propertyStack.Push( propertyInfo );

//                    var propVal = propertyInfo.GetValue( obj, null );
//                    var propertyMapItem = new PropertyMapItem( propertyInfo.Name, propertyInfo.PropertyType, propVal, _currentPath() );
//                    _map.Add( propertyMapItem );

//                    if ( !ShouldIgnore( propertyInfo ) && propVal != null )
//                        RecursiveMap( propVal );

//                    _propertyStack.Pop();
//                }
//            }
//        }

//        private static bool ShouldIgnore( PropertyInfo pInfo )
//        {
//            return pInfo.PropertyType.IsPrimitive || pInfo.PropertyType.IsValueType || pInfo.PropertyType.Module.ScopeName == "System";
//        }
//    }
//}
>>>>>>> working version
