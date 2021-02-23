using System;
using System.IO;
using System.Linq;
using Core.Entities;
using Microsoft.Extensions.Configuration;

namespace API.Tests.TestUtilities
{
    public static class TestData
    {
        public static IConfiguration Configuration { get; }
        public static Holding MockHolding { get; }
        public static IexStock MockIexStock { get; }
        private static readonly Random _random = new Random();

        static TestData()
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.Development.json", optional: false, reloadOnChange: false);
            Configuration = config.Build();

            MockHolding = new Holding()
            {
                Symbol = "FAKE",
                CompanyName = "Fake Stock",
                Value = 10,
                TotalShares = 2,
                User = new User()
            };

            MockIexStock = new IexStock() {
                Symbol = "Fake", 
                CompanyName = "Fake Stock", 
                LatestPrice = 1
            };
        }

        public static string CreateRandomString(int length = 10, int start = 97, int end = 122)
        {
            return Enumerable.Range(1, length)
                .Select(x => ((char) _random.Next(start, end)).ToString())
                .Aggregate((first, second) => $"{first}{second}");
        }
    }
}