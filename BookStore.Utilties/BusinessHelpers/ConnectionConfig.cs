using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Utilties.BusinessHelpers
{
    public class ConnectionConfig
    {
        //public static readonly string _connectionString = "Data Source=LAPTOP-3OR7VOKN\\MSSQLSERVER2022;Database=BookstoreDb;Initial Catalog=DemoSchool;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False";
        //public static readonly string _connectionString = "Server=.;Database=BookStoreDb;Trusted_Connection=True;TrustServerCertificate=True;";
        public static readonly string _connectionString = "Server=LAPTOP-3OR7VOKN\\MSSQLSERVER2022;Database=BookStoreDb;Trusted_Connection=True;TrustServerCertificate=True;";
    }
}
