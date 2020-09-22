using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Images.data
{
    public class UserContext : DbContext
    {
        private readonly string _connectionString;
        public UserContext(string cs)
        {
            _connectionString = cs;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }
        public DbSet<User> Users { get; set; }
    }
}
