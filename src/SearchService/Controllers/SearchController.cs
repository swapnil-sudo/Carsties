using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Entities;
using SearchService.Models;
using SearchService.RequestHelper;
using ZstdSharp.Unsafe;

namespace SearchService.Controllers
{
    [ApiController]
    [Route("api/search")]
    public class SearchController:ControllerBase
    {
        public async Task<ActionResult<List<Item>>> SearchItems([FromQuery] SearchParam searchParam){
                 
                 var query=DB.PagedSearch<Item,Item>();

                 query.Sort(x=>x.Ascending(a=>a.Make));

                 if(!string.IsNullOrEmpty(searchParam.searchTerm)){
                    query.Match(Search.Full,searchParam.searchTerm).SortByTextScore();
                 }

                 query=searchParam.OrderBy switch{
                    "make"=>query.Sort(x=>x.Ascending(a=>a.Make)),
                    "new"=>query.Sort(x=>x.Descending(a=>a.CreatedAt)),
                    _ =>query.Sort(x=>x.Ascending(a=>a.AuctionEnd))
                 };

                 query=searchParam.FilterBy switch{
                    "finished"=>query.Match(x=>x.AuctionEnd<DateTime.UtcNow),
                    "endingSoon"=>query.Match(x=>x.AuctionEnd<DateTime.UtcNow.AddHours(6)),
                    _ => query.Match(x=>x.AuctionEnd>DateTime.UtcNow)
                 };

                 if(!string.IsNullOrEmpty(searchParam.Seller)){
                    query.Match(x=>x.Seller==searchParam.Seller);
                 };

                   if(!string.IsNullOrEmpty(searchParam.Winner)){
                    query.Match(x=>x.Winner==searchParam.Winner);
                 };

                 query.PageNumber(searchParam.pageNumber);
                 query.PageSize(searchParam.pageSize);
                 var result=await query.ExecuteAsync();

                 return Ok(new {
                    result=result.Results,
                    pageCount=result.PageCount,
                    totalCount=result.TotalCount
                 });
        }
    }
}