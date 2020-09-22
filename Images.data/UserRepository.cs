using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Images.data
{
    public class UserRepository
    {
        private readonly string _connectionString;
        public UserRepository(string cs)
        {
            _connectionString = cs;
        }
        public List<User> GetUsers()
        {
            using (var context = new UserContext(_connectionString))
            {
                return context.Users.ToList();
            }
        }
        public void Add(User user)
        {
            using (var context = new UserContext(_connectionString))
            {
                context.Users.Add(user);
                context.SaveChanges();
            }
        }
        public User GetById(int id)
        {
            using (var context = new UserContext(_connectionString))
            {
                return context.Users.FirstOrDefault(p => p.Id == id);
            }
        }
        public void AddLikes(User user)
        {
            using (var context = new UserContext(_connectionString))
            {
                context.Users.Attach(user);
                context.Entry(user).State = EntityState.Modified;
                context.SaveChanges();
            }
        }
    }
}
