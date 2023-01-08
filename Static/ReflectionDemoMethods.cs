using Microsoft.Data.SqlClient;
using Reflection.CustomAttributes;
using System.Data;
using System.Reflection;
using System.Text;

namespace Reflection.Static
{
    internal static class ReflectionDemoMethods
    {
        private static string DatabaseName = "ReflectionDemoDb"; 

        internal static void AttributeTest(Type type)
        {
            var allMethods = type.GetMethods();
            var methodsWithAttributes = allMethods.Where(m => m.GetCustomAttribute(typeof(MethodForRunAttribute)) != null);
            var obj = Activator.CreateInstance(type);

            foreach (var item in methodsWithAttributes)
            {
                var attribute = item.GetCustomAttribute<MethodForRunAttribute>();
                Console.WriteLine($"{item.Name} run for {attribute.RunCount} times");
            }
        }

        internal static void CreateDB()
        {
            String str;
            SqlConnection myConn = new SqlConnection("Server=.;Integrated security=SSPI;database=master;TrustServerCertificate=True");

            // your db name

            // db creation query
            string query = $"CREATE DATABASE {DatabaseName} ;";

            SqlCommand myCommand = new SqlCommand(query, myConn);
            try
            {
                myConn.Open();
                myCommand.ExecuteNonQuery();
                Console.WriteLine("DataBase is Created Successfully");
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("DataBase is Created Fail");

            }
            finally
            {
                if (myConn.State == ConnectionState.Open)
                {
                    myConn.Close();
                }
            }
        }

        internal static void CreateSQLTable(Type type, string tableName)
        {
            var connection = new SqlConnection($"Server=.;Database={DatabaseName};Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True; Integrated security = true");

            // Construct the query
            // 1- Get the properties

            var properties = type.GetProperties();

            var queryBuilder = new StringBuilder();
            queryBuilder.Append($"CREATE TABLE {tableName} (");

            var columns = new List<string>();

            foreach(var property in properties)
            {
                //Check if property is primary 
                if (property.Name.ToLower() == "id" && property.PropertyType.Name == "Int32") //Auto Increment primary key
                {
                    columns.Add("Id int IDENTITY(1,1) PRIMARY KEY");
                }

                if(property.PropertyType.Name == "String")
                {
                    columns.Add($"{property.Name} nvarchar(max)");
                }               
            }

            string columnsString = string.Join(',', columns);
            queryBuilder.Append(columnsString);
            queryBuilder.Append(')');

            var command = new SqlCommand(queryBuilder.ToString(), connection);
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();

            Console.WriteLine($"{tableName} has been created successfully");
        }
    }
}
