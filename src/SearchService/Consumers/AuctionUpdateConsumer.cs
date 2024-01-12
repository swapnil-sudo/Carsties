using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Contracts.obj;
using MassTransit;
using MongoDB.Entities;
using SearchService.Models;

namespace SearchService.Consumers
{
    public class AuctionUpdateConsumer : IConsumer<AuctionUpdated>
    {
        public IMapper _mapper { get; }
        public AuctionUpdateConsumer(IMapper mapper)
        {
            this._mapper = mapper;
            
        }
        public async Task Consume(ConsumeContext<AuctionUpdated> context)
        {
         Console.WriteLine("---> Consuming auction updated: "+context.Message.Id);
         var item=  _mapper.Map<Item>(context.Message);

         var result=await DB.Update<Item>()
        .Match(a => a.ID == context.Message.Id)
        .ModifyOnly(x=>new{
          x.Color,
          x.Mileage,
          x.Make,
          x.Model,
          x.Year
        },item)
        .ExecuteAsync();
        
        if(!result.IsAcknowledged)
        throw new MessageException(typeof(AuctionUpdated),"Problem updating mongodb");
        
        }
    }
}