using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Images.web.Models;
using Microsoft.Extensions.Configuration;
using Images.data;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Images.web.Controllers
{
    public class HomeController : Controller
    {
        private string _connectionString;

        public HomeController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ConStr");
        }
        public IActionResult Home()
        {

            return View();
        }
        public IActionResult Getusers()
        {
            var repo = new UserRepository(_connectionString);
            var vm = new List<HomeViewModel>();
            var x = new List<int>();
            if (HttpContext.Session.Get<List<int>>("Liked") != null)
            {
                x = HttpContext.Session.Get<List<int>>("Liked");
                var z = repo.GetUsers();
                foreach (User v in z)
                {
                    var t = x.Any(p => p == v.Id);
                    if (t)
                    {
                        vm.Add(new HomeViewModel
                        {
                            Id = v.Id,
                            Name = v.Name,
                            Likes = v.Likes,
                            URL = v.URL,
                            able = true
                        });
                    }
                    else
                    {
                        vm.Add(new HomeViewModel
                        {
                            Id = v.Id,
                            Name = v.Name,
                            Likes = v.Likes,
                            URL = v.URL,
                            able = false
                        });

                    }
                }
                return Json(vm);

            }
            else
            {
                var z = repo.GetUsers();
                foreach (User v in z)
                {
                    vm.Add(new HomeViewModel
                    {
                        Id = v.Id,
                        Name = v.Name,
                        Likes = v.Likes,
                        URL = v.URL,
                        able = false
                    });  
                }               
                return Json(vm);
            }
        }
        [HttpPost]
        public IActionResult Like(int Id)
        {
            List<int> x = new List<int>();
            if (HttpContext.Session.Get<List<int>>("Liked") != null)
            {
                x = HttpContext.Session.Get<List<int>>("Liked");
            }
            x.Add(Id);
            HttpContext.Session.Set("Liked", x);
            var repo = new UserRepository(_connectionString);
            var v = repo.GetById(Id);
            v.Likes++;
            repo.AddLikes(v);
            return Json("");
        }
        public IActionResult enlarge(int Id)
        {
            var repo = new UserRepository(_connectionString);
            return View(repo.GetById(Id));
        }
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Add(User user)
        {
            var repo = new UserRepository(_connectionString);
            repo.Add(user);
            return Redirect("/home/home");
        }


    }
    public static class SessionExtensions
    {
        public static void Set<T>(this ISession session, string key, T value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        public static T Get<T>(this ISession session, string key)
        {
            string value = session.GetString(key);

            return value == null ? default(T) :
                JsonConvert.DeserializeObject<T>(value);
        }
    }
}
