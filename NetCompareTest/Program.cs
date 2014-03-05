using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
<<<<<<< HEAD
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

=======
using System.Text.RegularExpressions;
>>>>>>> working version
using NetCompareTest;

namespace NetCompareTest
{
    public class Address
    {
<<<<<<< HEAD
        static string BuildPath(IEnumerable<MappingEntry> entries)
        {
            var current = String.Empty;
            foreach (var entry in entries.Reverse())
            {
                current = String.Format("{0}{1}{2}", current + ((entry.IsCollectionItem) 
                                                                    ? String.Format("[{0}]", entry.Index) 
                                                                    : String.Empty), 
                                                                    
                                                                    current != String.Empty ? "." : String.Empty,
                                                                    
                                                                    entry.Name);
            }
            return current;
        }

        static readonly Stack<MappingEntry> PropertyStack = new Stack<MappingEntry>();
        static readonly Func<string> CurrentPath = () => BuildPath(PropertyStack);
        
        

static void Main(string[] args)
        {
            var user1 = new User
            {
                Name = "Foo",
                Number = 19,
                Address = new Address
                {
                    Street = "MyStreet",
                    State = "MyState",
                    Country = "SomeCountry"
                },

                Phones = new List<Phone>
                {
                    new Phone{ Number = "334-558-5656", Type = "Home"},
                    new Phone{ Number = "334-555-6677", Type = "Cell"}
                }
              
            };

            var user2 = new User
            {
                Name = "Foo",
                Number = 19,
                Address = new Address
                {
                    Street = "MyStreet",
                    State = "MyState",
                    Country = "SomeCountry"
                },

                Phones = new List<Phone>
                {
                    new Phone{ Number = "334-558-2060", Type = "Home"},
                    new Phone{ Number = "334-558-2061", Type = "Cell"}
                },
                Friends = new List<User>() { user1 }
            };

            var test = GetValueFromPath("Friends[0].Phones[1].Number", user2);

            RecursiveCallback(new MappingContext{ Object = user2 }, Console.WriteLine);
            PropertyStack.Clear();

            Console.Read();
=======
        public string Country { get; set; }

        public string State { get; set; }

        public string Street { get; set; }
    }

    public class Phone
    {
        public string Number { get; set; }

        public string Type { get; set; }
    }

    public class User
    {
        public Address Address { get; set; }
        /*
        public List<User> Friends { get; set; }
        */
        public string Name { get; set; }

        public int Number { get; set; }
        
        public List<Phone> Phones { get; set; }
        
        public User()
        {
            //Friends = new List<User>();
>>>>>>> working version
        }
    }

<<<<<<< HEAD
        public static object GetValueFromPath<T>(string path, T rootObj)
        {
            var segments = path.Split('.');
            
            var method = typeof(Enumerable).GetMethod("ElementAt",
                                                       BindingFlags.Static | BindingFlags.NonPublic |
                                                       BindingFlags.Public);
         
=======
    internal class Program
    {
        public static object GetValueFromPath<T>( string path, T rootObj )
        {
            var segments = path.Split( '.' );

            var method = typeof( Enumerable ).GetMethod( "ElementAt",
                                                       BindingFlags.Static | BindingFlags.NonPublic |
                                                       BindingFlags.Public );

>>>>>>> working version
            const string collectionRegex = @"\[\d]";
            const string indexRegex = @"[\d]";

            //Start
<<<<<<< HEAD
            var param = Expression.Parameter(rootObj.GetType(), "i");

