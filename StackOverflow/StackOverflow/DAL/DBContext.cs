using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace StackOverflow.DAL
{
    
    public class DBContext
    {
        const string ConnectionString = "data source = DESKTOP-1K0QJ09;initial catalog = StackOverflow2010;persist security info=True;Integrated Security = SSPI;";

        public DataTable ExecuteQuery(string keywords)
        {
            try
            {
                SqlConnection conn = new SqlConnection();
                conn.ConnectionString = ConnectionString;
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = string.Format(@"select Title, LEFT(Body,140) as Description, 
                                        (select
                                        COUNT(V.Id) from Votes V where V.PostId = P.Id
                                        ) as Votes, P.AnswerCount as Answers, U.DisplayName as UserCreated,
                                        U.Reputation,
                                        STUFF((SELECT distinct ', ' + B.Name
                                                 from Badges B
                                                 where B.UserId = U.Id
                                                    FOR XML PATH(''), TYPE
                                                    ).value('.', 'NVARCHAR(MAX)')
                                                , 1, 2, '') as Badges
                                        from Posts P
                                        INNER JOIN Users U on U.Id = P.OwnerUserId
                                        where {0}", keywords);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adapter.Fill(ds);
                return ds.Tables[0];
            }
            catch(Exception ex)
            {

            }
            return null;
        }
    }
}
