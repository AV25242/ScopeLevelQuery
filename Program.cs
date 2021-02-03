using System;
using System.Threading.Tasks;
using Couchbase;
using Couchbase.Query;

namespace ScopeLevelQuery
{


    class Program
    {
        static async Task Main(string[] args)
        {
            var cluster = await Cluster.ConnectAsync(new ClusterOptions
            {
                EnableTls = true
            }
            .WithConnectionString("couchbase://localhost")
            .WithCredentials("Administrator", "password"));
            var bucket = await cluster.BucketAsync("travel-sample");

            
            var queryResult = await cluster.QueryAsync<dynamic>("select * from `travel-sample` LIMIT 10", new Couchbase.Query.QueryOptions());
            await foreach (var row in queryResult)
            {
                Console.WriteLine(row);
            }

        
            Console.Read();
        }

       /* static async Task Main(string[] args)
        {
            var cluster = await Cluster.ConnectAsync("couchbase://localhost", "Administrator", "password");
            var bucket = await cluster.BucketAsync("travel-sample");

            var myscope = bucket.Scope("us");

            var options = new QueryOptions().Metrics(true);

            var queryResult = await myscope.QueryAsync<dynamic>("select * from airline LIMIT 10", options);
            await foreach (var row in queryResult)
            {
                Console.WriteLine(row);
            }

            Console.Read();

        }*/
    }
}
