using MongoDB.Bson;
using MongoDB.Driver;
using System.Diagnostics;

var client = new MongoClient();
var stopwatch = new Stopwatch();
//foreach (var database in client.ListDatabases().ToList())
//{
//    Console.WriteLine("1st");
//    Console.WriteLine(database);
//}
var synopsis = client.GetDatabase("Synopsis");
//foreach (var collection in synopsis.ListCollections().ToList())
//{
//    Console.WriteLine("2nd");
//    Console.WriteLine(collection);
//}
var principals = synopsis.GetCollection<BsonDocument>("Dataset");
var princips = principals.Find("{review_rating:1}").Limit(10).ToList();
//foreach (var prins in princips)
//{
//    Console.WriteLine("3rd");
//    Console.WriteLine(prins);
//}
stopwatch.Start();

var pp = principals.Aggregate().Limit(10).Lookup("review_id", "author_id", "review_rating", "review_likes").ToList();

stopwatch.Stop();

foreach (var pn in pp)
{
    Console.WriteLine("4th");
    Console.WriteLine(pn);
}

double elapsedSeconds = stopwatch.ElapsedMilliseconds / 1000.0;
Console.WriteLine($"Time taken for the 4th operation: {elapsedSeconds} seconds");

Console.WriteLine("Index:");
Console.WriteLine();


var client2 = new MongoClient();
var stopwatch2 = new Stopwatch();

var synopsis2 = client2.GetDatabase("Synopsis");
var principals2 = synopsis2.GetCollection<BsonDocument>("Dataset");

var sortDefinition2 = Builders<BsonDocument>.Sort.Ascending("review_likes").Descending("review_rating");

stopwatch2.Start();

var results2 = principals2.Find(new BsonDocument()).Sort(sortDefinition2).ToList();

stopwatch2.Stop();

foreach (var result in results2)
{
    Console.WriteLine(result);
}

double elapsedSeconds2 = stopwatch.ElapsedMilliseconds / 1000.0;
Console.WriteLine($"Time taken for the operation: {elapsedSeconds2} seconds");
