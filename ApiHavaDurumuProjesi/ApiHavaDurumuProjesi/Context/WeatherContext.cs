using ApiHavaDurumuProjesi.Entities;
using Microsoft.EntityFrameworkCore;

namespace ApiHavaDurumuProjesi.Context
{
    public class WeatherContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=UMUT\\SQLEXPRESS;initial catalog=ApiHavaDurumuDb;integrated Security=true");
        }
        public DbSet<City> Cities { get; set; }
    }
}
