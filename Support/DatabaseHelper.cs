using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace HelloBear.Support
{
    public class DatabaseHelper
    {
        public IConfiguration configuration { get; set; }

        /// <summary>
        /// CleanUp
        /// </summary>
        public void CleanUp()
        {
            var passwordHash = configuration["url"].Contains("stage") ? "AQAAAAIAAYagAAAAEDIPhoFbL0ev0WrLH/ZSL4gNxdaic19SQ7ZLFjIp2HG6n6VdzhSpLWe8cpYlftAacg==" : "AQAAAAIAAYagAAAAEBJMR80MbMKb7zkGRkeXP1TUq/cVaUqvj2HgguU9X1WDo1pZ70x3H9UinnxFiyg/aQ==";

            var connection = InitialDBConnection();

            string[] queries = { "delete from AspNetUsers where FirstName = 'Automation'",
                                "update AspNetUsers set FirstName = 'AutoAdmin', LastName = 'Tester', PhoneNumber = null where Email = 'auto.admin@yopmail.com'",
                                String.Format("update AspNetUsers set PasswordHash = '{0}' where Email = 'auto.admin@yopmail.com'", passwordHash),
                                "delete from Classes where ClassName like 'AutomationClass%'",
                                "delete from TextBooks where Name like 'Automation%'",
                                "delete from Lessons where Name like 'Automation%'",
                                "delete from Contents where Name like 'Automation%'",
                                "delete from PushAndListen where Name like 'Automation%'" };

            try
            {
                connection.Open();

                foreach (var query in queries)
                {
                    SqlCommand command = new SqlCommand(query, connection);

                    command.ExecuteNonQuery();
                }

                connection.Close();

                Console.WriteLine("Clean up successfully!");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }

        /// <summary>
        /// ConnectToDB
        /// </summary>
        /// <returns></returns>
        private SqlConnection InitialDBConnection()
        {
            var server = configuration["database:server"];
            var database = configuration["database:name"];
            var user = configuration["database:user"];
            var pass = configuration["database:password"];

            string connectionString = String.Format("Server=tcp:{0}.database.windows.net,1433;Initial Catalog={1};Persist Security Info=False;User ID={2};Password={3};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True;Connection Timeout=30;", server, database, user, pass);

            return new SqlConnection(connectionString);
        }
    }
}