            Expression workingExpression = null;
            foreach (var segment in segments)
            {
                var isCollection = Regex.IsMatch(segment, collectionRegex);

                if (isCollection)
                {
                    var propName = Regex.Replace(segment, collectionRegex, String.Empty);
                    var index = int.Parse(Regex.Match(segment, indexRegex).Value);
                    workingExpression = workingExpression == null
                        ? Expression.Property(param, propName)
                        : Expression.Property(workingExpression, propName);

                    var listType = (workingExpression as MemberExpression).Type.GetGenericArguments()[0];
                    var elementAtMethod = method.MakeGenericMethod(listType);

                    workingExpression = Expression.Call(elementAtMethod, workingExpression, Expression.Constant(index));
=======
            var param = Expression.Parameter( rootObj.GetType(), "i" );

            Expression workingExpression = null;
            foreach ( var segment in segments )
            {
                var isCollection = Regex.IsMatch( segment, collectionRegex );

                if ( isCollection )
                {
                    var propName = Regex.Replace( segment, collectionRegex, String.Empty );
                    var index = int.Parse( Regex.Match( segment, indexRegex ).Value );
                    workingExpression = workingExpression == null
                        ? Expression.Property( param, propName )
                        : Expression.Property( workingExpression, propName );

                    var listType = ( workingExpression as MemberExpression ).Type.GetGenericArguments()[ 0 ];
                    var elementAtMethod = method.MakeGenericMethod( listType );

                    workingExpression = Expression.Call( elementAtMethod, workingExpression, Expression.Constant( index ) );
>>>>>>> working version
                }
                else
                {
                    workingExpression = workingExpression == null
<<<<<<< HEAD
                   ? Expression.Property(param, segment)
                   : Expression.Property(workingExpression, segment);
                }
            }

            return Expression.Lambda(workingExpression, param).Compile().DynamicInvoke(rootObj);
=======
                   ? Expression.Property( param, segment )
                   : Expression.Property( workingExpression, segment );
                }
            }

            return Expression.Lambda( workingExpression, param ).Compile().DynamicInvoke( rootObj );
>>>>>>> working version
        }

        public class MappingContext
        {
            public dynamic Object { get; set; }
            public bool IsInCollection { get; set; }
            public int CollectionIndex { get; set; }
            public string ParentPropertyPath { get; set; }
        }

<<<<<<< HEAD
        public class MappingEntry
        {
            public string Name { get; set; }
            public int Index { get; set; }
            public bool IsCollectionItem { get; set; }
        }

        public static void RecursiveCallback(MappingContext context, Action<string> propertyAction )
        {
            if (ShouldIgnore(context.Object)) return;

            //handle collection
            if (context.Object as IEnumerable != null)
            {
                var items = (IEnumerable)context.Object;
                context.IsInCollection = true;
                foreach (var item in items)
                {
                    context.Object = item;
                    RecursiveCallback(context, propertyAction);
                    context.CollectionIndex++;
                }
                context.IsInCollection = false;
            }
            //handle non-collection
            else
            {
                foreach (var propertyInfo in context.Object.GetType().GetProperties())
                {
                    var propVal = propertyInfo.GetValue(context.Object, null);
                    // dive deeper 
                    if (!ShouldIgnore(propertyInfo) && propVal != null){}
                    {
                       
                        PropertyStack.Push(new MappingEntry() { Index = context.CollectionIndex, IsCollectionItem = context.IsInCollection, Name = propertyInfo.Name });

                        // do action
                        propertyAction(CurrentPath());

                        RecursiveCallback(new MappingContext { Object = propVal }, propertyAction);

                        PropertyStack.Pop();
                    }
                }
            }
=======
        private static void Main( string[] args )
        {
            var user1 = new User
            {
                Name = "Foo",
                Number = 19,
                Address = new Address
                {
                    Street = "MyStreet",
                    State = "MyState",
                    Country = "SomeCountry"
                },

                Phones = new List<Phone>
                {
                    new Phone{ Number = "334-558-5656", Type = "Home"},
                    new Phone{ Number = "334-555-6677", Type = "Cell"}
                }
            };

            new MappingContext(user1).StepForward();
            Console.Read();
>>>>>>> working version
        }

        private static bool ShouldIgnore(PropertyInfo pInfo)
        {
            return pInfo.PropertyType.IsPrimitive || pInfo.PropertyType.IsValueType || pInfo.PropertyType == typeof(string);
        }
        private static bool ShouldIgnore(object obj)
        {
            var objectType = obj.GetType();
            return objectType.IsPrimitive || objectType.IsValueType || objectType == typeof(string);
        }
    }

    public class User
    {

        public User()
        {
            Friends = new List<User>();
        }
        public string Name { get; set; }
        public int Number { get; set; }
        public Address Address { get; set; }

        public List<Phone> Phones { get; set; }
        public List<User> Friends { get; set; } 
    }
    public class Address
    {
        public string Street { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
    }
    public class Phone
    {
        public string Type { get; set; }
        public string Number { get; set; }
    }
}
