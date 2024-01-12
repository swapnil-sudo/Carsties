using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts;
using MassTransit;
using MongoDB.Entities;
using SearchService.Models;

namespace SearchService.Consumers
{
    public class AuctionDeleteConsumer : IConsumer<AuctionDeleted>
    {
        public async Task Consume(ConsumeContext<AuctionDeleted> context)
        {
            Console.WriteLine("---> Consuming Auction Deleted: "+context.Message.Id);

            var result=await DB.DeleteAsync<Item>(new{ID=context.Message.Id});

            if(!result.IsAcknowledged)
                throw new MessageException(typeof(AuctionDeleted),"Problem in deleting Auction");
        }
    }
}