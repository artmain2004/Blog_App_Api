//using Infrastructure.Data;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Design;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Infastructure.Data
//{
//    public class BlogAppDbContextFactory : IDesignTimeDbContextFactory<BlogAppDbContext>
//    {
//        public BlogAppDbContext CreateDbContext(string[] args)
//        {
//            var optionsBuilder = new DbContextOptionsBuilder<BlogAppDbContext>();

//            optionsBuilder.UseNpgsql("Host=localhost; Username=postgres; Password=password; Database=blog_app_db");

//            return new BlogAppDbContext(optionsBuilder.Options);
//        }
//    }
//}
