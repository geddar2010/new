using System.Data;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace BetVue.Web.Mvc
{
    public interface IDbInsight
    {
        IDbConnection Connection { get; }
    }

    public class DbInsight : IDbInsight
    {
        //private const string connStr =
         //       "Server=mi3-wsq2.a2hosting.com;Database=betcahos_new;User ID=betcahos_sa;Password=Pa$$w0rd123";

        public DbInsight(IConfigurationRoot configuration)
        {
            Connection = new SqlConnection(configuration.GetConnectionString("Default"));
        }

        public IDbConnection Connection { get; }
    }
}
