using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StackOverflow.Business;
using StackOverflow.Models;

namespace StackOverflow.Controllers
{
    [Route("api/[controller]")]
    public class StackOverflowController : Controller
    {
        [HttpPost("[action]")]
        public List<Forums> GetSearchResults([FromBody] string keywords)
        {
            StockoverflowBusiness business = new StockoverflowBusiness();
            return business.GetSearchResults(GetSearchString(keywords));
        }

        private string GetSearchString(string keywords)
        {
            string[] arrayString = keywords.Split(' ');
            string searchString = "Title =''";
            for(int i=0;i<arrayString.Length;i++)
            {
                for(int j=i+1; j < arrayString.Length - 1;j++)
                {
                    searchString += " OR Title like '%" + arrayString[i] + " " + arrayString[j] + "%' ";
                    searchString += " OR Title like '%" + arrayString[j] + " " + arrayString[i] + "%' ";
                }
            }

            return searchString;
        }
    }
}