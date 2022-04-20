using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Model.MongoDb;
using Model;

namespace MVCMongoDbRouteSystemLogin.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Model.MongoDb.Person> Person { get; set; }
        public DbSet<Model.City> City { get; set; }
        public DbSet<Model.MongoDb.Team> Team { get; set; }
    }
}
