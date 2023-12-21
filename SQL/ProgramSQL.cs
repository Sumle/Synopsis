using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

namespace SQLServerTest
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectionString = "Server=localhost;Database=Synopsis;User Id=sa;Password=Kode1234;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string sqlQuery = "SELECT [id], [review_id], [pseudo_author_id], [author_name], [review_text], [review_rating], [review_likes], [author_app_version], [review_timestamp] FROM [dbo].[Reviews]" +  
                        "ORDER BY [review_likes] ASC, [review_rating] DESC";

                    Stopwatch stopwatch = new Stopwatch();
                    stopwatch.Start();
                    using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    Console.WriteLine($"ID: {reader["id"]}, Review ID: {reader["review_id"]}, Pesudo author ID: {reader["pseudo_author_id"]}, Author Name: {reader["author_name"]}, " +
                                        $"Review Text: {reader["review_text"]}, Review Rating: {reader["review_rating"]}, Review Likes: {reader["review_likes"]}, Author app Version: {reader["author_app_version"]}" +
                                        $"Review Timestamp: {reader["review_timestamp"]}");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Ingen rækker fundet.");
                            }
                        }
                    }
                    stopwatch.Stop();
                    double elapsedSeconds = stopwatch.ElapsedMilliseconds / 1000.0;
                    Console.WriteLine($"Time taken for operation: {elapsedSeconds} seconds");

                    string sqlQuery2 = "SELECT [id], [review_id], [pseudo_author_id], [author_name], [review_text], [review_rating], [review_likes], [author_app_version], [review_timestamp] FROM [dbo].[Reviews]";

                    Console.WriteLine("Uden index:");
                    Console.WriteLine();
                    Stopwatch stopwatch2 = new Stopwatch();
                    stopwatch2.Start();
                    using (SqlCommand command2 = new SqlCommand(sqlQuery2, connection))
                    {
                        using (SqlDataReader reader2 = command2.ExecuteReader())
                        {
                            if (reader2.HasRows)
                            {
                                while (reader2.Read())
                                {
                                    Console.WriteLine($"ID: {reader2["id"]}, Review ID: {reader2["review_id"]}, Pesudo author ID: {reader2["pseudo_author_id"]}, Author Name: {reader2["author_name"]}, " +
                                        $"Review Text: {reader2["review_text"]}, Review Rating: {reader2["review_rating"]}, Review Likes: {reader2["review_likes"]}, Author app Version: {reader2["author_app_version"]}" +
                                        $"Review Timestamp: {reader2["review_timestamp"]}");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Ingen rækker fundet.");
                            }
                        }
                    }
                    stopwatch2.Stop();
                    double elapsedSeconds2 = stopwatch2.ElapsedMilliseconds / 1000.0;
                    Console.WriteLine($"Time taken for operation: {elapsedSeconds2} seconds");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Fejl: " + ex.Message);
                }
            }
        }
    }
}