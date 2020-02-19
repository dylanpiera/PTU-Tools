using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace PTU_Tools_Web.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public IQueryable<PokemonTableModel> GetPokemonTable(string pokemon) => (Regex.IsMatch(pokemon, @"^[A-Z][a-zA-Z_]+$")) ? Set<PokemonTableModel>().FromSqlRaw($"SELECT * FROM {pokemon}") : null;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<PokemonTableModel>().HasNoKey();
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}
