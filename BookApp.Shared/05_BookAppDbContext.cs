using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookApp.Shared
{
    public class BookAppDbContext : DbContext
    {
        // PM> Install-Package Microsoft.EntityFrameworkCore.SqlServer
        // PM> Install-Package Microsoft.Data.SqlClient
        // PM> Install-Package System.Configuration.ConfigurationManager
        // PM> Install-Package Microsoft.EntityFrameworkCore
        // PM> Install-Package Microsoft.EntityFrameworkCore.Tools
        // PM> Install-Package Microsoft.EntityFrameworkCore.InMemory

        public BookAppDbContext()
        {
            // Empty
            // ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }
        public BookAppDbContext(DbContextOptions<BookAppDbContext> options) : base(options)
        {
            // ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // 닷넷 프레임워크 기반에서 호출되는 코드 영역: 
            // App.config 또는 Web.config의 연결 문자열 사용
            // 직접 데이터베이스 연결문자열 설정 가능
            if (!optionsBuilder.IsConfigured)
            {
                string connectionString = ConfigurationManager.ConnectionStrings[
                    "ConnectionString"].ConnectionString;
                optionsBuilder.UseSqlServer(connectionString);
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>().Property(mbox => mbox.Created).HasDefaultValueSql("GetDate()");
        }
        public DbSet<Book> Books { get; set; }
    }
}
