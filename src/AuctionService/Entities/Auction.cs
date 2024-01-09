using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.Features;

namespace AuctionService.Entities
{
    public class Auction
    {
        public Guid Id { get; set; }
        public int ReservePrice { get; set; }
        public string Seller { get; set; }=string.Empty;
        public string Winner { get; set; }=string.Empty;
        public int? SoldAmount { get; set; }
        public int? CurrentHighBid { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime AuctionEnd { get; set; }        
        public Status Status{get;set;}
        public Item Item { get; set; }
    }
}