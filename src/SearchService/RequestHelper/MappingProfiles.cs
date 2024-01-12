using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Contracts;
using Contracts.obj;
using SearchService.Models;

namespace SearchService.RequestHelper
{
    public class MappingProfiles:Profile
    {
        public MappingProfiles()
        {
            CreateMap<AuctionCreated,Item>();
            CreateMap<AuctionUpdated,Item>();
        }
    }
}