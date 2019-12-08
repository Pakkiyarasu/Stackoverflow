using StackOverflow.DAL;
using StackOverflow.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace StackOverflow.Business
{
    public class StockoverflowBusiness
    {
        public List<Forums> GetSearchResults(string keywords)
        {
            DBContext context = new DBContext();
            DataTable dt =  context.ExecuteQuery(keywords);
            List<Forums> forums = new List<Forums>();
            if(dt != null && dt.Rows.Count > 0)
            {
                forums = dt.Rows.OfType<Forums>().ToList(); 
            }
            return forums;
        }
    }
}
