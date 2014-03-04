using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;


namespace NetCompareTest
{
    class Program
    {

        public class User
        {
            public string Name { get; set; }
            public int Number { get; set; }
            public Address Address { get; set; }

            public List<Phone> Phones { get; set; } 
        }


        public class Address
        {
            public string Street { get; set; }
            public string State { get; set; }
            public string Country { get; set; }
        }

        public class Phone
        {
            public string  Type { get; set; }
            public string Number { get; set; }
        }

        static IEnumerable<string> GetPropertInfos( object o, string parent = null )
        {
            Type t = o.GetType();
            PropertyInfo[] props = t.GetProperties( BindingFlags.Public | BindingFlags.Instance );
            foreach ( PropertyInfo prp in props )
            {
                if ( prp.PropertyType.Module.ScopeName != "CommonLanguageRuntimeLibrary" )
                {
                    // fix me: you have to pass parent + "." + t.Name instead of t.Name if parent != null
                    foreach ( var info in GetPropertInfos( prp.GetValue( o ), t.Name ) )
                        yield return info;
                }
                else
                {
                    var value = prp.GetValue( o );
                    var stringValue = ( value != null ) ? value.ToString() : "";
                    var info = t.Name + "." + prp.Name + ": " + stringValue;
                    if ( String.IsNullOrWhiteSpace( parent ) )
                        yield return info;
                    else
                        yield return parent + "." + info;
                }
            }
        }


        static void Main( string[] args )
        {


            var user = new User
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
                            }
            };

            foreach ( var info in GetPropertInfos( user ) )
                Console.WriteLine( info );

            Console.Read();
        }
    }
}
