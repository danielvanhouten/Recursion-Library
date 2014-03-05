using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace NetCompareTest
{
    internal class MappingContext
    {
        private static readonly Stack<PropertyMapItem> PropertyStack = new Stack<PropertyMapItem>();

        public MappingContext(object obj)
        {
            RootObject = obj;
            CurrentObject = obj;
        }

        public string CurrentPath
        {
            get { var current = String.Empty;
            foreach ( var entry in PropertyStack.Reverse() )
            {
                current = String.Format( "{0}{1}{2}", current + ( ( entry.IsCollectionItem )
                                                                    ? String.Format( "[{0}]", entry.CollectionIndex )
                                                                    : String.Empty ),

                                                                    current != String.Empty ? "." : String.Empty,

                                                                    entry.PropertyName );
            }
            return current; }
        }
        public int CollectionIndex { get; set; }
        public bool IsInCollection { get
            {
                return PropertyStack.Any() && PropertyStack.Peek().IsCollection;
            }
        }

        public object CurrentObject { get; set; }
        public object RootObject { get; set; }

        public void StepForward(object obj = null)
        {
            CurrentObject = obj ?? RootObject;
            PushPropertyData();
        }

        public void StepBack()
        {
            if (PropertyStack.Any())
                PropertyStack.Pop();

            CurrentObject = PropertyStack.Any() 
                ? PropertyStack.Peek().PropertyValue : RootObject;
        }

        private void MapSingleItem( object obj, int index = 0)
        {
            var currentObject = obj; // freeze in place 
            foreach (var propertyInfo in currentObject.GetType().GetProperties())
            {
                var propVal = propertyInfo.GetValue(currentObject, null);
                // dive deeper

                // do action : property mapped event
                // propertyAction( context.CurrentPath );

                PropertyStack.Push(new PropertyMapItem()
                                       {
                                           IsCollectionItem = IsInCollection,
                                           CollectionIndex = index,
                                           PropertyName = propertyInfo.Name,
                                           PropertyType = propertyInfo.PropertyType,
                                           PropertyValue = propVal
                                       });

                if (!ShouldIgnore(propertyInfo) && propVal != null)
                {
                    StepForward(propVal);
                }
                StepBack();
            }
        }

        public void PushPropertyData()
        {
            if ( ShouldIgnore( CurrentObject ) )
                return;

            var index = 0;
            if ( CurrentObject as IEnumerable != null )
            {
                foreach ( var item in ( IEnumerable )CurrentObject )
                {
                    MapSingleItem( item, index );
                    CollectionIndex = index;
                    index++;
                }
            }
            else
            {
                MapSingleItem(CurrentObject);
            }
        }

        private static bool ShouldIgnore( PropertyInfo pInfo )
        {
            return pInfo.PropertyType.IsPrimitive || pInfo.PropertyType.IsValueType || pInfo.PropertyType == typeof( string );
        }
        private static bool ShouldIgnore( object obj )
        {
            var objectType = obj.GetType();
            return objectType.IsPrimitive || objectType.IsValueType || objectType == typeof( string );
        }
    }
}