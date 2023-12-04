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

                    string sqlQuery = "SELECT [id], [review_id], [pseudo_author_id], [author_name], [review_text], [review_rating], [review_likes], [author_app_version], [review_timestamp] FROM [dbo].[Reviews]";

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
                    TimeSpan elapsedSeconds = stopwatch.Elapsed;
                    Console.WriteLine($"Time taken for the 1st operation: {elapsedSeconds} seconds");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Fejl: " + ex.Message);
                }
            }
        }
    }
}