using System;

namespace DataCrawlingDemo.DataModels
{
    class Product
    {
        // The unique identifier for each crawl
        public Guid CrawlId { get; set; }

        public string ProductName { get; set; }

        public double Price { get; set; }

        public string PriceUnit { get; set; }

        public string ImageUrl { get; set; }

        // it's optional, because we're not sure we can check it on the same page 
        // or we must redirect to another page
        public bool? IsAvailability { get; set; }

        public DateTime CrawledDate { get; set; }
    }
}
