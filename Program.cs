using Reflection.CustomAttributes;
using Reflection.Data;
using Reflection.Static;
using System.Collections.ObjectModel;
using System.Reflection;

namespace ReflectionDemo // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //1. Play around type, attribute, method
            Reflection_Type_Attribute_Method_Demo();

            //2. Create database and Table with Reflection
            Reflection_Create_Sql_Demo();

        }      


        static void Reflection_Type_Attribute_Method_Demo()
        {
            Type Person = typeof(Person);

            //Get Properties 
            Console.WriteLine("--- Get Properties --");
            foreach (var property in Person.GetProperties())
            {
                Console.WriteLine($"{property.Name} ({property.PropertyType})");
            }

            //Get Methods
            Console.WriteLine("\n \n--- Get Methods --");
            foreach (var method in Person.GetMethods())
            {
                Console.WriteLine($"{method.Name} - ({method.ReturnType.Name})");
                //method.method.ReturnParameter
            }


            Console.WriteLine("\n \n--- Test Attribute --");
            ReflectionDemoMethods.AttributeTest(Person);
        }

        static void Reflection_Create_Sql_Demo()
        {
            ReflectionDemoMethods.CreateDB();
            ReflectionDemoMethods.CreateSQLTable(typeof(Student), "Student");
            Console.WriteLine("Press any key to close");
            Console.ReadKey();
        }
    }

    
}