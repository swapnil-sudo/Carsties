using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Entities;

namespace SearchService.Models
{
    public class Item:Entity
    {
        public int ReservePrice { get; set; }
        public string Seller { get; set; }
        public string Winner { get; set; }
        public int? SoldAmount { get; set; }
        public int? CurrentHighBid { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime AuctionEnd { get; set; }        
        public string Status{get;set;}
        
        public string Make { get; set; }=string.Empty;
        public string Model { get; set; }=string.Empty;
        public int Year { get; set; }
        public string Color { get; set; }=string.Empty;
        public int Mileage { get; set; }
        public string ImageUrl { get; set; }=string.Empty;
    }
}