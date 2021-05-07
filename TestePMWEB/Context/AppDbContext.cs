using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using TestePMWEB.Models;

namespace TestePMWEB.Context
{
    public class AppDbContext : IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        //propriedades de mapeamento das entidades que foram definidas na Model
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<API_Log> API_Logs { get; set; }
        public DbSet<Tier> Tiers { get; set; }
        public DbSet<Cons_Cliente> Cons_Clientes { get; set; }
        public DbSet<Cons_RFV> Cons_RFV { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Cliente>().Property(c => c.ID).ValueGeneratedNever();
            builder.Entity<Cons_Cliente>().Property(c => c.ID_CLIENTE).ValueGeneratedNever();
            builder.Entity<Cons_RFV>().Property(c => c.ID_CLIENTE).ValueGeneratedNever();
            builder.Entity<Tier>().Property(t => t.Faixa).ValueGeneratedNever();

            builder.Entity<Pedido>().HasKey(p => new { p.ID_PEDIDO, p.ID_CLIENTE, p.ID_PRODUTO });

            base.OnModelCreating(builder);
        }
    }
}
