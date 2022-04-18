using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MVCMongoDbRouteSystem.Models;
using Model.MongoDb;
using Model;

namespace MVCMongoDbRouteSystem.Data
{
    public class MVCMongoDbRouteSystemContext : DbContext
    {
        public MVCMongoDbRouteSystemContext (DbContextOptions<MVCMongoDbRouteSystemContext> options)
            : base(options)
        {
        }
        public DbSet<Model.MongoDb.Person> Person { get; set; }

        public DbSet<Model.City> City { get; set; }

        public DbSet<Model.MongoDb.Team> Team { get; set; }

        //public DbSet<Model.Route> Rota { get; set; }

        public DbSet<Model.MongoDb.Route> Route { get; set; }
    }
}
